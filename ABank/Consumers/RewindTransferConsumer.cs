using ABank.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events.Implementations;
using System.Threading.Tasks;

namespace ABank.Consumers
{
    public class RewindTransferConsumer : IConsumer<RewindTransferEvent>
    {
        private readonly AppDbContext _appDbContext;
        public RewindTransferConsumer(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Consume(ConsumeContext<RewindTransferEvent> context)
        {
            var account = await _appDbContext.Accounts.SingleOrDefaultAsync(p => p.IbanNumber == context.Message.SenderIbanNumber);
            account.Balance += context.Message.Price;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
