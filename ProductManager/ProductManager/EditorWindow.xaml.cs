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
using System.Windows.Shapes;

namespace ProductManager
{
    /// <summary>
    /// Логика взаимодействия для EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private List<Product> products = new List<Product>();

        public EditorWindow()
        {
            InitializeComponent();
            UpdProducts();
        }

        private async void UpdProducts()
        {
            products = await GetProductsAsync();

            productsDG.ItemsSource = null;
            productsDG.ItemsSource = products;
        }

        private async Task<List<Product>> GetProductsAsync()
        {
            using (var context = new PmContext())
            {
                return await context.Products.ToListAsync();
            }
        }

        private async Task<bool> SaveProductsAsync()
        {
            try
            {
                using (var context = new PmContext())
                {
                    foreach (var product in products)
                    {
                        var productDb = await context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
                        if (productDb is null)
                        {
                            context.Products.Add(product);
                        }
                        else
                        {
                            productDb.Name = product.Name;
                            productDb.Price = product.Price;
                            productDb.CreationDate = product.CreationDate;
                        }
                    }
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteProductAsync(Product product)
        {
            try
            {
                using (var context = new PmContext())
                {
                    var productDb = await context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
                    context.Products.Remove(productDb);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private async void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            var flag = await SaveProductsAsync();
            if (flag) MessageBox.Show("Успешно!");
            UpdProducts();
        }

        private async void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            var selectedProduct = productsDG.SelectedItem as Product;
            var flag = await DeleteProductAsync(selectedProduct);
            if (flag) MessageBox.Show("Успешно!");
            UpdProducts();
        }
    }
}
