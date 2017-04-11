using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using Pencereler.Navigation;
using Pencereler.ViewModels;

namespace Pencereler.Sections
{
    public class PencerelercoSection : Section<TwitterSchema>
    {
		private TwitterDataProvider _dataProvider;

		public PencerelercoSection()
		{
			_dataProvider = new TwitterDataProvider(new TwitterOAuthTokens
			{
				ConsumerKey = "eH3URGJZLuci0cPcwb6gyX7Br",
                    ConsumerSecret = "tsmhZMVSSrCHFQWqx74D1Yput6OG4h5FEHJRDdO1IVDyxb0t23",
                    AccessToken = "2456546750-RO1nUqHxG0mODvKPKikAm90WA4ca4wU40e21JB8",
                    AccessTokenSecret = "9tmQqR582rZ5CkdHnu1HJg9TwDPMEFORj5qHRLHndNlOz"
			});
		}

		public override async Task<IEnumerable<TwitterSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new TwitterDataConfig
            {
                QueryType = TwitterQueryType.User,
                Query = @"pencerelerco"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<TwitterSchema>> GetNextPageAsync()
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

        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "@pencerelerco",

                    Page = typeof(Pages.PencerelercoListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
						viewModel.Header = item._id.ToSafeString();
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.UserProfileImageUrl.ToSafeString());

						viewModel.GroupBy = item._id.SafeType();

						viewModel.OrderBy = item._id;
                    },
					OrderType = OrderType.Ascending,
                    DetailNavigation = (item) =>
                    {
                        return new NavInfo
                        {
                            NavigationType = NavType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage
        {
            get { return null; }
        }
    }
}
