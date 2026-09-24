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
                    Purchaser = mappedData,
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

        public async Task<FetchPurchaserAllPurchasesGetResponseDTO> FetchPurchaserAllPurchasesAsync(int purchaserId, string sortBy = "id_asc", int pageNumber = 1, int pageSize = 10, string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<PurchaserMachinePurchase> Data, int TotalCount) result = await _purchaserRepository.FetchPurchasersAllPurchaseAsync(purchaserId, sortBy, pageNumber, pageSize, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(machinePurchase => machinePurchase.ToPurchaserMachinePurchaseDTO()).ToList();

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
                    Message = $"An error occurred while retrieving the purchases: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchPurchaserPurchaseGetResponseDTO> FetchPurchaserPurchaseAsync(int purchaseId)
        {
            try
            {
                PurchaserMachinePurchase? existingPurchaserPurchase = await _purchaserRepository.FetchPurchaserPurchaseAsync(purchaseId);

                if (existingPurchaserPurchase == null)
                {
                    return new FetchPurchaserPurchaseGetResponseDTO()
                    {
                        Message = "Purchase not found.",
                        IsSuccess = false
                    };
                }

                return new FetchPurchaserPurchaseGetResponseDTO()
                {
                    Purchase = existingPurchaserPurchase.ToPurchaserMachinePurchaseDTO(),
                    Message = "Purchase retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchPurchaserPurchaseGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the purchase details: {ex.Message}",
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
