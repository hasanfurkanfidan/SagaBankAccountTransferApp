using MassTransit;
using System;

namespace Shared.Events.Interfaces
{
    public interface ITransferFailedEvent : CorrelatedBy<Guid>
    {
    }
}
