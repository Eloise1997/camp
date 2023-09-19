using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastCRUD.Models.ViewModels
{
	public class NewsVm
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "標題")]
		public string Title { get; set; }

		[Required]
		[StringLength(3000)]
		[Display(Name = "內容")]
		public string Description { get; set; }

		//public DateTime CreatedTime { get; set; }

		//public DateTime ModifiedTime { get; set; }
	}

	public class NewsIndexVm
	{
		public int Id { get; set; }

		[Display(Name ="標題")]

		public string Title { get; set; }

		[Display(Name ="內容")]

		public string Description { get; set; }

		[Display(Name ="建檔日")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode=true)]

		public DateTime CreatedTime { get; set; }

		[Display(Name ="最近異動日")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd/HH:mm:ss}", ApplyFormatInEditMode = true)]

		public DateTime ModifiedTime { get; set; }
	}
}