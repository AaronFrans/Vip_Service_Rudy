using DomainLayer.Domain.Arangements;
using System;
using System.Collections.Generic;
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
using WpfViewModel;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SelectLimousine : Window
    {
        private ViewModel vm = new ViewModel();


        public SelectLimousine()
        {
            InitializeComponent();
            vm.AddItems();
            DataContext = vm;
        }

        private void LimousineFilter_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Zoek een limousine op naam")
                txtBox.Text = string.Empty;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string toSearch = LimousineFilter.Text;
            vm.FilterLimousines(toSearch);
        }

        private void NewClientButton_Click(object sender, RoutedEventArgs e)
        {
            if(vm.SelectedLimousine != null)
            FilterButton.Content = vm.SelectedLimousine.Name;
        }

        private void ExistingClientButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
