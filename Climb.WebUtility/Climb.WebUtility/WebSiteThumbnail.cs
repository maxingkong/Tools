using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Climb.WebUtility
{
    public class WebSiteThumbnail
    {
        Bitmap _bitmap;
        readonly string _mHtml;
        readonly int _mBrowserWidth;
        readonly int _mBrowserHeight;
        int _mThumbnailWidth, _mThumbnailHeight;
        private readonly int _bitmapCount = 0;
        public static float CureentH = 0;
        public static float CureentW = 0;


        private readonly List<string> _htmlList = new List<string>();
        public Dictionary<string, string> PicFilePathDic;

        public string MathMl { get; set; }

        public WebSiteThumbnail(string html, int browserWidth, int browserHeight, int thumbnailWidth, int thumbnailHeight)
        {
            _mHtml = html;
            _mBrowserHeight = browserHeight;
            _mBrowserWidth = browserWidth;
            _mThumbnailWidth = thumbnailWidth;
            _mThumbnailHeight = thumbnailHeight;
     
        }

        public WebSiteThumbnail(int bitmapct, List<string> list, Bitmap bitmap)
        {
            _bitmapCount = bitmapct;
            PicFilePathDic = new Dictionary<string, string>();
            _htmlList = list;
            this._bitmap = bitmap;
        }

        public WebSiteThumbnail(Bitmap bitmap)
        {
            this._bitmap = bitmap;
        }

        public static Bitmap GetWebSiteThumbnail(string html, int browserWidth, int browserHeight, int thumbnailWidth, int thumbnailHeight)
        {
            WebSiteThumbnail thumbnailGenerator = new WebSiteThumbnail(html, browserWidth, browserHeight, thumbnailWidth, thumbnailHeight);
            return thumbnailGenerator.GenerateWebSiteThumbnailImage();
        }



        public Bitmap GenerateWebSiteThumbnailImage()
        {
            Thread m_thread = new Thread(new ThreadStart(_GenerateWebSiteThumbnailImage));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return _bitmap;
        }

        private void _GenerateWebSiteThumbnailImage()
        {
            try
            {
                WebBrowser mWebBrowser = new WebBrowser();
                mWebBrowser.ScrollBarsEnabled = false;
                //m_WebBrowser.Navigate(m_Url);
                mWebBrowser.DocumentText = _mHtml;
                mWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
                while (mWebBrowser.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();
                mWebBrowser.Dispose();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

        #region 获取mathml的方法




        private void Delay(int Millisecond) //延迟系统时间，但系统又能同时能执行其它任务；
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(Millisecond) > DateTime.Now)
            {
                Application.DoEvents();//转让控制权            
            }
            return;
        }
        #endregion
        private bool WaitWebPageLoad(WebBrowser webBrowser1)
        {
            int i = 0;
            while (true)
            {
                Delay(50);  //系统延迟50毫秒，够少了吧！             
                if (webBrowser1.ReadyState == WebBrowserReadyState.Complete) //先判断是否发生完成事件。
                {
                    if (!webBrowser1.IsBusy) //再判断是浏览器是否繁忙                  
                    {
                        i = i + 1;
                        HtmlElement element = webBrowser1.Document.GetElementById("MathJax-Element-1-Frame");

                        if (element != null && !string.IsNullOrEmpty(element.InnerHtml)) //这是判断没有网络的情况下                           
                        {
                            string mthml = element.GetAttribute("data-mathml");
                            if (!string.IsNullOrEmpty(mthml))
                            {
                                return true;
                            }

                        }
                        continue;
                    }
                    i = 0;
                }
            }
        }
        private void WebBrowser_DocumentCompleted222(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser m_WebBrowser = (WebBrowser)sender;
            Console.WriteLine("完成加载");
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser m_WebBrowser = (WebBrowser)sender;
            m_WebBrowser.ClientSize = new Size(this._mBrowserWidth, this._mBrowserHeight);
            m_WebBrowser.ScrollBarsEnabled = false;
            // 这里 获取borwser内容的 高度 和宽度；
            HtmlElement element = m_WebBrowser.Document.GetElementById("sp_xx");
            string html = element.InnerText;

            string[] ary = html.Split('#');
            int w = Convert.ToInt32(ary[0]);
            int h = Convert.ToInt32(ary[1]);

            _bitmap = new Bitmap(w, h);
            CureentH = float.Parse(ary[1]);
            CureentW = float.Parse(ary[0]); ;
            m_WebBrowser.BringToFront();
            m_WebBrowser.DrawToBitmap(_bitmap, m_WebBrowser.Bounds);
            _bitmap = (Bitmap)_bitmap.GetThumbnailImage(w, h, null, IntPtr.Zero);
        }



        #region  批量获取图片的方法
        public void GetBitmaps(string html)
        {
            Thread m_thread = new Thread(new ThreadStart(() =>
            {

                GenerateWebSiteThumbnailImageList(html);
            }));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();

        }

        private void GenerateWebSiteThumbnailImageList(string html)
        {
            try
            {
                Console.WriteLine("开始使用webbrowzer 获取图片");
                WebBrowser mWebBrowser = new WebBrowser();
                mWebBrowser.ScrollBarsEnabled = false;
                //m_WebBrowser.Navigate(m_Url);
                mWebBrowser.DocumentText = html;
                mWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompletedSave);
                WaitWebPageLoad222(mWebBrowser);

                //开始解析
                mWebBrowser.Dispose();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }


        private void WaitWebPageLoad222(WebBrowser m_WebBrowser)
        {
            //int n = 0;
            //string sUrl;

            //while (true)
            //{
            //    Delay(50);  //系统延迟50毫秒，够少了吧！             
            //    if (m_WebBrowser.ReadyState == WebBrowserReadyState.Complete) //先判断是否发生完成事件。
            //    {
            //        if (!m_WebBrowser.IsBusy) //再判断是浏览器是否繁忙                  
            //        {
            //            n = n + 1;

            //            Console.WriteLine("完成图片的加载");


            //            break;

            //        }

            //        n = 0;
            //    }
            //}
            //m_WebBrowser.ScrollBarsEnabled = false;
            //// 这里 获取borwser内容的 高度 和宽度；
            //int currY = 0;
            //int allHeight = 0;
            //Console.WriteLine("获取对象");
            //HtmlElement element;
            //string[] ary;
            //for (int i = 0; i < _bitmapCount; i++)
            //{

            //    element = m_WebBrowser.Document.GetElementById("sp_xx" + i);
            //    ary = element.InnerText.Split('#');
            //    int w = Convert.ToInt32(ary[0]);
            //    int h = Convert.ToInt32(ary[1]);
            //    allHeight += h;
            //}
            //allHeight += 1;
            //int allWight = 720;
            //int width = m_WebBrowser.Document.Body.ScrollRectangle.Width < 1024 ? 1024 : m_WebBrowser.Document.Body.ScrollRectangle.Width;
            //int height = m_WebBrowser.Document.Body.ScrollRectangle.Height < 768 ? 768 : m_WebBrowser.Document.Body.ScrollRectangle.Height;

            //// 调节webBrowser的高度和宽度  --  （只用动态创建的WebBrowser才可以修改其大小，不知道为什么）
            //m_WebBrowser.Height = height;
            //m_WebBrowser.Width = allWight;
            ////滚动整个网页 设置此元素的上边缘或下边缘与此文档窗口对齐为止---这样可以截取网页滚动条中的内容
            //m_WebBrowser.Document.Body.ScrollIntoView(true);
            //Bitmap bitmap = new Bitmap(width, allHeight);  // 创建高度和宽度与网页相同的图片
            //Rectangle rectangle = new Rectangle(0, 0, width, allHeight);  // 绘图区域
            //m_WebBrowser.DrawToBitmap(bitmap, rectangle);  // 截图
            ////string  filename = PicFilePath + Guid.NewGuid().ToString("N") + ".png";
            ////bitmap.Save(filename);
            //System.Drawing.Graphics imgGraphics = Graphics.FromImage(bitmap);
            //int curry = 0;
            //System.Drawing.Image img = bitmap;
            //System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);
            //Console.WriteLine("共有图片：" + _bitmapCount);
            //string filename;
            //System.Drawing.Rectangle cropArea;
            ////System.Drawing.Bitmap bmpCrop;
            //for (int i = 0; i < _bitmapCount; i++)
            //{

            //    element = m_WebBrowser.Document.GetElementById("sp_xx" + i);
            //    ary = element.InnerText.Split('#');
            //    int w = Convert.ToInt32(ary[0]);
            //    int h = Convert.ToInt32(ary[1]);

            //    if (i == 116)
            //    {
            //        Console.WriteLine("1");
            //    }
            //    cropArea = new System.Drawing.Rectangle(0, curry, w, h); //要截取的区域大小
            //    using (System.Drawing.Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat))
            //    {
            //        //bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            //        filename = PicFilePath + Guid.NewGuid().ToString("N") + DateTime.Now.ToString("mm-ss-ms") + ".png";
            //        bmpCrop.Save(filename);
            //        Console.WriteLine("保存图片：" + filename);
            //        //  element = m_WebBrowser.Document.GetElementById("txt_" + i);

            //        if (!PicFilePathDic.ContainsKey(_htmlList[i]))
            //        {
            //            PicFilePathDic.Add(_htmlList[i], filename);
            //        }

            //        // bmpCrop.Dispose();
            //        //  GC.Collect();
            //    }



            //    curry += h;

            //}

            //img.Dispose();
            //bmpImage.Dispose();
        }

        private void WebBrowser_DocumentCompletedSave(object sender, WebBrowserDocumentCompletedEventArgs e)
        {


        }



        #endregion
    }
}
