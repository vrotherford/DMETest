using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Interfaces;

namespace Data
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private BasicContext db;

        public UserRepository()
        {
            this.db = new BasicContext();
        }

        public IEnumerable<User> GetUsers(int skipCount, int uCount, string sortBy, bool isDesc)
        {
            if (isDesc)
            {
                if (sortBy == "birth")
                {
                    return db.Users.OrderByDescending(u => u.birth).Skip(skipCount).Take(uCount).ToList();
                }
                else if (sortBy == "name")
                {
                    return db.Users.OrderByDescending(u => u.lastName).ThenByDescending(u => u.firstName).Skip(skipCount).Take(uCount).ToList();
                }
                else
                {
                    return db.Users.OrderByDescending(u => u.id).Skip(skipCount).Take(uCount).ToList();
                }
            }
            else
            {
                if (sortBy == "birth")
                {
                    return db.Users.OrderBy(u => u.birth).Skip(skipCount).Take(uCount).ToList();
                }
                else if (sortBy == "name")
                {
                    return db.Users.OrderBy(u => u.lastName).ThenBy(u => u.firstName).Skip(skipCount).Take(uCount).ToList();
                }
                else
                {
                    return db.Users.OrderBy(u => u.id).Skip(skipCount).Take(uCount).ToList();
                }
            }
            
        }

        public User GetUser(Guid id)
        {
            return db.Users.SingleOrDefault(u => u.id == id);
        }

        public void InsertUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User CheckUser(string login, string pass)
        {
            return db.Users.FirstOrDefault(u => u.email == login && u.password == pass);
        }

        public IEnumerable<User> FindUserByName(string fName, int skipCount, int uCount, string sortBy, bool isDesc)
        {
            if (isDesc)
            {
                if (sortBy == "birth")
                {
                    return db.Users.OrderByDescending(u => u.birth).Where(u => u.lastName.ToLower().Contains(fName.ToLower()) || u.firstName.ToLower().Contains(fName.ToLower())).Skip(skipCount).Take(uCount).ToList();
                }
                else if (sortBy == "name")
                {
                    return db.Users.OrderByDescending(u => u.lastName).Where(u => u.lastName.ToLower().Contains(fName.ToLower()) || u.firstName.ToLower().Contains(fName.ToLower())).Skip(skipCount).Take(uCount).ToList();
                }
                else
                {
                    return db.Users.OrderByDescending(u => u.id).Where(u => u.lastName.ToLower().Contains(fName.ToLower()) || u.firstName.ToLower().Contains(fName.ToLower())).Skip(skipCount).Take(uCount).ToList();
                }
            }
            else
            {
                if (sortBy == "birth")
                {
                    return db.Users.OrderBy(u => u.birth).Where(u => u.lastName.ToLower().Contains(fName.ToLower()) || u.firstName.ToLower().Contains(fName.ToLower())).Skip(skipCount).Take(uCount).ToList();
                }
                else if (sortBy == "name")
                {
                    return db.Users.OrderBy(u => u.lastName).Where(u => u.lastName.ToLower().Contains(fName.ToLower()) || u.firstName.ToLower().Contains(fName.ToLower())).Skip(skipCount).Take(uCount).ToList();
                }
                else
                {
                    return db.Users.OrderBy(u => u.id).Where(u => u.lastName.ToLower().Contains(fName.ToLower()) || u.firstName.ToLower().Contains(fName.ToLower())).Skip(skipCount).Take(uCount).ToList();
                }
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
