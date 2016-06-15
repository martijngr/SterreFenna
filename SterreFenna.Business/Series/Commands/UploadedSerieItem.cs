using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class UploadedSerieItem
    {
        public Stream Stream { get; set; }

        public string Filename { get; set; }

        public int Rank { get; set; }
    }
}
