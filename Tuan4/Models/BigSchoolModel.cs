using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Tuan4.Models
{
    public partial class BigSchoolModel : DbContext
    {
        public BigSchoolModel()
            : base("name=BigSchoolsModel")
        {
        }

        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Following> Following { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Attendance)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);
        }
    }
}
