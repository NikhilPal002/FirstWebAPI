using FirstWebAPI.Models.Domain;

namespace FirstWebAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync();
    }
}