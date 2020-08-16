using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
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
    /// Interaction logic for AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Window
    {
        private ViewModel vm;
        public AddNewClient(ViewModel vm)
        {
            this.vm = vm;
            InitializeComponent();
            DataContext = vm;
        }

        private async void MakeClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsFilledIn();

                Client toAdd = new Client();
                toAdd = new Client((ClientType)ClientType.SelectedItem, vm.GetDiscounts((ClientType)ClientType.SelectedItem), new Address(Street.Text, Town.Text, StreetNumber.Text), Name.Text, BtwNumber.Text, new List<ReservationsPerYear>());

                vm.AddClient(toAdd);

                Mouse.OverrideCursor = Cursors.Wait;
                await vm.SetupAsync("ReservationForm");
                Mouse.OverrideCursor = null;
                ReservationForm rf = new ReservationForm(vm);
                rf.Show();
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private void IsFilledIn()
        {
            if (string.IsNullOrWhiteSpace(Name.Text))
                throw new Exception("Geef aub een naam in");
            if (string.IsNullOrWhiteSpace(Town.Text))
                throw new Exception("Geef aub een gemeente naam in");
            if (string.IsNullOrWhiteSpace(Street.Text))
                throw new Exception("Geef aub een straat naam in");
            if (string.IsNullOrWhiteSpace(StreetNumber.Text))
                throw new Exception("Geef aub een straat nummer in");
        }

    }
}

