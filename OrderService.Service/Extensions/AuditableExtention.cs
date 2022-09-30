using OrderService.Domain.Commons;
using OrderService.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.Extensions
{
    public static class AuditableExtention
    {
        public static void Create(this Auditable auditable)
        {
            auditable.CreatedAt = DateTime.UtcNow;
            auditable.CreatedBy = HttpContextHelper.UserId;
        }

        public static void Update(this Auditable auditable)
        {
            auditable.UpdatedAt = DateTime.UtcNow;
            auditable.UpdatedBy = HttpContextHelper.UserId;
        }

        public static void Delete(this Auditable auditable)
        {
            auditable.DeletedAt = DateTime.UtcNow;
            auditable.DeletedBy = HttpContextHelper.UserId;
        }
    }
}
