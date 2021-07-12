using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Domain.Factories;

namespace Infra.Repositories
{
    internal class PokemonRepository : IPokemonRepository
    {
        private readonly List<Pokemon> _pokemons;
        private int _delay;

        public PokemonRepository()
        {
            _delay = 0;

            _pokemons = new List<Pokemon>
            {
                PokemonFactory.Bulbassauro(),
                PokemonFactory.Charmander(),
                PokemonFactory.Squirtle()
            };
        }

        public async Task<List<Pokemon>> GetAllPokemons(CancellationToken cancellationToken)
        {
            await Task.Delay(_delay, cancellationToken);
            
            return _pokemons;
        }

        public void SetDelay(int seconds)
        {
            _delay = seconds * 1000;
        }
    }
}