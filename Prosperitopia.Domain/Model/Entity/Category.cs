using Prosperitopia.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Entity
{
    public class Category : BaseEntity, INamedEntity, IDescribedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
