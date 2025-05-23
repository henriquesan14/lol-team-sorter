﻿using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Application.Contracts.Data
{
    public interface IPermissionRepository : IAsyncRepository<Permission, PermissionId>
    {
    }
}
