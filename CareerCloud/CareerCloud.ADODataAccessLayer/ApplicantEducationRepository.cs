using CareerCloud.DataAccessLayer;
using CareerCloud.Poco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
      
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach(ApplicantEducationPoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                           ([Id]
                                           ,[Applicant]
                                           ,[Major]
                                           ,[Certificate_Diploma]
                                           ,[Start_Date]
                                           ,[Completion_Date]
                                           ,[Completion_Percent])
                                     VALUES
                                           (@Id
                                           ,@Applicant
                                           ,@Major
                                           ,@Certificate_Diploma
                                           ,@Start_Date
                                           ,@Completion_Date
                                           ,@Completion_Percent)";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Major", poco.Major); 
                cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                   
            }


        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select * from Applicant_Educations";

            int x = 0;
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
            while(rdr.Read())
            {
                ApplicantEducationPoco poco = new ApplicantEducationPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Applicant = rdr.GetGuid(1);
                poco.Major = rdr.GetString(2);
                poco.CertificateDiploma = rdr.GetString(3);
                poco.StartDate = rdr.IsDBNull(4) ? null : (DateTime?)rdr.GetDateTime(4);
                poco.CompletionDate = rdr.IsDBNull(5) ? null : (DateTime?)rdr.GetDateTime(5);
                poco.CompletionPercent = (byte?) rdr.GetByte(6);
                poco.TimeStamp = (byte[]) rdr[7];

                pocos[x] = poco;
                x++;               
            }
            conn.Close();
            return pocos.Where(p => p != null).ToList();

        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantEducationPoco poco in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations]
                                      WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantEducationPoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                      SET [Applicant] = @Applicant
                                      ,[Major] = @Major
                                      ,[Certificate_Diploma] = @Certificate_Diploma
                                      ,[Start_Date] = @Start_Date
                                      ,[Completion_Date] = @Completion_Date
                                      ,[Completion_Percent] = @Completion_Percent
                                 WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Major", poco.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}
