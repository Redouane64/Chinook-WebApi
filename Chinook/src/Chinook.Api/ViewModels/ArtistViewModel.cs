using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Api.ViewModels
{
    public class ArtistViewModel
    {
		public string Name { get; set; }
		public ICollection<string> Albums { get; set; }
	}
}
