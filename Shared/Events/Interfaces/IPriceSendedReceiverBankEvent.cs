using MassTransit;
using System;

namespace Shared.Events.Interfaces
{
    public interface IPriceSendedReceiverBankEvent :  CorrelatedBy<Guid>
    {
        public string ReceiverIbanNumber { get; set; }
        public decimal Price { get; set; }
    }
}
