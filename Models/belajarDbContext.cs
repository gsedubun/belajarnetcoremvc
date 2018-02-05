using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace belajarnetcoremvc.Models
{
    public class belajarDbContext :DbContext
    {
        public belajarDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<TblRole> TblRole { get; set; }

        public DbSet<TblUser> TblUser { get; set; }
        public DbSet<TblUserRole> TblUserRole { get; set; }
        

    }
    public class TblUser
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

    }

    public class TblRole{
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }

    public class TblUserRole
    {
        [Key]
        public int ID { get; set; }
        public TblUser TblUser { get; set; }
        public TblRole TblRole { get; set; }

    }
}