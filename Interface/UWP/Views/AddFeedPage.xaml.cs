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
                iTunesSearch.SearchResult result = await (new iTunesSearch.SearchRequest()).SearchAsync("p%", iTunesSearch.Media.Podcast);

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

                //Task t = Task.Run(async () => {
                //    foreach (var item in result.Results)
                //    {
                //        PodcastRss20Feed feed = (PodcastRss20Feed)await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //        feed.Image.Url = item.ArtworkUrl60;
                //        feedList.Add(feed);
                //    }
                //});
                //t.Wait();

                //FeedMenuList.SelectionMode = ListViewSelectionMode.Single;
                //FeedMenuList.ItemsSource = feedList;

                //FeedMenuList.SelectionMode = ListViewSelectionMode.Single;

                //Parallel.ForEach(result.Results, async item => {
                //    try
                //    {
                //        if (item.FeedUrl == null) { return; }

                //        PodcastRss20Feed feed = (PodcastRss20Feed)await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //        if (feed.Image != null)
                //        {
                //            feed.Image.Url = item.ArtworkUrl60;
                //        }
                //        else {
                //            feed.Image = new PodcastRss20FeedImage();
                //        }
                //        feedList.Add(feed);

                //        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                //        {
                //            FeedMenuList.ItemsSource = feedList;
                //        });
                //    }
                //    catch (Exception ex){
                //        //throw;
                //    }
                //});


                //factory.CreateFeedAsync(new Uri("http://feeds.feedburner.com/Animeforever"));

                //Task t = Task.Run(() =>
                //{
                //    Parallel.ForEach(result.Results, async item =>
                //    {
                //        try
                //        {
                //            PodcastRss20Feed feed = (PodcastRss20Feed)await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //            feed.Image.Url = item.ArtworkUrl60;
                //            feedList.Add(feed);
                //        }
                //        catch { }
                //    });
                //});


                //Task<List<PodcastRss20Feed>> parent = Task.Run(() =>
                //{
                //    var results = new List<PodcastRss20Feed>();
                //    TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);

                //    foreach (var item in result.Results)
                //    {
                //        tf.StartNew(() =>
                //        {
                //            try
                //            {
                //                PodcastRss20Feed feed = (PodcastRss20Feed)factory.CreateFeed(new Uri(item.FeedUrl));
                //                feed.Image.Url = item.ArtworkUrl60;
                //                results.Add(feed);
                //            }
                //            catch { }
                //        });
                //    }
                //    return results;
                //});
                //var finalTask = parent.ContinueWith(parentTask =>
                //{
                //    foreach (PodcastRss20Feed i in parentTask.Result)
                //    {
                //        PodcastRss20Feed c = i;
                //    }
                //});
                //finalTask.Wait();

                //Task<PodcastRss20Feed[]> parent = Task.Run(() =>
                //{
                //    var results = new PodcastRss20Feed[3];
                //    TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);
                //    tf.StartNew(() => {
                //        Task.Delay(TimeSpan.FromSeconds(10));
                //        results[0] = new PodcastRss20Feed("0"); 
                //    });
                //    tf.StartNew(() => {
                //        Task.Delay(TimeSpan.FromSeconds(10));
                //        results[1] = new PodcastRss20Feed("1");
                //    });
                //    tf.StartNew(() => {
                //        Task.Delay(TimeSpan.FromSeconds(10));
                //        results[2] = new PodcastRss20Feed("2");
                //    });
                //    return results;
                //});
                //var finalTask = parent.ContinueWith(parentTask =>
                //{
                //    foreach (PodcastRss20Feed i in parentTask.Result)
                //    {
                //        var c = i.FeedUri;
                //    }
                //    //Console.WriteLine(i);
                //});
                //finalTask.Wait();

                //Task<int>[] tasks = new Task<int>[3];
                //tasks[0] = Task.Run(() => {
                //    Task.Delay(TimeSpan.FromSeconds(2000));
                //    return 1;
                //});
                //tasks[1] = Task.Run(() => {
                //    Task.Delay(TimeSpan.FromSeconds(1000));
                //    return 2;
                //});
                //tasks[2] = Task.Run(() => {
                //    Task.Delay(TimeSpan.FromSeconds(3000));
                //    return 3;
                //});

                //while (tasks.Length > 0)
                //{
                //    int i = Task.WaitAny(tasks);
                //    Task<int> completedTask = tasks[i];
                //   // Console.WriteLine(completedTask.Result);

                //    SearchTextBox.Text += completedTask.Result;

                //    var temp = tasks.ToList();
                //    temp.RemoveAt(i);
                //    tasks = temp.ToArray();
                //}



                //Task<PodcastRss20Feed>[] tasks = new Task<PodcastRss20Feed>[result.ResultCount ];

                //for (int i = 0; i < result.ResultCount; i++)
                //{
                //    var item = result.Results[i];
                //    tasks[i] = Task.Run(async() => { //posso usar o método CreateFeedAsync com a minha Task?
                //        try
                //        {
                //            PodcastRss20Feed feed = (PodcastRss20Feed) await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //            feed.Image.Url = item.ArtworkUrl60;
                //            return feed;
                //        }
                //        catch
                //        {
                //            return null;
                //        }
                //    });
                //    //feedList.Add(feed);
                //}

                //while (tasks.Length > 0)
                //{
                //    int i = Task.WaitAny(tasks);
                //    Task<PodcastRss20Feed> completedTask = tasks[i];
                //    // Console.WriteLine(completedTask.Result);

                //    //SearchTextBox.Text += completedTask.Result;
                //    feedList.Add(completedTask.Result);
                //    FeedMenuList.ItemsSource = feedList;

                //    var temp = tasks.ToList();
                //    temp.RemoveAt(i);
                //    tasks = temp.ToArray();
                //}

                //var customerTasks = result.Results.Select( item =>
                //{
                //    Task<IFeed> feed = null;
                //    try
                //    {
                //         feed = factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //         //feed.Result.Image.Url = item.ArtworkUrl60;
                //    }
                //    catch {
                //        feed = new Task<IFeed>(() => { return null; });
                //    }
                //    return feed;
                //});
                //var customers = await Task.WhenAll(customerTasks);

                //FeedMenuList.ItemsSource = customers;


                //BlockingCollection<PodcastRss20Feed> col = new BlockingCollection<PodcastRss20Feed>();

                //Stopwatch sw = new Stopwatch();

                //sw.Start();

                //Task<PodcastRss20Feed>[] tasks = new Task<PodcastRss20Feed>[result.ResultCount];

                //for (int i = 0; i < result.ResultCount; i++)
                //{
                //    var item = result.Results[i];
                //    tasks[i] = Task.Run(async () =>
                //    { //posso usar o método CreateFeedAsync com a minha Task?
                //        try
                //        {
                //            PodcastRss20Feed feed = (PodcastRss20Feed)await factory.CreateFeedAsync(new Uri(item.FeedUrl));
                //            feed.Image.Url = item.ArtworkUrl60;
                //            return feed;
                //        }
                //        catch
                //        {
                //            return null;
                //        }
                //    });
                //    //feedList.Add(feed);
                //}

                //while (tasks.Length > 0)
                //{
                //    int i = Task.WaitAny(tasks);
                //    Task<PodcastRss20Feed> completedTask = tasks[i];
                //    // Console.WriteLine(completedTask.Result);

                //    //SearchTextBox.Text += completedTask.Result;
                //    col.Add(completedTask.Result);

                //    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                //    {
                //        FeedMenuList.ItemsSource = col;
                //    });

                //    var temp = tasks.ToList();
                //    temp.RemoveAt(i);
                //    tasks = temp.ToArray();
                //}

                //sw.Stop();

                //SearchTextBox.Text += " | " + sw.Elapsed;


                AddFeedProgressRing.IsActive = false;

                RootPivot.Visibility = Visibility.Collapsed;
                FeedMenuList.Visibility = Visibility.Visible;

                await new MessageDialog("Downloading!!!  " + result.ResultCount).ShowAsync();
            }
        }
    }
}
