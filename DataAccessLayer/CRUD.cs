using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Npgsql;
using ProductModel;


namespace DataAccessLayer
{
   public class CRUD
    {
        public static List<ProductModel> GetAllData()
        {
            string Query = @"SELECT id, ""ProductName"", ""Price"" FROM ""Products""";
            List<ProductModel> products = new List<ProductModel>();

            NpgsqlConnection connection = DBHelper.GetConnection();
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(Query, connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
              ProductModel model = new ProductModel();
                model.Id = Convert.ToInt32(reader["id"]);
                model.ProductName = reader["ProductName"].ToString();
                model.Price = Convert.ToDecimal(reader["Price"]);
                products.Add(model);

            }
            connection.Close();
            return products;

        }
    }
}
