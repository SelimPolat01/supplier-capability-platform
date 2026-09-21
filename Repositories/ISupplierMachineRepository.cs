
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public interface ISupplierMachineRepository
    {
        public Task<(List<SupplierMachine> Data, int TotalCount)> FetchAllSupplierMachinesAsync(int pageNumber, int pageSize, string sortBy, string? searchString = null);
        public Task<(List<SupplierMachine> Data, int TotalCount)> FetchSupplierAllMachinesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null);

        public Task<SupplierMachine?> FetchMachineAsync(int machineId, int userId);
        public Task<SupplierMachine> AddMachineAsync(SupplierMachine machine);

        public Task<SupplierMachine?> EditMachineAsync(SupplierMachine machine, int userId);

        public Task<bool> RemoveMachineAsync(int machineId, int userId);
    }
}
