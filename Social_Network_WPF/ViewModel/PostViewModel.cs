using MongoDB.Bson;

public class PostViewModel
{
    private readonly PostDal _postDal;
    private readonly ReactionDal _reactionDal;


    public PostViewModel(PostDal postDal, ReactionDal reactionDal)
    {
        _postDal = postDal;
        _reactionDal = reactionDal;
    }


    public async Task AddPostAsync(string content, ObjectId userId)
    {
        var newPost = new PostDto
        {
            userId = userId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        await _postDal.InsertPostAsync(newPost);
        
    }

    public async Task AddReactionAsync(ObjectId userId, ObjectId targetId, string reactionType)
    {
        var reaction = new ReactionDto
        {
            UserId = userId,
            TargetId = targetId,
            Type = reactionType,
            CreatedAt = DateTime.UtcNow
        };

        await _reactionDal.AddReactionAsync(reaction);
    }
}
