using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class CreateModel : PageModel
    {


        private void InsertNewUser(string password, string email , string name)
        {
            string sqlQuery = "INSERT INTO Resturant (name ,password, email) VALUES (@name ,@password, @email)";
            SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@email", email);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private bool ValidUserToInsert(string email)
        {

            string sqlQuery = "SELECT email FROM Resturant WHERE  email = @email";


            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Parameters.AddWithValue("@email", email);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {

                            return false ;

                        }
                    }
                }
            }
            return true;
        }



        
        public void OnPost()
        {
            string name = Request.Form["name"];
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            if (ValidUserToInsert(email.ToLower()))
            {
                InsertNewUser(password , email.ToLower(), name);
                TempData["validateUser"] = "you have created a new user";
            }
            else
            {
                TempData["validateUser"] = "this email already exits";
            }

        }
    }
}
