using Newtonsoft.Json;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain;
using Prosperitopia.Domain.Model.Entity;
using System.Net.Http;

namespace Prosperitopia.DataAccess.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(ProsperitopiaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
