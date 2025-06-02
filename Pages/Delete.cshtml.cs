using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly string _connectionString = "Data Source=Resturant.db";
        public string ActiveEmail { get; private set; }

        private void GetActiveEmail()
        {
            const string sqlQuery = "SELECT ActiveEmail FROM ActiveUser WHERE ID = 1";

           
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlQuery;

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ActiveEmail = reader["ActiveEmail"].ToString();
                            }
                        }
                    }
                }
            
           
        }

        private bool DeleteAccount()
        {
            const string sqlQuery = "DELETE FROM Resturant WHERE email = @activeEmail";

            
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Parameters.AddWithValue("@activeEmail", ActiveEmail);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            
           
        }

        public void OnGet()
        {
            GetActiveEmail();
        }

        public IActionResult OnPost()
        {
            GetActiveEmail();
            if (string.IsNullOrEmpty(ActiveEmail))
            {
                // Handle error - no active email found
                return RedirectToPage("./Error");
            }

            bool deleted = DeleteAccount();
            if (!deleted)
            {
                // Handle error - account not deleted
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }
    }
}