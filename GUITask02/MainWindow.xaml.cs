using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using Facebook;
using System.Net;
using System.IO;

namespace GUITask02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static FacebookClient client;
        private List<Person> MyCollection;
        private List<Pages> MyPages;
        private string access_token = "EAACEdEose0cBAICtLhyzfdww0gtfKDI1Y7LLFCUs7ZAUZABlVQ9hjEZBe1rs3yhWAhsCGENhQaAlraMdq2uggdwaB0LVZB8ZBRHIAW86mk978qZCUsSlXV02XZBDbZAeJnbLhGV93NvzrlCQS7U96eJKIZBrrDT1bWWvs0ntXRZAhu3ZBVI3srVUQ6E4SENsESrMl0ZD";

        public MainWindow()
        {
            InitializeComponent();
            GetFriendList();
            GetUserDetails();
            GetPages();
        }


        class Person
        {
            public string Id { set; get; }
            public string Name { set; get; }

        }

        public void GetFriendList()
        {
            client = new FacebookClient();
            MyCollection = new List<Person>();
            client.AccessToken = access_token;
            dynamic Me = client.Get("me/friends");
            foreach (var item in Me.data)
            {
                MyCollection.Add(new Person { Id = item.id, Name = item.name });
            }
            ListB.ItemsSource = MyCollection;
        }


        public void GetUserDetails()
        {
            client = new FacebookClient();
            client.AccessToken = access_token;
            dynamic Me = client.Get("me?fields=birthday,email,id,name,picture");
            user_name.Content = Me.name;
            user_dob.Content = Me.birthday;
            user_email.Content = Me.email;
            string url = Me.picture.data.url;

            var image = new BitmapImage();
            int BytesToRead = 100;

            WebRequest request = WebRequest.Create(new Uri(url, UriKind.Absolute));
            request.Timeout = -1;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            BinaryReader reader = new BinaryReader(responseStream);
            MemoryStream memoryStream = new MemoryStream();

            byte[] bytebuffer = new byte[BytesToRead];
            int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

            while (bytesRead > 0)
            {
                memoryStream.Write(bytebuffer, 0, bytesRead);
                bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
            }

            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            image.StreamSource = memoryStream;
            image.EndInit();

            user_image.Source = image;
        }


        public void GetPages()
        {
            client = new FacebookClient();
            client.AccessToken = access_token;
            dynamic Me = client.Get("me/accounts");
            MyPages = new List<Pages>();
            foreach (var page in Me.data)
            {
                MyPages.Add(new Pages { Id = page.id, Name = page.name });
            }
            ListPages.ItemsSource = MyPages;

        }


        class Pages
        {
            public string Id { set; get; }
            public string Name { set; get; }
        }
    }
}
