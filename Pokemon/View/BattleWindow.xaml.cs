using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using Pokemon.Model;

namespace Pokemon.View
{
    public partial class BattleWindow : Window
    {
        private Monster playerMonster;
        private Monster opponentMonster;
        private List<Spell> playerSpells;
        private List<Spell> opponentSpells;

        public BattleWindow(Monster playerMonster, Monster opponentMonster)
        {
            InitializeComponent();
            this.playerMonster = playerMonster;
            this.opponentMonster = opponentMonster;

            // Afficher les noms et la santé des monstres
            PlayerMonsterNameLabel.Content = playerMonster.Name;
            OpponentMonsterNameLabel.Content = opponentMonster.Name;
            PlayerMonsterHealthLabel.Content = $"Health: {playerMonster.Health}";
            OpponentMonsterHealthLabel.Content = $"Health: {opponentMonster.Health}";

            // Afficher les images des monstres
            if (!string.IsNullOrEmpty(playerMonster.ImageUrl))
            {
                PlayerMonsterImage.Source = new BitmapImage(new Uri(playerMonster.ImageUrl, UriKind.Absolute));
            }
            if (!string.IsNullOrEmpty(opponentMonster.ImageUrl))
            {
                OpponentMonsterImage.Source = new BitmapImage(new Uri(opponentMonster.ImageUrl, UriKind.Absolute));
            }

            // Charger les sorts du joueur
            playerSpells = playerMonster.Spells.ToList();
            opponentSpells = opponentMonster.Spells.ToList();
            PlayerSpellComboBox.ItemsSource = playerSpells;
            PlayerSpellComboBox.DisplayMemberPath = "Name";  // Affiche les noms des sorts
        }

        // Méthode pour mettre à jour les HP
        private void UpdateHealthBars(int playerHP, int opponentHP)
        {
            // Mise à jour de la barre de HP du joueur
            PlayerMonsterHealthBar.Value = playerHP;
            PlayerMonsterHealthLabel.Content = $"Health: {playerHP}";

            // Mise à jour de la barre de HP de l'adversaire
            OpponentMonsterHealthBar.Value = opponentHP;
            OpponentMonsterHealthLabel.Content = $"Health: {opponentHP}";
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerSpellComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a spell to attack.");
                return;
            }

            // Récupérer le sort sélectionné par le joueur
            var selectedSpell = (Spell)PlayerSpellComboBox.SelectedItem;

            // Attaquer l'adversaire
            opponentMonster.Health -= selectedSpell.Damage;
            OpponentMonsterHealthLabel.Content = $"Health: {opponentMonster.Health}";

            // Vérifier si l'adversaire est vaincu
            if (opponentMonster.Health <= 0)
            {
                ShowEndBattlePopup($"{playerMonster.Name} defeated {opponentMonster.Name}!", "Battle Won");
                return;
            }

            // Attaque de l'adversaire
            var random = new Random();
            var opponentSpell = opponentSpells[random.Next(opponentSpells.Count)];
            playerMonster.Health -= opponentSpell.Damage;
            PlayerMonsterHealthLabel.Content = $"Health: {playerMonster.Health}";

            // Mettre à jour les barres et labels de HP
            UpdateHealthBars(playerMonster.Health, opponentMonster.Health);

            // Vérifier si le joueur est vaincu
            if (playerMonster.Health <= 0)
            {
                ShowEndBattlePopup($"{opponentMonster.Name} defeated {playerMonster.Name}!", "Battle Lost");
                return;
            }

            // Mettre à jour l'état de la bataille
            BattleStatusLabel.Content = $"{playerMonster.Name} used {selectedSpell.Name}! {opponentMonster.Name} used {opponentSpell.Name}!";
        }

        private void ShowEndBattlePopup(string message, string title)
        {
            var endBattleWindow = new EndBattleWindow(message);
            endBattleWindow.ShowDialog();

            if (endBattleWindow.RestartBattle)
            {
                RestartBattle();
            }
            else
            {
                this.Close();
            }
        }

        private void RestartBattle()
        {
            // Ouvrir la fenêtre de sélection de monstres
            var monsterSelectionWindow = new MonsterSelectionWindow();
            monsterSelectionWindow.Show();

            // Fermer la fenêtre actuelle
            this.Close();

        }
    }
}
