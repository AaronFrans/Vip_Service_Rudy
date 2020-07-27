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

        public IClientRepository clients { get; private set; }
        public IVloot vloot { get; private set; }

        public UnitOfWork(ManagerContext context)
        {
            this.context = context;
            this.clients = new ClientRepository(context);
            this.vloot = new Vloot(context);
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
