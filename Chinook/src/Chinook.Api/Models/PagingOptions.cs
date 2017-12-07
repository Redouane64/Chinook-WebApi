using System.ComponentModel.DataAnnotations;

namespace Chinook.Api.Models
{
	public class PagingOptions
	{
		[Range(0, 9999)]
		public int? Limit
		{
			get; set;
		}

		[Range(1, 999)]
		public int? Offset
		{
			get; set;
		}
	}
}
