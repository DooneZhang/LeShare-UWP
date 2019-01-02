using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.ViewManagement;
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
        //声明 InterstitialAd 对象和几个字符串字段，这些字段代表间隙广告的应用程序 ID 和广告单元 ID
        InterstitialAd InterstitialAd = null;
        string myAppId = "9pnvkq808v57";
        string myAdUnitId = "1100037148";
        public SplashPage()
        {
            this.InitializeComponent();
            InterstitialAd = new InterstitialAd();
            InterstitialAd.RequestAd(AdType.Display, myAppId, myAdUnitId);

            if (settings.Values.ContainsKey("First"))
            { //应用首次启动，必定不会含"First"这个键，让应用导航到GuidePage这个页面，GuidePage这个页面就是对应用的介绍啦
                InterstitialAd.AdReady += MyInterstitialAd_AdReady;
                InterstitialAd.ErrorOccurred += MyInterstitialAd_ErrorOccurred;
                InterstitialAd.Completed += MyInterstitialAd_Completed;
                InterstitialAd.Cancelled += MyInterstitialAd_Cancelled;

            }
        }
        //以下四个为广告事件
        void MyInterstitialAd_AdReady(object sender, object e)
        {          
                InterstitialAd.Show();          
        }

        void MyInterstitialAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            // Your code goes here.
            Frame.Navigate(typeof(MainPage));
            istrue = true;
        }

        void MyInterstitialAd_Completed(object sender, object e)
        {
            Frame.Navigate(typeof(MainPage));
            istrue = true;
        }

        void MyInterstitialAd_Cancelled(object sender, object e)
        {
            Frame.Navigate(typeof(MainPage));
            istrue = true;
        }

        int n = 0;
        bool istrue=false;
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
                    Interval = new TimeSpan(0, 0, 7)
                };
                timer.Tick += (sender, args) =>
                {

                    if (n == 0&& istrue == false)
                    {
                        Frame.Navigate(typeof(MainPage));
                        InterstitialAd.Close();
                        n = 1;
                    }
                };
                timer.Start();
            }
        }
        private StatusBar statusbar;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
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
             
            // 判断是否存在 StatusBar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                //静态方法取得当前 StatusBar 实例
                statusbar = StatusBar.GetForCurrentView();

                //显示状态栏
                await statusbar.HideAsync();
            }
        }


    }
}
