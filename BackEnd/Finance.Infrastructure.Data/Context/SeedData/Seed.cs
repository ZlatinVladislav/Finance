using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Finance.Infrastructure.Data.Context.SeedData
{
    public class Seed
    {
        public static async Task SeedData(FinanceDBContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser {DisplayName = "Bob", UserName = "bob", Email = "bob@test.com"},
                    new AppUser {DisplayName = "Tom", UserName = "tom", Email = "tom@test.com"},
                    new AppUser {DisplayName = "Jane", UserName = "jane", Email = "jane@test.com"}
                };

                foreach (var user in users) await userManager.CreateAsync(user, "Pa$$word1");
            }

            if (context.Transaction.Any()) return;

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Money = 7000,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    }
                }
            };

            await context.Transaction.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }
    }
}