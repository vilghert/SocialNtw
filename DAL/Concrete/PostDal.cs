using MongoDB.Bson;
using MongoDB.Driver;

public class PostDal : IPostDal
{
    private readonly IMongoCollection<PostDto> _posts;

    public PostDal(IMongoDatabase database)
    {
        _posts = database.GetCollection<PostDto>("posts");
    }
    public async Task InsertPostAsync(PostDto post)
    {
        await _posts.InsertOneAsync(post);
    }

    public async Task<List<PostDto>> GetAllPostsAsync()
    {
        return await _posts.Find(_ => true).ToListAsync();
    }

    public async Task<PostDto> GetPostByIdAsync(ObjectId postId)
    {
        return await _posts.Find(p => p.Id == postId).FirstOrDefaultAsync();
    }

    public async Task AddPostAsync(PostDto post)
    {
        await _posts.InsertOneAsync(post);
    }

    public async Task UpdatePostAsync(PostDto post)
    {
        await _posts.ReplaceOneAsync(p => p.Id == post.Id, post);
    }

    public async Task DeletePostAsync(ObjectId postId)
    {
        await _posts.DeleteOneAsync(p => p.Id == postId);
    }
}
