using iTunesSearch;
using QDFeedParser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BGPCastUWP.Interface.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddFeedPage : Page
    {
        public AddFeedPage()
        {
            this.InitializeComponent();

            FeedMenuList.SelectionMode = ListViewSelectionMode.Single;
            FeedMenuList.IsItemClickEnabled = true;
            FeedMenuList.ItemClick += FeedMenuList_ItemClick;
        }

        private void FindAppBarButtom_Click(object sender, RoutedEventArgs e)
        {
            FindAppBarButtom.Visibility = Visibility.Collapsed;
            AddTextBlock.Visibility = Visibility.Collapsed;
            CancelAppBarButtom.Visibility = Visibility.Visible;
            SearchTextBox.Visibility = Visibility.Visible;
            SearchTextBox.Focus(FocusState.Programmatic);

            //TopCommandBar.SecondaryCommands.ToList().ForEach(c => ((AppBarButton)c).Visibility = Visibility.Collapsed);
        }

        private void CancelAppBarButtom_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SearchTextBox.Text))
            {
                FindAppBarButtom.Visibility = Visibility.Visible;
                AddTextBlock.Visibility = Visibility.Visible;
                CancelAppBarButtom.Visibility = Visibility.Collapsed;
                SearchTextBox.Visibility = Visibility.Collapsed;
            }
            else {
                SearchTextBox.Text = String.Empty;
                SearchTextBox.Focus(FocusState.Programmatic);
            }
        }

        private async void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !String.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                e.Handled = true;
                RootPivot.Visibility = Visibility.Collapsed;
                FeedMenuList.Visibility = Visibility.Visible;
                AddFeedProgress.Visibility = Visibility.Visible;

                SearchResult result = await (new SearchRequest()).SearchAsync(SearchTextBox.Text, iTunesSearch.Media.Podcast);

                FeedMenuList.ItemsSource = result.Results;
                //if hava only one line of artist name, we put the releaseDate again

                AddFeedProgress.Visibility = Visibility.Collapsed;

                FindAppBarButtom.Visibility = Visibility.Visible;
                AddTextBlock.Visibility = Visibility.Visible;
                CancelAppBarButtom.Visibility = Visibility.Collapsed;
                SearchTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void FeedMenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemX = FeedMenuList.ContainerFromItem(e.ClickedItem);

            var item = ((ListView)sender).ItemFromContainer(itemX);

            if (item != null)
            {
                ((Frame)this.Parent).Navigate(typeof(FeedPreviewPage), item);
            }
        }
    }
}
