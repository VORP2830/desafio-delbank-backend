using System.Text;
using System.Text.Json;
using LocadoraDVD.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace LocadoraDVD.Infra.Data.Cache
{
    public class Cache : ICache
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly ILogger<Cache> _logger;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(15);

        public Cache(IConfiguration configuration, ILogger<Cache> logger)
        {
            _redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_URL") ?? configuration.GetConnectionString("RedisConnection"));
            _db = _redis.GetDatabase();
            _logger = logger;
        }

        public async Task<bool> Delete<T>(string id)
        {
            try
            {
                var key = typeof(T).Name.ToLower() + ":" + id;
                await _db.KeyDeleteAsync(key);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar o cache Redis");
                throw;
            }
        }

        public async Task<T> Get<T>(string id)
        {
            var key = typeof(T).Name.ToLower() + ":" + id;
            var value = await _db.StringGetAsync(key);

            if (!value.HasValue)
            {
                return default;
            }

            var result = JsonSerializer.Deserialize<T>(value);
            return result;
        }

        public async Task Store<T>(string id, T value, params string[] @params)
        {
            var complexKey = GenerateKeyWithParams(typeof(T).Name.ToLower() + ":" + id, @params);
            var cache = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(complexKey, cache, _cacheDuration);
        }


        private string GenerateKeyWithParams(string key, string[] @params)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Chave não pode ser nula ou vazia", nameof(key));
            }

            var sb = new StringBuilder(key);

            foreach (var param in @params)
            {
                sb.Append('&').Append(param);
            }

            return sb.ToString();
        }

    }
}
