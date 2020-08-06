using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
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
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        ViewModel vm;
        public ReservationForm(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
            vm.Setup("ReservationForm");
            SetupVisibilities();
        }

        private void SetupVisibilities()
        {
            if (vm.EndHourVis == true)
            {
                EndTimePanel.Visibility = Visibility.Visible;
            }
            else
            {
                EndTimePanel.Visibility = Visibility.Collapsed;
            }
            if (vm.ExtraHourVis == true)
            {
                ExtraHoursPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ExtraHoursPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsFilledIn();
                BindTimeInfo();
                BindAddressInfo();
                ReserveringConfirmation rc = new ReserveringConfirmation(vm);
                rc.Show();
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void IsFilledIn()
        {
            if (StartTimeDate.SelectedDate == null)
                throw new Exception("Kies aub een start datum.");
            if (EndTimePanel.Visibility == Visibility.Visible)
            {
                if (EndTimeDate.SelectedDate == null)
                    throw new Exception("Kies aub een eind datum.");
            }
            if (string.IsNullOrWhiteSpace(StartStreet.Text))
                throw new Exception("Vul de straat van de start locatie in.");
            if (string.IsNullOrWhiteSpace(StartStreetNumber.Text))
                throw new Exception("Vul het straat nummer van de start locatie in.");
            if (string.IsNullOrWhiteSpace(EndStreet.Text))
                throw new Exception("Vul de straat van de eind locatie in.");
            if (string.IsNullOrWhiteSpace(EndStreetNumber.Text))
                throw new Exception("Vul de straat van de eind locatie in.");
        }

        private void BindAddressInfo()
        {

            Address startAddress = new Address(StartStreet.Text, (string)StartTown.SelectedItem, StartStreetNumber.Text);
            Address endAddress = new Address(EndStreet.Text, (string)StartTown.SelectedItem, EndStreetNumber.Text);

            vm.BindAdress(startAddress, endAddress);
        }
        private void BindTimeInfo()
        {
            DateTime StartTime = DateTime.MinValue;
            DateTime EndTime = DateTime.MinValue;
            int? ExtraHour = null;

            StartTime = (DateTime)StartTimeDate.SelectedDate;
            StartTime = StartTime.AddHours((int)StartTimeBox.SelectedItem);

            if (EndTimePanel.Visibility == Visibility.Visible)
            {

                EndTime = (DateTime)EndTimeDate.SelectedDate;
                EndTime = EndTime.AddHours((int)EndTimeBox.SelectedItem);
                if (StartTime > EndTime)
                    throw new Exception("De starttijd mag niet eerder zijn dan de eindtijd komen.");

            }
            else if (ExtraHoursPanel.Visibility == Visibility.Visible)
            {
                if ((int)ExtraHoursBox.SelectedItem != 0)
                {
                    ExtraHour = (int)ExtraHoursBox.SelectedItem;
                }
                
            }


            if ((EndTime - StartTime).TotalHours > 11)
            {
                throw new Exception("Een arangement mag maximum 11 uur duren.");
            }

            vm.BindDates(StartTime, EndTime, ExtraHour);

            vm.IsStartAllowed();

        }


    }
}
