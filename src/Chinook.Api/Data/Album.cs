using System;
using System.Collections.Generic;

namespace Chinook.Api.Data
{
    public partial class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
