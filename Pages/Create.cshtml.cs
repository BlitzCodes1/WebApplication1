using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Pages
{
    public class CreateModel : PageModel
    {


        private void InsertNewUser(string password, string email, string name, string phone)
        {
            string sqlQuery = "INSERT INTO Resturant (name ,password, email ,datetime , phone) " +
                              "VALUES (@name ,@password, @email ,@dateTime , @phone)";
            SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@dateTime", DateTime.Now.ToString());
            if(!phone.IsNullOrEmpty())
            {
                command.Parameters.AddWithValue("@phone", phone);
            }else
            {
                command.Parameters.AddWithValue("@phone", "");
            }

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
            string phone = Request.Form["phone"];
           

            if (ValidUserToInsert(email.ToLower()))
            {
                InsertNewUser(password , email.ToLower(), name , phone);
                TempData["validateUser"] = "you have created a new user";
            }
            else
            {
                TempData["validateUser"] = "this email already exits";
            }

        }
    }
}
