using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; // Burayı ekledik
using Lab10.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Namespace
{
    public class OtherPageModel : PageModel
    {
        public List<Room> listrooms = new List<Room>();
        public void OnGet(){
            try{

            
            string connectionString ="Server=.\\SQLEXPRESS;Database=Lab10;Trusted_Connection=True;TrustServerCertificate=True";
            using(SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();
                String sql ="SELECT * FROM rooms";
                using(SqlCommand command = new SqlCommand(sql, connection)){
                    using(SqlDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            Room rooms = new Room();
                            rooms.RoomId = reader.GetInt32(0);
                            rooms.RoomName= reader.GetString(1);
                           
                            
                            rooms.Capacity=reader.GetInt32(2);
                            listrooms.Add(rooms);
                        }
                    }

                }
            }
            }
            catch(Exception ex){
                Console.WriteLine("EXCEPTION"+ex.ToString());
            }
        }
    }
}