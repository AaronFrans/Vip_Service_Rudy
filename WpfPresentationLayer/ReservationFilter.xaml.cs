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
    /// Interaction logic for ReservationFilter.xaml
    /// </summary>
    public partial class ReservationFilter : Window
    {
        ViewModel vm;
        public ReservationFilter(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void ShowReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            if(vm.FilterByClient || vm.FilterByDate)
            {

            }
            else
            {
                MessageBox.Show("Selecteer aub minimum een van de keuzes.");
            }
        }
    }
}
