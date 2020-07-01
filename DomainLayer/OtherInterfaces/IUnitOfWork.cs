using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.OtherInterfaces
{
    interface IUnitOfWork : IDisposable
    {
        public IClientRepository clients { get; }
        public IVloot vloot { get; }
        int Complete();
    }
}
