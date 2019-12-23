using ProductManager.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataAccess
{
    public class DbInit<T> : CreateDatabaseIfNotExists<PmContext>
    {
        protected override void Seed(PmContext context)
        {
            IList<User> users = new List<User>();

            users.Add(new User() { Login = "Hello", Password = "Baybay" });

            context.Users.AddRange(users);

            base.Seed(context);
        }
    }
}
