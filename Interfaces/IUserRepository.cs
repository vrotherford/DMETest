using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(int skipCount, int uCount, string sortBy, bool isDesc);
        User GetUser(Guid id);
        void InsertUser(User user);
        User CheckUser(string login, string pass);
        IEnumerable<User> FindUserByName(string fName, int skipCount, int uCount, string sortBy, bool isDesc);
    }
}
