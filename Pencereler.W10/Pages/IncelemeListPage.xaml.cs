//---------------------------------------------------------------------------
//
// <copyright file="IncelemeListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class IncelemeListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public IncelemeListPage()
        {
			ViewModel = ViewModelFactory.NewList(new IncelemeSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("3d84e856-23a9-49f9-81a9-fa8876343f8d");
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
