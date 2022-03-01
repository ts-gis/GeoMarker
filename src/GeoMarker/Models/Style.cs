using System.Drawing;

namespace GeoMarker.Models
{
    /// <summary>
    /// 标记样式, 作为值属性使用，用json存储
    /// </summary>
    public class Style
    {
        /// <summary>
        /// 点大小
        /// </summary>
        /// <value></value>
        public double PointSize { get; set; }

        /// <summary>
        /// 点颜色
        /// </summary>
        /// <value></value>
        public Color PointColor { get; set; }

        /// <summary>
        /// 线宽
        /// </summary>
        /// <value></value>
        public double LineWidth { get; set; }

        /// <summary>
        /// 线颜色
        /// </summary>
        /// <value></value>
        public Color LineColor { get; set; }

        /// <summary>
        /// 面外线框宽度
        /// </summary>
        /// <value></value>
        public double PolygonOutlineWidth { get; set; }
        
        /// <summary>
        /// 面外线框颜色
        /// </summary>
        /// <value></value>
        public Color PolygonOutlineColor { get; set; }

        /// <summary>
        /// 面内部颜色
        /// </summary>
        /// <value></value>
        public Color PolygonInnerColor { get; set; }
    }
}