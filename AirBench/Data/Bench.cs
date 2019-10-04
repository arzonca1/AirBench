using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AirBench.Data
{
    public class Bench
    {
        public Bench()
        {
            Comments = new List<Comment>();
        }

        public Task<string> GetCreator()
        {
            IUserRepository iur = new UserRepository();
            return iur.GetUserName(CreatorId);

        }

        private decimal GetCommentAverage()
        {
            if (Comments.Count() == 0) return 0; //just so we don't get a math error 
            decimal sum = 0;
            foreach(var comment in Comments)
            {
                sum += comment.Rating;
            }
            return sum / Comments.Count();
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
        public decimal RatingAverage => GetCommentAverage();
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public string Description { get; set; }
        public User Creator { get; set; }
        public string CreatorName => Creator.FirstName + " " + Creator.LastName.Substring(0, 1) + ".";
    }
}