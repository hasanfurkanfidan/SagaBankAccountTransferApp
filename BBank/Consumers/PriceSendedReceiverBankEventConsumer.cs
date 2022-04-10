using BBank.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events.Implementations;
using Shared.Events.Interfaces;
using System.Threading.Tasks;

namespace BBank.Consumers
{
    public class PriceSendedReceiverBankEventConsumer : IConsumer<IPriceSendedReceiverBankEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public PriceSendedReceiverBankEventConsumer(AppDbContext appDbContext, IPublishEndpoint publishEndpoint)
        {
            _appDbContext = appDbContext;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<IPriceSendedReceiverBankEvent> context)
        {

            var account = await _appDbContext.Accounts.SingleOrDefaultAsync(p => p.IbanNumber == context.Message.ReceiverIbanNumber);
            if (account != null)
            {
                account.Balance += context.Message.Price;
                await _appDbContext.SaveChangesAsync();
                var transferCompletedEvent = new TranserCompletedEvent(context.Message.CorrelationId);
                await _publishEndpoint.Publish(transferCompletedEvent);
            }
            else
            {
                var transferFailedEvent = new TransferFailedEvent(context.Message.CorrelationId);
                await _publishEndpoint.Publish(transferFailedEvent);
            }
        }
    }
}
