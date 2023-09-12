using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Transfers.Database
{
    public static class Services
    {
        public static IServiceCollection ConfigureServicesDatabase(this IServiceCollection services)
        {
            services.AddScoped<TransfersContext>();
            return services;
        }
    }
}
