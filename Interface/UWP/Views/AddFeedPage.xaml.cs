using QDFeedParser;
using System;
using System.Collections.Generic;
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
                AddFeedProgressRing.IsActive = true;
                iTunesSearch.SearchResult result = await (new iTunesSearch.SearchRequest()).SearchAsync( "pelada+na+net");
                
                HttpFeedFactory factory = new HttpFeedFactory(new QDFeedParser.Xml.PodcastFeedXmlParser(), new PodcastFeedInstanceProvider());

                List<PodcastRss20Feed> feedList = new List<PodcastRss20Feed>();

                //foreach (var item in result.Results)
                //{
                //    PodcastRss20Feed feed =  (PodcastRss20Feed) await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //    feed.Image.Url = item.ArtworkUrl60;
                //    feedList.Add(feed);
                //}

                //Parallel.ForEach(result.Results, async item => {
                //    PodcastRss20Feed feed = (PodcastRss20Feed) await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //    feed.Image.Url = item.ArtworkUrl60;
                //    feedList.Add(feed);
                //});

                Task t = Task.Run(async () => {
                    foreach (var item in result.Results)
                    {
                        PodcastRss20Feed feed = (PodcastRss20Feed)await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                        feed.Image.Url = item.ArtworkUrl60;
                        feedList.Add(feed);
                    }
                });
                t.Wait();

                FeedMenuList.SelectionMode = ListViewSelectionMode.Single;
                FeedMenuList.ItemsSource = feedList;
                AddFeedProgressRing.IsActive = false;

                RootPivot.Visibility = Visibility.Collapsed;
                FeedMenuList.Visibility = Visibility.Visible;
                await new MessageDialog("Downloading!!!  " + result.ResultCount).ShowAsync();
            }
        }
    }
}
