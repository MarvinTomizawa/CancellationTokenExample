using Application.Repositories;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
            => service.AddSingleton<IPokemonRepository, PokemonRepository>();
    }
}