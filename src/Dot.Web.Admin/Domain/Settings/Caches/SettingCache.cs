﻿using Dot.Caching.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class SettingCache : ISettingCache
    {
        private const string CACHEKEY = "COF_Settings";
        private const string SETTING_TABLE_KEY = "SettingTable";

        private readonly IObjectCache _cache;
        public SettingCache(IObjectCacheFactory cacheFactory)
        {
            _cache = cacheFactory.Get(CACHEKEY);
        }

        public async Task<Dictionary<string, string>> GetOrAddSettingsTableAsync(Func<Task<Dictionary<string, string>>> getter)
        {
            return await _cache.GetOrAddAsync(SETTING_TABLE_KEY, getter);
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
