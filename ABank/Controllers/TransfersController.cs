using ABank.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Events.Implementations;
using Shared.Events.Interfaces;
using System.Threading.Tasks;

namespace ABank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ISendEndpointProvider _sendEnpointProvider;
        public TransfersController(AppDbContext appDbContext, ISendEndpointProvider sendEndpointProvider)
        {
            _dbContext = appDbContext;
            _sendEnpointProvider = sendEndpointProvider;
        }
        [HttpPost]
        public async Task<IActionResult>Transfer(TransferDto transferDto)
        {
            var account = await _dbContext.Accounts.SingleOrDefaultAsync(p => p.IbanNumber == transferDto.SenderIbanNumber);
            account.Balance -= transferDto.Price;
            await _dbContext.SaveChangesAsync();
            var transferStartedEvent = new TransferStartedEvent
            {
                Price = transferDto.Price,
                SenderIbanNumber = transferDto.SenderIbanNumber,
                ReceiverIbanNumber = transferDto.ReceiverIbanNumber,
            };
            var endpoint = await _sendEnpointProvider.GetSendEndpoint(new System.Uri($"queue:{RabbitMqConstants.TranserSagaQueue}"));
            await endpoint.Send<ITranserStartedEvent>(transferStartedEvent); // Send çünkü Sadece State Machine dinliyor!
            return Ok();
        }
    }
}
