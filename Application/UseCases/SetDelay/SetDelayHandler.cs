using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using MediatR;

namespace Application.UseCases.SetDelay
{
    public class SetDelayHandler : IRequestHandler<SetDelayCommand, SetDelayResponse>
    {
        private readonly IPokemonRepository _repository;

        public SetDelayHandler(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public Task<SetDelayResponse> Handle(SetDelayCommand request, CancellationToken cancellationToken)
        {
            _repository.SetDelay(request.Delay);
            
            return Task.FromResult(new SetDelayResponse
            {
                Delay = request.Delay
            });
        }
    }
}