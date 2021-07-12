using MediatR;

namespace Application.UseCases.SetDelay
{
    public class SetDelayCommand : IRequest<SetDelayResponse>
    {
        public int Delay { get; set; }
    }
}