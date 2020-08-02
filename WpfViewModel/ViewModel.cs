using DataLayer.UoW;
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

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

        public Array Types{ get; set;} = Enum.GetValues(typeof(ClientType));

        public string[] Arangements { get; set; } = new string[] { "Airport", "Business", "Nightlife" ,"Wedding","Wellness"};

        public List<ClientDiscount> GetDiscounts(ClientType type)
        {
            return sm.GetDiscountsForType(type);
        }
        public void AddItems()
        {
            ServiceManager sm = new ServiceManager(new UnitOfWork(new DataLayer.Func.ManagerContext("Production")));
            limousines = new List<Limousine>(sm.GetVehicles());

            LimousineInfo = new ObservableCollection<LimousineView>();

            clientsUnfiltered = sm.GetClients();
            Clients = new ObservableCollection<Client>(sm.GetClients());

            MakeLimousineInfo(limousines);


        }

        private void MakeLimousineInfo(IEnumerable<Limousine> limousines)
        {
            var toAdd = new LimousineView();
            foreach (var item in limousines)
            {
                toAdd = new LimousineView();
                toAdd.Name = item.Name;
                toAdd.FirstHourPrice = item.FirstHourPrice;
                toAdd.NightLife = item.Arangements.Where(x => x.GetType() == typeof(NightLife)).Select(a => ((a as NightLife).Price as int?)).FirstOrDefault();
                toAdd.Wedding = item.Arangements.Where(x => x.GetType() == typeof(Wedding)).Select(a => ((a as Wedding).Price as int?)).FirstOrDefault();
                toAdd.Wellness = item.Arangements.Where(x => x.GetType() == typeof(Wellness)).Select(a => ((a as Wellness).Price as int?)).FirstOrDefault();

                LimousineInfo.Add(toAdd);
            }
        }
        public void FilterLimousines(string toSort)
        {
            if (!string.IsNullOrWhiteSpace(toSort) || !toSort.Equals("Zoek een limousine op naam"))
            {
                List<LimousineView> newList = LimousineInfo.Where(c => c.Name.ToLower().Contains(toSort)).ToList();
                LimousineInfo = new ObservableCollection<LimousineView>(newList);
            }
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
