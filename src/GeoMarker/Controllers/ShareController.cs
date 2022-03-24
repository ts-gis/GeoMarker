using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using GeoMarker.Controllers.Dto;
using GeoMarker.Infrastucture.EFCore;
using GeoMarker.Infrastucture.Exceptions;
using GeoMarker.Infrastucture.Extensions;
using GeoMarker.Services;

namespace GeoMarker.Controllers
{
    public class ShareController : CustomControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly GlobleUtils globleUtils;

        public ShareController(AppDbContext dbContext, GlobleUtils globleUtils)
        {
            this.dbContext = dbContext;
            this.globleUtils = globleUtils;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomAllAsync()
        {
            return Ok(await dbContext.Shares.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShareAsync(int id)
        {
            var share = await dbContext.Shares
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (share == null) throw new BusinessException(404, "数据不存在");

            // 过期判断
            if (share.Expire != null && share.Expire < DateTime.Now)
            {
                dbContext.Remove(share);
                await dbContext.SaveChangesAsync();
                throw new BusinessException(405, "数据已过期");
            }

            return Ok(share.Value);
        }

        /// <summary>
        /// 创建分享 (快照式)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ShareCreateDto dto)
        {
            var value = "";
            var name = "";
            object styleBase = null;

            if (dto.Type == 0)
            {
                var layer = await dbContext.Layers
                    .AsNoTracking()
                    .Include(x => x.Markers)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (layer == null) return NotFound();

                name = layer.Name;
                styleBase = layer.Style;
                value = await layer.Markers.ConvertToFeatureCollectionAsync();
            }
            else if (dto.Type == 1)
            {
                var marker = await dbContext.Markers
                    .AsNoTracking()
                    .Include(x=>x.Layer)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (marker == null) return NotFound();

                name = marker.Name;
                styleBase = marker.Layer.Style;
                value = JsonSerializer.Serialize(new { marker.Geometry, marker.Style, marker.Description }, globleUtils.JsonSerializerOptions);
            }

            var ret = await dbContext.Shares.AddAsync(new Models.Share(name, value)
            {
                StyleBase = styleBase,
                Expire = dto.Expire,
            });

            await dbContext.SaveChangesAsync();

            return Ok(ret.Entity.Id);
        }
    }
}
