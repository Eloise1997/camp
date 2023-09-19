using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstApi.Models
{
	public class City
	{
        /// <summary>
        /// 縣市
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; }
    }

    /// <summary>
    /// 鄉鎮市區
    /// </summary>
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}