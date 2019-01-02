using LeShare.Views;
using Microsoft.Services.Store.Engagement;
using Microsoft.WindowsAzure.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace LeShare
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            //如果是移动设备显示按钮
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
            
           // Debug.WriteLine(Info.SystemFamily+ Info.SystemVersion + Info.SystemArchitecture + Info.ApplicationVersion + Info.DeviceManufacturer +Info.DeviceModel);

            //上传自定义事件
            StoreServicesCustomEventLogger logger = StoreServicesCustomEventLogger.GetDefault();
            logger.Log(Info.SystemFamily + Info.SystemVersion);
            logger.Log(Info.SystemArchitecture);
            logger.Log(Info.ApplicationVersion);
            logger.Log(Info.DeviceManufacturer+ Info.DeviceModel);

        }

        public static class Info
        {
            public static string SystemFamily { get; }
            public static string SystemVersion { get; }
            public static string SystemArchitecture { get; }
            public static string ApplicationVersion { get; }
            public static string DeviceManufacturer { get; }
            public static string DeviceModel { get; }

            static Info()
            {
                // get the system family name
                AnalyticsVersionInfo ai = AnalyticsInfo.VersionInfo;
                SystemFamily = ai.DeviceFamily;

                // get the system version number
                string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
                ulong v = ulong.Parse(sv);
                ulong v1 = (v & 0xFFFF000000000000L) >> 48;
                ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
                ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
                ulong v4 = (v & 0x000000000000FFFFL);
                SystemVersion = $"{v1}.{v2}.{v3}.{v4}";

                // get the package architecure
                Package package = Package.Current;
                SystemArchitecture = package.Id.Architecture.ToString();

                // get the app version
                PackageVersion pv = package.Id.Version;
                ApplicationVersion = $"{pv.Major}.{pv.Minor}.{pv.Build}.{pv.Revision}";

                // get the device manufacturer and model name
                EasClientDeviceInformation eas = new EasClientDeviceInformation();
                DeviceManufacturer = eas.SystemManufacturer;
                DeviceModel = eas.SystemProductName;
            }
        }
        private async void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
            else{
                //设置事件已经处理  
                e.Handled = true;
                //设置在最后一个界面跳出弹出窗口  
                var messageDig = new MessageDialog("确定退出吗？");

                var btn_OK = new UICommand("确定");
                var btn_NO = new UICommand("取消");

                messageDig.Commands.Add(btn_OK);
                messageDig.Commands.Add(btn_NO);

                //展示窗口，获取按钮是否退出  
                var result = await messageDig.ShowAsync();
                //如果是确定退出就直接让应用程序退出  
                if (null != result && result.Label == "确定")
                {
                    Application.Current.Exit();
                }
            }
        }
        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            //调用Azure推送添加到新的InitNotificationsAsync方法
            InitNotificationsAsync();

            //返回键
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;


            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(SplashPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
                //是否加入返回键
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
                rootFrame.Navigated += OnNavigated;
            }
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        //标题栏返回
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        //返回的方法
        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
                return;
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        //Azure推送
        public static async void InitNotificationsAsync()
        {
            ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
            //获取本地应用设置容器
            //如果存在Push_Enable这个键再判断
            try
            {
                if (settings.Values.ContainsKey("Push_Enable"))
                {
                    var hub_name = "LeShare_Push";
                    var DefaultListenSharedAccessSignature = "Endpoint=sb://leshare.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=u40pX6RbtkmBIAJq55tuKieMU3lKucYEn0hJi4Y4inI=";
                    //注册推送通知
                    var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                    var hub = new NotificationHub(hub_name, DefaultListenSharedAccessSignature);

                    //判断是否打开推送开关,如何打开则显示开并且注册推送通知,否则关闭
                    if ((bool)settings.Values["Push_Enable"] == true)
                    {
                        //上传注册的通道
                        var result = await hub.RegisterNativeAsync(channel.Uri);
                    }else
                    {
                        //关闭通道
                        channel.Close();
                    }
                }else
                {
                    //如果不存在Push_Enable这个键就说明默认没有打开接收通知，无需注册通知通道
                    settings.Values["Push_Enable"] = false;
                }
            }catch
            {
                /*
                var dialog = new MessageDialog("网络异常!请检查网络" );
                await dialog.ShowAsync();
                */
            }
            /*显示注册成功的id标识
            if (result.RegistrationId != null)
            {
                var dialog = new MessageDialog("Registration successful: " + result.RegistrationId);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }
            */
        }
    }
}
