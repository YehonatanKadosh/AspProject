using AspProject_Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AspProject_DataBase.Context
{
    public class DBContext : DbContext
    {
        //Timer timer;
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            //timer = new Timer(tick, null, 10000, 30000);
        }

        //private void tick(object state)
        //{
        //    Task.Run(async () =>
        //   {
        //       await Products.Include(p => p.Buyer).Include(p => p.Seler).ForEachAsync((p) =>
        //       {
        //           if (DateTime.Now >= p.LastModified + TimeSpan.FromMinutes(1) && p.State == AspProject_Entities.Enums.ProductState.InCart)
        //           {
        //               p.State = AspProject_Entities.Enums.ProductState.UnSold;
        //               p.Buyer = p.Seler;
        //           }
        //       });
        //   });
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
