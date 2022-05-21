using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Repositories.Database
{
    public static class Cache
    {
        static ObjectCache cache = MemoryCache.Default;

        public static object get(string chave)
        {
            return cache.Get(chave);
        }

        public static void add(string chave, object objeto, int segundos)
        {
            CacheItemPolicy politica = new CacheItemPolicy();
            politica.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(segundos);
            cache.Add(chave, objeto, politica);
        }

        public static void remove(string chave)
        {
            cache.Remove(chave);
        }
    }
}
