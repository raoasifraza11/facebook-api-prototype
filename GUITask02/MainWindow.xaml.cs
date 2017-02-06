using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Facebook;

namespace GUITask02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static FacebookClient client;
        private List<Person> MyCollection;

        public MainWindow()
        {
            InitializeComponent();
            GetFriendList();
            getAbout();
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
            client.AccessToken = "EAACEdEose0cBANVH1bjGJKlLoRM8bpyRdnlmHg1UZCA044TjKFIT1VrN2x4TpoYBOdMLihwQ7PTjnD0Y92FYWxMms2K6TjrUkip5jZCkQ2A47JZBTMZBUyp86VvyJe5pIEDRwdkqCNeVRvfwpPZC0U2ztvWNXKSnAmYHhONK6cxZAGZBg6tFZCWVczekiuKmBzMZD";
            dynamic Me = client.Get("me/friends");
            foreach (var item in Me.data)
            {
                MyCollection.Add(new Person { Id = item.id, Name = item.name });
            }
            ListB.ItemsSource = MyCollection;
        }


        public void getAbout()
        {
            client = new FacebookClient();
            client.AccessToken = "EAACEdEose0cBANVH1bjGJKlLoRM8bpyRdnlmHg1UZCA044TjKFIT1VrN2x4TpoYBOdMLihwQ7PTjnD0Y92FYWxMms2K6TjrUkip5jZCkQ2A47JZBTMZBUyp86VvyJe5pIEDRwdkqCNeVRvfwpPZC0U2ztvWNXKSnAmYHhONK6cxZAGZBg6tFZCWVczekiuKmBzMZD";
            dynamic Me = client.Get("me?fields=birthday,email,id,name,picture");
            user_name.Content = Me.name;
            user_dob.Content = Me.birthday;
            user_email.Content = Me.email;
        }

    }
}
