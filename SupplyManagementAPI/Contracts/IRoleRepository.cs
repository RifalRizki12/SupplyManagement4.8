using SupplyManagementAPI.Contracts;
using SupplyManagementAPI.Models;
using System;

namespace SupplyManagementAPI.Contracts
{
    public interface IRoleRepository : IGeneralRepository<Role>
    {
        Guid? GetDefaultGuid();
    }
}
