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
using static LeShare.MainPage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LeShare.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WebPage : Page
    {
        public WebPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch (Menu.name)
            {
                //留言板
                case "Comments": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/comment/index.html")); break;
                //爱奇艺
                case "Iqiyi": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/aiqiyi/index.html")); break;
                //迅雷
                case "Thunder": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/thunder/index.html")); break;
                //优酷
                case "YouKu": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/youku/index.html")); break;
                //腾讯视频
                case "QQLive": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/qqlive/index.html")); break;
                //响巢看看
                case "KanKan": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/kankan/index.html")); break;
                //虾米音乐
                case "XiaMi": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/xiami/index.html")); break;
                //暴风影音
                case "BaoFeng": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/baofeng/index.html")); break;
                //乐视视频
                case "LeShi": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/leshi/index.html")); break;
                //搜狐视频
                case "SouHu": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/souhu/index.html")); break;
                //QQ音乐
                case "QQMusic": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/qqmusic/index.html")); break;
                //网易云音乐
                case "WangYiYun": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/wangyi/index.html")); break;
                //酷狗音乐
                case "KuGou": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/kugou/index.html")); break;
                //酷我音乐
                case "KuWo": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/kuwo/index.html")); break;
                //百度音乐
                case "BaiduMusic": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/baidumusic/index.html")); break;
                //QQ旋风
                case "QQXuanFeng": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/xuanfeng/index.html")); break;
                //百度云
                case "BaiduYun": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/baiduyun/index.html")); break;
                //PPTV
                case "PPTV": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/pptv/index.html")); break;
                //芒果TV
                case "MangGuo": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/mangguo/index.html")); break;
                //百度文库
                case "BaiduWenku": WebSite.Navigate(new Uri("ms-appx-web:///Assets/HTML/getvip/menu/wenku/index.html")); break;
                //会员体验活动
                case "Sales": WebSite.Navigate(new Uri("http://s.moreplay.cn/index.php?c=list&sortid=3&page=1")); break;
                //下载破解百度云
                case "PJ_BaiduYun": WebSite.Navigate(new Uri("http://s.moreplay.cn/index.php?c=read&id=29&page=1")); break;
                //下载破解迅雷
                case "PJ_Thunder": WebSite.Navigate(new Uri("http://s.moreplay.cn/index.php?c=read&id=30&page=1")); break;
                //下载破解爱奇艺
                case "PJ_Iqiyi": WebSite.Navigate(new Uri("http://s.moreplay.cn/index.php?c=list&sortid=15&page=1")); break;
                //下载破解优酷
                case "PJ_YouKu": WebSite.Navigate(new Uri("http://s.moreplay.cn/index.php?c=list&sortid=15&page=1")); break;
                //下载破解播放器
                case "PJ_Player": WebSite.Navigate(new Uri("http://s.moreplay.cn/index.php?c=list&sortid=15&page=1")); break;               
            }
        }
    }
}
