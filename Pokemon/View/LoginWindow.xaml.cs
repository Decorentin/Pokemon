using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Pokemon.View
{
    public partial class LoginWindow : Window
    {
        private string connectionString = @"Data Source=ACEREVO\SQLEXPRESS01;Initial Catalog=ExerciceMonster;Integrated Security=True";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT PasswordHash FROM Login WHERE Username = @Username";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        var result = cmd.ExecuteScalar();
                        if (result != null && VerifyPassword(password, result.ToString()))
                        {
                            // Ouvrir la fenêtre principale ici
                            var MonsterSelectionWindow = new MonsterSelectionWindow();
                            MonsterSelectionWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid credentials.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return hash == storedHash;
            }
        }

        private void OpenRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
