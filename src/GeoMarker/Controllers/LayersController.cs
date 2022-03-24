using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using GeoMarker.Controllers.Dto;
using GeoMarker.Infrastucture.EFCore;
using GeoMarker.Models;
using GeoMarker.Infrastucture.Exceptions;
using GeoMarker.Infrastucture.Extensions;

namespace GeoMarker.Controllers
{
    public class LayersController : CustomControllerBase
    {
        private readonly AppDbContext dbContext;

        public LayersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region 图层 api
        /// <summary>
        /// 获取所有的图层基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<LayerDto>> GetAsync()
        {
            return Ok(await dbContext.Layers
                .AsNoTracking()
                .Select(x => new LayerDto(x.Id, x.Name) { Description = x.Description })
                .ToListAsync());
        }

        /// <summary>
        /// 根据id获取图层详细信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LayerDetailDto>> GetAsync([FromRoute] int id)
        {
            var layer = await dbContext.Layers.FindAsync(id);
            if (layer == null) return NotFound();

            return Ok(new LayerDetailDto(layer.Id, layer.Name)
            {
                Description = layer.Description,
                Style = layer.Style
            });
        }

        /// <summary>
        /// 是否存在 图层名
        /// </summary>
        /// <param name="name">图层名</param>
        /// <returns></returns>
        [HttpGet("exist")]
        public async Task<ActionResult<bool>> ExistNameAsync([FromQuery] string name)
        {
            return Ok(await dbContext.Layers.AnyAsync(x => x.Name == name));
        }

        /// <summary>
        /// 创建图层
        /// </summary>
        /// <param name="layer">图层信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] LayerCreateDto layer)
        {
            var existed = await dbContext.Layers.AnyAsync(x => x.Name == layer.Name);
            if (existed)
                throw new BusinessException(301,"图层名已存在");

            var entity = await dbContext.Layers.AddAsync(new Layer( layer.Name) { Description = layer.Description });
            await dbContext.SaveChangesAsync();

            return Ok(entity.Entity);
        }

        /// <summary>
        /// 更新姓名
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">新名字</param>
        /// <returns></returns>
        [HttpPost("{id}/newname")]
        public async Task<IActionResult> ChangeNameAsync([FromRoute] int id, [FromBody] string name)
        {
            var entity = await dbContext.Layers.FindAsync(id);
            if (entity == null) return BadRequest("layer id : {id} not exist");

            var otherEntity = await dbContext.Layers.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
            if (entity.Id != otherEntity.Id) return BadRequest($"layer name : {name} existed");

            entity.Name = name;

            await dbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// 通过id更新图层
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="layer">图层更新信息</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] LayerUpdateDto layer)
        {
            var entity = await dbContext.Layers.FindAsync(id);
            if (entity == null) return BadRequest($"layer id : {id} not exist");

            entity.Description = layer.Description;
            entity.Style = layer.Style;

            await dbContext.SaveChangesAsync();
            return Ok(entity);
        }

        /// <summary>
        /// 通过id删除图层
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var entity = await dbContext.Layers.FindAsync(id);
            if (entity == null) return BadRequest($"layer id : {id} not exist");

            dbContext.Layers.Remove(entity);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
        #endregion

        #region 标记 api
        /// <summary>
        /// 根据图层id获取markers
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpGet("{id}/markers")]
        public async Task<IActionResult> GetMarkersAsync([FromRoute] int id, [FromQuery] MarkerRequestDto requestDto)
        {
            var layer = await dbContext.Layers
                .AsNoTracking()
                .Include(l => l.Markers.Where(
                       x => string.IsNullOrWhiteSpace(requestDto.Search) ||
                       x.Name.ToLower().Contains(requestDto.Search.ToLower())))
                .FirstOrDefaultAsync(l => l.Id == id);

            if (layer == null)
                return BadRequest($"layer id : {id} not exist");

            // 组建geojson
            if (requestDto.GeoFormat.ToLower() == "geojson")
            {
                return Ok(await layer.Markers.ConvertToFeatureCollectionAsync());
            }

            return Ok(layer.Markers.Select(m => new MarkerDto(m.Id, m.LayerId, m.Name, m.Geometry, m.Style)));
        }

        /// <summary>
        /// 添加标记
        /// </summary>
        /// <param name="id">图层id</param>
        /// <param name="markers">标记</param>
        /// <returns></returns>
        [HttpPost("{id}/markers")]
        public async Task<IActionResult> AddMarkerAsync([FromRoute] int id, [FromBody] List<MarkerCreateDto> markers)
        {
            var existLayer = await dbContext.Layers.AnyAsync(l => l.Id == id);
            if (!existLayer) return BadRequest($"layer id : {id} not exist");

            await dbContext.Markers.AddRangeAsync(markers.Select(marker=> new Marker(id, marker.Name, marker.Geometry) { Style = marker.Style }));
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        #endregion
    }
}
