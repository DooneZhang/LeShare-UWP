using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VungleSDK;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LeShare.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        //获取 VungleAd 实例
        VungleAd sdkInstance;
        bool IsCompletedView, CallToActionClicked;
        private string appID = "59191661729f4c0373000dc8";
        private string placement1 = "REWARD-2844023";
        private string placement2 = "INTER-1295052";
        private string placement3 = "FLEX-4716134";

      //  private string appID = "598a531697c455bc70001f98";
     //   private string placement1 = "DEFAULT59086";
      //  private string placement2 = "NONREWA96669";
     //   private string placement3 = "REWARDE30999";

        public TaskPage()
        {
            InitializeComponent();
            //初始化SDK
            string[] placementList = new string[] {placement1, placement2, placement3 };

            //Obtain Vungle SDK instance
            sdkInstance = AdFactory.GetInstance(appID, placementList);

            //注册广告事件

            //广告状态改变时
            sdkInstance.OnAdPlayableChanged += SdkInstance_OnAdPlayableChanged;
            //广告播放前
            sdkInstance.OnAdStart += SdkInstance_OnAdStart;
            //广告播放后
            sdkInstance.OnAdEnd += SdkInstance_OnAdEnd;

            sdkInstance.Diagnostic += SdkInstance_Diagnostic;

            sdkInstance.OnInitCompleted += SdkInstance_OnInitCompleted;

            //  InitSDK.IsEnabled = false;

            //加载广告栏2 
            sdkInstance.LoadAd(placement2);
            //加载广告栏3
            sdkInstance.LoadAd(placement3);

        }
        // OnInitCompleted
        //   e.Initialized - 如果初始化成功为 true，失败为 false
        //   e.ErrorMessage - 当 e.Initialized 为 false 时的失败原因
        private async void SdkInstance_OnInitCompleted(object sender, ConfigEventArgs e)
        {
            var placementsInfo = "OnInitCompleted: " + e.Initialized;
            //初始化成功
            if (e.Initialized == true)
            {
                //打印广告位置列表
                for (var i = 0; i < e.Placements.Length; i++)
                {
                    placementsInfo += "\n\tPlacement" + (i + 1) + ": " + e.Placements[i].ReferenceId;
                    if (e.Placements[i].IsAutoCached == true)
                        placementsInfo += " (Auto-Cached)";
                }
            }
            //初始化失败
            else
            {
                placementsInfo += "\n\t" + e.ErrorMessage;
            }
            System.Diagnostics.Debug.WriteLine(placementsInfo);
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() => ChangeLoadButtonsState(e.Initialized)));
        }

        // 广告可用性状态改变时调用此事件
        // OnAdPlayableAdPlayable
        //   e.AdPlayable - 如果广告可以播放为 true，否则为 false
        //   e.Placement - 字符串中的广告位置 ID
        private async void SdkInstance_OnAdPlayableChanged(object sender, AdPlayableEventArgs e)
        {
            // e.AdPlayable - true if an ad is available to play, false otherwise
            // e.Placement  - placement ID in string
            System.Diagnostics.Debug.WriteLine("OnAdPlayable: " + e.Placement + " - " + e.AdPlayable);
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() => ChangePlayButtonState(e.AdPlayable, e.Placement)));
        }

        // Event Handler called before playing an ad
        private void SdkInstance_OnAdStart(object sender, AdEventArgs e)
        {
            // e.Id        - Vungle app ID in string
            // e.Placement - placement ID in string

            System.Diagnostics.Debug.WriteLine("OnAdStart(" + e.Id + "): " + e.Placement);
        }

       
        // Event handler called when the user leaves ad and control is return to the hosting app
        private void SdkInstance_OnAdEnd(object sender, AdEndEventArgs e)
        {
            // OnAdEnd
            //   e.Id - 字符串中的 Vungle 应用 ID
            //   e.Placement - 字符串中的广告位置 ID
            //   e.IsCompletedView- 观看视频内容 80% 或更多时为 true
            //   e.CallToActionClicked - 用户点击了结束卡上的下载按钮时为 true
            //   e.WatchedDuration - 已弃用

            IsCompletedView = e.IsCompletedView;

            CallToActionClicked = e.CallToActionClicked;

          
        }

        //在 SDK 想要打印诊断日志时调用的事件处理程序
        private void SdkInstance_Diagnostic(object sender, DiagnosticLogEvent e)
        {
            System.Diagnostics.Debug.WriteLine(e.Level + " " + e.Type + " " + e.Exception + " " + e.Message);
        }

        private void InitSDK_Click(Object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPage));
        }

        private async void PlayPlacement1_Click(Object sender, RoutedEventArgs e)
        {
            AdConfig adConfig = new AdConfig
            {
                IncentivizedDialogBody = "确定要放弃观看任务么?现在放弃将无法获得账号.",
                IncentivizedDialogCloseButton = "放弃",
                IncentivizedDialogContinueButton = "继续",
                IncentivizedDialogTitle = "放弃任务?",
                SoundEnabled = false
            };
            //播放广告栏1
            await sdkInstance.PlayAdAsync(adConfig, placement1);
         
        }

        private void LoadPlacement2_Click(Object sender, RoutedEventArgs e)
        {
            //加载广告栏2 
            sdkInstance.LoadAd(placement2);
        }

        private void LoadPlacement3_Click(Object sender, RoutedEventArgs e)
        {
            //加载广告栏3
            sdkInstance.LoadAd(placement3);
        }

        //plays in native container
        private async void PlayPlacement2_Click(Object sender, RoutedEventArgs e)
        {
            //Play ad for placement2
            embeddedControl.AppID = appID;
            embeddedControl.Placements = placement1 + "," + placement2 + "," + placement3;
            embeddedControl.Placement = placement2;
            embeddedControl.SoundEnabled = false;
          
            embeddedControl.OnAdStart += Embedded_OnAdStart;
            embeddedControl.OnAdEnd += Embedded_OnAdEnd;

            var nEmb = await embeddedControl.PlayAdAsync();
        }

        private async void PlayPlacement3_Click(Object sender, RoutedEventArgs e)
        {
            AdConfig adConfig = new AdConfig
            {
                IncentivizedDialogBody = "确定要放弃观看任务么?现在放弃将无法获得账号.",
                IncentivizedDialogCloseButton = "放弃",
                IncentivizedDialogContinueButton = "继续",
                IncentivizedDialogTitle = "放弃任务?",
                SoundEnabled = false
            };

            await sdkInstance.PlayAdAsync(adConfig, placement3);
        }

        private void ChangeLoadButtonsState(bool isInitialized)
        {
        //    LoadPlacement2.IsEnabled = isInitialized;
        //    LoadPlacement3.IsEnabled = isInitialized;
        }

        private void ChangePlayButtonState(bool adPlayable, string placement)
        {
            if (placement.Equals(placement1))
            {
                PlayPlacement1.IsEnabled = adPlayable;
            }
            else if (placement.Equals(placement2))
            {
                PlayPlacement2.IsEnabled = adPlayable;
            }
            else if (placement.Equals(placement3))
            {
                PlayPlacement3.IsEnabled = adPlayable;
            }
        }

        private void AnimateHeight(double value)
        {
            var anim = new DoubleAnimation()
            {
                From = embeddedControl.Height,
                To = value,
                Duration = TimeSpan.FromMilliseconds(500),
                EnableDependentAnimation = true
            };
            Storyboard.SetTarget(anim, embeddedControl);
            Storyboard.SetTargetProperty(anim, "Height");
            var sb = new Storyboard();
            sb.Children.Add(anim);
            sb.Begin();
        }

        // Event Handler called before playing an ad
        private void Embedded_OnAdStart(object sender, AdEventArgs e)
        {
            var nowait = Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AnimateHeight(200);
            });
        }

        // Event handler called when the user leaves ad and control is return to the hosting app
        private void Embedded_OnAdEnd(object sender, AdEndEventArgs e)
        {
            var nowait = Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AnimateHeight(1);

            });
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsCompletedView == true || CallToActionClicked == true)
            {
                Frame.Navigate(typeof(WebPage));
            }
        }
        int click_times = 0;
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            click_times += 1;
            if (IsCompletedView == true || CallToActionClicked == true)
            {
                Frame.Navigate(typeof(WebPage));
            }

            else if (click_times >= 10)
            {
               if( PlayPlacement1.IsEnabled !=true && PlayPlacement2.IsEnabled !=true && PlayPlacement3.IsEnabled != true)
                {
                    Frame.Navigate(typeof(WebPage));
                }
               
            }
        }

       
    }
}
