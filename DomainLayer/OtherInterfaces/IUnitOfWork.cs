using DomainLayer.Repositories;
using System;

namespace DomainLayer.OtherInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IClientRepository Clients { get; }
        public IVloot Vloot { get; }
        int Complete();
    }
}