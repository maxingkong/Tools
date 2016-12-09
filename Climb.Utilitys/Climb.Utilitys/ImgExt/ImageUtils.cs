using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;


namespace Climb.Utilitys.ImgExt
{
    /// <summary>图片的相关操作
    /// </summary>
    public class ImageUtils
    {
        #region 缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            var originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW":  //指定高宽缩放（可能变形）                
                    break;
                case "W":   //指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H":   //指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut": //指定高宽裁减（不变形）                
                    if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
            }

            //新建一个bmp图片
            var bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            var g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region 转换生成缩略图

        /// <summary>
        /// 转换生成缩略图
        /// </summary>
        /// <param name="imgByte"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Image MakeThumbnail(byte[] imgByte, int width, int height, int mode)
        {
            var originalImage = ByteToImage(imgByte);
            return MakeThumbnail(originalImage, width, height, mode);
        }
        public static Image MakeThumbnail(Image originalImage, int width, int height, int mode)
        {

            var towidth = width;
            var toheight = height;

            var x = 0;
            var y = 0;
            var ow = originalImage.Width;
            var oh = originalImage.Height;

            switch (mode)
            {
                case (int)ThumbnailType.FixedBoth:  //指定高宽缩放（可能变形）                
                    break;
                case (int)ThumbnailType.FixedW:   //指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case (int)ThumbnailType.FixedH:   //指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case (int)ThumbnailType.Cut: //指定高宽裁减（不变形）                
                    if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
            }
            //新建一个bmp图片
            var bitmap = new Bitmap(towidth, toheight);
            //新建一个画板
            var g = Graphics.FromImage(bitmap);
            try
            {
                //设置高质量插值法
                g.InterpolationMode = InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
                return bitmap;
                //以jpg格式保存缩略图
                //  bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region 图片水印
        /// <summary>
        /// 图片水印处理方法
        /// </summary>
        /// <param name="path">需要加载水印的图片路径（绝对路径）</param>
        /// <param name="waterpath">水印图片（绝对路径）</param>
        /// <param name="location">水印位置（传送正确的代码）</param>
        public static string ImageWatermark(string path, string waterpath, string location)
        {
            var kzName = Path.GetExtension(path);
            if (kzName == ".jpg" || kzName == ".bmp" || kzName == ".jpeg")
            {
                var time = DateTime.Now;
                var filename = "" + time.Year + time.Month + time.Day + time.Hour + time.Minute + time.Second + time.Millisecond;
                var img = Image.FromFile(path);
                var waterimg = Image.FromFile(waterpath);
                var g = Graphics.FromImage(img);
                var local = GetLocation(location, img, waterimg);
                g.DrawImage(waterimg, new Rectangle(int.Parse(local[0].ToString()), int.Parse(local[1].ToString()), waterimg.Width, waterimg.Height));
                waterimg.Dispose();
                g.Dispose();
                var newpath = Path.GetDirectoryName(path) + filename + kzName;
                img.Save(newpath);
                img.Dispose();
                File.Copy(newpath, path, true);
                if (File.Exists(newpath))
                {
                    File.Delete(newpath);
                }
            }
            return path;
        }

        /// <summary>
        /// 图片水印位置处理方法
        /// </summary>
        /// <param name="location">水印位置</param>
        /// <param name="img">需要添加水印的图片</param>
        /// <param name="waterimg">水印图片</param>
        private static ArrayList GetLocation(string location, Image img, Image waterimg)
        {
            var local = new ArrayList();
            int x;
            int y;

            switch (location)
            {
                case "LT":
                    x = 10;
                    y = 10;
                    break;
                case "T":
                    x = img.Width / 2 - waterimg.Width / 2;
                    y = img.Height - waterimg.Height;
                    break;
                case "RT":
                    x = img.Width - waterimg.Width;
                    y = 10;
                    break;
                case "LC":
                    x = 10;
                    y = img.Height / 2 - waterimg.Height / 2;
                    break;
                case "C":
                    x = img.Width / 2 - waterimg.Width / 2;
                    y = img.Height / 2 - waterimg.Height / 2;
                    break;
                case "RC":
                    x = img.Width - waterimg.Width;
                    y = img.Height / 2 - waterimg.Height / 2;
                    break;
                case "LB":
                    x = 10;
                    y = img.Height - waterimg.Height;
                    break;
                case "B":
                    x = img.Width / 2 - waterimg.Width / 2;
                    y = img.Height - waterimg.Height;
                    break;
                default:
                    x = img.Width - waterimg.Width;
                    y = img.Height - waterimg.Height;
                    break;
            }
            local.Add(x);
            local.Add(y);
            return local;
        }
        #endregion

        #region 文字水印
        /// <summary>
        /// 文字水印处理方法
        /// </summary>
        /// <param name="path">图片路径（绝对路径）</param>
        /// <param name="size">字体大小</param>
        /// <param name="letter">水印文字</param>
        /// <param name="color">颜色</param>
        /// <param name="location">水印位置</param>
        public static string LetterWatermark(string path, int size, string letter, Color color, string location)
        {
            #region

            var kzName = Path.GetExtension(path);
            if (kzName == ".jpg" || kzName == ".bmp" || kzName == ".jpeg")
            {
                var time = DateTime.Now;
                var filename = "" + time.Year + time.Month + time.Day + time.Hour + time.Minute + time.Second + time.Millisecond;
                var img = Image.FromFile(path);
                var gs = Graphics.FromImage(img);
                var local = GetLocation(location, img, size, letter.Length);
                var font = new Font("宋体", size);
                Brush br = new SolidBrush(color);
                gs.DrawString(letter, font, br, float.Parse(local[0].ToString()), float.Parse(local[1].ToString()));
                gs.Dispose();
                var newpath = Path.GetDirectoryName(path) + filename + kzName;
                img.Save(newpath);
                img.Dispose();
                File.Copy(newpath, path, true);
                if (File.Exists(newpath))
                {
                    File.Delete(newpath);
                }
            }
            return path;

            #endregion
        }

        /// <summary>
        /// 文字水印位置的方法
        /// </summary>
        /// <param name="location">位置代码</param>
        /// <param name="img">图片对象</param>
        /// <param name="width">宽(当水印类型为文字时,传过来的就是字体的大小)</param>
        /// <param name="height">高(当水印类型为文字时,传过来的就是字符的长度)</param>
        private static ArrayList GetLocation(string location, Image img, int width, int height)
        {
            #region

            var local = new ArrayList();  //定义数组存储位置
            float x = 10;
            float y = 10;

            switch (location)
            {
                case "LT":
                    local.Add(x);
                    local.Add(y);
                    break;
                case "T":
                    x = img.Width / 2 - (width * height) / 2;
                    local.Add(x);
                    local.Add(y);
                    break;
                case "RT":
                    x = img.Width - width * height;
                    break;
                case "LC":
                    // ReSharper disable once PossibleLossOfFraction
                    y = img.Height / 2;
                    break;
                case "C":
                    x = img.Width / 2 - (width * height) / 2;
                    // ReSharper disable once PossibleLossOfFraction
                    y = img.Height / 2;
                    break;
                case "RC":
                    x = img.Width - height;
                    // ReSharper disable once PossibleLossOfFraction
                    y = img.Height / 2;
                    break;
                case "LB":
                    y = img.Height - width - 5;
                    break;
                case "B":
                    x = img.Width / 2 - (width * height) / 2;
                    y = img.Height - width - 5;
                    break;
                default:
                    x = img.Width - width * height;
                    y = img.Height - width - 5;
                    break;
            }
            local.Add(x);
            local.Add(y);
            return local;

            #endregion
        }
        #endregion

        #region 调整光暗
        /// <summary>
        /// 调整光暗
        /// </summary>
        /// <param name="mybm">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        /// <param name="val">增加或减少的光暗值</param>
        public Bitmap LdPic(Bitmap mybm, int width, int height, int val)
        {
            var bm = new Bitmap(width, height);//初始化一个记录经过处理后的图片对象
            int x;//x、y是循环次数，后面三个是记录红绿蓝三个值的
            for (x = 0; x < width; x++)
            {
                int y;//x、y是循环次数，后面三个是记录红绿蓝三个值的
                for (y = 0; y < height; y++)
                {
                    var pixel = mybm.GetPixel(x, y);
                    var resultR = pixel.R + val;//x、y是循环次数，后面三个是记录红绿蓝三个值的
                    var resultG = pixel.G + val;//x、y是循环次数，后面三个是记录红绿蓝三个值的
                    var resultB = pixel.B + val;//x、y是循环次数，后面三个是记录红绿蓝三个值的
                    bm.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 反色处理
        /// <summary>
        /// 反色处理
        /// </summary>
        /// <param name="mybm">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap RePic(Bitmap mybm, int width, int height)
        {
            var bm = new Bitmap(width, height);//初始化一个记录处理后的图片的对象
            int x;
            for (x = 0; x < width; x++)
            {
                int y;
                for (y = 0; y < height; y++)
                {
                    var pixel = mybm.GetPixel(x, y);
                    var resultR = 255 - pixel.R;
                    var resultG = 255 - pixel.G;
                    var resultB = 255 - pixel.B;
                    bm.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 浮雕处理
        /// <summary>
        /// 浮雕处理
        /// </summary>
        /// <param name="oldBitmap">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap Fd(Bitmap oldBitmap, int width, int height)
        {
            var newBitmap = new Bitmap(width, height);
            for (var x = 0; x < width - 1; x++)
            {
                for (var y = 0; y < height - 1; y++)
                {
                    var color1 = oldBitmap.GetPixel(x, y);
                    var color2 = oldBitmap.GetPixel(x + 1, y + 1);
                    var r = Math.Abs(color1.R - color2.R + 128);
                    var g = Math.Abs(color1.G - color2.G + 128);
                    var b = Math.Abs(color1.B - color2.B + 128);
                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;
                    newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return newBitmap;
        }
        #endregion

        #region 拉伸图片
        /// <summary>
        /// 拉伸图片
        /// </summary>
        /// <param name="bmp">原始图片</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        public static Bitmap ResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap bap = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(bap);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bap, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bap.Width, bap.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return bap;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 滤色处理
        /// <summary>
        /// 滤色处理
        /// </summary>
        /// <param name="mybm">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap FilPic(Bitmap mybm, int width, int height)
        {
            var bm = new Bitmap(width, height);//初始化一个记录滤色效果的图片对象
            int x;

            for (x = 0; x < width; x++)
            {
                int y;
                for (y = 0; y < height; y++)
                {
                    var pixel = mybm.GetPixel(x, y);
                    bm.SetPixel(x, y, Color.FromArgb(0, pixel.G, pixel.B));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 左右翻转
        /// <summary>
        /// 左右翻转
        /// </summary>
        /// <param name="mybm">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap RevPicLr(Bitmap mybm, int width, int height)
        {
            var bm = new Bitmap(width, height);
            int y; //x,y是循环次数,z是用来记录像素点的x坐标的变化的
            for (y = height - 1; y >= 0; y--)
            {
                int x; //x,y是循环次数,z是用来记录像素点的x坐标的变化的
                int z; //x,y是循环次数,z是用来记录像素点的x坐标的变化的
                for (x = width - 1, z = 0; x >= 0; x--)
                {
                    var pixel = mybm.GetPixel(x, y);
                    bm.SetPixel(z++, y, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 上下翻转
        /// <summary>
        /// 上下翻转
        /// </summary>
        /// <param name="mybm">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap RevPicUd(Bitmap mybm, int width, int height)
        {
            var bm = new Bitmap(width, height);
            int x;
            for (x = 0; x < width; x++)
            {
                int y;
                int z;
                for (y = height - 1, z = 0; y >= 0; y--)
                {
                    var pixel = mybm.GetPixel(x, y);
                    bm.SetPixel(x, z++, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 压缩图片
        /// <summary>
        /// 压缩到指定尺寸
        /// </summary>
        /// <param name="oldfile">原文件</param>
        /// <param name="newfile">新文件</param>
        public bool Compress(string oldfile, string newfile)
        {
            try
            {
                var img = Image.FromFile(oldfile);
                var newSize = new Size(100, 125);
                var outBmp = new Bitmap(newSize.Width, newSize.Height);
                var g = Graphics.FromImage(outBmp);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.Dispose();
                var encoderParams = new EncoderParameters();
                var quality = new long[1];
                quality[0] = 100;
                var encoderParam = new EncoderParameter(Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                var arrayIci = ImageCodecInfo.GetImageEncoders();
                var jpegIci = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
                img.Dispose();
                if (jpegIci != null)
                {
                    outBmp.Save(newfile, ImageFormat.Jpeg);
                }
                outBmp.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 图片灰度化
        public Color Gray(Color c)
        {
            var rgb = Convert.ToInt32(((0.3 * c.R) + (0.59 * c.G)) + (0.11 * c.B));
            return Color.FromArgb(rgb, rgb, rgb);
        }
        #endregion

        #region 转换为黑白图片

        /// <summary>
        /// 转换为黑白图片
        /// </summary>
        /// <param name="mybm"></param>
        /// <param name="width">图片的长度</param>
        /// <param name="height">图片的高度</param>
        public Bitmap BwPic(Bitmap mybm, int width, int height)
        {
            var bm = new Bitmap(width, height);
            int x; //x,y是循环次数，result是记录处理后的像素值
            for (x = 0; x < width; x++)
            {
                int y; //x,y是循环次数，result是记录处理后的像素值
                for (y = 0; y < height; y++)
                {
                    var pixel = mybm.GetPixel(x, y);
                    var result = (pixel.R + pixel.G + pixel.B) / 3; //x,y是循环次数，result是记录处理后的像素值
                    bm.SetPixel(x, y, Color.FromArgb(result, result, result));
                }
            }
            return bm;
        }
        #endregion

        #region 获取图片中的各帧

        /// <summary>
        /// 获取图片中的各帧
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavedPath"></param>
        public void GetFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);
            int count = gif.GetFrameCount(fd); //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            for (int i = 0; i < count; i++)    //以Jpeg格式保存各帧
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }
        #endregion

        #region 将图片从Image转换成byte
        /// <summary>
        /// 将图片从Image转换成byte
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToByte(Image image)
        {
            // ImageFormat formate = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }

        }
        #endregion

        #region 将图片从Image转换成Stream
        public static Stream ImageToStream(Image image)
        {
            MemoryStream ms = new MemoryStream();
            ImageFormat format = GetImageFormate(image);
            image.Save(ms, format);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
        #endregion

        #region 将图片转换成Base64
        /// <summary>
        /// 将图片转换成Base64
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, GetImageFormate(image));
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            string base64 = Convert.ToBase64String(arr);
            return base64;
        }
        /// <summary>
        /// 将图片转换成Base64编码,带有头部
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64WithHeader(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, GetImageFormate(image));
            byte[] bytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(bytes, 0, (int)ms.Length);
            ms.Close();
            string base64 = string.Format(@"data:image/{0};base64,{1}", GetImageExtension(image), Convert.ToBase64String(bytes));
            return base64;
        }
        #endregion

        #region 将Base64的字符转换成图片
        /// <summary>
        /// 将Base64编码转换成图片
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                MemoryStream memStream = new MemoryStream(bytes);
                Image img = Image.FromStream(memStream);
                return img;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将带有头部编码的Base64编码转换成图片
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Image Base64ToImageWithHeader(string base64)
        {
            try
            {
                base64 = base64.Substring(base64.IndexOf(',') + 1);
                byte[] bytes = Convert.FromBase64String(base64);
                MemoryStream memStream = new MemoryStream(bytes);
                Image img = Image.FromStream(memStream);
                return img;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 去除base64的头部
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string Base64HeaderRemove(string base64)
        {
            try
            {
                base64 = base64.Substring(base64.IndexOf(',') + 1);
                return base64;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region 将byte转换成图片
        /// <summary>
        /// 将byte转换成图片
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image ByteToImage(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        #endregion

        #region 获取特定的图像编解码信息
        /// <summary>
        /// 获取特定的图像编解码信息
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
        #endregion

        #region 产生波形滤镜效果

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue"></param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    var dx = bXDir ? (2 * Math.PI * j) / dBaseAxisLen : (2 * Math.PI * i) / dBaseAxisLen;
                    dx += dPhase;
                    var dy = Math.Sin(dx);
                    // 取得当前点的颜色
                    var nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    var nOldY = bXDir ? j : j + (int)(dy * dMultValue);
                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
        #endregion

        #region 根据图片获取扩展名
        /// <summary>
        /// 根据图片获取扩展名
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string GetImageExtension(Image img)
        {
            Type type = typeof(ImageFormat);
            var imageFormatList = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i != imageFormatList.Length; i++)
            {
                ImageFormat formatClass = (ImageFormat)imageFormatList[i].GetValue(null, null);
                if (formatClass.Guid.Equals(img.RawFormat.Guid))
                {
                    return imageFormatList[i].Name;
                }
            }
            return "";
        }
        #endregion

        #region 获取图片的格式
        /// <summary>
        /// 获取图片的格式
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormate(Image img)
        {
            var type = typeof(ImageFormat);
            var imageFormatList = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i != imageFormatList.Length; i++)
            {
                ImageFormat formatClass = (ImageFormat)imageFormatList[i].GetValue(null, null);
                if (formatClass.Guid.Equals(img.RawFormat.Guid))
                {
                    return formatClass;
                }
            }
            return ImageFormat.Jpeg;
        }
        #endregion

    }
}
