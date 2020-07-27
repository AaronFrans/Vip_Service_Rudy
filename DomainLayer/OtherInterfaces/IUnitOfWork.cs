using DomainLayer.Repositories;
using System;

namespace DomainLayer.OtherInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IClientRepository clients { get; }
        public IVloot vloot { get; }
        int Complete();
    }
}
