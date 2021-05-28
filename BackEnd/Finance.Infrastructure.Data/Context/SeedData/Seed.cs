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
            var userId = Guid.NewGuid().ToString();
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser {Id = userId, DisplayName = "Vlad", UserName = "vlad", Email = "vlad@test.com"},
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
                    Money = 7001,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7002,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7003,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7004,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7005,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7006,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7007,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7008,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7009,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7010,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                },
                new Transaction
                {
                    Money = 7011,
                    TransactionStatus = false,
                    TransactionType = new TransactionType
                    {
                        Id = Guid.NewGuid(),
                        TransactionTypes = "type1"
                    },
                    AppUserId = userId
                }
            };

            var transactionTypes = new List<TransactionType>
            {
                new TransactionType
                {
                    Id = Guid.NewGuid(),
                    TransactionTypes = "type1"
                },
                new TransactionType
                {
                    Id = Guid.NewGuid(),
                    TransactionTypes = "type2"
                },
                new TransactionType
                {
                    Id = Guid.NewGuid(),
                    TransactionTypes = "type3"
                },
                new TransactionType
                {
                    Id = Guid.NewGuid(),
                    TransactionTypes = "type4"
                },
                new TransactionType
                {
                    Id = Guid.NewGuid(),
                    TransactionTypes = "type5"
                }
            };

            var banks = new List<Bank>
            {
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank1"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank2"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank3"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank4"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank5"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank6"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank7"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank8"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank9"
                },
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "bank10"
                }
            };


            var description = new List<UserDescription>
            {
                new UserDescription
                {
                    UserDescriptionId = userId,
                    Description =
                        @"In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used as a placeholder before final copy is available. It is also used to temporarily replace text in a process called greeking, which allows designers to consider the form of a webpage or publication, without the meaning of the text influencing the design.
"
                }
            };
            
            await context.Banks.AddRangeAsync(banks);
            await context.UserDescriptions.AddRangeAsync(description);
            await context.Transaction.AddRangeAsync(transactions);
            await context.TransactionType.AddRangeAsync(transactionTypes);
            await context.SaveChangesAsync();
        }
    }
}