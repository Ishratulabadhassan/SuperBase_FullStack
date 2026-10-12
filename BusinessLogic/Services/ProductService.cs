using BusinessLogic.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

using DataAccessLayer;
namespace BusinessLogic.Services


public class ProductService : IProductService
{
    public List<ProductModel> GetAllData()
    {
        return DataAccessLayer.CRUD.GetAllData();
    }
}