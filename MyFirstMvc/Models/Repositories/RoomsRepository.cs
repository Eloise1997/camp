using MyFirstMvc.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Models.Repositories
{
    public class RoomsRepository : BaseRepository<Room>
    {
        private AppDbContext _db = new AppDbContext();

        public List<Room> GetAll()
        {
            return _db.Rooms.ToList();
        }
    }
}