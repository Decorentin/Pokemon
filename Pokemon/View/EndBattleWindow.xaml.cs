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

namespace Pokemon.View
{
    public partial class EndBattleWindow : Window
    {
        public bool RestartBattle { get; private set; } = false;

        public EndBattleWindow(string message)
        {
            InitializeComponent();
            BattleResultLabel.Content = message;
        }

        private void RestartBattleButton_Click(object sender, RoutedEventArgs e)
        {
            RestartBattle = true;
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
