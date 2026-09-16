using TedarikciKabiliyetYonetimSistemi.Entities;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;
using Machine = TedarikciKabiliyetYonetimSistemi.Entities.Machine;

namespace TedarikciKabiliyetYonetimSistemi.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IWebHostEnvironment _env;

        public MachineService(IMachineRepository machineRepository, IWebHostEnvironment env)
        {
            _machineRepository = machineRepository;
            _env = env;
        }

        public async Task<AddMachinePostResponseDTO> AddMachineAsync(int userId, AddMachinePostRequestDTO addMachinePostRequestDTO)
        {
            string imageFileUrl = "";

            if (addMachinePostRequestDTO.ImageFile != null && addMachinePostRequestDTO.ImageFile.Length > 0)
            {
                string[] permittedExtensions = { ".pdf", ".jpeg", ".jpg", ".png" };
                string fileExtension = Path.GetExtension(addMachinePostRequestDTO.ImageFile.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !permittedExtensions.Contains(fileExtension))
                {
                    return new AddMachinePostResponseDTO { Message = "Invalid file type.", IsSuccess = false };
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await addMachinePostRequestDTO.ImageFile.CopyToAsync(fileStream);
                }

                imageFileUrl = "/uploads/" + uniqueFileName;
            }
            else
            {
                return new AddMachinePostResponseDTO { Message = "Machine image file is required.", IsSuccess = false };

            }

            try
            {
                Machine machine = addMachinePostRequestDTO.ToMachine(userId, imageFileUrl);
                Machine addedMachine = await _machineRepository.AddMachineAsync(machine);
                return addedMachine.ToAddMachinePostResponseDTO("Machine added successfully.", true);
            }
            catch (Exception ex)
            {
                return new AddMachinePostResponseDTO()
                {
                    Message = $"An error occurred: {ex.Message}",
                    IsSuccess = false,
                };
            }
        }

        public async Task<FetchAllMachinesGetResponseDTO> FetchAllMachinesAsync(int userId, int pageNumber = 1, int pageSize = 10, string sortBy = "id_asc", string? searchString = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                (List<Machine> Data, int TotalCount) result = await _machineRepository.FetchAllMachinesAsync(userId, pageNumber, pageSize, sortBy, searchString);
                int totalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);

                return new FetchAllMachinesGetResponseDTO()
                {
                    Data = result.Data,
                    CurrentPage = pageNumber,
                    TotalPages = totalPages,
                    Message = "Machines retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new FetchAllMachinesGetResponseDTO()
                {
                    Message = $"An error occurred while retrieving the machines: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public async Task<Machine?> FetchMachineAsync(int userId, int machineId)
        {
            return await _machineRepository.FetchMachineAsync(machineId, userId);
        }

        public async Task<EditMachinePatchResponseDTO> EditMachineAsync(EditMachinePatchRequestDTO editMachinePatchRequestDTO, int userId)
        {
            Machine machine = editMachinePatchRequestDTO.ToMachine(userId);
            Machine? updatedMachine = await _machineRepository.EditMachineAsync(machine, userId);

            if (updatedMachine == null) return new EditMachinePatchResponseDTO() { Message = "Machine not found or unauthorized operation.", IsSuccess = false };

            return new EditMachinePatchResponseDTO()
            {
                Message = "The machine has been successfully updated.",
                IsSuccess = true,
                EditedMachineId = updatedMachine.Id
            };
        }

        public async Task<RemoveMachineDeleteResponseDTO> RemoveMachineAsync(int userId, int machineId)
        {
            bool machineIsDeleted = await _machineRepository.RemoveMachineAsync(machineId, userId);

            if (machineIsDeleted) return new RemoveMachineDeleteResponseDTO() { Message = "The machine was successfully deleted.", IsSuccess = true, DeletedMachineId = machineId };
            else return new RemoveMachineDeleteResponseDTO() { Message = "The machine could not be deleted.", IsSuccess = false };
        }
    }
}
