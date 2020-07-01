using CareerCloud.DataAccessLayer;
using CareerCloud.Poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantResumePoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                                   ([Id]
                                                   ,[Applicant]
                                                   ,[Resume]
                                                   ,[Last_Updated])
                                             VALUES
                                                   (@Id
                                                   ,@Applicant
                                                   ,@Resume
                                                   ,@Last_Updated)";


                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select * from Applicant_Resumes";

            int x = 0;
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            ApplicantResumePoco[] pocos = new ApplicantResumePoco[1000];
            while (rdr.Read())
            {
                ApplicantResumePoco poco = new ApplicantResumePoco();
                poco.Id = rdr.GetGuid(0);
                poco.Applicant = rdr.GetGuid(1);
                poco.Resume = rdr.GetString(2);
                poco.LastUpdated = (rdr.IsDBNull(3) ? null : (DateTime?)rdr.GetDateTime(3));
                                   
                pocos[x] = poco;
                x++;
            }
            conn.Close();
            return pocos.Where(p => p != null).ToList();
       
        }
        public IList<ApplicantResumePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantResumePoco poco in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes]
                                          WHERE ID = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        public void Update(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantResumePoco poco in items)
            {
                cmd.CommandText = @" UPDATE[dbo].[Applicant_Resumes]
                                           SET [Applicant] = @Applicant
                                              ,[Resume] = @Resume
                                              ,[Last_Updated] = @Last_Updated
                                          WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

    }
}