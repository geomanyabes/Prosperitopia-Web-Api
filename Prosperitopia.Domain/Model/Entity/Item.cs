using Prosperitopia.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Entity
{
    public class Item : BaseEntity, INamedEntity, IDescribedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
