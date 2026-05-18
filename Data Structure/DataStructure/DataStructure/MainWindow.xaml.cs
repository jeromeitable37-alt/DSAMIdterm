using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataStructure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // AVAILABLE ITEMS
        ObservableCollection<Shopping> shopping =
            new ObservableCollection<Shopping>();

        // SHOPPING CART
        ObservableCollection<Shopping> shoppings =
            new ObservableCollection<Shopping>();

        public MainWindow()
        {
            InitializeComponent();

            // Available items grid
            ShoppingGrid.ItemsSource = shopping;

            // Cart grid
            AddtoCart.ItemsSource = shoppings;
        }

        // REMOVE ITEM
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Shopping selectedShopping = ShoppingGrid.SelectedItem as Shopping;

            if (selectedShopping != null)
            {
                shopping.Remove(selectedShopping);

                ShoppingGrid.Items.Refresh();

                MessageBox.Show("Item Removed Successfully!");

                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select an item first.");
            }
        }

        // GRID SELECTION
        private void ShoppingGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ShoppingGrid.SelectedItem is Shopping selectedShopping)
                {
                    txtId.Text = selectedShopping.Id;
                    txtName.Text = selectedShopping.Name;
                    txtDescription.Text = selectedShopping.Description;
                    txtPrice.Text = selectedShopping.Price;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Shopping System!\n" + ex.Message);
            }
        }

        // SHOPPING CLASS
        public class Shopping
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }

        // ADD ITEM
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtDescription.Text) ||
                    string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("Please fill in all fields.",
                                    "Missing Input",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                shopping.Add(new Shopping
                {
                    Id = txtId.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Price = txtPrice.Text.Trim()
                });

                MessageBox.Show("Item Added Successfully!",
                                "Success",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Adding Item!\n" + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        // CLEAR METHOD
        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();

            ShoppingGrid.SelectedItem = null;
        }

        // CLEAR BUTTON
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearFields();

                MessageBox.Show("Fields Cleared!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Clearing Fields!\n" + ex.Message);
            }
        }

        // ADD TO CART
        private void btnaddtoCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // GET SELECTED ITEM FROM AVAILABLE ITEMS GRID
                Shopping selectedShopping =
                    ShoppingGrid.SelectedItem as Shopping;

                if (selectedShopping != null)
                {
                    // ADD ITEM TO CART COLLECTION
                    shoppings.Add(new Shopping
                    {
                        Id = selectedShopping.Id,
                        Name = selectedShopping.Name,
                        Description = selectedShopping.Description,
                        Price = selectedShopping.Price
                    });

                    // REFRESH CART GRID
                    AddtoCart.Items.Refresh();

                    MessageBox.Show("Item Added To Cart!");
                }
                else
                {
                    MessageBox.Show("Please select an item first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Adding To Cart!\n" + ex.Message);
            }
        }
    }
}