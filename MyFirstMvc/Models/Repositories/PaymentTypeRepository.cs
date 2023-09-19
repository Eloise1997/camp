using MyFirstMvc.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Models.Repositories
{
    public class PaymentTypeRepository : BaseRepository<PaymentType>
    {
        private AppDbContext _db = new AppDbContext();

        public PaymentType GetTypeName(int typeCode)
        {
            return _db.PaymentTypes.FirstOrDefault(x => x.Id == typeCode && x.Enable);
        }
    }
}