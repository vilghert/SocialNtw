using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

namespace Social_Network
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            var mongoClient = new MongoClient("mongodb+srv://victoriia:iraros2005@vlnu.rsmja.mongodb.net/?retryWrites=true&w=majority&appName=VLNU");
            var database = mongoClient.GetDatabase("socialntw");
            var userDal = new UserDal(database);
            var postDal = new PostDal(database);

            _viewModel = new MainViewModel(userDal, postDal);
            DataContext = _viewModel;

        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoginAsync();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            _viewModel.UpdatePassword(passwordBox.Password);
        }

    }
}