using Deliverables.Data.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deliverables.Data.Implementation
{
    public class DataContextConfig
    {
        public static void BuildDomainModel(DbModelBuilder builder)
        {
            ConfigureConventions(builder);

            ConfigureEntities(builder);

            ConfigureRelations(builder);

            ConfigureConstraints(builder);
        }

        private static void ConfigureConventions(DbModelBuilder builder)
        {
            builder.Conventions.Remove<PluralizingTableNameConvention>();
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private static void ConfigureEntities(DbModelBuilder builder)
        {
            var baseEntityType = typeof(BaseEntity);
            var domainAssembly = Assembly.GetAssembly(baseEntityType);

            var entitiesTypes = domainAssembly.ExportedTypes
                .Where(t => t.IsClass && t != baseEntityType && baseEntityType.IsAssignableFrom(t));

            foreach (var type in entitiesTypes)
            {
                var entityMethod = builder.GetType().GetMethod("Entity").MakeGenericMethod(new Type[] { type });
                entityMethod.Invoke(builder, null);
            }
        }

        private static void ConfigureRelations(DbModelBuilder builder)
        { }

        private static void ConfigureConstraints(DbModelBuilder builder)
        { }
    }
}
