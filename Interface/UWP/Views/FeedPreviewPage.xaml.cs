using BGPCastUWP.Interface.UWP.Controls;
using iTunesSearch;
using QDFeedParser;
using QDFeedParser.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class FeedPreviewPage : Page
    {
        public FeedPreviewPage()
        {
            this.InitializeComponent();

            EpisodeMenuList.SelectionMode = ListViewSelectionMode.Single;
//            EpisodeMenuList.ItemsSource = navlist;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ItemResult itemResult = (ItemResult)e.Parameter;
            Uri uriFeed = new Uri((itemResult).FeedUrl);

            HttpFeedFactory factory = new HttpFeedFactory(new PodcastFeedXmlParser(), new PodcastFeedInstanceProvider());
            PodcastRss20Feed feed = (PodcastRss20Feed)await factory.CreateFeedAsync(uriFeed);
            
            //TODO: Criar um User Control para essa lista, de forma e escolher a melhor maineira de exibir a Imagem.
            feed.Image = (feed.Image == null ? new PodcastRss20FeedImage()  { Url = itemResult.ArtworkUrl60 } : feed.Image);
            HeaderPreviewToggleButton.Content = feed;
            //TODO: Colocar a ProgressBar
            
            EpisodeMenuList.ItemsSource = feed.Items;
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void EpisodeMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

        private void HeaderPreviewToggleButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderPreviewToggleButton.IsChecked = HeaderPreviewToggleButton.IsChecked & true;
        }
    }
}
