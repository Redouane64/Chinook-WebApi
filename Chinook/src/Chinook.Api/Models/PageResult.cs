using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Api.Models
{
    public class PageResult<T>
    {
		public T[] Items
		{
			get; set;
		}

		public int TotalSize
		{
			get; set;
		}
	}
}
