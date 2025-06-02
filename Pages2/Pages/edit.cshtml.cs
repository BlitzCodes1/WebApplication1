using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Pages
{
    public class editModel : PageModel
    {

        public string activeEmail;
        public Dictionary<string, string> activeUser = new Dictionary<string, string>();

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
        private void GetAciveInfo()
        {


            string sqlQuery = $"SELECT * FROM Resturant WHERE  email = @activeEmail";
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



                            activeUser.Add("name", reader["name"].ToString());
                            activeUser.Add("email", reader["email"].ToString());
                            activeUser.Add("datetime", reader["datetime"].ToString());
                            activeUser.Add("phone", reader["phone"].ToString());


                        }
                    }
                }
            }


        }

        private void UpdateData(string name, string email, string phone, string password)
        {
            string sqlQuery = "UPDATE Resturant SET name = @name, email = @email, phone = @phone, password = @password " +
                              "WHERE email = @activeEmail"; 
            SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;


            command.Parameters.AddWithValue("@activeEmail", activeEmail);

            if (!name.IsNullOrEmpty())
            {
                command.Parameters.AddWithValue("@name", name);
            } else { command.Parameters.AddWithValue("@name", activeUser["name"]); }
            if (!email.IsNullOrEmpty())
            {
                command.Parameters.AddWithValue("@email", email);
            }else { command.Parameters.AddWithValue("@email", activeUser["email"]); }
            if (!phone.IsNullOrEmpty())
            {
                command.Parameters.AddWithValue("@phone", phone);
            }else { command.Parameters.AddWithValue("@phone", activeUser["phone"]); }
            if (!password.IsNullOrEmpty())
            {
                command.Parameters.AddWithValue("@password", password);
            }else { command.Parameters.AddWithValue("@password", activeUser["password"]); }

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void OnGet()
        {
            GetAciveEmail();
            GetAciveInfo();
        }
        public void OnPost()
        {
            string password = Request.Form["password"];
            string email = Request.Form["email"];
            string phone = Request.Form["phone"];
            string name = Request.Form["name"];

            

        }
    }
}
