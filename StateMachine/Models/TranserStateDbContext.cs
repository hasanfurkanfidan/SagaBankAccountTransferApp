using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StateMachine.Models
{
    internal class TranserStateDbContext : SagaDbContext
    {
        public TranserStateDbContext(DbContextOptions<TranserStateDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override IEnumerable<ISagaClassMap> Configurations { get { yield return new TranserStateMap(); } }
    }
}
