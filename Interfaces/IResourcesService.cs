using CloudResourceManagementSystem.DTOs;

namespace CloudResourceManagementSystem.Interfaces
{
    public interface IResourcesService
    {
        Task<List<ResourceResponseDto>> GetAll();
    }
}
