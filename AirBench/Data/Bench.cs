using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirBench.Data
{
    public class Bench
    {
        public Bench()
        {
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        [Required]
        public int Seats { get; set; }
        public string Name { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}