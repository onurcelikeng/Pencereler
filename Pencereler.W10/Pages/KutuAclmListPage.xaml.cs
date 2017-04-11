//---------------------------------------------------------------------------
//
// <copyright file="KutuAclmListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class KutuAclmListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public KutuAclmListPage()
        {
			ViewModel = ViewModelFactory.NewList(new KutuAclmSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("cdb176dc-3ba8-41a2-a769-a00434fa5623");
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
