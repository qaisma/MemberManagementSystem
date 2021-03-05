﻿// Licence file C:\Users\Qais\Documents\ReversePOCO.txt not found.
// Please obtain your licence file at www.ReversePOCO.co.uk, and place it in your documents folder shown above.
// Defaulting to Trial version.
// <auto-generated>
// ReSharper disable All

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MemberManagementSystem.Models.DAL
{
    #region Database context interface

    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    public interface IMMSDbContext : IDisposable
    {
        DbSet<Account> Accounts { get; set; } // Accounts
        DbSet<Comapny> Comapnies { get; set; } // Comapnies
        DbSet<Member> Members { get; set; } // Members
        DbSet<User> Users { get; set; } // Users

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }

    #endregion

    #region Database context

    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    public class MMSDbContext : DbContext, IMMSDbContext
    {
        public MMSDbContext()
        {
        }

        public MMSDbContext(DbContextOptions<MMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } // Accounts
        public DbSet<Comapny> Comapnies { get; set; } // Comapnies
        public DbSet<Member> Members { get; set; } // Members
        public DbSet<User> Users { get; set; } // Users

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=MemberManagementSystem;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ComapnyConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }

    #endregion

    #region Database context factory

    public class MMSDbContextFactory : IDesignTimeDbContextFactory<MMSDbContext>
    {
        public MMSDbContext CreateDbContext(string[] args)
        {
            return new MMSDbContext();
        }
    }

    #endregion

    #region Fake Database context

    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    public class FakeMMSDbContext : IMMSDbContext
    {
        public DbSet<Account> Accounts { get; set; } // Accounts
        public DbSet<Comapny> Comapnies { get; set; } // Comapnies
        public DbSet<Member> Members { get; set; } // Members
        public DbSet<User> Users { get; set; } // Users

        public FakeMMSDbContext()
        {
            _database = null;

            Accounts = new FakeDbSet<Account>("MemberId", "CompanyId");
            Comapnies = new FakeDbSet<Comapny>("Id");
            Members = new FakeDbSet<Member>("Id");
            Users = new FakeDbSet<User>("Id");

        }

        public int SaveChangesCount { get; private set; }
        public virtual int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(x => 1, acceptAllChangesOnSuccess, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private DatabaseFacade _database;
        public DatabaseFacade Database { get { return _database; } }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Fake DbSet

    // ************************************************************************
    // Fake DbSet
    // Implementing Find:
    //      The Find method is difficult to implement in a generic fashion. If
    //      you need to test code that makes use of the Find method it is
    //      easiest to create a test DbSet for each of the entity types that
    //      need to support find. You can then write logic to find that
    //      particular type of entity, as shown below:
    //      public class FakeBlogDbSet : FakeDbSet<Blog>
    //      {
    //          public override Blog Find(params object[] keyValues)
    //          {
    //              var id = (int) keyValues.Single();
    //              return this.SingleOrDefault(b => b.BlogId == id);
    //          }
    //      }
    //      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx
    public class FakeDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IAsyncEnumerable<TEntity>, IListSource where TEntity : class
    {
        private readonly PropertyInfo[] _primaryKeys;
        private readonly ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;

        public FakeDbSet()
        {
            _primaryKeys = null;
            _data        = new ObservableCollection<TEntity>();
            _query       = _data.AsQueryable();
        }

        public FakeDbSet(params string[] primaryKeys)
        {
            _primaryKeys = typeof(TEntity).GetProperties().Where(x => primaryKeys.Contains(x.Name)).ToArray();
            _data        = new ObservableCollection<TEntity>();
            _query       = _data.AsQueryable();
        }

        public override TEntity Find(params object[] keyValues)
        {
            if (_primaryKeys == null)
                throw new ArgumentException("No primary keys defined");
            if (keyValues.Length != _primaryKeys.Length)
                throw new ArgumentException("Incorrect number of keys passed to Find method");

            var keyQuery = this.AsQueryable();
            keyQuery = keyValues
                .Select((t, i) => i)
                .Aggregate(keyQuery,
                    (current, x) =>
                        current.Where(entity => _primaryKeys[x].GetValue(entity, null).Equals(keyValues[x])));

            return keyQuery.SingleOrDefault();
        }

        public override ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return new ValueTask<TEntity>(Task<TEntity>.Factory.StartNew(() => Find(keyValues), cancellationToken));
        }

        public override ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return new ValueTask<TEntity>(Task<TEntity>.Factory.StartNew(() => Find(keyValues)));
        }

        IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAsyncEnumerator(cancellationToken);
        }

        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            _data.Add(entity);
            return null;
        }

        public override void AddRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities.ToList())
                _data.Add(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            AddRange(entities.ToArray());
        }

        public override Task AddRangeAsync(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return Task.Factory.StartNew(() => AddRange(entities));
        }

        public override void AttachRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            AddRange(entities);
        }

        public override void RemoveRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities.ToList())
                _data.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            RemoveRange(entities.ToArray());
        }

        public override void UpdateRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            RemoveRange(entities);
            AddRange(entities);
        }

        public IList GetList()
        {
            return _data;
        }

        IList IListSource.GetList()
        {
            return _data;
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new FakeDbAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
        }

    }

    public class FakeDbAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public FakeDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var m = expression as MethodCallExpression;
            if (m != null)
            {
                var resultType = m.Method.ReturnType; // it should be IQueryable<T>
                var tElement = resultType.GetGenericArguments()[0];
                var queryType = typeof(FakeDbAsyncEnumerable<>).MakeGenericType(tElement);
                return (IQueryable) Activator.CreateInstance(queryType, expression);
            }
            return new FakeDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var queryType = typeof(FakeDbAsyncEnumerable<>).MakeGenericType(typeof(TElement));
            return (IQueryable<TElement>) Activator.CreateInstance(queryType, expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = new CancellationToken())
        {
            return _inner.Execute<TResult>(expression);
        }
    }

    public class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public FakeDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public FakeDbAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAsyncEnumerator(cancellationToken);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.AsEnumerable().GetEnumerator();
        }
    }

    public class FakeDbAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public FakeDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current
        {
            get { return _inner.Current; }
        }
        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return new ValueTask(Task.CompletedTask);
        }
    }

    #endregion

    #region POCO classes

    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    // Accounts
    public class Account
    {
        public int MemberId { get; set; } // MemberId (Primary key)
        public int CompanyId { get; set; } // CompanyId (Primary key)
        public int Balance { get; set; } // Balance
        public byte Status { get; set; } // Status

        // Foreign keys

        /// <summary>
        /// Parent Comapny pointed by [Accounts].([CompanyId]) (FK_Accounts_Comapnies)
        /// </summary>
        public virtual Comapny Comapny { get; set; } // FK_Accounts_Comapnies

        /// <summary>
        /// Parent Member pointed by [Accounts].([MemberId]) (FK_Accounts_Members)
        /// </summary>
        public virtual Member Member { get; set; } // FK_Accounts_Members

        public Account()
        {
            Balance = 0;
        }
    }

    // Comapnies
    public class Comapny
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child Accounts where [Accounts].[CompanyId] point to this entity (FK_Accounts_Comapnies)
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; } // Accounts.FK_Accounts_Comapnies

        public Comapny()
        {
            Accounts = new List<Account>();
        }
    }

    // Members
    public class Member
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Address { get; set; } // Address (length: 500)
        public int UserId { get; set; } // UserId

        // Reverse navigation

        /// <summary>
        /// Child Accounts where [Accounts].[MemberId] point to this entity (FK_Accounts_Members)
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; } // Accounts.FK_Accounts_Members

        // Foreign keys

        /// <summary>
        /// Parent User pointed by [Members].([UserId]) (FK_Members_Users)
        /// </summary>
        public virtual User User { get; set; } // FK_Members_Users

        public Member()
        {
            Accounts = new List<Account>();
        }
    }

    // Users
    public class User
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 200)
        public string Email { get; set; } // Email (length: 200)
        public string LoginName { get; set; } // LoginName (length: 200)
        public string LoginPassword { get; set; } // LoginPassword (length: 200)

        // Reverse navigation

        /// <summary>
        /// Child Members where [Members].[UserId] point to this entity (FK_Members_Users)
        /// </summary>
        public virtual ICollection<Member> Members { get; set; } // Members.FK_Members_Users

        public User()
        {
            Members = new List<Member>();
        }
    }


    #endregion

    #region POCO Configuration

    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    // Accounts
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", "dbo");
            builder.HasKey(x => new { x.MemberId, x.CompanyId }).HasName("PK_CompaniesAccounts").IsClustered();

            builder.Property(x => x.MemberId).HasColumnName(@"MemberId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CompanyId).HasColumnName(@"CompanyId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Balance).HasColumnName(@"Balance").HasColumnType("int").IsRequired();
            builder.Property(x => x.Status).HasColumnName(@"Status").HasColumnType("tinyint").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Comapny).WithMany(b => b.Accounts).HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Accounts_Comapnies");
            builder.HasOne(a => a.Member).WithMany(b => b.Accounts).HasForeignKey(c => c.MemberId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Accounts_Members");
        }
    }

    // Comapnies
    public class ComapnyConfiguration : IEntityTypeConfiguration<Comapny>
    {
        public void Configure(EntityTypeBuilder<Comapny> builder)
        {
            builder.ToTable("Comapnies", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_Comapnies").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(100)").IsRequired().HasMaxLength(100);
        }
    }

    // Members
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_Members").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(150)").IsRequired().HasMaxLength(150);
            builder.Property(x => x.Address).HasColumnName(@"Address").HasColumnType("nvarchar(500)").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.User).WithMany(b => b.Members).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Members_Users");
        }
    }

    // Users
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_Users").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(200)").IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar(200)").IsRequired().HasMaxLength(200);
            builder.Property(x => x.LoginName).HasColumnName(@"LoginName").HasColumnType("nvarchar(200)").IsRequired().HasMaxLength(200);
            builder.Property(x => x.LoginPassword).HasColumnName(@"LoginPassword").HasColumnType("nvarchar(200)").IsRequired().HasMaxLength(200);
        }
    }


    #endregion

}
// </auto-generated>
