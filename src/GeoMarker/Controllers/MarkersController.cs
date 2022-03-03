using GeoMarker.Dto;
using GeoMarker.EFCore;
using GeoMarker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoMarker.Controllers
{
    public class MarkersController : CustomControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly ITenantService tenantService;

        public MarkersController(
            AppDbContext dbContext,
            ITenantService tenantService)
        {
            this.dbContext = dbContext;
            this.tenantService = tenantService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var marker = await dbContext.Markers.AsNoTracking()
                .Include(x => x.Layer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (marker == null) return NotFound();

            if(marker.Layer.Tenant != tenantService.TenantName)
                return Forbid("无权访问");

            return Ok(new MarkerDto(marker.Id, marker.LayerId, marker.Name, marker.Geometry, marker.Style));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id,[FromBody] MarkerUpdateDto updateDto)
        {
            var marker = await dbContext.Markers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (marker == null) return BadRequest($"id : {id} not exist");

            if(!string.IsNullOrWhiteSpace(updateDto.Name))
                marker.Name = updateDto.Name;
            if(updateDto.Style != null)
                marker.Style = updateDto.Style;

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var marker = await dbContext.Markers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (marker == null) return BadRequest($"id : {id} not exist");

            dbContext.Markers.Remove(marker);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
