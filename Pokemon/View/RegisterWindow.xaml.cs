using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Pokemon.View
{
    public partial class RegisterWindow : Window
    {
        private string connectionString = @"Data Source=ACEREVO\SQLEXPRESS01;Initial Catalog=ExerciceMonster;Integrated Security=True";

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Vérifier que les mots de passe correspondent
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Vérification de l'existence du nom d'utilisateur
                    string checkQuery = "SELECT COUNT(*) FROM Login WHERE Username = @Username";
                    using (var checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        int userExists = (int)checkCmd.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show("This username is already taken. Please choose another.");
                            return;
                        }
                    }

                    // Si le nom d'utilisateur n'existe pas, procéder à l'inscription
                    string hashedPassword = HashPassword(password);
                    string query = "INSERT INTO Login (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");

                        // Ouvrir la fenêtre de connexion
                        var loginWindow = new LoginWindow();
                        loginWindow.Show();

                        this.Close(); // Fermer la fenêtre d'inscription
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Fonction pour hasher le mot de passe
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        // Méthode pour revenir à la fenêtre de connexion
        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();  // Fermer la fenêtre d'enregistrement
        }
    }
}
