using MongoDB.Bson;
using MongoDB.Driver;
using Social_Network.DAL.Concrete;

namespace Social_Network
{
    class Program
    {
        private static MongoClient _client;
        private static IMongoDatabase _database;
        private static UserDal _userDal;
        private static CommentDal _commentDal;
        private static PostDal _postDal;

        static async Task Main(string[] args)
        {
            string connectionString = "mongodb+srv://victoriia:iraros2005@vlnu.rsmja.mongodb.net/?retryWrites=true&w=majority&appName=VLNU";
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("socialntw");

            _userDal = new UserDal(_database);
            _commentDal = new CommentDal(_database);
            _postDal = new PostDal(_database);

            bool running = true;

            while (running)
            {
                Console.WriteLine("Social Network - Choose an action:");
                Console.WriteLine("1. Add a new user");
                Console.WriteLine("2. Delete a user");
                Console.WriteLine("3. List all users");
                Console.WriteLine("4. Add a new post");
                Console.WriteLine("5. List all posts");
                Console.WriteLine("6. Add a new comment");
                Console.WriteLine("7. List all comments");
                Console.WriteLine("8. Exit");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddUser();
                        break;
                    case "2":
                        await RemoveUser();
                        break;
                    case "3":
                        await ListAllUsers();
                        break;
                    case "4":
                        await AddPost();
                        break;
                    case "5":
                        await ListAllPosts();
                        break;
                    case "6":
                        await AddComment();
                        break;
                    case "7":
                        await ListAllComments();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static async Task AddUser()
        {
            Console.Write("Enter e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            UserDto newUser = new UserDto
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Interests = new List<string> { "example interest" }
            };

            await _userDal.AddUserAsync(newUser);

            Console.WriteLine("User successfully added.");
        }


        private static async Task RemoveUser()
        {
            Console.Write("Enter the user ID to delete: ");
            var userId = new ObjectId(Console.ReadLine());
            await _userDal.DeleteUserAsync(userId);
            Console.WriteLine("User successfully deleted.");
        }

        private static async Task ListAllUsers()
        {
            var users = await _userDal.GetAllUsersAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, E-mail: {user.Email}, First name: {user.FirstName}, Last name: {user.LastName}");
            }
        }

        private static async Task AddPost()
        {
            Console.Write("Enter the post author's ID: ");
            var userId = new ObjectId(Console.ReadLine());

            Console.Write("Enter the post content: ");
            string content = Console.ReadLine();

            PostDto newPost = new PostDto
            {
                AuthorId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _postDal.InsertPostAsync(newPost);
            Console.WriteLine("Post successfully added.");
        }

        private static async Task ListAllPosts()
        {
            var posts = await _postDal.GetAllPostsAsync();
            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.Id}, Author: {post.AuthorId}, Content: {post.Content}, Created At: {post.CreatedAt}");
            }
        }

        private static async Task AddComment()
        {
            Console.Write("Enter the post ID: ");
            var postId = new ObjectId(Console.ReadLine());

            Console.Write("Enter the user ID leaving the comment: ");
            var userId = new ObjectId(Console.ReadLine());

            Console.Write("Enter the comment content: ");
            string content = Console.ReadLine();

            CommentDto newComment = new CommentDto
            {
                PostId = postId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _commentDal.AddCommentAsync(newComment);
            Console.WriteLine("Comment successfully added.");
        }

        private static async Task ListAllComments()
        {
            var comments = await _commentDal.GetAllCommentsAsync();
            foreach (var comment in comments)
            {
                Console.WriteLine($"ID: {comment.Id}, Post ID: {comment.PostId}, Author: {comment.UserId}, Content: {comment.Content}, Date: {comment.CreatedAt}");
            }
        }
    }
}