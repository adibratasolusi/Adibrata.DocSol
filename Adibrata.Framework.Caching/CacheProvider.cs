using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Adibrata.Framework.Caching
{
 public class DataCache
    {
     const int CacheDuration = 60;

        public static void Insert<T>(string key, T value)
        {
            lock (MemoryCache.Default)
            {
                MemoryCache.Default.Remove(key.ToString());
                DateTimeOffset expires = new DateTimeOffset(DateTime.Now.AddMinutes(CacheDuration));
                MemoryCache.Default.Add(key.ToString(), value, expires);
            }
        }

        public static void Insert<T>(string key, int subKey, T value)
        {
            lock (MemoryCache.Default)
            {
                MemoryCache.Default.Remove(string.Format("{0}-{1}", key.ToString(), subKey));
                DateTimeOffset expires = new DateTimeOffset(DateTime.Now.AddMinutes(CacheDuration));
                MemoryCache.Default.Add(string.Format("{0}-{1}", key.ToString(), subKey), value, expires);
            }
        }

        public static T Get<T>(string key)
        {
            if (MemoryCache.Default.Contains(key.ToString()))
            {
                return (T)MemoryCache.Default.Get(key.ToString());
            }
            else
            {
                return default(T);
            }
        }

        public static T Get<T>(string key, int subKey)
        {
            if (MemoryCache.Default.Contains(string.Format("{0}-{1}", key.ToString(), subKey)))
            {
                return (T)MemoryCache.Default.Get(string.Format("{0}-{1}", key.ToString(), subKey));
            }
            else
            {
                return default(T);
            }
        }

        public static bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key.ToString());
        }

        public static bool Contains(string key, int subKey)
        {
            return MemoryCache.Default.Contains(string.Format("{0}-{1}", key.ToString(), subKey));
        }

        public static void Remove(string key)
        {
            MemoryCache.Default.Remove(key.ToString());
        }

        public static void Remove(string key, int subKey)
        {
            MemoryCache.Default.Remove(string.Format("{0}-{1}", key.ToString(), subKey));
        }
    }
}


