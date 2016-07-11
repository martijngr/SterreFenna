using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Domain
{
    public interface IIdentifyable
    {
        int Id { get; set; }
    }
}