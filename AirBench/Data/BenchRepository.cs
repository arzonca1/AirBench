using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace AirBench.Data
{
    public class BenchRepository : IBenchRepository
    {
        async public Task<List<Bench>> GetBenchListAsync()
        {
            using(var context = new Context())
            {
                List<Bench> benches = await context.Benches
                    .Include(x => x.Comments)//will need this to calculate rating
                    .Include(x => x.Creator) 
                    .ToListAsync();

                foreach(var bench in benches)
                {
                    bench.Creator.Email = string.Empty;
                    bench.Creator.HashedPassword = string.Empty; 
                    bench.Creator.LastName = bench.Creator.LastName.Substring(0, 1);
                    //added to hide personal information of users
                    //a bit of processing overhead but we do not want to send a list of hashes and emails out into the wild

                }
                return benches;

            }
        }

        async public Task<Bench> GetBenchSingleAsync(int id)
        {
            using (var context = new Context())
            {
                Bench bench = await context.Benches
                    .Include(x => x.Comments)
                    .Include(x => x.Creator)
                    .SingleAsync(x => x.Id == id);

                bench.Creator.Email = string.Empty;
                bench.Creator.HashedPassword = string.Empty; //added to hide personal information of users
                bench.Creator.LastName = bench.Creator.LastName.Substring(0, 1);
                
                bench.Comments = bench.Comments.OrderByDescending(c=>c.CreatedOn).ToList();
                
                return bench;
            }
        }

        async public Task<Bench> AddBenchAsync(int id, int seats, decimal longitude, decimal latitude, string name, int creatorId, string description)
        {
            using (var context = new Context())
            {
                Bench bench = new Bench();
                bench.Id = id;
                bench.Seats = seats; 
                bench.Longitude = longitude;
                bench.Latitude = latitude;
                bench.Name = name;
                bench.CreatorId = creatorId;
                bench.Description = description;

                context.Benches.Add(bench);

                await context.SaveChangesAsync();
                return bench; 
            }
        }

        async public Task<Comment> AddCommentAsync(int benchId, string text, int rating, int userId)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.Rating = rating;
            comment.UserId = userId;
            comment.CreatedOn = DateTimeOffset.Now;
            using(var context = new Context())
            {
                Bench bench = await context.Benches.Include(x => x.Comments).SingleAsync(x => x.Id == benchId);
                comment.Bench = bench;
                bench.Comments.Add(comment);

                await context.SaveChangesAsync();
                return comment; 
            }

        }

    }
}