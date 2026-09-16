using Microsoft.EntityFrameworkCore;
using TedarikciKabiliyetYonetimSistemi.Database;
using TedarikciKabiliyetYonetimSistemi.Entities;

namespace TedarikciKabiliyetYonetimSistemi.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MachineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Machine> AddMachineAsync(Machine machine)
        {
            await _dbContext.Machines.AddAsync(machine);
            await _dbContext.SaveChangesAsync();
            return machine;
        }

        public async Task<(List<Machine> Data, int TotalCount)> FetchAllMachinesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null)
        {
            var query = _dbContext.Machines
                .AsNoTracking()
                .Where(machine => machine.AppUserId == userId);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(machine =>
                    machine.MachineGroup.Contains(searchString) ||
                    machine.MachineType.Contains(searchString) ||
                    machine.BrandAndModel.Contains(searchString) ||
                    machine.CapacitySpecs.Contains(searchString));
            }
            ;

            int totalMachineCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(machine => machine.Id),
                "id_desc" => query.OrderByDescending(machine => machine.Id),
                "group_asc" => query.OrderBy(machine => machine.MachineGroup),
                "group_desc" => query.OrderByDescending(machine => machine.MachineGroup),
                "type_asc" => query.OrderBy(machine => machine.MachineType),
                "type_desc" => query.OrderByDescending(machine => machine.MachineType),
                "brand-model_asc" => query.OrderBy(machine => machine.BrandAndModel),
                "brand-model_desc" => query.OrderByDescending(machine => machine.BrandAndModel),
                "year_asc" => query.OrderBy(machine => machine.ProductionYear),
                "year_desc" => query.OrderByDescending(machine => machine.ProductionYear),
                "quantity_asc" => query.OrderBy(machine => machine.Quantity),
                "quantity_desc" => query.OrderByDescending(machine => machine.Quantity),
                "axis-count_asc" => query.OrderBy(machine => machine.AxisCount),
                "axis-count_desc" => query.OrderByDescending(machine => machine.AxisCount),
                "capacity_asc" => query.OrderBy(machine => machine.CapacitySpecs),
                "capacity_desc" => query.OrderByDescending(machine => machine.CapacitySpecs),
                "added_asc" => query.OrderBy(machine => machine.CreatedAt),
                "added_desc" => query.OrderByDescending(machine => machine.CreatedAt),
                _ => query.OrderBy(machine => machine.Id),
            };

            var machines = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (machines, totalMachineCount);
        }

        public async Task<Machine?> FetchMachineAsync(int machineId, int userId)
        {
            return await _dbContext.Machines.AsNoTracking().FirstOrDefaultAsync(machine => machine.Id == machineId && machine.AppUserId == userId);
        }

        public async Task<Machine?> EditMachineAsync(Machine machine, int userId)
        {
            Machine? existingMachine = await _dbContext.Machines.FirstOrDefaultAsync(m => m.Id == machine.Id && machine.AppUserId == userId);

            if (existingMachine == null) return null;

            existingMachine.MachineGroup = machine.MachineGroup;
            existingMachine.MachineType = machine.MachineType;
            existingMachine.Quantity = machine.Quantity;
            existingMachine.AxisCount = machine.AxisCount;
            existingMachine.BrandAndModel = machine.BrandAndModel;
            existingMachine.CapacitySpecs = machine.CapacitySpecs;
            existingMachine.ProductionYear = machine.ProductionYear;

            await _dbContext.SaveChangesAsync();

            return existingMachine;
        }

        public async Task<bool> RemoveMachineAsync(int machineId, int userId)
        {
            int deletedRows = await _dbContext.Machines
                .Where(machine => machine.Id == machineId && machine.AppUserId == userId)
                .ExecuteDeleteAsync();

            return deletedRows > 0;
        }
    }
}
