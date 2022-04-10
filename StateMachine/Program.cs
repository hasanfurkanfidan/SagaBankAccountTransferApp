using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;
using StateMachine.Models;
using StateMachine.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StateMachine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddSagaStateMachine<TranserStateMachine, TranserStateInstance>().EntityFrameworkRepository(opt =>
                        {
                            opt.AddDbContext<DbContext, TranserStateDbContext>((sp, options) =>
                            {
                                options.UseSqlServer(hostContext.Configuration.GetConnectionString("SqlCon"), m =>
                                {
                                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                });
                            });
                        });
                        cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
                        {
                            configure.Host(hostContext.Configuration.GetConnectionString("RabbitMQ"));
                            configure.ReceiveEndpoint(RabbitMqConstants.TranserSagaQueue, e =>
                            {
                                e.ConfigureSaga<TranserStateInstance>(provider);
                            });
                        }));
                        services.AddMassTransitHostedService();
                        services.AddHostedService<Worker>();
                    });
                });
    }
}
