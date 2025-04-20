using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Application.ViewModels
{
    public record UserViewModel(Guid Id, string Name, string Username, GroupViewModel Group);
}
