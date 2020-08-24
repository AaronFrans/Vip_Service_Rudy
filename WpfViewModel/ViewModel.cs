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
        /// <summary>
        /// The service manager to acces the database.
        /// </summary>
        public ServiceManager sm = new ServiceManager(new UnitOfWork(new DataLayer.Func.ManagerContext("Production")));

        /// <summary>
        /// Original limousines in the database.
        /// </summary>
        private List<Limousine> limousines;
        /// <summary>
        /// A collection with objects that have info about a limousine. They may also have been filtered.
        /// </summary>
        private ObservableCollection<LimousineView> limousineInfo;
        /// <summary>
        /// A collection with objects that have info about a limousine. They may also have been filtered.
        /// </summary>
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

        /// <summary>
        /// Original reservations in the database. 
        /// </summary>
        private List<Reservering> reservationsUnfiltered;
        /// <summary>
        /// A collection of reservations that may also have been filtered.
        /// </summary>
        private ObservableCollection<Reservering> reservations;
        /// <summary>
        /// A collection of reservations that may also have been filtered.
        /// </summary>
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

        /// <summary>
        /// Original clients in the database. 
        /// </summary>
        private List<Client> clientsUnfiltered;
        /// <summary>
        /// A collection of clients that may also have been filtered.
        /// </summary>
        private ObservableCollection<Client> clients;
        /// <summary>
        /// A collection of clients that may also have been filtered.
        /// </summary>
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

        /// <summary>
        /// Add a new client to the database.
        /// </summary>
        /// <param name="toAdd">Client to add to the database.</param>
        public void AddClient(Client toAdd)
        {
            sm.AddClient(toAdd);
            SelectedClient = toAdd;
        }

        /// <summary>
        /// Limousine selected for reservation.
        /// </summary>
        public LimousineView SelectedLimousine { get; set; }
        /// <summary>
        /// Client selected for reservation.
        /// </summary>
        public Client SelectedClient { get; set; }
        /// <summary>
        /// Arangement selected for reservation.
        /// </summary>
        public string SelectedArangement { get; set; }
        /// <summary>
        /// Date selected for reservation.
        /// </summary>
        public DateTime SelectedDate { get; set; }
        /// <summary>
        /// Start time selected for reservation.
        /// </summary>
        public TimeSpan? SelectedStartTime { get; set; }
        /// <summary>
        /// End time selected for reservation.
        /// </summary>       
        public TimeSpan? SelectedEndTime { get; set; }
        /// <summary>
        /// Extra hours selected for reservation.
        /// </summary>
        public int? SelectedExtraHours { get; set; }
        /// <summary>
        /// Extra hours selected for reservation.
        /// </summary>
        public Address SelectedBeginLocation { get; set; }
        /// <summary>
        /// Begin location selected for reservation.
        /// </summary>
        public Address SelectedEndLocation { get; set; }
        /// <summary>
        /// Reservation selected to show.
        /// </summary>
        public Reservering SelectedReservation { get; set; }

        /// <summary>
        /// Whether you should be able to filter by client.
        /// </summary>
        public bool FilterByClient { get; set; }
        /// <summary>
        /// Whether you should be able to filter by date.
        /// </summary>
        public bool FilterByDate { get; set; }

        /// <summary>
        /// Possible client types.
        /// </summary>
        public Array Types { get; set; } = Enum.GetValues(typeof(ClientType));
        /// <summary>
        /// Possible arangement types.
        /// </summary>
        public List<string> Arangements { get; set; } = new List<string>() { "Airport", "Business", "Nightlife", "Wedding", "Wellness" };
        /// <summary>
        /// Allowed towns.
        /// </summary>
        public List<string> AllowedLocations { get; set; } = new List<string>() { "Antwerpen", "Brussel", "Charleroi", "Gent", "Hasselt" };
        /// <summary>
        /// All hours in a day.
        /// </summary>
        public List<int> HoursInADay { get; set; } = new List<int>();
        /// <summary>
        /// Possible Extra hours.
        /// </summary>
        public List<int> ExtraHours { get; set; } = new List<int>();
        /// <summary>
        /// Whether end hours should be visible.
        /// </summary>
        public bool EndHourVis { get; set; }
        /// <summary>
        /// Whether extra hours should be visible.
        /// </summary>
        public bool ExtraHourVis { get; set; }
        /// <summary>
        /// Whether hour types should be visible.
        /// </summary>
        public bool HourVis { get; set; } = false;
        /// <summary>
        /// Whether first hour should be visible.
        /// </summary>
        public bool FistHourVis { get; set; }
        /// <summary>
        /// Whether night hours should be visible.
        /// </summary>
        public bool NightHourVis { get; set; }
        /// <summary>
        /// Whether day hours should be visible.
        /// </summary>
        public bool DayHourVis { get; set; }

        /// <summary>
        /// Get discounts for client type.
        /// </summary>
        /// <param name="type">Type opf client to get discounts for.</param>
        /// <returns>A list of ClientDiscount objects</returns>
        public List<ClientDiscount> GetDiscounts(ClientType type)
        {
            return sm.GetDiscountsForType(type);
        }
        /// <summary>
        /// Loads items depending on given parameter.
        /// </summary>
        /// <param name="toLoad">Which items to load.</param>
        /// <returns>A task that loads the items.</returns>
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

        /// <summary>
        /// Checks whether a given start hour is allowed.
        /// </summary>
        public void IsStartAllowed()
        {
            switch (SelectedArangement)
            {
                case "Wedding":
                    {
                        if (!Wedding.IsStartAllowed((TimeSpan)SelectedStartTime))
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

        /// <summary>
        /// Sets up visibilities foer hour types.
        /// </summary>
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

        /// <summary>
        /// Binds the given addresses.
        /// </summary>
        /// <param name="startAddress">The start location.</param>
        /// <param name="endAddress">The end location.</param>
        public void BindAdress(Address startAddress, Address endAddress)
        {
            SelectedBeginLocation = startAddress;
            SelectedEndLocation = endAddress;
        }
        /// <summary>
        /// Binds the given dates.
        /// </summary>
        /// <param name="startDate">When reservation starts.</param>
        /// <param name="endDate">When reservation ends.</param>
        /// <param name="extraHours">The extra hours to add to the arangement (if applicable).</param>
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
                SelectedEndTime = new TimeSpan(startDate.Hour + GetDefaultDuration(), 0, 0);
            }
            SelectedExtraHours = extraHours;

        }
        /// <summary>
        /// Get the default duration depending on the selected arangement.
        /// </summary>
        /// <returns>An integer representing the default duration of the chosen arangement.</returns>
        private int GetDefaultDuration()
        {
            return SelectedArangement switch
            {
                "Wedding" => Wedding.Duration,
                "Nightlife" => Nightlife.Duration,
                _ => 0,
            };
        }
        /// <summary>
        /// Sets up visibilities for the selected arangement.
        /// </summary>
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

        /// <summary>
        /// Makes the LimousineView items from the given limousines.
        /// </summary>
        /// <param name="limousines">Limousines to transform.</param>
        private void MakeLimousineInfo(IEnumerable<Limousine> limousines)
        {
            var toAdd = new LimousineView();
            foreach (var item in limousines)
            {
                toAdd = new LimousineView();
                toAdd.Name = item.Name;
                toAdd.FirstHourPrice = item.FirstHourPrice;
                toAdd.Nightlife = item.Arangements.Where(x => x.GetType() == typeof(Nightlife)).Select(a => ((a as Nightlife).Price as int?)).FirstOrDefault();
                toAdd.Wedding = item.Arangements.Where(x => x.GetType() == typeof(Wedding)).Select(a => ((a as Wedding).Price as int?)).FirstOrDefault();
                toAdd.Wellness = item.Arangements.Where(x => x.GetType() == typeof(Wellness)).Select(a => ((a as Wellness).Price as int?)).FirstOrDefault();

                LimousineInfo.Add(toAdd);
            }
        }
        /// <summary>
        /// Filters limousines on given name.
        /// </summary>
        /// <param name="toSort">The name to filter by.</param>
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
        /// <summary>
        /// Checks if given limousine has given arangement type.
        /// </summary>
        /// <param name="name">Limousine to check.</param>
        /// <param name="type">Arangement to check.</param>
        /// <returns></returns>
        public bool HasArangement(string name, string type)
        {
            return limousines.Where(l => l.Name.Equals(name)).Single().HasArangement(type);
        }

        /// <summary>
        /// Filter clients by name.
        /// </summary>
        /// <param name="toSort">Name to filter by.</param>
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
        /// <summary>
        /// Filter clients by client number.
        /// </summary>
        /// <param name="toSort">Client number to filter by.</param>
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

        /// <summary>
        /// Make a reservation.
        /// </summary>
        public void MakeReservation()
        {
            SelectedReservation = sm.HireVehicle(SelectedLimousine.Name, SelectedArangement, SelectedBeginLocation,
                                  SelectedClient.ClientNumber, DateTime.Now, SelectedEndLocation, SelectedDate, SelectedExtraHours, SelectedStartTime, SelectedEndTime);
        }
        /// <summary>
        /// Get the total duration of the reservation.
        /// </summary>
        /// <returns>An integer representing the duration in hours hours.</returns>
        public int GetDuration()
        {
            int toReturn = 0;
            foreach (var item in SelectedReservation.Details.Hours)
            {
                toReturn += item.NrOfHours;
            }
            return toReturn;
        }

        /// <summary>
        /// Filter reservation by clientnumber, date or both.
        /// </summary>
        /// <param name="clientNumber">Client number to filter by.</param>
        /// <param name="date">Date to filter by.</param>
        public void FilterReservations(string clientNumber, DateTime? date)
        {
            DateTime Date = DateTime.MinValue;
            if (date != null)
            {
                Date = (DateTime)date;
            }
            int ClientNumber = 0;
            if (FilterByClient)
            {
                ClientNumber = int.Parse(clientNumber);
            }
            if (FilterByClient && FilterByDate)
            {

                Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered.Where(r => r.Client.ClientNumber == ClientNumber & r.ReservationDate.Year == Date.Year
                & r.ReservationDate.Month == Date.Month & r.ReservationDate.Day == Date.Day));
            }
            else if (FilterByDate)
            {
                Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered.Where(r => r.ReservationDate.Year == Date.Year
               & r.ReservationDate.Month == Date.Month & r.ReservationDate.Day == Date.Day));
            }
            else if (FilterByClient)
            {
                Reservations = new ObservableCollection<Reservering>(reservationsUnfiltered.Where(r => r.Client.ClientNumber == ClientNumber));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Lets the event handler know that a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property that has changed.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
