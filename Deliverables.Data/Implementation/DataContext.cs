using System.Collections;
using System.Data.Entity;
using System.Diagnostics;
using SourceScrub.DataAccess.Abstraction;
using Deliverables.Data.Abstraction;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Deliverables.Data.Models;
using System.Configuration;

namespace Deliverables.Data.Implementation
{
    public class DataContext : IdentityDbContext<User>, IDataContext
    {
        #region Private fields

        private static string _defaultConnectionStringName = "DefaultConnection";
        private DataContext dataContext;

        #endregion

        #region .ctor

        static DataContext()
        {            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Deliverables.Data.Migrations.Configuration>());
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _defaultConnectionStringName = nameOrConnectionString;
            InitializeSettings();
        }

        public DataContext(string nameOrConnectionString, IDatabaseInitializer<DataContext> initializer)
            : base(nameOrConnectionString)
        {
            _defaultConnectionStringName = nameOrConnectionString;
            Database.SetInitializer(initializer);
            InitializeSettings();
        }

        public DataContext()
            : base(ConfigurationManager.AppSettings[_defaultConnectionStringName] ?? _defaultConnectionStringName)
        {
            InitializeSettings();
        }

        public DataContext(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        private void InitializeSettings()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Database.Log = s => Debug.WriteLine(s);
        }

        #endregion
        
        #region Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DataContextConfig.BuildDomainModel(modelBuilder);
        }

        #endregion

        #region IDataContext Implementation

        public DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public DbContextTransaction BeginTransation()
        {
            return Database.BeginTransaction();
        }

        #endregion
    }
}
