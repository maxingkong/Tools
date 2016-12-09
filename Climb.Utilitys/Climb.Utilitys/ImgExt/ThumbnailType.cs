using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climb.Utilitys.ImgExt
{
    /// <summary>图片裁剪类型枚举
    /// </summary>
    public enum ThumbnailType
    {
        /// <summary>
        /// 指定高宽裁减（不变形）  
        /// </summary>
        Cut = 1,
        /// <summary>
        /// 指定宽度,高度自动
        /// </summary>
        FixedW = 2,
        /// <summary>
        /// 指定高度,宽度自动
        /// </summary>
        FixedH = 4,
        /// <summary>
        /// 宽度跟高度都指定,但是会变形
        /// </summary>
        FixedBoth = 8
    }
}
