using ProductManager.DataAccess;
using ProductManager.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SignInBtnClick(object sender, RoutedEventArgs e)
        {
            var user = await GetUserAsync(loginTB.Text, passTB.Password);
            if (user is null)
            {
                MessageBox.Show("Некорректные данные.");
                passTB.Password = string.Empty;
                return;
            }

            var window = new EditorWindow();
            window.Show();
            this.Close();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SignInBtnClick(sender, e);
            }
        }

        private async Task<User> GetUserAsync(string login, string password)
        {
            using (var context = new PmContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            }
        }
    }
}
