using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ReservationDetails.xaml
    /// </summary>
    public partial class ReservationDetails : Window
    {
        ViewModel vm;
        public ReservationDetails(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
            vm.Setup("ReservationConfirmation");
            SetupHourPanels();
        }

        private void SetupHourPanels()
        {
            SetupHourPanelVis();
        }


        private void SetupHourPanelVis()
        {
            if (vm.HourVis)

                HourPanel.Visibility = Visibility.Visible;

            else
                HourPanel.Visibility = Visibility.Collapsed;
            if (vm.FistHourVis)
            {
                FirstHourPanel.Visibility = Visibility.Visible;
                FirstHourDur.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Eerste uur")).NrOfHours.ToString();
                FirstHourPrice.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Eerste uur")).TotalPrice.ToString();
            }
            else
                FirstHourPanel.Visibility = Visibility.Collapsed;
            if (vm.DayHourVis)
            {
                DayHourPanel.Visibility = Visibility.Visible;
                DayHourDur.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Dag uren")).NrOfHours.ToString();
                DayHourPrice.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Dag uren")).TotalPrice.ToString();
            }
            else
                DayHourPanel.Visibility = Visibility.Collapsed;
            if (vm.NightHourVis)
            {
                NightHourPanel.Visibility = Visibility.Visible;
                NightHourDur.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Nacht uren")).NrOfHours.ToString();
                NightHourPrice.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Nacht uren")).TotalPrice.ToString();
            }
            else
                NightHourPanel.Visibility = Visibility.Collapsed;
            if (vm.ExtraHourVis)
            {
                ExtraHourPanel.Visibility = Visibility.Visible;
                ExtraHourDur.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Extra uren")).NrOfHours.ToString();
                ExtraHourPrice.Text = vm.SelectedReservation.Details.Hours.Single(h => h.Type.Equals("Extra uren")).TotalPrice.ToString();
            }
            else
                ExtraHourPanel.Visibility = Visibility.Collapsed;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
