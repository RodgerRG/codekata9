using System.Data;
using Code.Kata._9.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Code.Kata._9.Data.Repos;

public class CheckoutRepo : ICheckoutRepo, IDisposable, IAsyncDisposable
{
    private readonly ApiDbContext _dbContext;
    private readonly ILogger<CheckoutRepo> _logger;
    private readonly DbSet<Checkout> _checkouts;

    public CheckoutRepo(ApiDbContext dbContext, ILogger<CheckoutRepo> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _checkouts = _dbContext.Checkouts;
    }

    public async Task<IEnumerable<Checkout>> GetPaginatedItems(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _checkouts.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<Checkout> GetItem(int itemKey, CancellationToken cancellationToken)
    {
        var item = await _checkouts.FirstOrDefaultAsync(c => c.CheckoutId == itemKey,
            cancellationToken: cancellationToken);
        if (item is null) throw new DataException("Checkout with the given ID does not exist");
        return item;
    }

    public async Task AddItem(Checkout item, CancellationToken cancellationToken)
    {
        await _checkouts.AddAsync(item, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task ReplaceItem(int oldItemKey, Checkout item, CancellationToken cancellationToken)
    {
        var oldItem =
            await _checkouts.FirstOrDefaultAsync(c => c.CheckoutId == oldItemKey, cancellationToken: cancellationToken);
        if (oldItem is not null) _checkouts.Remove(oldItem);
        await _checkouts.AddAsync(item, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItem(int currentItemKey, Checkout updatedItem, CancellationToken cancellationToken)
    {
        var existingItem = await _checkouts.FirstOrDefaultAsync(c => c.CheckoutId == currentItemKey, cancellationToken);
        if (existingItem is null) throw new ArgumentException("Checkout with given ID does not exist");

        existingItem.PricingCatalogueId = updatedItem.PricingCatalogueId;
        existingItem.ExpiryDate = updatedItem.ExpiryDate;
        existingItem.CheckoutItems = updatedItem.CheckoutItems;
        existingItem.Total = updatedItem.Total;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItem(int itemKey, CancellationToken cancellationToken)
    {
        var item = await _checkouts.FirstOrDefaultAsync(c => c.CheckoutId == itemKey, cancellationToken: cancellationToken);
        if (item is null) return;
        
        _checkouts.Remove(item);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}