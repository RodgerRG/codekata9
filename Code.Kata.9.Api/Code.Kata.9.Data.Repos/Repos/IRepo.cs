namespace Code.Kata._9.Data.Repos;


/**
 * The inheritors here are all basically doing the same thing.
 * An abstract class using a template pattern for the finding of
 * The specific item by ID would suffice to reduce the code duplication. - Rodger, 22/08/24
 */
public interface IRepo<TItem, in TKey>
{
    public Task<IEnumerable<TItem>> GetPaginatedItems(int page, int pageSize, CancellationToken cancellationToken);
    public Task<TItem> GetItem(TKey itemKey, CancellationToken cancellationToken);
    public Task AddItem(TItem item, CancellationToken cancellationToken);
    public Task ReplaceItem(TKey oldItemKey, TItem item, CancellationToken cancellationToken);
    public Task UpdateItem(TKey currentItemKey, TItem updatedItem, CancellationToken cancellationToken);
    public Task DeleteItem(TKey itemKey, CancellationToken cancellationToken);
    
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}