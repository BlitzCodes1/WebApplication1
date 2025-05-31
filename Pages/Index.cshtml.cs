using System.Data.SQLite;
using System.Diagnostics;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
       

        

       


        private bool VerifyUser(string email , string password )
        {
            
            string sqlQuery = "SELECT email ,password FROM Resturant WHERE password = @password AND email = @email";

           
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                       
                        if (reader.Read())
                        {
                                               
                            return true; 
                            
                        }
                    }
                }
            } 

            
            return false; 
        }
        private void SetActiveEmail(string email)
        {
            string sqlQuery = "Update ActiveUser set (ActiveEmail) = @email where ID = 1";
            SQLiteConnection connection = new SQLiteConnection("Data Source=Resturant.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
    
            command.Parameters.AddWithValue("@email", email);

            command.ExecuteNonQuery();
            connection.Close();
        }


 
   
        public  bool condition;
        public IActionResult OnPost()
        {
            string password = Request.Form["password"];
            string email = Request.Form["email"];

            condition = VerifyUser(email.ToLower(), password);

            if (condition)
            {

                SetActiveEmail(email);
                return RedirectToPage("./mainPage");

            }
            else
            {
                TempData["ErrorMessage"] = "The username or email are incorrect";
                return RedirectToPage();
            }
           
        }

    }
}