using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using MediatR;

namespace Application.UseCases.GetAllPokemons
{
    public class GetAllPokemonsHandler: IRequestHandler<GetAllPokemonQuery, PokemonResponse>
    {
        private readonly IPokemonRepository _pokemonRepository;

        public GetAllPokemonsHandler(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<PokemonResponse> Handle(GetAllPokemonQuery request, CancellationToken cancellationToken)
        {
            var pokemons = await _pokemonRepository.GetAllPokemons(cancellationToken);

            return new PokemonResponse
            {
                Pokemons = pokemons
            };
        }
    }
}