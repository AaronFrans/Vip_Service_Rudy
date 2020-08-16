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
    /// Interaction logic for SelectReservation.xaml
    /// </summary>
    public partial class SelectReservation : Window
    {
        private ViewModel vm;

        public SelectReservation(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
            SetupPanels();
        }

        private void SetupPanels()
        {
            if (vm.FilterByClient)
            {
                ClientPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ClientPanel.Visibility = Visibility.Collapsed;
            }
            if (vm.FilterByDate)
            {
                DatePanel.Visibility = Visibility.Visible;
            }
            else
            {
                DatePanel.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsFilled();
                vm.FilterReservations(ClientInput.Text, DateInput.SelectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void IsFilled()
        {
            if (vm.FilterByClient)
            {
                if (!int.TryParse(ClientInput.Text, out int clientNummer))
                    throw new Exception("Een klantenNummer moet een geheel getal zijn");
            }
            if (vm.FilterByDate)
            {
                if (DateInput.SelectedDate == null)
                    throw new Exception("Selecteer aub een datum om op te zoeken");
            }
        }

        private void SelectTheReservation_Click(object sender, RoutedEventArgs e)
        {
            ReservationDetails rd = new ReservationDetails(vm);
            rd.Show();
            Close();
        }
    }
}
