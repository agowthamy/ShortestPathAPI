using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortestRouteAPI.Models.Dtos
{
    public class ShortestPathData
    {
        public List<string> NodeNames { get; set; } = new List<string>();
        public int Distance { get; set; }
    }
}