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
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Microsoft.Services.Store.Engagement;
using Windows.Storage;
using Windows.Services.Store;
using System.Threading.Tasks;
using static LeShare.App;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace LeShare
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //应用商店加载项
        private StoreContext context = null;
        StoreProduct subscriptionStoreProduct;
        // Assign this variable to the Store ID of your subscription add-on.
        private string subscriptionStoreId = "9N1RFBKS8DDT";
        private string productID = "";
        //默认没有订阅任何产品
        private bool userOwnsSubscription = false;
        private string AccoutJson = "";

        //自定义统计事件
        StoreServicesCustomEventLogger logger = StoreServicesCustomEventLogger.GetDefault();
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
        private StatusBar statusbar;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 判断是否存在 StatusBar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                //静态方法取得当前 StatusBar 实例
                statusbar = StatusBar.GetForCurrentView();

                //显示状态栏
                await statusbar.HideAsync();
            }
            //判断反馈按钮是否可用
            if (StoreServicesFeedbackLauncher.IsSupported())
            {
                this.Feedback.Visibility = Visibility.Visible;
            }
            //判断是否打开推送开关,如何打开则显示开关
            if ((bool)settings.Values["Push_Enable"] == true)
            {
                Push_Switch.IsOn = true;
            }
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
            logger.Log("爱奇艺");
        }

        private void YouKu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "YouKu";
            Frame.Navigate(typeof(TaskPage));
            //点击优酷按钮的统计
            logger.Log("优酷");
        }
        private void QQLive_Click(object sender, RoutedEventArgs e)
        {

            Menu.name = "QQLive";
            Frame.Navigate(typeof(TaskPage));
            //点击腾讯视频按钮的统计
            logger.Log("腾讯视频");
        }

        private void LeShi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "LeShi";
            Frame.Navigate(typeof(TaskPage));
            //点击乐视视频按钮的统计
            logger.Log("乐视");
        }

        private void SouHu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "SouHu";
            Frame.Navigate(typeof(TaskPage));
            logger.Log("搜狐");
        }
        private void PPTV_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PPTV";
            Frame.Navigate(typeof(TaskPage));
            //点击PPTV聚力按钮的统计
            logger.Log("PPTV");
        }

        private void WangYiYun_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "WangYiYun";
            Frame.Navigate(typeof(TaskPage));
            //点击网易云音乐按钮的统计
            logger.Log("网易云音乐");
        }

        private void XiaMi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "XiaMi";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            logger.Log("虾米音乐");
        }

        //下载类
        private void Thunder_Click(object sender, RoutedEventArgs e)
        {

            Menu.name = "Thunder";
            Frame.Navigate(typeof(TaskPage));
            //点击迅雷按钮的统计
            logger.Log("迅雷");
        }

        private void Notice_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BBS_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BBS));
            //点击论坛按钮的统计
            logger.Log("论坛");
        }


        private void PJ_Iqiyi_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_Iqiyi";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            logger.Log("破解_爱奇艺");
        }

        private void PJ_YouKu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_YouKu";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            logger.Log("破解_优酷");
        }

        private void PJ_Thunder_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_Thunder";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            logger.Log("破解_迅雷");
        }

        private void PJ_BaiduYun_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "PJ_BaiduYun";
            Frame.Navigate(typeof(TaskPage));
            //点击虾米音乐按钮的统计
            logger.Log("破解_百度云");
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "Sales";
            Frame.Navigate(typeof(TaskPage));
            //点击活动按钮的统计
            logger.Log("活动");
        }      

        private void QQMusic_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "QQMusic";
            Frame.Navigate(typeof(TaskPage));
            //点击QQ音乐按钮的统计
            logger.Log("QQ音乐");
        }

        private void KuGou_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "KuGou";
            Frame.Navigate(typeof(TaskPage));
            //点击爱奇艺按钮的统计
            logger.Log("酷狗音乐");
        }

        private void KuWo_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "KuWo";
            Frame.Navigate(typeof(TaskPage));
            //点击爱奇艺按钮的统计
            logger.Log("酷我音乐");
        }

        private void MangGuo_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "MangGuo";
            Frame.Navigate(typeof(TaskPage));
            //点击芒果按钮的统计
            logger.Log("芒果视频");
        }

        private void BaiduYun_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "BaiduYun";
            Frame.Navigate(typeof(TaskPage));
            //点击百度网盘按钮的统计
            logger.Log("百度云");
        }

        private void KanKan_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "KanKan";
            Frame.Navigate(typeof(TaskPage));
            //点击看看按钮的统计
            logger.Log("看看视频");
        }

        private void WenKu_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "WenKu";
            Frame.Navigate(typeof(TaskPage));
            //点击百度文库按钮的统计
            logger.Log("百度文库");
        }

        private void BaoFen_Click(object sender, RoutedEventArgs e)
        {
            Menu.name = "BaoFen";
            Frame.Navigate(typeof(TaskPage));
            //点击暴风影音按钮的统计
            logger.Log("暴风影音");
        }

        private void Comments_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Menu.name = "Comments";
            Frame.Navigate(typeof(WebPage));
            //点击暴风影音按钮的统计
            logger.Log("留言");
        }

        private async void Feedback_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //调用系统级反馈
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
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
            SettingView.IsPaneOpen = !SettingView.IsPaneOpen;
            MenuView.IsPaneOpen = !MenuView.IsPaneOpen;
        }
        ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
        //获取本地应用设置容器
        private async void Push_Switch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SettingView.IsPaneOpen == true)
            {
                context = StoreContext.GetDefault();
                productID = subscriptionStoreId;
                //获取订阅信息.传给userOwnsSubscription值。
                userOwnsSubscription = await CheckIfUserHasSubscriptionAsync();
                if (userOwnsSubscription == true)
                {
                    ToggleSwitch toggleSwitch = sender as ToggleSwitch;

                    if (toggleSwitch != null)
                    {
                        if (toggleSwitch.IsOn == true)
                        {
                            settings.Values["Push_Enable"] = true;
                            //执行一遍注册、判断、再打开通道的操作
                            InitNotificationsAsync();
                        }
                        else
                        {
                            settings.Values["Push_Enable"] = false;
                            //执行一遍注册、判断、再关闭通道的操作
                            InitNotificationsAsync();
                        }
                    }
                }
                else
                {
                    ToggleSwitch toggleSwitch = sender as ToggleSwitch;
                    var messageDig = new MessageDialog("只有订阅用户才可以享受推送通知服务！");
                    //展示窗口，获取按钮是否退出  
                    var result = await messageDig.ShowAsync();
                    settings.Values["Push_Enable"] = false;
                    toggleSwitch.IsOn = false;                   
                }
            }
        }


        // This is the entry point method for the example.检查加载项信息，是否有可试用
        public async Task SetupSubscriptionInfoAsync()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            //检查用户是否有订阅的许可证传给userOwnsSubscription值
            userOwnsSubscription = await CheckIfUserHasSubscriptionAsync();
            if (userOwnsSubscription)
            {
                Loading.IsActive = false;
                var messageDig = new MessageDialog("您已订阅此服务！");
                //展示窗口，获取按钮是否退出  
                var result = await messageDig.ShowAsync();
                // 解锁所有加载项订阅的特性功能
                return;
            }

            //获取订阅信息.传给subscriptionStoreProduct值。
            subscriptionStoreProduct = await GetSubscriptionProductAsync();
            if (subscriptionStoreProduct == null)
            {
                Loading.IsActive = false;
                var messageDig = new MessageDialog("此订阅暂不提供！");
                //展示窗口，获取按钮是否退出  
                var result = await messageDig.ShowAsync();
                return;
            }

            //检查第一个SKU是否是试用版，并通知客户试用版可用。
            //如果试用可用，Skus数组将始终具有两个可购买的SKU，以及
            // first one is the trial. Otherwise,第一个是审判。否则，该数组将只有一个SKU。
            StoreSku sku = subscriptionStoreProduct.Skus[0];
            if (sku.SubscriptionInfo.HasTrialPeriod)
            {
                //如果存在试用
                //您可以在这里向客户显示订阅购买信息。你可以使用
                // sku.SubscriptionInfo.BillingPeriod and sku.SubscriptionInfo.BillingPeriodUnit
                //提供续约详情。
            }
            else
            {
                //不存在试用
                //您可以在这里向客户显示订阅购买信息。你可以使用
                // sku.SubscriptionInfo.BillingPeriod and sku.SubscriptionInfo.BillingPeriodUnit
                //提供续约详情。

            }

            // Prompt the customer to purchase the subscription.
            await PromptUserToPurchaseAsync();
        }

        //检查用户是否有订阅的许可证
        private async Task<bool> CheckIfUserHasSubscriptionAsync()
        {
            StoreAppLicense appLicense = await context.GetAppLicenseAsync();
            //检查客户是否具有订阅权限。
            foreach (var addOnLicense in appLicense.AddOnLicenses)
            {
                StoreLicense license = addOnLicense.Value;
                if (license.SkuStoreId.StartsWith(productID))
                {
                    if (license.IsActive)
                    {
                        // The expiration date is available in the license.ExpirationDate property.                      
                        return true;
                        //客户有订阅的许可证。
                    }
                }
            }
            //客户没有订阅的许可证。
            return false;
        }

        //查找产品订阅信息
        private async Task<StoreProduct> GetSubscriptionProductAsync()
        {
            //加载此应用程序的可销售外接程序，并检查试用是否仍然存在
            //这个客户可以使用。如果他们以前获得过审判，他们就不会。
            //能够再次获得试用，Store..Skus属性将
            //仅包含一个SKU。
            StoreProductQueryResult result =
                await context.GetAssociatedStoreProductsAsync(new string[] { "Durable" });

            if (result.ExtendedError != null)
            {
                Loading.IsActive = false;
                var messageDig1 = new MessageDialog("获得此订阅时出现了一些问题。");
                //展示窗口，获取按钮是否退出  
                var result1 = await messageDig1.ShowAsync();
                return null;
            }
            //查找表示订阅的产品。
            foreach (var item in result.Products)
            {
                StoreProduct product = item.Value;
                if (product.StoreId == productID)
                {
                    return product;
                }
            }
            return null;
        }

        //购买订阅产品的结果。
        private async Task PromptUserToPurchaseAsync()
        {
            //请求购买订阅产品。如果有试用期，将提供试用期。
            //给客户。否则，将提供非审判SKU。
            StorePurchaseResult result = await subscriptionStoreProduct.RequestPurchaseAsync();
            // 捕获错误消息的操作，如果任何国家。
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
                //错误代码
            }
            switch (result.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    //显示一个UI来确认客户已经购买了您的订阅
                    //并解锁订阅的特性。
                    Loading.IsActive = false;
                    var messageDig1 = new MessageDialog("您已成功订阅此服务一个月！");
                    //展示窗口，获取按钮是否退出  
                    var result1 = await messageDig1.ShowAsync();
                    break;

                case StorePurchaseStatus.NotPurchased:
                    Loading.IsActive = false;
                    var messageDig2 = new MessageDialog("购买没有完成。可能已经取消了购买。");
                    //展示窗口，获取按钮是否退出  
                    var result2 = await messageDig2.ShowAsync();
                    break;

                case StorePurchaseStatus.ServerError:
                case StorePurchaseStatus.NetworkError:
                    Loading.IsActive = false;
                    var messageDig3 = new MessageDialog("由于服务器或网络错误，购买不成功。");
                    //展示窗口，获取按钮是否退出  
                    var result3 = await messageDig3.ShowAsync();
                    break;

                case StorePurchaseStatus.AlreadyPurchased:
                    Loading.IsActive = false;
                    var messageDig4 = new MessageDialog("已经拥有此订阅。");
                    //展示窗口，获取按钮是否退出  
                    var result4 = await messageDig4.ShowAsync();
                    break;
            }
        }

        private async void Subscription_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Loading.IsActive = true;

            MessageDialog Subscription_Dialog = new MessageDialog("订阅账号可享受：\n1、跳过广告任务获取账号（不包括软件内其他位置的一些非必需操作性广告）\n2、获得推送提醒功能。\n注意：由于账号资源特殊，用户对价格不满意可以取消次月订阅，请以当前订阅价格为准，一经订阅，在订阅当月不予退款。\n是否同意并订阅？", "订阅会员");
            bool? result = null;
            Subscription_Dialog.Commands.Add(
               new UICommand("立即请阅", new UICommandInvokedHandler((cmd) => result = true)));

            Subscription_Dialog.Commands.Add(
              new UICommand("放弃订阅", new UICommandInvokedHandler((cmd) => result = false)));

            await Subscription_Dialog.ShowAsync();
            if (result == true)
            {
                productID = subscriptionStoreId;
                // 购买加载项的一系列操作
                await SetupSubscriptionInfoAsync();
            }
            else
            {
                Loading.IsActive = false;
            }
        }

        private async void Assess_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?Productid=9PNVKQ808V57"));
        }
    }
}
