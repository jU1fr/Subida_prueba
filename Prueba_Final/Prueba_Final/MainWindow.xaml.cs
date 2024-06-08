using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Prueba_Final
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                string query = "SELECT * FROM Alumnos";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvAlumnos.ItemsSource = dataTable.DefaultView;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = ConexionBD.GetConnection())
            {
                string query = "INSERT INTO Alumnos (Nombre, Apellido, Edad, Carrera) VALUES (@Nombre, @Apellido, @Edad, @Carrera)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                command.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                command.Parameters.AddWithValue("@Edad", int.Parse(txtEdad.Text));
                command.Parameters.AddWithValue("@Carrera", txtCarrera.Text);
                command.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvAlumnos.SelectedItem != null)
            {
                DataRowView row = dgvAlumnos.SelectedItem as DataRowView;

                using (SqlConnection connection = ConexionBD.GetConnection())
                {
                    string query = "UPDATE Alumnos SET Nombre = @Nombre, Apellido = @Apellido, Edad = @Edad, Carrera = @Carrera WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                    command.Parameters.AddWithValue("@Edad", int.Parse(txtEdad.Text));
                    command.Parameters.AddWithValue("@Carrera", txtCarrera.Text);
                    command.Parameters.AddWithValue("@Id", row["Id"]);
                    command.ExecuteNonQuery();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Seleccione un alumno para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvAlumnos.SelectedItem != null)
            {
                DataRowView row = dgvAlumnos.SelectedItem as DataRowView;

                using (SqlConnection connection = ConexionBD.GetConnection())
                {
                    string query = "DELETE FROM Alumnos WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", row["Id"]);
                    command.ExecuteNonQuery();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Seleccione un alumno para eliminar.");
            }
        }
    }
}
