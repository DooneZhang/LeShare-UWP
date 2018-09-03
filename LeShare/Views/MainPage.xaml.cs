using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using LeShare.Views;
using Windows.Web.Http;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace LeShare
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

       
        public MainPage()
        {
          

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;      
        }
          
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。

            base.OnNavigatedTo(e);
            //注意，应用从挂起恢复时不会调用此方法
            //为了保证数据完整性，此方法可灵活放置在恢复页面的事件中（如页面onload事件），请确保和TrackPageEnd成对使用并避免重复调用
           
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //注意：以下几种情形不会调用此方法：
            //1.首页按“后退”键
            //2.应用挂起时
            //为了保证数据完整性，此方法可灵活放置在跳转页面（离开页面）或离开应用的事件中，请确保和TrackPageStart成对使用并避免重复调用
            base.OnNavigatedFrom(e);
           
        }

        private TextBlock Info;
        private StackPanel InfoControl;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {           
             
        }  

        public static class Menu
        {           
            //定义视频名称选项
            public static string name;
         
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private async void MorePlayVideo_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?Productid=9nblggh42kp6"));
        }

        private async void Hexagon_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?Productid=9pc1r6m0ssq3"));
        }

        private async void ClearSquare_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?Productid=9nq452490z8d"));
        }

        private async void Shift_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?Productid=9pc1r6m0ssq3"));
        }

        private async void Info_Loaded(object sender, RoutedEventArgs e)
        {

            Info = (TextBlock)sender;
            string Uri = "http://s.moreplay.cn/Control.json";
            Uri uri = new Uri(Uri);
            HttpClient client = new HttpClient();
            string ControlJson = await client.GetStringAsync(uri);
            JObject JSON = JObject.Parse(ControlJson);
            string info = (string)JSON["Info"];
            Info.Text = info;

        }

        private void InfoControl_Loaded(object sender, RoutedEventArgs e)
        {
            InfoControl = (StackPanel)sender;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            InfoControl.Visibility = Visibility.Collapsed;
        }

        //视频类
        private void Iqiyi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "Iqiyi";
            Frame.Navigate(typeof(TaskPage));
            //点击爱奇艺按钮的统计
          
        }

        private void YouKu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "YouKu";
            Frame.Navigate(typeof(TaskPage));
            //点击优酷按钮的统计
          
        }
        private void QQLive_Click(object sender, RoutedEventArgs e)
        {

            Menu.name = "QQLive";
            Frame.Navigate(typeof(TaskPage));
            //点击腾讯视频按钮的统计
           
        }

        private void LeShi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "LeShi";
            Frame.Navigate(typeof(TaskPage));
            //点击乐视视频按钮的统计
           
        }

        private void SouHu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "SouHu";
            Frame.Navigate(typeof(TaskPage));
           
        }
        private void PPTV_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PPTV";
            Frame.Navigate(typeof(TaskPage));
            //点击PPTV聚力按钮的统计
           
        }



        private void WangYiYun_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "WangYiYun";
            Frame.Navigate(typeof(TaskPage));
            //点击网易云音乐按钮的统计
          
        }

        private void XiaMi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "XiaMi";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
           
        }

        //下载类
        private void Thunder_Click(object sender, RoutedEventArgs e)
        {

            Menu.name = "Thunder";
            Frame.Navigate(typeof(TaskPage));
            //点击迅雷按钮的统计
            
        }

        private void Notice_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BBS_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BBS));
            //点击论坛按钮的统计
          
        }


        private void PJ_Iqiyi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_Iqiyi";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
           
        }

        private void PJ_YouKu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_YouKu";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            
        }

        private void PJ_Thunder_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_Thunder";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
           
        }

        private void PJ_BaiduYun_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_BaiduYun";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "Sales";
            Frame.Navigate(typeof(TaskPage));
            //点击活动按钮的统计
           
        }      

        private void QQMusic_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "QQMusic";
            Frame.Navigate(typeof(TaskPage));
            //点击QQ音乐按钮的统计
          
        }

        private void KuGou_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "Iqiyi";
            Frame.Navigate(typeof(TaskPage));
            //点击爱奇艺按钮的统计
          
        }

        private void KuWo_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "Iqiyi";
            Frame.Navigate(typeof(TaskPage));
            //点击爱奇艺按钮的统计
          
        }

        private void MangGuo_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "MangGuo";
            Frame.Navigate(typeof(TaskPage));
            //点击芒果按钮的统计
          
        }

        private void BaiduYun_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "BaiduYun";
            Frame.Navigate(typeof(TaskPage));
            //点击百度网盘按钮的统计
        }

        private void KanKan_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "KanKan";
            Frame.Navigate(typeof(TaskPage));
            //点击看看按钮的统计
        }

        private void WenKu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "WenKu";
            Frame.Navigate(typeof(TaskPage));
            //点击百度文库按钮的统计
        }

        private void BaoFen_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "BaoFen";
            Frame.Navigate(typeof(TaskPage));
            //点击暴风影音按钮的统计
           
        }

        private void Iqiyi_pj_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_Iqiyi";
            Frame.Navigate(typeof(TaskPage));
            //点击暴风影音按钮的统计
        }

        private void Youku_pj_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_YouKu";
            Frame.Navigate(typeof(TaskPage));
            //点击暴风影音按钮的统计
        }

        private void Thunder_pj_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_Thunder";
            Frame.Navigate(typeof(TaskPage));
            //点击暴风影音按钮的统计
        }

        private void Baiduyun_pj_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_BaiduYun";
            Frame.Navigate(typeof(TaskPage));
            //点击暴风影音按钮的统计
        }

        private void Comments_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Menu.name = "Comments";
            Frame.Navigate(typeof(WebPage));
            //点击暴风影音按钮的统计
        }

        private async void Feedback_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?Productid=9PNVKQ808V57"));
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //把Split的打开状态调整为相反
            MenuView.IsPaneOpen = !MenuView.IsPaneOpen;
        }

        private void Help_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }

        private async void BBS_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://s.moreplay.cn"));
        }

        private void Quit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void About_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private async void More_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://publisher/?name=MEP Studio"));
        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //把Split的打开状态调整为相反
            SettingsView.IsPaneOpen = !SettingsView.IsPaneOpen;
        }

       
    }
}
