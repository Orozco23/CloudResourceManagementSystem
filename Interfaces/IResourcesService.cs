using CloudResourceManagementSystem.DTOs;

namespace CloudResourceManagementSystem.Interfaces
{
    public interface IResourcesService
    {
        Task<List<ResourceResponseDto>> GetAll();
        Task<List<ResourceResponseDto>> Sort(string sortBy);
        Task<List<ResourceResponseDto>> Filter(string property, string input);
    }
}
