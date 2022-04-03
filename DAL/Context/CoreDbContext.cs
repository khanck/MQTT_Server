using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models.Core;


namespace DAL.Context
{
    public class CoreDbContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=BD52;Initial Catalog=MQTT;MultipleActiveResultSets=true;User ID=sa;Password=tech96369;ConnectRetryCount=0",
        //        options => options.EnableRetryOnFailure());
        //}
   
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies(true);
        //}
        public DbSet<Connections> connection { get; set; }
        public DbSet<Messages> message { get; set; }
        public object Configuration { get; internal set; }
    }
}
