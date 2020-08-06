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
    /// Interaction logic for ReserveringConfirmation.xaml
    /// </summary>
    public partial class ReserveringConfirmation : Window
    {
        private ViewModel vm;

        public ReserveringConfirmation(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            vm.MakeReservation();
            ReservationDetails rd = new ReservationDetails(vm);
            rd.Show();
            Close();
        }

        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {
            SelectLimousine sl = new SelectLimousine();
            sl.Show();
            Close();
        }
    }
}
