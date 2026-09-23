using Microsoft.EntityFrameworkCore;
using SupplierCapabilitiesAndManagementSystem.Database;
using SupplierCapabilitiesAndManagementSystem.Entities;

namespace SupplierCapabilitiesAndManagementSystem.Repositories
{
    public class SupplierMachineRepository : ISupplierMachineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierMachineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<SupplierMachine> Data, int TotalCount)> FetchAllSuppliersMachinesAsync(int pageNumber, int pageSize, string sortBy, string? searchString = null)
        {
            var query = _dbContext.SupplierMachines.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(supplierMachine =>
                supplierMachine.MachineGroup.Contains(searchString) ||
                supplierMachine.BrandAndModel.Contains(searchString) ||
                supplierMachine.MachineType.Contains(searchString) ||
                supplierMachine.CapacitySpecs.Contains(searchString));
            }

            int totalSupplierMacineCount = await query.CountAsync();

            query = sortBy switch
            {
                "id_asc" => query.OrderBy(mp => mp.Id),
                "id_desc" => query.OrderByDescending(mp => mp.Id),
                "group_asc" => query.OrderBy(mp => mp.MachineGroup).ThenBy(m => m.Id),
                "group_desc" => query.OrderByDescending(mp => mp.MachineGroup).ThenByDescending(m => m.Id),
                "type_asc" => query.OrderBy(mp => mp.MachineType).ThenBy(m => m.Id),
                "type_desc" => query.OrderByDescending(mp => mp.MachineType).ThenByDescending(m => m.Id),
                "brand-model_asc" => query.OrderBy(mp => mp.BrandAndModel).ThenBy(m => m.Id),
                "brand-model_desc" => query.OrderByDescending(mp => mp.BrandAndModel).ThenByDescending(m => m.Id),
                "production-year_asc" => query.OrderBy(mp => mp.ProductionYear).ThenBy(m => m.Id),
                "production-year_desc" => query.OrderByDescending(mp => mp.ProductionYear).ThenByDescending(m => m.Id),
                "quantity_asc" => query.OrderBy(mp => mp.Quantity).ThenBy(m => m.Id),
                "quantity_desc" => query.OrderByDescending(mp => mp.Quantity).ThenByDescending(m => m.Id),
                "axis-count_asc" => query.OrderBy(mp => mp.AxisCount).ThenBy(m => m.Id),
                "axis-count_desc" => query.OrderByDescending(mp => mp.AxisCount).ThenByDescending(m => m.Id),
                "capacity_asc" => query.OrderBy(mp => mp.CapacitySpecs).ThenBy(m => m.Id),
                "capacity_desc" => query.OrderByDescending(mp => mp.CapacitySpecs).ThenByDescending(m => m.Id),
                "price_asc" => query.OrderBy(mp => mp.Price).ThenBy(m => m.Id),
                "price_desc" => query.OrderByDescending(mp => mp.Price).ThenBy(m => m.Id),
                "supplier-id_asc" => query.OrderBy(mp => mp.AppUserId).ThenBy(m => m.Id),
                "supplier-id_desc" => query.OrderByDescending(mp => mp.AppUserId).ThenBy(m => m.Id),
                _ => query.OrderBy(mp => mp.Id)
            };

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (result, totalSupplierMacineCount);
        }

        public async Task<(List<SupplierMachine> Data, int TotalCount)> FetchSupplierAllMachinesAsync(int userId, int pageNumber, int pageSize, string sortBy, string? searchString = null)
        {
            var query = _dbContext
                .SupplierMachines
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
                "price_asc" => query.OrderBy(machine => machine.Price).ThenBy(m => m.Id),
                "price_desc" => query.OrderByDescending(machine => machine.Price).ThenByDescending(m => m.Id),
                "group_asc" => query.OrderBy(machine => machine.MachineGroup).ThenBy(m => m.Id),
                "group_desc" => query.OrderByDescending(machine => machine.MachineGroup).ThenByDescending(m => m.Id),
                "type_asc" => query.OrderBy(machine => machine.MachineType).ThenBy(m => m.Id),
                "type_desc" => query.OrderByDescending(machine => machine.MachineType).ThenByDescending(m => m.Id),
                "brand-model_asc" => query.OrderBy(machine => machine.BrandAndModel).ThenBy(m => m.Id),
                "brand-model_desc" => query.OrderByDescending(machine => machine.BrandAndModel).ThenByDescending(m => m.Id),
                "year_asc" => query.OrderBy(machine => machine.ProductionYear).ThenBy(m => m.Id),
                "year_desc" => query.OrderByDescending(machine => machine.ProductionYear).ThenByDescending(m => m.Id),
                "quantity_asc" => query.OrderBy(machine => machine.Quantity).ThenBy(m => m.Id),
                "quantity_desc" => query.OrderByDescending(machine => machine.Quantity).ThenByDescending(m => m.Id),
                "axis-count_asc" => query.OrderBy(machine => machine.AxisCount).ThenBy(m => m.Id),
                "axis-count_desc" => query.OrderByDescending(machine => machine.AxisCount).ThenByDescending(m => m.Id),
                "capacity_asc" => query.OrderBy(machine => machine.CapacitySpecs).ThenBy(m => m.Id),
                "capacity_desc" => query.OrderByDescending(machine => machine.CapacitySpecs).ThenByDescending(m => m.Id),
                "added_asc" => query.OrderBy(machine => machine.CreatedAt).ThenBy(m => m.Id),
                "added_desc" => query.OrderByDescending(machine => machine.CreatedAt).ThenByDescending(m => m.Id),
                _ => query.OrderBy(machine => machine.Id),
            };

            var machines = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (machines, totalMachineCount);
        }

        public async Task<SupplierMachine?> FetchMachineAsync(int machineId, int userId)
        {
            return await _dbContext
                .SupplierMachines
                .AsNoTracking()
                .FirstOrDefaultAsync(machine => machine.Id == machineId && machine.AppUserId == userId);
        }

        public async Task<SupplierMachine> AddMachineAsync(SupplierMachine machine)
        {
            await _dbContext.AddAsync(machine);
            await _dbContext.SaveChangesAsync();

            return machine;
        }

        public async Task<SupplierMachine?> EditMachineAsync(SupplierMachine machine, int userId)
        {
            SupplierMachine? existingMachine = await _dbContext.SupplierMachines.FirstOrDefaultAsync(m => m.Id == machine.Id && m.AppUserId == userId);

            if (existingMachine == null) return null;

            existingMachine.Price = machine.Price;
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
            int deletedRows = await _dbContext
                .SupplierMachines
                .Where(machine => machine.Id == machineId && machine.AppUserId == userId)
                .ExecuteDeleteAsync();

            return deletedRows > 0;
        }
    }
}
