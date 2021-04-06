﻿using AspProject_DataBase.Context;
using AspProject_Entities.Models;
using AspProject_Entities.Enums;
using AspProject_Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AspProject_Services.Services
{
    public class ProductService : IProductService
    {
        private readonly DBContext Context;
        public ProductService(DBContext dBContext)
        {
            Context = dBContext;
        }
        public void AddProduct(Product product)
        {
            Context.Products.Add(product);
            Context.SaveChanges();
        }
        public void AddProductToCart(int id, User user)
        {
            Product product = Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.Id == id).FirstOrDefault();
            product.LastModified = DateTime.Now;
            product.Buyer = user;
            product.State = ProductState.InCart;
            Context.SaveChanges();
        }
        public IEnumerable<Product> GetAllAvailableProducts()
        => Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.State == ProductState.UnSold).ToList();
        public IEnumerable<Product> GetAllAvailableProducts(List<int> productsInAnnonymusCart)
        => Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.State == ProductState.UnSold && !productsInAnnonymusCart.Contains(p.Id)).ToList();
        public IEnumerable<Product> GetCart(User user)
        => Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.Buyer == user && p.State == ProductState.InCart).ToList();
        public IEnumerable<Product> GetCart(List<int> productsInAnnonymusCart)
        => Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.State == ProductState.UnSold && productsInAnnonymusCart.Contains(p.Id)).ToList();
        public Product GetProductByID(int id)
        => Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.Id == id).FirstOrDefault();
        private async Task Purchase(IEnumerable<Product> productsToPurchase, User user = null)
        {
            await Context.Products.Where(product => productsToPurchase.Contains(product)).ForEachAsync((product) =>
            {
                product.State = ProductState.Sold;
                if (user != null) product.Buyer = user;
                else product.Buyer = null;
            });
            Context.SaveChanges();
        }
        public async Task Purchase(User user) => await Purchase(GetCart(user), user);
        public async Task Purchase(List<int> productsInAnnonymusCart) => await Purchase(GetCart(productsInAnnonymusCart));
        public void RemoveFromCart(int id, User user)
        {
            Product product = Context.Products.Include(p => p.Seler).Include(p => p.Buyer).Where(p => p.Id == id).FirstOrDefault();
            product.LastModified = DateTime.Now;
            product.Buyer = product.Seler;
            product.State = ProductState.UnSold;
            Context.SaveChanges();
        }
    }
}
