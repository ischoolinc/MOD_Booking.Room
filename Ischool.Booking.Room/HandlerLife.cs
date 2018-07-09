using CefSharp;
using CefSharp.WinForms;
using FISCA;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ischool.Booking.Room
{
    class HandlerLife : ILifeSpanHandler
    {

        bool ILifeSpanHandler.DoClose(IWebBrowser browserControl, IBrowser browser)
        { return false; }

        void ILifeSpanHandler.OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public delegate void TestDelegate(string message);

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {

            newBrowser = null;
            string target_Url = HttpUtility.UrlDecode(targetUrl);
            target_Url = target_Url.ToLower();
            if (target_Url.Contains("http://www.google.com/url?q=http://1campus.com.tw/"))
            {
                //來自交流平台
                string new_url = target_Url.Replace("http://www.google.com/url?q=http://1campus.com.tw/", "");

                new_url = HttpUtility.UrlDecode(new_url);
                new_url = new_url.Remove(new_url.IndexOf('&'), new_url.Length - new_url.IndexOf('&'));

                TestDelegate ma2 = delegate (string input)
                {
                    try
                    {
                        Features.Invoke(input);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("啟動功能發生錯誤：\n" + ex.Message);
                    }
                };

                FISCA.Presentation.MotherForm.Form.Invoke(ma2, new[] { new_url });
                return true;
            }
            else if (target_Url.Contains("http://1campus.com.tw/"))
            {
                //來自其它HTML位置
                string new_url = target_Url.Replace("http://1campus.com.tw/", "");
                new_url = HttpUtility.UrlDecode(new_url);

                TestDelegate ma2 = delegate (string input)
                {
                    try
                    {
                        Features.Invoke(input);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("啟動功能發生錯誤：\n" + ex.Message);
                    }
                };
                FISCA.Presentation.MotherForm.Form.Invoke(ma2, new[] { new_url });

                return true;
            }
            else
            {
                var chromiumWebBrowser = (ChromiumWebBrowser)browserControl; chromiumWebBrowser.Load(targetUrl);
                return true;
            }
        }
    }
}
