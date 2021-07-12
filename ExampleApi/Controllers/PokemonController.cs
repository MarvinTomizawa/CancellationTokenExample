using System;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.GetAllPokemons;
using Application.UseCases.SetDelay;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PokemonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PokemonResponse> GetAllPokemons([FromQuery] bool canTimeout,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"{DateTime.Now}: Iniciado request");
            PokemonResponse response;
            
            if (canTimeout)
            {
                response = await _mediator.Send(new GetAllPokemonQuery(), cancellationToken);
            }
            else
            {
                response = await _mediator.Send(new GetAllPokemonQuery(), CancellationToken.None);
            }

            Console.WriteLine($"{DateTime.Now}: Retornado pokemons");
            
            return response;
        }

        [HttpPost("delay")]
        public async Task<IActionResult> SetDelay([FromBody] SetDelayCommand command,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}