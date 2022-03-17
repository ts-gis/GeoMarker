using GeoMarker.Controllers.Dto;
using GeoMarker.Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// 更新maker
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id,[FromBody] MarkerUpdateDto updateDto)
        {
            var marker = await dbContext.Markers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (marker == null) return BadRequest($"marker id : {id} not exist");

            marker.Name = updateDto.Name;
            marker.Style = updateDto.Style;

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// 删除marker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
