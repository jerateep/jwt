using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using testrm.Data;
using testrm.Models;

namespace testrm.Data
{
    public static class DBinitialize
    {
        public static void INIT(IServiceProvider serviceProvider)
        {
            var context = new Data.DB_BackOfficeContext(serviceProvider.GetRequiredService<DbContextOptions<DB_BackOfficeContext>>());
            context.Database.EnsureCreated();
            if (context.Product.Any())
            {
                return;
            }
            context.User.AddRange(Genuser());
            context.SaveChanges();
            context.Product.AddRange(GenProduct());
            context.SaveChanges();
        }
        public static IEnumerable<User> Genuser()
        {
            List<User> data = new List<User>
            {
                new User { user = "A",pass = "123", name = "A", mail = "a@a.com"},
                new User { user = "B",pass = "123", name = "B", mail = "b@b.com"},
            };
            return data; 
        }
        public static IEnumerable<Product> GenProduct()
        {
            List<Product> data = new List<Product>
            {
                new Product { name = "pen", price = 5.0},
                new Product { name = "book",price = 10.0},
                new Product { name = "box", price = 25.0},
            };
            return data;
        }
    }

}