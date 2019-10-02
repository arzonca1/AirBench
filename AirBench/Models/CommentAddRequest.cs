using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Models
{
    public class CommentAddRequest
    {
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}