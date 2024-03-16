using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Interface
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        string? ModifiedBy { get; set; }
        bool? IsDeleted { get; set; }
    }
}
