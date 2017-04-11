//---------------------------------------------------------------------------
//
// <copyright file="PodcastListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/16/2017 10:17:04 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using Pencereler.Sections;
using Pencereler.ViewModels;
using AppStudio.Uwp;

namespace Pencereler.Pages
{
    public sealed partial class PodcastListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public PodcastListPage()
        {
			ViewModel = ViewModelFactory.NewList(new PodcastSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("726d4e59-6eba-4612-a771-9b3584b1800e");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
