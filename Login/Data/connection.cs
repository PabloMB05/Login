using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Windows.Forms;
using System.Data;
using Mysqlx.Prepare;
using System.Data.SqlClient;

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

            if (connMaster.State == ConnectionState.Open)
            {
                MessageBox.Show("conexion establecida");
            }
            else
            {
                MessageBox.Show("conexion fallida");
            }
        }
        public void closeConnection()
        {

        }
        public static void comprobarLogin(String nombre, String contrasena)
        {
            try
            {
                // Asegúrate de que la conexión esté abierta
                if (connMaster.State != ConnectionState.Open)
                {
                    openConnection();
                }

                // Consulta parametrizada para evitar inyección SQL
                string query = "SELECT * FROM usuario WHERE nombre = @nombre AND contrasena = @contrasena";

                using (MySqlCommand cmd = new MySqlCommand(query, connMaster))
                {
                    // Agregar parámetros
                    cmd.Parameters.Add(new MySqlParameter("@nombre", MySqlDbType.VarChar) { Value = nombre });
                    cmd.Parameters.Add(new MySqlParameter("@contrasena", MySqlDbType.VarChar) { Value = contrasena });

                    // Ejecutar consulta
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Redirigir a la siguiente ventana
                            Interfaz interfaz = new Interfaz();
                            interfaz.Show();
                            // Credenciales válidas
                            MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);



                            // Cerrar ventana de inicio de sesión si corresponde
                            //Application.OpenForms["Form1"].Close();

                        }
                        else
                        {
                            // Credenciales inválidas
                            MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar el inicio de sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión si ya no se necesita
                if (connMaster.State == ConnectionState.Open)
                {
                    connMaster.Close();
                }
            }
        }
    }
}
 
