using AspProject_DataBase.Context;
using AspProject_Entities.Models;
using AspProject_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject_Services.Services
{
    public class ProductService : IProductService
    {
        private DBContext Context;
        public ProductService(DBContext dBContext)
        {
            Context = dBContext;
        }
        public void AddProduct(Product product)
        {
            Context.Products.Add(product);
            Context.SaveChanges();
        }
    }
}
