using CareerCloud.DataAccessLayer;
using CareerCloud.Poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobSkillPoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                           ([Id]
                                           ,[Job]
                                           ,[Skill]
                                           ,[Skill_Level]
                                           ,[Importance])
                                     VALUES
                                           (@Id
                                           ,@Job
                                           ,@Skill
                                           ,@Skill_Level
                                           ,@Importance)";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Job", poco.Job);
                cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                cmd.Parameters.AddWithValue("@Importance", poco.Importance);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select * from Company_Job_Skills";

            int x = 0;
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[5100];
            while (rdr.Read())
            {
                CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Job = rdr.GetGuid(1);
                poco.Skill = rdr.GetString(2);
                poco.SkillLevel = rdr.GetString(3);
                poco.Importance = rdr.GetInt32(4);
                poco.TimeStamp = (byte[])rdr[5];

                pocos[x] = poco;
                x++;
            }
            conn.Close();
            return pocos.Where(p => p != null).ToList();

        }

        public IList<CompanyJobSkillPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobSkillPoco poco in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills]
                                       WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseADO.connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (CompanyJobSkillPoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                       SET [Job] = @Job
                                          ,[Skill] = @Skill
                                          ,[Skill_Level] = @Skill_Level
                                          ,[Importance] = @Importance
                                        WHERE id = @id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Job", poco.Job);
                cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                cmd.Parameters.AddWithValue("@Importance", poco.Importance);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
        }
    }
}