using Prosperitopia.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Entity
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
