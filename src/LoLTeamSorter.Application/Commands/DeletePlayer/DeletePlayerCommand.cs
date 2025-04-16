using MediatR;

namespace LoLTeamSorter.Application.Commands.DeletePlayer
{
    public record DeletePlayerCommand(Guid Id) : IRequest<Unit>
    {
    }
}
