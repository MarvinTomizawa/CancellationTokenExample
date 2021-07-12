using MediatR;

namespace Application.UseCases.GetAllPokemons
{
    public class GetAllPokemonQuery : IRequest<PokemonResponse>
    {
    }
}