using AspProject_DataBase.Context;
using AspProject_Entities.Models;
using AspProject_Entities.Enums;
using AspProject_Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
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

        public object GetAllAvailable()
        => Context.Products.Include(p => p.Owner).Include(p => p.User).Where(p => p.State == ProductState.UnSold).ToList();

        public Product getProductByID(int id)
        => Context.Products.Include(p => p.Owner).Include(p => p.User).Where(p => p.Id == id).FirstOrDefault();
    }
}
