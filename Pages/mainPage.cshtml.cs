using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class mainPageModel : PageModel
    {
        public string activeName;
        public string activeEmail;

        private void GetAciveEmail()
        {
            string sqlQuery = "SELECT ActiveEmail FROM ActiveUser WHERE  ID = 1";


            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                   
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {

                         activeEmail = reader["ActiveEmail"].ToString();

                        }
                    }
                }
            }


        }
        private void GetAciveName()
        {
            string sqlQuery = $"SELECT name FROM Resturant WHERE  email = @activeEmail";


            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Parameters.AddWithValue("@activeEmail", activeEmail);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {

                            activeName = reader["name"].ToString();

                        }
                    }
                }
            }


        }


        public void OnGet()
        {
            GetAciveEmail();    
            GetAciveName();    
        }
        public void OnPost()
        {
            string username = Request.Form["username"];

        }
    }
}
