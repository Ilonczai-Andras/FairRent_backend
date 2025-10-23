using FairRent.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FairRent.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
