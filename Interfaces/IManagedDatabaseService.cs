using CloudResourceManagementSystem.DTOs;

namespace CloudResourceManagementSystem.Interfaces
{
    public interface IManagedDatabaseService
    {
        Task Create(ManagedDatabaseRequestDto dto);
        Task<IEnumerable<ManagedDatabaseResponseDto>> GetAll();
    }
}
