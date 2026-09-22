using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class PurchaserService : IPurchaserService
    {
        private readonly IPurchaserRepository _purchaserRepository;

        public PurchaserService(IPurchaserRepository purchaserRepository)
        {
            _purchaserRepository = purchaserRepository;
        }

        public async Task<FetchAllPurchasersGetResponseDTO> FetchAllPurchasersAsync(string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<AppUser> Data, int TotalCount) result = await _purchaserRepository.FetchAllPurchasersAsync(sortBy, pageNumber, pageSize, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(purchaser => purchaser.ToPurchaserDTO()).ToList();

                return new FetchAllPurchasersGetResponseDTO()
                {
                    Data = mappedData,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "Purchasers retrieved successfully.",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new FetchAllPurchasersGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the purchasers: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchPurchaserGetResponseDTO> FetchPurchaserAsync(int purchaserId)
        {
            try
            {
                AppUser? existingPurchaser = await _purchaserRepository.FetchPurchaserAsync(purchaserId);
                if (existingPurchaser == null)
                {
                    return new FetchPurchaserGetResponseDTO()
                    {
                        Message = "Purchaser not found.",
                        IsSuccess = false
                    };
                }

                var mappedData = existingPurchaser.ToPurchaserDTO();

                return new FetchPurchaserGetResponseDTO()
                {
                    Data = mappedData,
                    Message = "Purchaser details retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchPurchaserGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the purchaser: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchPurchaserAllPurchasesGetResponseDTO> FetchPurchaserAllPurchasesAsync(int purhcaserId, string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<PurchaserMachinePurchase> Data, int TotalCount) result = await _purchaserRepository.FetchPurchaserAllPurchaseAsync(purhcaserId, sortBy, pageNumber, pageSize, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(machinePurchase => new PurchaserMachinePurchaseDTO()
                {
                    Id = machinePurchase.Id,
                    Quantity = machinePurchase.Quantity,
                    TotalPurchasedPrice = machinePurchase.TotalPurchasedPrice,
                    PurchasedPrice = machinePurchase.PurchasedPrice,
                    PurchaseDate = machinePurchase.PurchaseDate,
                    MachineDetails = new SupplierMachineDTO()
                    {
                        Id = machinePurchase.SupplierMachine.Id,
                        AppUserId = machinePurchase.SupplierMachine.AppUserId,
                        Price = machinePurchase.SupplierMachine.Price,
                        MachineGroup = machinePurchase.SupplierMachine.MachineGroup,
                        MachineType = machinePurchase.SupplierMachine.MachineType,
                        BrandAndModel = machinePurchase.SupplierMachine.BrandAndModel,
                        ProductionYear = machinePurchase.SupplierMachine.ProductionYear,
                        Quantity = machinePurchase.SupplierMachine.Quantity,
                        AxisCount = machinePurchase.SupplierMachine.AxisCount,
                        CapacitySpecs = machinePurchase.SupplierMachine.CapacitySpecs,
                        ImageUrl = machinePurchase.SupplierMachine.ImageUrl,
                        CreatedAt = machinePurchase.SupplierMachine.CreatedAt
                    }
                }).ToList();

                return new FetchPurchaserAllPurchasesGetResponseDTO()
                {
                    Data = mappedData,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "Purchases retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchPurchaserAllPurchasesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the purchasers: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<AddPurchaserMachinePurchasePostResponseDTO> AddPurchaserMachinePurchaseAsync(int purchaserId, int machineId, int quantity)
        {
            if (quantity <= 0)
            {
                return new AddPurchaserMachinePurchasePostResponseDTO()
                {
                    Message = "Quantity must be greater than zero.",
                    IsSuccess = false
                };
            }

            try
            {
                var result = await _purchaserRepository.AddPurchaserMachinePurchaseAsync(purchaserId, machineId, quantity);

                if (result.PurchaseId == 0)
                {
                    return new AddPurchaserMachinePurchasePostResponseDTO()
                    {
                        Message = result.ErrorMessage,
                        IsSuccess = false
                    };
                }

                return new AddPurchaserMachinePurchasePostResponseDTO()
                {
                    Message = "Machine purchased successfully.",
                    IsSuccess = true,
                    AddedMachinePurchaseId = result.PurchaseId
                };
            }
            catch (Exception ex)
            {
                return new AddPurchaserMachinePurchasePostResponseDTO()
                {
                    Message = $"An error occurred during purchase: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
