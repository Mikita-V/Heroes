using System;
using System.Collections.Generic;
using System.Data.Entity;
using ORM.Entities;

namespace ORM.Db
{
    public class HeroesInitializer: DropCreateDatabaseIfModelChanges<HeroesContext>
    {
        protected override void Seed(HeroesContext context)
        {
            var users = new List<User>()
            {
                new User
                {
                    Name = "Jack",
                    BirthDate = new DateTime(1991, 6, 15)
                },
                new User
                {
                    Name = "Joe",
                    BirthDate = new DateTime(1992, 11, 10)
                },
                new User
                {
                    Name = "Christine",
                    BirthDate = new DateTime(1993, 10, 20)
                }
            };

            var rewards = new List<Reward>()
            {
                new Reward {Title = "Back-end", Description = "Best back-end developer"},
                new Reward {Title = "Front-end", Description = "Best fron-end developer"},
                new Reward {Title = "Full-stack", Description = "Best full-stack developer"},
                new Reward {Title = "UX", Description = "Best UX/UI designer"},
                new Reward {Title = "DA", Description = "Best database analyst"},
                new Reward {Title = "DevOps", Description = "Best DevOps"}
            };

            for (int i = 0; i < users.Count; i++)
            {
                users[i].Rewards.Add(rewards[i]);
            }

            context.Rewards.AddRange(rewards);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}