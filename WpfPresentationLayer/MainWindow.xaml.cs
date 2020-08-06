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
    /// Interaction logic for Main_Window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
        }

        private void SearchReservationButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationFilter rf = new ReservationFilter(vm);
            rf.Show();
            Close();
        }
    }
}
