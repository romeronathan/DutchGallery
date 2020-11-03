using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder : IDisposable
    {
        private readonly DutchContext _ctx;
        private readonly IHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx, IHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task SeedAsync()
        {
             _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("romeronathan@hotmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Nathan",
                    LastName = "Romero",
                    Email = "romeronathan@hotmail.com",
                    UserName = "romeronathan@hotmail.com"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in seeder");
                }
            }

            if (!_ctx.Products.Any())
      {
                // Create Sample Data
                var filepath = Path.Combine(_hosting.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _ctx.SaveChanges();
            }
        }
    }
}
