namespace Deliverables.Data.Migrations
{
    using Implementation;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Deliverables.Data.Implementation.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Deliverables.Data.Implementation.DataContext context)
        {
            AddTechnicalSkillForTest(context);
        }

        private void AddTechnicalSkillForTest(DataContext context)
        {
            if (!context.Set<TechnicalSkill>().Any())
            {

                var redundantWords = new List<TechnicalSkill>()
                {
                    new TechnicalSkill {Name = "Front End", LevelId = 1},
                    new TechnicalSkill {Name = "Azure", LevelId =2},
                };

                context.Set<TechnicalSkill>().AddRange(redundantWords);

                context.SaveChanges();
            }
        }
    }
}
