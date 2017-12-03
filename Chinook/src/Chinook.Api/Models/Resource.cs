using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Api.Models
{
	public abstract class Resource : Link
    {
		public Link Self
		{
			get; set;
		}
	}
}
