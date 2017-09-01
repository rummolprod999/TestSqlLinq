using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using System.Linq;
using MySql.Data.Entity;
using Annot = System.ComponentModel.DataAnnotations.Schema;

namespace TestSqlLinq
{
    internal class Program
    {
        public static string connectionString = $"Server=localhost;port=3306;Database=tender2;User Id=root;password=1234;CharSet=utf8;Convert Zero Datetime=True;default command timeout=3600;Connection Timeout=3600";
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

            using (CompResContext db = new CompResContext())
            {
                //db.ArchiveComplaintResults.Load();
                var ArCompRes = db.ArchiveComplaintResults.Where(p => p.Archive.Contains("test")).ToList();
                foreach (var v in ArCompRes)
                {
                    Console.WriteLine($"{v.Id} {v.Archive} {v.Region}");
                }
                var Afors = db.ArchiveForResults.Where(p => p.ArchiveComplaintResId == 14).ToList();
                foreach (var v in Afors)
                {
                    db.ArchiveForResults.Remove(v);
                }
                db.SaveChanges();
                /*ArchiveComplaintRes a  = new ArchiveComplaintRes {Archive = "testfor", Region = "77"};
                db.ArchiveComplaintResults.Add(a);
                db.SaveChanges();
                ArchiveFor c = new ArchiveFor {Name = "54545", Name2 = "54645", ArchiveComplaintRes = a};
                ArchiveFor d = new ArchiveFor {Name = "54545", Name2 = "54645", ArchiveComplaintRes = a};
                db.ArchiveForResults.AddRange(new List<ArchiveFor>{c, d});
                db.SaveChanges();*/

                /*ArchiveComplaintRes b = db.ArchiveComplaintResults.FirstOrDefault();
                if (b != null) b.Archive = "grlishgkf";*/
                //db.SaveChanges();
            }
            Console.ReadKey();

        }
    }
    
    [Annot.Table("arhiv_complaint_result")]
    public class ArchiveComplaintRes
    {
        [Key, Annot.DatabaseGeneratedAttribute(Annot.DatabaseGeneratedOption.Identity)]
        [Annot.Column("id")]
        public int Id { get; set; }
        
        [Annot.Column("arhiv")]
        public string Archive { get; set; }
        
        [Annot.Column("region")]
        public string Region { get; set; }
        
        public ICollection<ArchiveFor> ArchiveFor { get; set; }
        
        public ArchiveComplaintRes()
        {
            ArchiveFor = new List<ArchiveFor>();
        }
    }

    [Annot.Table("archive_for")]
    public class ArchiveFor
    {
        [Key, Annot.DatabaseGeneratedAttribute(Annot.DatabaseGeneratedOption.Identity)]
        [Annot.Column("id")]
        public int Id { get; set; }

        [Annot.Column("name")]
        public string Name { get; set; }

        [Annot.Column("name2")]
        public string Name2 { get; set; }
        
        [Annot.Column("id_arhiv_complaint_result")]
        public int ArchiveComplaintResId { get; set; }
        
        public ArchiveComplaintRes ArchiveComplaintRes { get; set; }
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
        public DbSet<ArchiveFor> ArchiveForResults { get; set; }
        
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArchiveComplaintRes>()
                .ToTable("arhiv_complaint_result");
            modelBuilder.Entity<ArchiveComplaintRes>()
                .HasKey(b => b.Id).HasEntitySetName("id");
            modelBuilder.Entity<ArchiveComplaintRes>()
                .Property(b => b.Archive).HasColumnName("arhiv");
            modelBuilder.Entity<ArchiveComplaintRes>()
                .Property(b => b.Region).HasColumnName("region");
            
        }*/
    }

}