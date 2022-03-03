using GeoMarker.Models;
using System.ComponentModel.DataAnnotations;

namespace GeoMarker.Dto
{
    /// <summary>
    /// 图层 简略信息
    /// </summary>
    /// <param name="Id">id</param>
    /// <param name="Name">名</param>
    public record LayerDto(int Id, string Name)
    {
        /// <summary>
        /// 图层描述
        /// </summary>
        public string Description { get; init; }
    }

    /// <summary>
    /// 图层 详细信息
    /// </summary>
    /// <param name="Id">id</param>
    /// <param name="Name">名</param>
    public record LayerDetailDto(int Id, string Name) : LayerDto(Id, Name)
    {
        /// <summary>
        /// 样式
        /// </summary>
        public Style Style { get; init; }
    }

    /// <summary>
    /// 图层 创建
    /// </summary>
    public record LayerCreateDto
    {
        /// <summary>
        /// 名 必须填写
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 图层 更新
    /// </summary>
    public record LayerUpdateDto
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public Style Style { get; set; }
    }
}
