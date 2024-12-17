using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Windows.Forms;
using System.Data;

namespace Login.Data
{
    internal class connection
    {
        public static String server = "127.0.0.1";
        public static String database = "login";
        public static String user = "root";
        public static String password = "root";
        //realizamos la conecion
        public static MySqlConnection connMaster = new MySqlConnection();


        public static void openConnection()
        {

        String connectionString = $"server={server}; database={database} ;user={user}; password={password}";
            connMaster = new MySqlConnection(connectionString);
            connMaster.Open();

            if(connMaster.State == ConnectionState.Open)
            {
                MessageBox.Show("conexion establecida");
            }
            else
            {
                MessageBox.Show("conexion fallida");
            }
    }
        public void closeConnection() {
        
        }
        public void comprobarLogin()
        {

        }
    }
}
