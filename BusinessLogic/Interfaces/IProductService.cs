using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{ 
public interface IProductService
{
    List<ProductModel> GetAllData();

}
}
