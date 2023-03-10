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
                var output = cnn.Query<PersonModel>($"SELECT * FROM public.owa_person", new DynamicParameters());
                return output.ToList();

            }
        }

        public static List<ProjectModel> LoadProjectModel()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>($"SELECT * FROM public.owa_project", new DynamicParameters());
                return output.ToList();

            }
        }

        public static void SaveProjectPersonModel(ProjectPersonModel model)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO public.owa_project_person (project_id, person_id, hours) VALUES (@project_id, @person_id, @hours)", model);

            }
        }
    }
}
