

using Billify.API.Bussiness.Services;
using Billify.API.Common.Interfaces;
using Clientify.API.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Billify.API.Bussiness
{
    public class DIConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DtoEntityMapper));
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IClientService, ClientService>();
        }
    }
}
