using CloudResourceManagementSystem.DTOs;

namespace CloudResourceManagementSystem.Interfaces
{
    public interface IVirtualMachineService
    {
        Task Create(VirtualMachineRequestDTO dto);
    }
}
