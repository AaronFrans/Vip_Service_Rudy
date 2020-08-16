using DataLayer.UoW;
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WpfViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ServiceManager sm = new ServiceManager(new UnitOfWork(new DataLayer.Func.ManagerContext("Test")));

        private List<Limousine> limousines;
        private ObservableCollection<LimousineView> limousineInfo;
        public ObservableCollection<LimousineView> LimousineInfo
        {
            get
            {
                return limousineInfo;
            }
            set
            {
                limousineInfo = value;
                RaisePropertyChanged("LimousineInfo");
            }
        }

        private List<Reservering> reservationsUnfiltered;
        private ObservableCollection<Reservering> reservations;
        public ObservableCollection<Reservering> Reservations
        {
            get
            {
                return reservations;
            }
            set
            {
                reservations = value;
                RaisePropertyChanged("Reservations");
            }
        }

        private List<Client> clientsUnfiltered;
        private ObservableCollection<Client> clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                RaisePropertyChanged("Clients");
            }
        }

        public void AddClient(Client toAdd)
        {
            sm.AddClient(toAdd);
            SelectedClient = toAdd;
        }

        public LimousineView SelectedLimousine { get; set; }
        public Client SelectedClient { get; set; }
        public string SelectedArangement { get; set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan? SelectedStartTime { get; set; }
        public TimeSpan? SelectedEndTime { get; set; }
        public int? SelectedExtraHours { get; set; }
        public Address SelectedBeginLocation { get; set; }
        public Address SelectedEndLocation { get; set; }
        public Reservering SelectedReservation { get; set; }

        public bool FilterByClient { get; set; }
        public bool FilterByDate { get; set; }

        public Array Types { get; set; } = Enum.GetValues(typeof(ClientType));
        public List<string> Arangements { get; set; } = new List<string>() { "Airport", "Business", "Nightlife", "Wedding", "Wellness" };
        public List<string> AllowedLocations { get; set; } = new List<string>() { "Antwerpen", "Brussel", "Charleroi", "Gent", "Hasselt" };
        public List<int> HoursInADay { get; set; } = new List<int>();
        public List<int> ExtraHours { get; set; } = new List<int>();
        public bool EndHourVis { get; set; }
        public bool ExtraHourVis { get; set; }
        public bool HourVis { get; set; } = false;
        public bool FistHourVis { get; set; }
        public bool NightHourVis { get; set; }
        public bool DayHourVis { get; set; }

        public List<ClientDiscount> GetDiscounts(ClientType type)
        {
            return sm.GetDiscountsForType(type);
        }
        public async Task SetupAsync(string toLoad)
        {
            switch (toLoad)
            {
                case "Limousines":
                    {
                        limousines = await Task.Run(() => sm.GetVehicles());
                        LimousineInfo = new ObservableCollection<LimousineView>();
                        await Task.Run(() => MakeLimousineInfo(limousines));
                    }
                    break;
                case "Clients":
                    {
                        clientsUnfiltered = await Task.Run(() => sm.GetClients());
                        Clients = new ObservableCollection<Client>(clientsUnfiltered);
                    } 
                    break;
                case "ReservationForm":
                    {
                        for (int i = 1; i <= 24; i++)
                        {
                            HoursInADay.Add(i);
                        }
                        await Task.Run(() => SetupForArangement());
                    }
                    break;
                case "ReservationDetails":
                    {
                        SetupHourVis();
                    }
                    break;
                case "Reservations":
                    {
                        reservationsUnfiltered = await Task.Run(() => sm.GetReservations());
                        Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered);
                    }
                    break;
            }
        }

        public void IsStartAllowed()
        {
            switch (SelectedArangement)
            {
                case "Wedding":
                    {
                        if(!Wedding.IsStartAllowed((TimeSpan)SelectedStartTime))
                            throw new Exception("Het geselcteerde arangement kan aleen een start uur hebben tussen 7u en 15u.");
                    }
                    break;
                case "Nightlife":
                    {
                        if (!Nightlife.IsStartAllowed((TimeSpan)SelectedStartTime))
                            throw new Exception("Het geselcteerde arangement kan aleen een start uur hebben tussen 7u en 15u.");
                    }
                    break;
                case "Wellness":
                    {
                        if (!Wellness.IsStartAllowed((TimeSpan)SelectedStartTime))
                            throw new Exception("Het geselcteerde arangement kan aleen een start uur hebben tussen 7u en 15u.");
                    }
                    break;
            }
        }

        private void SetupHourVis()
        {
            if (SelectedReservation.Details.Hours.Any(h => h.Type.Equals("Eerste uur")))
            {
                HourVis = true;
                FistHourVis = true;
            }
            else
            {
                FistHourVis = false;
            }
            if (SelectedReservation.Details.Hours.Any(h => h.Type.Equals("Dag uren")))
            {
                HourVis = true;
                DayHourVis = true;
            }
            else
            {
                DayHourVis = false;
            }
            if (SelectedReservation.Details.Hours.Any(h => h.Type.Equals("Nacht uren")))
            {
                HourVis = true;
                NightHourVis = true;
            }
            else
            {
                NightHourVis = false;
            }
            if (SelectedReservation.Details.Hours.Any(h => h.Type.Equals("Extra uren")))
            {
                HourVis = true;
                ExtraHourVis = true;
            }
            else
            {
                ExtraHourVis = false;
            }
        }

        public void BindAdress(Address startAddress, Address endAddress)
        {
            SelectedBeginLocation = startAddress;
            SelectedEndLocation = endAddress;
        }
        public void BindDates(DateTime startDate, DateTime endDate, int? extraHours)
        {
            SelectedStartTime = new TimeSpan(startDate.Hour, 0, 0);
            SelectedDate = new DateTime(startDate.Year, startDate.Month, startDate.Day).AddHours(startDate.Hour);
            if (EndHourVis == true)
            {
                SelectedEndTime = new TimeSpan((int)(endDate - startDate).TotalHours, 0, 0) + SelectedStartTime;
            }
            else if (ExtraHourVis == true)
            {
                    SelectedEndTime = new TimeSpan(startDate.Hour + GetExtraHour(), 0, 0);
            }
            SelectedExtraHours = extraHours;

        }
        private int GetExtraHour()
        {
            return SelectedArangement switch
            {
                "Wedding" => Wedding.Duration,
                "Nightlife" => Nightlife.Duration,
                _ => 0,
            };
        }
        private void SetupForArangement()
        {
            switch (SelectedArangement)
            {
                case "Airport":
                    {
                        EndHourVis = true;
                        ExtraHourVis = false;
                    }
                    break;
                case "Business":
                    {
                        EndHourVis = true;
                        ExtraHourVis = false;
                    }
                    break;
                case "Wedding":
                    {
                        EndHourVis = false;
                        ExtraHourVis = true;
                        for (int i = 0; i <= Arangement.MaxAmountOfHours - Wedding.Duration; i++)
                        {
                            ExtraHours.Add(i);
                        }
                    }
                    break;
                case "Nightlife":
                    {
                        EndHourVis = false;
                        ExtraHourVis = true;
                        for (int i = 0; i <= Arangement.MaxAmountOfHours - Nightlife.Duration; i++)
                        {
                            ExtraHours.Add(i);
                        }
                    }
                    break;
                case "Wellness":
                    {
                        EndHourVis = false;
                        ExtraHourVis = false;
                    }
                    break;
            }
        }

        private void MakeLimousineInfo(IEnumerable<Limousine> limousines)
        {
            var toAdd = new LimousineView();
            foreach (var item in limousines)
            {
                toAdd = new LimousineView();
                toAdd.Name = item.Name;
                toAdd.FirstHourPrice = item.FirstHourPrice;
                toAdd.NightLife = item.Arangements.Where(x => x.GetType() == typeof(Nightlife)).Select(a => ((a as Nightlife).Price as int?)).FirstOrDefault();
                toAdd.Wedding = item.Arangements.Where(x => x.GetType() == typeof(Wedding)).Select(a => ((a as Wedding).Price as int?)).FirstOrDefault();
                toAdd.Wellness = item.Arangements.Where(x => x.GetType() == typeof(Wellness)).Select(a => ((a as Wellness).Price as int?)).FirstOrDefault();

                LimousineInfo.Add(toAdd);
            }
        }
        public void FilterLimousines(string toSort)
        {
            if (!toSort.Equals("Zoek een limousine op naam"))
            {
                if (!string.IsNullOrWhiteSpace(toSort))
                {
                    List<LimousineView> newList = LimousineInfo.Where(c => c.Name.ToLower().Contains(toSort)).ToList();
                    LimousineInfo = new ObservableCollection<LimousineView>(newList);
                }
                else
                {
                    MakeLimousineInfo(limousines);
                }
            }
        }
        public bool HasArangement(string name, string type)
        {
            return limousines.Where(l => l.Name.Equals(name)).Single().HasArangement(type);
        }

        public void FilterClientsByName(string toSort)
        {
            if (!toSort.Equals("Zoek een klant op naam"))
            {
                if (!string.IsNullOrWhiteSpace(toSort))
                {
                    List<Client> newList = clientsUnfiltered.Where(c => c.Name.ToLower().Contains(toSort)).ToList();
                    Clients = new ObservableCollection<Client>(newList);
                }
                else
                {
                    Clients = new ObservableCollection<Client>(clientsUnfiltered);
                }
            }
        }
        public void FilterClientsByNumber(string toSort)
        {
            if (!toSort.Equals("Zoek een klant op klant nummer"))
            {
                if (!string.IsNullOrWhiteSpace(toSort))
                {
                    List<Client> newList = clientsUnfiltered.Where(c => c.ClientNumber.ToString().Equals(toSort)).ToList();
                    Clients = new ObservableCollection<Client>(newList);
                }
                else
                {
                    Clients = new ObservableCollection<Client>(clientsUnfiltered);
                }
            }

        }

        public void MakeReservation()
        {
            SelectedReservation = sm.HireVehicle(SelectedLimousine.Name, SelectedArangement, SelectedBeginLocation,
                                  SelectedClient.ClientNumber, DateTime.Now, SelectedEndLocation, SelectedDate, SelectedExtraHours, SelectedStartTime, SelectedEndTime);
        }
        public int GetDuration()
        {
            int toReturn = 0;
            foreach (var item in SelectedReservation.Details.Hours)
            {
                toReturn += item.NrOfHours;
            }
            return toReturn;
        }

        public void FilterReservations(string clientNumber, DateTime? date)
        {
            DateTime Date = DateTime.MinValue;
            if(date != null)
            {
                Date = (DateTime)date;
            }
            int ClientNumber = 0;
            if (FilterByClient)
            {
                ClientNumber = int.Parse(clientNumber);
            }
            if(FilterByClient && FilterByDate)
            {

                Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered.Where(r => r.Client.ClientNumber == ClientNumber & r.ReservationDate.Year == Date.Year
                & r.ReservationDate.Month == Date.Month & r.ReservationDate.Day == Date.Day));
            }
            else if(FilterByDate)
            {
                Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered.Where(r => r.ReservationDate.Year == Date.Year
               & r.ReservationDate.Month == Date.Month & r.ReservationDate.Day == Date.Day));
            }
            else if(FilterByClient)
            {
                Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered.Where(r => r.Client.ClientNumber == ClientNumber));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
