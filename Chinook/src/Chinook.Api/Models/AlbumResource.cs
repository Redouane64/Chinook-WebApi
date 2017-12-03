using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Api.Models
{
    public class AlbumResource : Resource
    {
		public string Title { get; set; }
		public string Artist { get; set; }
	}
}
