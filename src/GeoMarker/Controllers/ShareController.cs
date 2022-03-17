﻿using GeoMarker.Controllers.Dto;
using GeoMarker.Infrastucture.EFCore;
using GeoMarker.Infrastucture.Exceptions;
using GeoMarker.Infrastucture.Extensions;
using GeoMarker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

            if (dto.Type == 0)
            {
                var layer = await dbContext.Layers
                    .AsNoTracking()
                    .Include(x => x.Markers)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (layer == null) return NotFound();

                value = await layer.Markers.ConvertToFeatureCollectionAsync();

            }
            else if (dto.Type == 1)
            {
                var maker = await dbContext.Markers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (maker == null) return NotFound();

                name = maker.Name;
                value = JsonSerializer.Serialize(maker, globleUtils.JsonSerializerOptions);
            }

            await dbContext.Shares.AddAsync(new Models.Share(name, value)
            {
                Expire = dto.Expire,
            });

            await dbContext.SaveChangesAsync();

            return Ok("");
        }
    }
}
