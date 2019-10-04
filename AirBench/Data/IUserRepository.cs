using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AirBench.Data
{
    public interface IUserRepository
    {
        Task<User> GetUser(string email);
        Task<User> RegisterUser(string email, string hashedpassword, string firstName, string lastName);
        Task<string> GetUserName(int id);
    }
}