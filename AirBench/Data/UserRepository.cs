using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Text;

namespace AirBench.Data
{
    public class UserRepository : IUserRepository
    {
        async public Task<User> GetUser(string email)
        {
            using(var context = new Context())
            {
                return await context.Users.SingleOrDefaultAsync(x => x.Email.Equals(email));
            }
        }

        async public Task<string> GetUserName(int id)
        {
            using(var context = new Context())
            {
                User user = await context.Users.SingleAsync(x => x.Id == id);
                return user.FirstName + " " + user.LastName + ".";

            }
        }

        async public Task<User> RegisterUser(string email, string hashedpassword, string firstName, string lastName)
        {
            using(var context = new Context())
            {
//                if (context.Users.SingleOrDefaultAsync(x => x.Email.Equals(email)) != null) return null;

                User newUser = new User();
                newUser.Email = email;
                newUser.HashedPassword = hashedpassword;
                newUser.FirstName = firstName;
                newUser.LastName = lastName; 

                context.Users.Add(newUser);

                await context.SaveChangesAsync();

                return newUser; 

            }
        }

    }
}