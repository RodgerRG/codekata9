using System.Data;
using Code.Kata._9.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Code.Kata._9.Data.Repos;

public class PricingCatalogueRepo : IPricingCatalogueRepo, IDisposable, IAsyncDisposable
{
    private readonly ApiDbContext _context;
    private readonly ILogger<PricingCatalogueRepo> _logger;
    private readonly DbSet<PricingCatalogue> _pricingCatalogues;

    public PricingCatalogueRepo(ApiDbContext context, ILogger<PricingCatalogueRepo> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _pricingCatalogues = context.PricingCatalogues;
    }

    public async Task<IEnumerable<PricingCatalogue>> GetPaginatedItems(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _pricingCatalogues.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<PricingCatalogue> GetItem(int itemKey, CancellationToken cancellationToken)
    {
        var catalogue = await _pricingCatalogues.FirstOrDefaultAsync(pc => pc.PricingCatalogueId == itemKey,
            cancellationToken: cancellationToken);
        if (catalogue is null) throw new DataException("Pricing catalogue with given ID does not exist");

        return catalogue;
    }

    public async Task AddItem(PricingCatalogue item, CancellationToken cancellationToken)
    {
        await _pricingCatalogues.AddAsync(item, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task ReplaceItem(int oldItemKey, PricingCatalogue item, CancellationToken cancellationToken)
    {
        var oldItem =
            await _pricingCatalogues.FirstOrDefaultAsync(pc => pc.PricingCatalogueId == oldItemKey,
                cancellationToken: cancellationToken);
        if (oldItem is not null) _pricingCatalogues.Remove(oldItem);
        await _pricingCatalogues.AddAsync(item, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItem(int currentItemKey, PricingCatalogue updatedItem, CancellationToken cancellationToken)
    {
        var existingItem =
            await _pricingCatalogues.FirstOrDefaultAsync(pc => pc.PricingCatalogueId == currentItemKey,
                cancellationToken);
        if (existingItem is null) throw new ArgumentException("Checkout with given ID does not exist");

        existingItem.PricingCatalogueId = updatedItem.PricingCatalogueId;
        existingItem.PricingInfos = updatedItem.PricingInfos;
        existingItem.AssociatedCheckouts = updatedItem.AssociatedCheckouts;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItem(int itemKey, CancellationToken cancellationToken)
    {
        var item = await _pricingCatalogues.FirstOrDefaultAsync(pc => pc.PricingCatalogueId == itemKey, cancellationToken: cancellationToken);
        if (item is null) return;
        
        _pricingCatalogues.Remove(item);
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