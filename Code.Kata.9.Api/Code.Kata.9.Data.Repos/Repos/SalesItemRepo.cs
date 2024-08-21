using System.Data;
using Code.Kata._9.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Code.Kata._9.Data.Repos;

public class SalesItemRepo : ISalesItemRepo, IDisposable, IAsyncDisposable
{
    private readonly ApiDbContext _context;
    private readonly ILogger<SalesItemRepo> _logger;
    private readonly DbSet<SalesItem> _salesItems;

    public SalesItemRepo(ApiDbContext context, ILogger<SalesItemRepo> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        _salesItems = _context.SalesItems;
    }

    public async Task<IEnumerable<SalesItem>> GetPaginatedItems(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _salesItems.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<SalesItem> GetItem(int itemKey, CancellationToken cancellationToken)
    {
        var salesItem = await _salesItems.FirstOrDefaultAsync(si => si.SalesItemId == itemKey,
            cancellationToken: cancellationToken);
        if (salesItem is null) throw new DataException("Sales item with given ID does not exist");

        return salesItem;
    }

    public async Task AddItem(SalesItem item, CancellationToken cancellationToken)
    {
        await _salesItems.AddAsync(item, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task ReplaceItem(int oldItemKey, SalesItem item, CancellationToken cancellationToken)
    {
        var oldItem =
            await _salesItems.FirstOrDefaultAsync(si => si.SalesItemId == oldItemKey,
                cancellationToken: cancellationToken);
        if (oldItem is not null) _salesItems.Remove(oldItem);
        await _salesItems.AddAsync(item, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItem(int currentItemKey, SalesItem updatedItem, CancellationToken cancellationToken)
    {
        var existingItem =
            await _salesItems.FirstOrDefaultAsync(si => si.SalesItemId == currentItemKey,
                cancellationToken);
        if (existingItem is null) throw new ArgumentException("Checkout with given ID does not exist");

        existingItem.SalesItemId = updatedItem.SalesItemId;
        existingItem.PricingInfos = updatedItem.PricingInfos;
        existingItem.ItemDescription = updatedItem.ItemDescription;
        existingItem.ItemName = updatedItem.ItemName;
        existingItem.PricingInfos = updatedItem.PricingInfos;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItem(int itemKey, CancellationToken cancellationToken)
    {
        var item = await _salesItems.FirstOrDefaultAsync(si => si.SalesItemId == itemKey, cancellationToken: cancellationToken);
        if (item is null) return;
        
        _salesItems.Remove(item);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}