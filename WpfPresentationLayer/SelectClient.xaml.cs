using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfViewModel;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for SelectClient.xaml
    /// </summary>
    public partial class SelectClient : Window
    {
        private ViewModel vm = new ViewModel();
        public SelectClient()
        {
            InitializeComponent();
            vm.AddItems();
            DataContext = vm;
        }

        private void ClientNumberFilter_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Zoek een klant op klant nummer")
                txtBox.Text = string.Empty;
        }
        private void ClientNameFilter_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Zoek een klant op naam")
                txtBox.Text = string.Empty;
        }

        private void NameFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string toSearch = ClientNameFilter.Text;
            vm.FilterClientsByName(toSearch);
        }
        private void NumberFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string toSearch = ClientNumberFilter.Text;
            vm.FilterClientsByNumber(toSearch);
        }

        private void SelectClientButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
