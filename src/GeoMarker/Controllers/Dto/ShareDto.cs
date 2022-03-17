using System.ComponentModel.DataAnnotations;

namespace GeoMarker.Controllers.Dto
{
    public class ShareCreateDto
    {
        /// <summary>
        /// 0 - layer
        /// 1 - maker
        /// </summary>
        [Required]
        public int Type { get; set; }

        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expire { get; set; }
    }
}
