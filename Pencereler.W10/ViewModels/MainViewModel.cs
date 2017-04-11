using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Twitter;
using AppStudio.DataProviders.LocalStorage;
using Pencereler.Sections;


namespace Pencereler.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel Inceleme { get; private set; }
        public ListViewModel NaslYaplr { get; private set; }
        public ListViewModel KutuAclm { get; private set; }
        public ListViewModel Roportajlar { get; private set; }
        public ListViewModel Podcast { get; private set; }
        public ListViewModel Pencerelerco { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Pencereler";
            Inceleme = ViewModelFactory.NewList(new IncelemeSection(), visibleItems);
            NaslYaplr = ViewModelFactory.NewList(new NaslYaplrSection(), visibleItems);
            KutuAclm = ViewModelFactory.NewList(new KutuAclmSection(), visibleItems);
            Roportajlar = ViewModelFactory.NewList(new RoportajlarSection(), visibleItems);
            Podcast = ViewModelFactory.NewList(new PodcastSection(), visibleItems);
            Pencerelerco = ViewModelFactory.NewList(new PencerelercoSection(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Inceleme;
            yield return NaslYaplr;
            yield return KutuAclm;
            yield return Roportajlar;
            yield return Podcast;
            yield return Pencerelerco;
        }
    }
}
