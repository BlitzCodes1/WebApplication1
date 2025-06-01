using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebApplication1.Pages
{
    public class AccountModel : PageModel
    {
        public string activeName;
        public string activeEmail;

        public Dictionary<string, string> activeUser= new Dictionary<string, string>();

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


        public void OnGet()
        {
            GetAciveEmail();
           
            GetAciveInfo();
        }
        public void OnPost()
        {
          

        }
    }
}
