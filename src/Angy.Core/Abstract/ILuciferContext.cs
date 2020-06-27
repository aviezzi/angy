using System.Threading.Tasks;
using Angy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core.Abstract
{
    public interface ILuciferContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<MicroCategory> MicroCategories { get; set; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}