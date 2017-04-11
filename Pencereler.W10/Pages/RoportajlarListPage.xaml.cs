//---------------------------------------------------------------------------
//
// <copyright file="RoportajlarListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class RoportajlarListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public RoportajlarListPage()
        {
			ViewModel = ViewModelFactory.NewList(new RoportajlarSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("95225f01-18a3-4bd8-95c0-ba7eb929f0d7");
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
