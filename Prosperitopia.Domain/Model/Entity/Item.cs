using Microsoft.EntityFrameworkCore;
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
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        [Precision(18, 2)]
        public decimal? Price { get; set; }
        public string Category { get; set; }
    }
}
