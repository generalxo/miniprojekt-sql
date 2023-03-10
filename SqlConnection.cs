using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;

namespace MiniprojektSql
{
    public class SqlConnection
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<PersonModel> LoadPersonModel()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>($"SELECT * FROM public.kkj_person", new DynamicParameters());
                return output.ToList();

            }
        }
    }
}
