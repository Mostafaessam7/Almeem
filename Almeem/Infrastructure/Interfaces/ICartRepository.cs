using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(string id);
        Task<Cart?> SetCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(string id);
    }
}
