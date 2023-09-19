using FastCRUD.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastCRUD.Models.ViewModel
{
	public class CategoryVm
	{
		public int Id { get; set; }

		[Display(Name = "分類名稱")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name ="顯示順序")]
		[Required]
		[Range(1,1000)]
		public int DisplayOrder { get; set; }
	}

	public static class CategoryExtensions
	{
		public static CategoryVm ToViewModel(this Category category)
		{
			return new CategoryVm
			{
				Id = category.Id,
				Name = category.Name,
				DisplayOrder = category.DisplayOrder
			};
		}
	}
}