using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Data
{
    public class Comment
    {
        public Comment(){} 

        public int id { get; set; }
        public string Text { get; set; }
        public int BenchId { get; set; }
        public Bench Bench { get; set; }
        public int Rating { get; set; }
    }
}