namespace LoLTeamSorter.Application.ViewModels
{
    public record GroupViewModel(Guid Id, string Name, List<PermissionViewModel> Permissions);
}
