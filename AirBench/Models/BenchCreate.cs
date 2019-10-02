using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Models
{
    public class BenchCreate
    {
        public int? Count { get; set; }
        public BenchCreate()
        {
            Count = 1; 
        }
    }
}