using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House_TARgv20.ApplicationServices.Services;
using House_TARgv20.Core.ServiceInterface;
using House_TARgv20.Data;
using House_TARgv20.Core.Domain;

namespace House_TARgv20.Tests.Validation
{
    public class Test_Base : IDisposable
    {
        protected IServiceProvider serviceProvider { get; }

        protected Test_Base()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {

            services.AddScoped<IHouseService, HouseServices>();

            services.AddDbContext<HouseDbContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
    }
}
