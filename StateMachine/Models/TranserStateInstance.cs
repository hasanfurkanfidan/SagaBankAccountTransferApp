using Automatonymous;
using MassTransit;
using System;

namespace StateMachine.Models
{
    public class TranserStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string ReceiverIbanNumber { get; set; }
        public string SenderIbanNumber { get; set; }
        public decimal Price { get; set; }
        public string CurrentState { get; set; }
    }
}
