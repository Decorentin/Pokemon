using Pokemon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pokemon.View
{
    public partial class MonsterSelectionWindow : Window
    {
        private List<Monster> availableMonsters;
        private List<Spell> allSpells; // Liste des sorts dans le jeu

        public MonsterSelectionWindow()
        {
            InitializeComponent();
            LoadMonsters();
        }

        private void LoadMonsters()
        {
            // Charger les monstres avec leurs sorts depuis la base de données
            availableMonsters = GetMonstersWithSpells();

            // Lier les monstres aux ListBox
            YourMonsterListBox.ItemsSource = availableMonsters;
            OpponentMonsterListBox.ItemsSource = availableMonsters;
        }

        private List<Monster> GetMonstersWithSpells()
        {
            List<Monster> monsters = new List<Monster>();

            try
            {
                using (var connection = new SqlConnection(@"Data Source=ACEREVO\SQLEXPRESS01;Initial Catalog=ExerciceMonster;Integrated Security=True"))
                {
                    connection.Open();
                    string query = @"SELECT m.Id, m.Name, m.Health, m.ImageUrl, s.Name AS SpellName, s.Damage, s.Description 
                                     FROM Monster m
                                     LEFT JOIN MonsterSpell ms ON m.Id = ms.MonsterID
                                     LEFT JOIN Spell s ON ms.SpellID = s.Id";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int monsterId = reader.GetInt32(0);
                            Monster monster = monsters.Find(m => m.Id == monsterId);

                            if (monster == null)
                            {
                                monster = new Monster
                                {
                                    Id = monsterId,
                                    Name = reader.GetString(1),
                                    Health = reader.GetInt32(2),
                                    ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Spells = new List<Spell>()
                                };
                                monsters.Add(monster);
                            }

                            // Ajouter les sorts pour chaque monstre
                            if (!reader.IsDBNull(4))
                            {
                                Spell spell = new Spell
                                {
                                    Name = reader.GetString(4),
                                    Damage = reader.GetInt32(5),
                                    Description = reader.GetString(6)
                                };
                                monster.Spells.Add(spell);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return monsters;
        }

        private void StartBattleButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les monstres sélectionnés
            var selectedYourMonster = (Monster)YourMonsterListBox.SelectedItem;
            var selectedOpponentMonster = (Monster)OpponentMonsterListBox.SelectedItem;

            if (selectedYourMonster == null || selectedOpponentMonster == null)
            {
                MessageBox.Show("Please select both a monster to play and an opponent!");
                return;
            }

            // Ouvrir la fenêtre de combat avec les monstres sélectionnés
            var battleWindow = new BattleWindow(selectedYourMonster, selectedOpponentMonster);
            battleWindow.Show();
            this.Close();
        }

        private void YourMonsterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMonster = (Monster)YourMonsterListBox.SelectedItem;

            if (selectedMonster != null)
            {
                // Mettre à jour les détails
                YourMonsterDetailsTextBlock.Text = $"Name: {selectedMonster.Name}\nHealth: {selectedMonster.Health}\nSpells: {string.Join(", ", selectedMonster.Spells.Select(s => s.Name))}";

                // Mettre à jour l'image
                if (!string.IsNullOrEmpty(selectedMonster.ImageUrl))
                {
                    YourMonsterImage.Source = new BitmapImage(new Uri(selectedMonster.ImageUrl, UriKind.Absolute));
                }
                else
                {
                    YourMonsterImage.Source = null; // Aucune image disponible
                }
            }
        }

        private void OpponentMonsterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMonster = (Monster)OpponentMonsterListBox.SelectedItem;

            if (selectedMonster != null)
            {
                // Mettre à jour les détails
                OpponentMonsterDetailsTextBlock.Text = $"Name: {selectedMonster.Name}\nHealth: {selectedMonster.Health}\nSpells: {string.Join(", ", selectedMonster.Spells.Select(s => s.Name))}";

                // Mettre à jour l'image
                if (!string.IsNullOrEmpty(selectedMonster.ImageUrl))
                {
                    OpponentMonsterImage.Source = new BitmapImage(new Uri(selectedMonster.ImageUrl, UriKind.Absolute));
                }
                else
                {
                    OpponentMonsterImage.Source = null; // Aucune image disponible
                }
            }
        }


        private string GetMonsterDetails(Monster monster)
        {
            var details = new StringBuilder();
            details.AppendLine($"Name: {monster.Name}");
            details.AppendLine($"Health: {monster.Health}");
            if (monster.Spells.Any())
            {
                details.AppendLine("Spells:");
                foreach (var spell in monster.Spells)
                {
                    details.AppendLine($"- {spell.Name} (Damage: {spell.Damage})");
                    details.AppendLine($"  Description: {spell.Description}");
                }
            }
            else
            {
                details.AppendLine("No spells available.");
            }
            return details.ToString();
        }
        private void ViewSpellsButton_Click(object sender, RoutedEventArgs e)
        {
            // Extraire tous les sorts de la liste des monstres disponibles
            var allSpells = availableMonsters.SelectMany(m => m.Spells).Distinct().ToList();

            // Ouvrir la fenêtre des sorts avec la liste des sorts uniques
            var spellsWindow = new SpellsWindow(allSpells);
            spellsWindow.Show();
        }

    }
}
