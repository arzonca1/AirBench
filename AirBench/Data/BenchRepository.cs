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
                return await context.Benches
                    .Include(x => x.Comments) //will need this to calculate rating 
                    .ToListAsync();
            }
        }

        async public Task<Bench> GetBenchSingleAsync(int id)
        {
            using (var context = new Context())
            {
                Bench bench = await context.Benches
                    .Include(x => x.Comments)
                    .SingleAsync(x => x.Id == id);

                return bench;
            }
        }

        async public Task<Bench> AddBenchAsync(int id, decimal longitude, decimal latitude, string name)
        {
            using (var context = new Context())
            {
                Bench bench = new Bench();
                bench.Id = id;
                bench.Longitude = longitude;
                bench.Latitude = latitude;
                bench.Name = name;

                context.Benches.Add(bench);

                await context.SaveChangesAsync();
                return bench; 
            }
        }

        async public Task<Comment> AddCommentAsync(int benchId, string text, int rating)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.Rating = rating; 
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