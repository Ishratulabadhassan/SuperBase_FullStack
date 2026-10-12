using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class DalProduct : IProductRepository
    {
        public void GetData()
        {
            DBHelper.MSSQlServer();
        }
    }
}
