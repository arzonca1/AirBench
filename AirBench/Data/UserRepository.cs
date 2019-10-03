using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

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

        async public Task<User> RegisterUser(string email, string hashedpassword, string name)
        {
            using(var context = new Context())
            {
//                if (context.Users.SingleOrDefaultAsync(x => x.Email.Equals(email)) != null) return null;

                User newUser = new User();
                newUser.Email = email;
                newUser.HashedPassword = hashedpassword;
                newUser.Name = name;

                context.Users.Add(newUser);

                await context.SaveChangesAsync();

                return newUser; 

            }
        }

    }
}