using MassTransit;
using System;

namespace Shared.Events.Interfaces
{
    public interface ITranserCompletedEvent : CorrelatedBy<Guid>
    {
    }
}
