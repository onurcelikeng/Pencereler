using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Pencereler.Sections;
namespace Pencereler.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Pencereler";
            Inceleme = ViewModelFactory.NewList(new IncelemeSection());
            NaslYaplr = ViewModelFactory.NewList(new NaslYaplrSection());
            KutuAclm = ViewModelFactory.NewList(new KutuAclmSection());
            Roportajlar = ViewModelFactory.NewList(new RoportajlarSection());
            Podcast = ViewModelFactory.NewList(new PodcastSection());
            Pencerelerco = ViewModelFactory.NewList(new PencerelercoSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Inceleme { get; private set; }
        public ListViewModel NaslYaplr { get; private set; }
        public ListViewModel KutuAclm { get; private set; }
        public ListViewModel Roportajlar { get; private set; }
        public ListViewModel Podcast { get; private set; }
        public ListViewModel Pencerelerco { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
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
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
