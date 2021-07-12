using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetAllPokemons(CancellationToken token);

        void SetDelay(int seconds);
    }
}