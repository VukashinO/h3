using PizzaHut.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaHut.Models.SqlRepository
{
    public class SqlUserRepository : IUserRepository
    {
        private AppDbContext db;
        public SqlUserRepository(AppDbContext context)
        {
            db = context;
        }

        public User Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return user;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Update(User userChanges)
        {
            var user = db.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return userChanges;
        }
    }
}
