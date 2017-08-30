using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using System.Linq;
using MySql.Data.Entity;


namespace TestSqlLinq
{
    internal class Program
    {
        static string connectionString = $"Server=localhost;port=3306;Database=tender2;User Id=root;password=1234;CharSet=utf8;Convert Zero Datetime=True;default command timeout=3600;Connection Timeout=3600";
        public static void Main(string[] args)
        {
            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            /*using (MySqlConnection connect = ConnectToDb.GetDbConnection())
            {
                connect.Open();
                CompResContext db = new CompResContext(connect, false);
                var ArCompRes = db.ArchiveComplaintResults;
                foreach (var v in ArCompRes)
                {
                    Console.WriteLine($"{v.Id} {v.Archive} {v.Region}");
                }
            }*/

            CompResContext db = new CompResContext();
            var ArCompRes = db.ArchiveComplaintResults.ToList();
            foreach (var v in ArCompRes)
            {
                Console.WriteLine($"{v.Id} {v.Archive} {v.Region}");
            }
        }
    }
    
    [Table(Name = "arhiv_complaint_result")]
    public class ArchiveComplaintRes
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        
        [Column(Name = "arhiv")]
        public int Archive { get; set; }
        
        [Column(Name = "region")]
        public int Region { get; set; }
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class CompResContext : DbContext
    {
        public CompResContext()
            : base(nameOrConnectionString: "ConnectMysql")
        {

        }

        public CompResContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }
 
        public DbSet<ArchiveComplaintRes> ArchiveComplaintResults { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArchiveComplaintRes>()
                .ToTable("arhiv_complaint_result");
            modelBuilder.Entity<ArchiveComplaintRes>()
                .HasKey(b => b.Id).HasEntitySetName("id");
            modelBuilder.Entity<ArchiveComplaintRes>()
                .Property(b => b.Archive).HasColumnName("arhiv");
            modelBuilder.Entity<ArchiveComplaintRes>()
                .Property(b => b.Region).HasColumnName("region");
            
        }
    }

}