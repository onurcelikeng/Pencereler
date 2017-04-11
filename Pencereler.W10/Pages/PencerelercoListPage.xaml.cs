//---------------------------------------------------------------------------
//
// <copyright file="PencerelercoListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/16/2017 10:17:04 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Twitter;
using Pencereler.Sections;
using Pencereler.ViewModels;
using AppStudio.Uwp;

namespace Pencereler.Pages
{
    public sealed partial class PencerelercoListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public PencerelercoListPage()
        {
			ViewModel = ViewModelFactory.NewList(new PencerelercoSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("1883a44b-c23b-4a1d-be23-7508f3fe4594");
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
