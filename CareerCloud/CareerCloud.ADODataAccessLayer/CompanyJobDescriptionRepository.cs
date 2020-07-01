using CareerCloud.DataAccessLayer;
using CareerCloud.Poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
  public  class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobDescriptionPoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                           ([Id]
                                           ,[Job]
                                           ,[Job_Name]
                                           ,[Job_Descriptions])
                                     VALUES
                                           (@Id
                                           ,@Job
                                           ,@Job_Name
                                           ,@Job_Descriptions)";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Job", poco.Job);
                cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select * from Company_Jobs_Descriptions";

            int x = 0;
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1100];
            while (rdr.Read())
            {
                CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Job = rdr.GetGuid(1);
                poco.JobName = rdr.GetString(2);
                poco.JobDescriptions = rdr.GetString(3);
                poco.TimeStamp = (byte[])rdr[4];

                pocos[x] = poco;
                x++;
            }
            conn.Close();
            return pocos.Where(p => p != null).ToList();

        }

        public IList<CompanyJobDescriptionPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobDescriptionPoco poco in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                          WHERE id =@id";


                cmd.Parameters.AddWithValue("@Id", poco.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobDescriptionPoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                           SET [Job] = @Job
                                              ,[Job_Name] = @Job_Name
                                              ,[Job_Descriptions] = @Job_Descriptions
                                         WHERE id = @id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Job", poco.Job);
                cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}