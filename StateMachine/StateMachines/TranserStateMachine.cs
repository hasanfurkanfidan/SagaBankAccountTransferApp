using Automatonymous;
using Shared;
using Shared.Events.Implementations;
using Shared.Events.Interfaces;
using StateMachine.Models;
using System;

namespace StateMachine.StateMachines
{
    public class TranserStateMachine : MassTransitStateMachine<TranserStateInstance>
    {
        public Event<ITranserStartedEvent> TranserStartedEvent { get; set; }
        public Event<IPriceSendedReceiverBankEvent> PriceSendedReceiverBankEvent { get; set; }
        public Event<ITranserCompletedEvent> ITransferCompletedEvent { get; set; }
        public Event<ITransferFailedEvent> ITransferFailedEvent { get; set; }

        public State TranserStarted { get; set; }
        public State TransferCompleted { get; set; }
        public State TranserFailed { get; set; }
        public TranserStateMachine()
        {
            InstanceState(x => x.CurrentState);
            Event(() => TranserStartedEvent, y => y.CorrelateBy<object>(p => new { }, z => new { }).SelectId(context => System.Guid.NewGuid()));

            Initially(When(TranserStartedEvent).Then(context =>
            {
                context.Instance.SenderIbanNumber = context.Data.SenderIbanNumber;
                context.Instance.ReceiverIbanNumber = context.Data.ReceiverIbanNumber;
                context.Instance.Price = context.Data.Price;
            })
            .Then(context => { Console.WriteLine($"OrderCreatedRequestEvent Before :{context.Data.ReceiverIbanNumber} "); })

            .TransitionTo(TranserStarted)
             .Publish(context => new PriceSendedReceiverBankEvent(context.Instance.CorrelationId)
             {
                 Price = context.Data.Price,
                 ReceiverIbanNumber = context.Data.ReceiverIbanNumber
             }));
            During(TranserStarted,
                 When(ITransferCompletedEvent).TransitionTo(TransferCompleted).Finalize(),
                 When(ITransferFailedEvent).TransitionTo(TranserFailed).Send(new Uri($"queue:{RabbitMqConstants.RewindTransferQueue}"), context => new RewindTransferEvent
                 {
                     Price = context.Instance.Price,
                     SenderIbanNumber = context.Instance.SenderIbanNumber
                 })
                );
        }
    }
}
