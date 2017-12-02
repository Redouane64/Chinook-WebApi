using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Api.Models
{
    public class ArtistModel
    {
		public string Name { get; set; }
		public ICollection<string> Albums { get; set; }
	}
}
