using DataLayer.Func;
using DataLayer.Repositories;
using DomainLayer.OtherInterfaces;
using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.UoW
{
    /// <summary>
    /// The link between the manager and the context. Implements the IUnitOfWork interface.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ManagerContext context;
        /// <summary>
        /// Gives access to client objects in the database.
        /// </summary>
        public IClientRepository Clients { get; private set; }
        /// <summary>
        /// Gives access to limousine and reservation objects in the database.
        /// </summary>
        public IVloot Vloot { get; private set; }
        /// <summary>
        /// Contructor thay makes connection to the database.
        /// </summary>
        /// <param name="context">Which context to use.</param>
        public UnitOfWork(ManagerContext context)
        {
            this.context = context;
            this.Clients = new ClientRepository(context);
            this.Vloot = new Vloot(context);
        }
        /// <summary>
        /// Saves the changes made to the database.
        /// </summary>
        /// <returns>An integer that represents whether the changes saved successfully.</returns>
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
        /// <summary>
        /// Disposes of the context.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
