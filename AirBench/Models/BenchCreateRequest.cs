using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Models
{
    public class BenchCreateRequest
    {
        
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public int CreatorId { get; set; }
        public BenchCreateRequest()
        {
            
        }

    }
}