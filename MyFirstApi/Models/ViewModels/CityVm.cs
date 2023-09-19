using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstApi.Models.ViewModels
{
	public class CityVm
	{
		/// <summary>
		/// 縣市
		/// </summary>
			public int Id { get; set; }
			public string Name { get; set; }
			public List<DistrictVm> Districts { get; set; }
		}
		/// <summary>
		/// 鄉鎮市區
		/// </summary>
		public class DistrictVm
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
		public static class CityExtensions
		{
			public static DistrictVm ToViewModel(this District district)
			{
				return new DistrictVm
				{
					Id = district.Id,
					Name = district.Name
				};
			}
			public static CityVm ToViewModel(this City city)
			{
				return new CityVm
				{
					Id = city.Id,
					Name = city.Name,
					Districts = city.Districts.Select(d => d.ToViewModel()).ToList()
				};
			}

		}
	}