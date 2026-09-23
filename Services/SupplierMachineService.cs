using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Repositories;

namespace SupplierCapabilitiesAndManagementSystem.Services
{
    public class SupplierMachineService : ISupplierMachineService
    {
        private readonly ISupplierMachineRepository _supplierMachineRepository;
        private readonly IWebHostEnvironment _env;

        public SupplierMachineService(ISupplierMachineRepository machineRepository, IWebHostEnvironment env)
        {
            _supplierMachineRepository = machineRepository;
            _env = env;
        }

        public async Task<FetchAllSuppliersMachinesGetResponseDTO> FetchAllSupplierMachinesAsync(int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<SupplierMachine> Data, int TotalCount) result = await _supplierMachineRepository.FetchAllSuppliersMachinesAsync(pageNumber, pageSize, sortBy, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(supplierMachine => supplierMachine.ToSupplierMachineDTO()).ToList();

                return new FetchAllSuppliersMachinesGetResponseDTO()
                {
                    Data = mappedData,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    Message = "All suppliers machines retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchAllSuppliersMachinesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the suppliers machines: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchSupplierAllMachinesGetResponseDTO> FetchAllMachinesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<SupplierMachine> Data, int TotalCount) result = await _supplierMachineRepository.FetchSupplierAllMachinesAsync(userId, pageNumber, pageSize, sortBy, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
                var mappedData = result.Data.Select(supplierMachine => supplierMachine.ToSupplierMachineDTO()).ToList();

                return new FetchSupplierAllMachinesGetResponseDTO()
                {
                    Data = mappedData,
                    CurrentPage = pageNumber,
                    TotalCount = result.TotalCount,
                    TotalPages = totalPages,
                    Message = "Supplier's all machines retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchSupplierAllMachinesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the supplier machines: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<FetchSupplierMachineGetResponseDTO> FetchMachineAsync(int userId, int machineId)
        {
            try
            {
                SupplierMachine? existingSupplierMachine = await _supplierMachineRepository.FetchMachineAsync(machineId, userId);

                if (existingSupplierMachine == null)
                {
                    return new FetchSupplierMachineGetResponseDTO()
                    {
                        Message = "Supplier machine not found.",
                        IsSuccess = false
                    };
                }

                return new FetchSupplierMachineGetResponseDTO()
                {
                    Machine = existingSupplierMachine.ToSupplierMachineDTO(),
                    Message = "Supplier machine retrieved successfully.",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new FetchSupplierMachineGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the supplier machine: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
        public async Task<AddSupplierMachinePostResponseDTO> AddMachineAsync(int userId, AddSupplierMachinePostRequestDTO addSupplierMachinePostRequestDTO)
        {
            string imageFileUrl = "";

            if (addSupplierMachinePostRequestDTO.ImageFile != null && addSupplierMachinePostRequestDTO.ImageFile.Length > 0)
            {
                string[] permittedExtensions = { ".pdf", ".jpeg", ".jpg", ".png" };
                string fileExtension = Path.GetExtension(addSupplierMachinePostRequestDTO.ImageFile.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !permittedExtensions.Contains(fileExtension))
                {
                    return new AddSupplierMachinePostResponseDTO { Message = "Invalid file type.", IsSuccess = false };
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await addSupplierMachinePostRequestDTO.ImageFile.CopyToAsync(fileStream);
                }

                imageFileUrl = "/uploads/" + uniqueFileName;
            }
            else
            {
                return new AddSupplierMachinePostResponseDTO { Message = "Supplier machine image file is required.", IsSuccess = false };

            }

            try
            {
                SupplierMachine supplierMachine = addSupplierMachinePostRequestDTO.ToSupplierMachine(userId, imageFileUrl);
                SupplierMachine addedSupplierMachine = await _supplierMachineRepository.AddMachineAsync(supplierMachine);
                return addedSupplierMachine.ToAddSupplierMachinePostResponseDTO("Supplier machine added successfully.", true);
            }
            catch (Exception ex)
            {
                return new AddSupplierMachinePostResponseDTO()
                {
                    Message = $"An error occurred: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<EditSupplierMachinePatchResponseDTO> EditMachineAsync(EditSupplierMachinePatchRequestDTO editSupplierMachinePatchRequestDTO, int userId)
        {
            SupplierMachine supplierMachine = editSupplierMachinePatchRequestDTO.ToSupplierMachine(userId);
            SupplierMachine? updatedSupplierMachine = await _supplierMachineRepository.EditMachineAsync(supplierMachine, userId);

            if (updatedSupplierMachine == null) return new EditSupplierMachinePatchResponseDTO() { Message = "Machine not found or unauthorized operation.", IsSuccess = false };

            return new EditSupplierMachinePatchResponseDTO()
            {
                Message = "The machine has been successfully updated.",
                IsSuccess = true,
                EditedMachineId = updatedSupplierMachine.Id
            };
        }

        public async Task<RemoveSupplierMachineDeleteResponseDTO> RemoveMachineAsync(int userId, int supplierMachineId)
        {
            bool supplierMachineIsDeleted = await _supplierMachineRepository.RemoveMachineAsync(supplierMachineId, userId);

            if (supplierMachineIsDeleted) return new RemoveSupplierMachineDeleteResponseDTO() { Message = "The machine was successfully deleted.", IsSuccess = true, DeletedMachineId = supplierMachineId };
            else return new RemoveSupplierMachineDeleteResponseDTO() { Message = "The machine could not be deleted.", IsSuccess = false };
        }
    }
}
