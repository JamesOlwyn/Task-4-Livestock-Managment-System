using System.Windows;

namespace Task4Menu.UI
{
    public partial class SplashPage : Window
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        private void GoToInventory(object sender, RoutedEventArgs e)
        {
            Inventory inventoryPage = new Inventory();
            inventoryPage.Show();
            this.Close();
        }

        private void GoToStatistics(object sender, RoutedEventArgs e)
        {
            Statistics statisticsPage = new Statistics();
            statisticsPage.Show();
            this.Close();
        }

        private void GoToManagement(object sender, RoutedEventArgs e)
        {
            Management managementPage = new Management();
            managementPage.Show();
            this.Close();
        }
    }
}
