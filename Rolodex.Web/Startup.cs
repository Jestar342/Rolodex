using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rolodex.Handlers;
using Rolodex.Messages.Events;
using Rolodex.Models;
using Rolodex.Persistence;
using Rolodex.Web.EventHandlers;
using Rolodex.Web.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Rolodex.Web
{
    public class StartUp
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddMvc()
               .AddRazorOptions(options => options.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml"));

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<CreateRolodexEntryHandler>();
                cfg.AddConsumer<RolodexEntryCreatedHandler>();
                cfg.AddBus(provider => Bus.Factory.CreateUsingInMemory(
                               busCfg => busCfg.ReceiveEndpoint(ep => ep.ConfigureConsumers(provider))
                           )
                );
            });

            services.AddSingleton<IHostedService, BusService>();

            services.AddSingleton(new List<RolodexViewModel>());

            services.AddSingleton<IEventRepository, EventRepositoryPublishingWrapper>(
                provider => new EventRepositoryPublishingWrapper(new CrappyInMemoryEventRepositoryUsedForDemoOnly(),
                                                                 provider.GetRequiredService<IBusControl>()
                )
            );
        }
    }

    public class EventRepositoryPublishingWrapper : IEventRepository
    {
        readonly IEventRepository _repository;
        readonly IBusControl      _busControl;

        public EventRepositoryPublishingWrapper(IEventRepository repository, IBusControl busControl)
        {
            _repository = repository;
            _busControl = busControl;
        }

        public List<IEvent> Events
            => _repository.Events;

        public void Save(IAggregateRoot aggregateRoot)
        {
            var events = aggregateRoot.UnCommittedEvents;
            _repository.Save(aggregateRoot);
            var tasks = events.Select(x => _busControl.Publish(x));
            TaskUtil.Await(Task.WhenAll(tasks));
        }

        public TEntity Get<TEntity>(Guid entityId) where TEntity : class, IAggregateRoot, new()
            => _repository.Get<TEntity>(entityId);
    }

    public class BusService : IHostedService {
        readonly IBusControl _busControl;

        public BusService(IBusControl busControl) { _busControl = busControl; }
        public Task StartAsync(CancellationToken cancellationToken)
            => _busControl.StartAsync(cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken)
            => _busControl.StopAsync(cancellationToken);
    }
}