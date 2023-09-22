using MyFirstMvc.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Models.Repositories
{
    public class RoomTypesRepository : BaseRepository<RoomType>
    {
        private AppDbContext _db = new AppDbContext();

        public List<RoomType> GetAll()
        {
            return _db.RoomTypes.ToList();
        }
    }
}