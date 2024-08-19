using Microsoft.EntityFrameworkCore;

namespace Code.Kata._9.Data;

public class ApiDbContext : DbContext
{
    #region DB Set Declarations
        
    #endregion
    
    #region Model Configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    #endregion
}