using Core.Entities;
using Infrastructure.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Repository
{
    public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<bool> DeleteCartAsync(string key)
            => await _database.KeyDeleteAsync(key);

        public async Task<Cart?> GetCartAsync(string key)
        {
            var data = await _database.StringGetAsync(key);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data!);
        }

        public async Task<Cart?> SetCartAsync(Cart cart)
        {
            var created = await _database.StringSetAsync(cart.Id,
                JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetCartAsync(cart.Id);
        }
    }
}
