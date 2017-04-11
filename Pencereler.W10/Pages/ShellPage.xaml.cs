using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using Pencereler.Navigation;

namespace Pencereler.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Anasayfa",
                "Anasayfa",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"3d84e856-23a9-49f9-81a9-fa8876343f8d",
                "İnceleme",                
                AppNavigation.ActionFromPage("IncelemeListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"2733e536-ed7c-4c99-9178-8ae57d4f3b90",
                "Nasıl Yapılır",                
                AppNavigation.ActionFromPage("NaslYaplrListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"cdb176dc-3ba8-41a2-a769-a00434fa5623",
                "Kutu Açılımı",                
                AppNavigation.ActionFromPage("KutuAclmListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"95225f01-18a3-4bd8-95c0-ba7eb929f0d7",
                "Röportajlar",                
                AppNavigation.ActionFromPage("RoportajlarListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"726d4e59-6eba-4612-a771-9b3584b1800e",
                "Podcast",                
                AppNavigation.ActionFromPage("PodcastListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"1883a44b-c23b-4a1d-be23-7508f3fe4594",
                "@pencerelerco",                
                AppNavigation.ActionFromPage("PencerelercoListPage"),
				AppNavigation.IconFromGlyph("\ue134")));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}