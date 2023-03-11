using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;

namespace MiniprojektSql
{
    public class SqlConnection
    {
        //Conection String
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //Load/Select Model Methods Start
        //These Methods will load correspinding Model from DB to a List
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
        public static List<ProjectPersonModel> LoadProjectPersonModel()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectPersonModel>($"SELECT * FROM public.owa_project_person", new DynamicParameters());
                return output.ToList();
            }
        }
        //Load Model Methods End

        //Save/Insert Model Methods Start
        public static void SaveProjectPersonModel(ProjectPersonModel model)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO public.owa_project_person (project_id, person_id, hours) VALUES (@project_id, @person_id, @hours)", model);
            }
        }

        public static void SavePerson(PersonModel person)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO public.owa_person (person_name) VALUES (@person_name)", person);
            }
        }

        public static void SaveProject(ProjectModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO public.owa_project (project_name) VALUES (@project_name)", project);
            }
        }

        //Save Model Methods End
    }
}
