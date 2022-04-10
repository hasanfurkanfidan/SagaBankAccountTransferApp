using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StateMachine.Models
{
    public class TranserStateMap : SagaClassMap<TranserStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<TranserStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.SenderIbanNumber).HasMaxLength(256);
        }
    }
}
