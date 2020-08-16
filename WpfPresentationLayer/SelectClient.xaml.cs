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
        private ViewModel vm;
        public SelectClient(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
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

        private async void SelectClientButton_Click(object sender, RoutedEventArgs e)
        {
            if(vm.SelectedClient !=null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                await vm.SetupAsync("ReservationForm");
                Mouse.OverrideCursor = null;
                ReservationForm rf = new ReservationForm(vm);
                rf.Show();
                Close();

            }
            else
            {
                MessageBox.Show("Selecteer aub een klant.");
            }
        }
    }
}
