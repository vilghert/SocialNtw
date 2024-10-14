using System.Collections.ObjectModel;
using System.ComponentModel;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly UserDal _userDal;
    private readonly PostDal _postDal;

    public ObservableCollection<PostDto> Posts { get; set; } = new ObservableCollection<PostDto>();

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private string _friendsCount;
    public string FriendsCount
    {
        get => _friendsCount;
        set
        {
            _friendsCount = value;
            OnPropertyChanged(nameof(FriendsCount));
        }
    }

    public MainViewModel(UserDal userDal, PostDal postDal)
    {
        _userDal = userDal;
        _postDal = postDal;
    }

    public async Task LoginAsync()
    {
        var user = await _userDal.LoginAsync(Email, Password);
        if (user != null)
        {
            FriendsCount = $"Кількість друзів: {user.Friends.Count}";
            await LoadPostsAsync();
        }
        else
        {
            
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public void UpdatePassword(string password)
    {
        Password = password;
        OnPropertyChanged(nameof(Password));
    }

    private async Task LoadPostsAsync()
    {
        var posts = await _postDal.GetAllPostsAsync();
        Posts.Clear();
        foreach (var post in posts)
        {
            Posts.Add(post);
        }
    }
}