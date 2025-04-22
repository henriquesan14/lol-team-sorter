using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Extensions;

namespace LoLTeamSorter.Application.Queries.GetPermissions
{
    public class GetPermissionQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPermissionsQuery, List<PermissionByCategoryViewModel>>
    {
        public async Task<List<PermissionByCategoryViewModel>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.Permissions.GetAllAsync();

            var ordemDesejada = new Dictionary<string, int>
            {
                { "View", 1 },
                { "Create", 2 },
                { "Generate", 2 },
                { "Edit", 3 },
                { "Update", 3 },
                { "Delete", 4 }
            };

            var groupBy = list
                .GroupBy(p => p.PermissionCategory)
                .Select(g => new PermissionByCategoryViewModel(
                    g.Key.GetDescription(),
                    g
                    .OrderBy(p => GetOrdemPersonalizada(p.Name, ordemDesejada))
                    .ToViewModelList()
                    .ToList()
                ))
                .ToList();

            return (groupBy);
        }

        private static int GetOrdemPersonalizada(string nomePermissao, Dictionary<string, int> ordem)
        {
            foreach (var item in ordem)
            {
                if (nomePermissao.Contains(item.Key, StringComparison.OrdinalIgnoreCase))
                    return item.Value;
            }
            return int.MaxValue;
        }
    }
}
