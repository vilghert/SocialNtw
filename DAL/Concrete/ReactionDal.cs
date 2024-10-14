using MongoDB.Bson;
using MongoDB.Driver;

public class ReactionDal
{
    private readonly IMongoCollection<ReactionDto> _reactions;

    public ReactionDal(IMongoDatabase database)
    {
        _reactions = database.GetCollection<ReactionDto>("reactions");
    }

    public async Task AddReactionAsync(ReactionDto reaction)
    {
        await _reactions.InsertOneAsync(reaction);
    }

    public async Task<List<ReactionDto>> GetReactionsForTargetAsync(ObjectId targetId)
    {
        return await _reactions.Find(r => r.TargetId == targetId).ToListAsync();
    }

    public async Task RemoveReactionAsync(ObjectId reactionId)
    {
        await _reactions.DeleteOneAsync(r => r.Id == reactionId);
    }
}
