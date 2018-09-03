using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LeShare.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        public SplashPage()
        {
            this.InitializeComponent();
        }

        int n = 0;
        ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
        //获取本地应用设置容器

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!settings.Values.ContainsKey("First"))
            { //应用首次启动，必定不会含"First"这个键，让应用导航到GuidePage这个页面，GuidePage这个页面就是对应用的介绍啦

            }
            else
            {
                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 3)
                };
                timer.Tick += (sender, args) =>
                {

                    if (n == 0)
                    {
                        Frame.Navigate(typeof(MainPage));
                        n = 1;
                    }
                };
                timer.Start();
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            //判断settings容器里面有没有"First"这个键
            if (!settings.Values.ContainsKey("First"))
            { //应用首次启动，必定不会含"First"这个键，让应用导航到GuidePage这个页面，GuidePage这个页面就是对应用的介绍啦
                Frame.Navigate(typeof(GuidePage));
                //在settings容器里面写入"First"这个键值对，应用再次启动时，就不会在导航到介绍页面了。
                settings.Values["First"] = "yes";
            }
            else
            {

            }
        }


    }
}
