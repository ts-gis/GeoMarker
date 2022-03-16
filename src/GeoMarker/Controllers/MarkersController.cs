using GeoMarker.Controllers.Dto;
using GeoMarker.Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMarker.Controllers
{
    public class MarkersController : CustomControllerBase
    {
        private readonly AppDbContext dbContext;

        public MarkersController(
            AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [NonAction]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var marker = await dbContext.Markers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (marker == null) return NotFound();

            return Ok(new MarkerDto(marker.Id, marker.LayerId, marker.Name, marker.Geometry, marker.Style));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id,[FromBody] MarkerUpdateDto updateDto)
        {
            var marker = await dbContext.Markers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (marker == null) return BadRequest($"marker id : {id} not exist");

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

            if (marker == null) return BadRequest($"marker id : {id} not exist");

            dbContext.Markers.Remove(marker);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
