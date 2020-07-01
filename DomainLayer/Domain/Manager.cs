using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain
{
    class Manager
    {
        private IUnitOfWork unitOfWork;

        public Manager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void HireVehicle()
        { 

        }


    }
}
