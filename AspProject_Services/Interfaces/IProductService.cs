using AspProject_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject_Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);
    }
}
