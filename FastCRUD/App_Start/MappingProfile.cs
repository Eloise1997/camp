using AutoMapper;
using FastCRUD.Models.EFModels;
using FastCRUD.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastCRUD.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			// 可以兩個方向都寫, 但也可以直接用 ReverseMap() 來反轉,表示兩個方向都要做
			CreateMap<News,NewsIndexVm>().ReverseMap(); // 雙向

			CreateMap<News, NewsVm>(); // 單向

			CreateMap<NewsVm, News>()
				.ForMember(dest => dest.CreatedTime,
				option => option.MapFrom(src => DateTime.Now))
				.ForMember(dest => dest.ModifiedTime,
				option => option.MapFrom(src => DateTime.Now));
		}
	}
}