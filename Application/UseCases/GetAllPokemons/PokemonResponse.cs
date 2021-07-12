using System.Collections.Generic;
using Domain;

namespace Application.UseCases.GetAllPokemons
{
    public class PokemonResponse
    {
        public IList<Pokemon> Pokemons { get; set; }
    }
}