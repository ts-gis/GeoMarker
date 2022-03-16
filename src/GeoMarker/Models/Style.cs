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
        public double PointSize { get; set; } = 21;

        /// <summary>
        /// Hex 点颜色
        /// </summary>
        /// <value></value>
        public string PointColor { get; set; } = "#ff0000";

        /// <summary>
        /// 线宽 
        /// </summary>
        /// <value></value>
        public double LineWidth { get; set; } = 3;

        /// <summary>
        /// Hex 线颜色
        /// </summary>
        /// <value></value>
        public string LineColor { get; set; } = "#ff0000";

        /// <summary>
        /// 面外线框宽度
        /// </summary>
        /// <value></value>
        public double PolygonOutlineWidth { get; set; } = 2;

        /// <summary>
        /// Hex 面外线框颜色
        /// </summary>
        /// <value></value>
        public string PolygonOutlineColor { get; set; } = "#ffffff";

        /// <summary>
        /// Hex 面内部颜色
        /// </summary>
        /// <value></value>
        public string PolygonInnerColor { get; set; } = "#ff0000";

        public double PolygonInnerAlpha { get; set; } = 0.7;
    }
}