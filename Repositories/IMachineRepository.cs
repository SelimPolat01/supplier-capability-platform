
using TedarikciKabiliyetYonetimSistemi.Entities;

public interface IMachineRepository
{
    public Task<Machine> AddMachineAsync(Machine machine);

    public Task<(List<Machine> Data, int TotalCount)> FetchAllMachinesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null);

    public Task<Machine?> FetchMachineAsync(int machineId, int userId);

    public Task<Machine?> EditMachineAsync(Machine machine, int userId);

    public Task<bool> RemoveMachineAsync(int machineId, int userId);
}
