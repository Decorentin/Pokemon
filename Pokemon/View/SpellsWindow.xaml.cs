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
using Pokemon.Model;

namespace Pokemon.View
{
    public partial class SpellsWindow : Window
    {
        private List<Spell> allSpells; // Liste des sorts dans le jeu

        public SpellsWindow(List<Spell> spells)
        {
            InitializeComponent();
            SpellsListBox.ItemsSource = spells;
        }

        private void SpellsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSpell = (Spell)SpellsListBox.SelectedItem;
            if (selectedSpell != null)
            {
                SpellNameLabel.Text = $"Name: {selectedSpell.Name}";
                SpellDamageLabel.Text = $"Damage: {selectedSpell.Damage}";
                SpellDescriptionLabel.Text = $"Description: {selectedSpell.Description}";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la fenêtre des sorts
            this.Close();
        }
    }
}
