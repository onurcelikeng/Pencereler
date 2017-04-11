using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.YouTube;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using Pencereler.Navigation;
using Pencereler.ViewModels;

namespace Pencereler.Sections
{
    public class KutuAclmSection : Section<YouTubeSchema>
    {
		private YouTubeDataProvider _dataProvider;
		
		public KutuAclmSection()
		{
			_dataProvider = new YouTubeDataProvider(new YouTubeOAuthTokens
			{
				ApiKey = "AIzaSyDqtZQEkgGKFhInigm_VVmEkRJ0FXePQu4"
			});
		}

		public override async Task<IEnumerable<YouTubeSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new YouTubeDataConfig
            {
                QueryType = YouTubeQueryType.Playlist,
                Query = @"PLG-qa-RqPD_oSYfIikw9NlyXNGM8wihAh",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<YouTubeSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<YouTubeSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<YouTubeSchema>
                {
                    Title = "Kutu Açılımı",

                    Page = typeof(Pages.KutuAclmListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.KutuAclmDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<YouTubeSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, YouTubeSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = null;
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(null);
                    viewModel.Content = item.EmbedHtmlFragment;
					viewModel.Source = item.ExternalUrl;
                });

                var actions = new List<ActionConfig<YouTubeSchema>>
                {
                    ActionConfig<YouTubeSchema>.Link("Kaynağa Git", (item) => item.ExternalUrl.ToSafeString()),
                };

                return new DetailPageConfig<YouTubeSchema>
                {
                    Title = "Kutu Açılımı",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
