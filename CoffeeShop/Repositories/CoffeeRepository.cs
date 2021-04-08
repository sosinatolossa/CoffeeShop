using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using CoffeeShop.Models;

namespace CoffeeShop.Repositories
{
    public class CoffeeRepository : ICoffeeRepository

    {
        private readonly string _connectionString;
        public CoffeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Coffee> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, [Title], BeanVarietyId FROM Coffee;";
                    var reader = cmd.ExecuteReader();
                    var theCoffee = new List<Coffee>();
                    while (reader.Read())
                    {
                        var coffee = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("BeanVarietyId")))
                        {
                            coffee.BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId"));
                        }
                        theCoffee.Add(coffee);
                    }

                    reader.Close();

                    return theCoffee;
                }
            }
        }

        public Coffee Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Title], BeanVarietyId 
                          FROM Coffee
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    Coffee coffee = null;
                    if (reader.Read())
                    {
                        coffee = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("Notes")))
                        {
                            coffee.BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId"));
                        }
                    }

                    reader.Close();

                    return coffee;
                }
            }
        }

        public void Add(Coffee coffee)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO BeanVariety ([Title] BeanVarietyId)
                        OUTPUT INSERTED.ID
                        VALUES (@title, @beanVarietyId)";
                    cmd.Parameters.AddWithValue("@title", coffee.Title);
                    cmd.Parameters.AddWithValue("@beanVarietyId", coffee.BeanVarietyId);
                    if (coffee.BeanVarietyId == 0)
                    {
                        cmd.Parameters.AddWithValue("@beanVarietyId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@beanVarietyId", coffee.BeanVarietyId);
                    }

                    coffee.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Coffee coffee)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Coffee 
                           SET [Title] = @title, 
                               BeanVarietyId = @BeanVarietyId
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", coffee.Id);
                    cmd.Parameters.AddWithValue("@title", coffee.Title);
                    cmd.Parameters.AddWithValue("@beanVarietyId", coffee.BeanVarietyId);
                    if (coffee.BeanVarietyId == 0)
                    {
                        cmd.Parameters.AddWithValue("@beanVarietyId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@beanVarietyId", coffee.BeanVarietyId);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM BeanVariety WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
