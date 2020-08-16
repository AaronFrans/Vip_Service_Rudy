using DataLayer.Func;
using DataLayer.Repositories;
using DomainLayer.OtherInterfaces;
using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManagerContext context;

        public IClientRepository Clients { get; private set; }
        public IVloot Vloot { get; private set; }

        public UnitOfWork(ManagerContext context)
        {
            this.context = context;
            this.Clients = new ClientRepository(context);
            this.Vloot = new Vloot(context);
        }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
