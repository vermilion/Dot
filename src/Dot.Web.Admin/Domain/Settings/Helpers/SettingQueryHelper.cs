using Cofoundry.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Cofoundry.Domain.Internal
{
    public class SettingQueryHelper
    {
        #region constructor

        private readonly IDbUnstructuredDataSerializer _dbUnstructuredDataSerializer;

        public SettingQueryHelper(
            IDbUnstructuredDataSerializer dbUnstructuredDataSerializer
            )
        {
            _dbUnstructuredDataSerializer = dbUnstructuredDataSerializer;
        }

        #endregion

        public T FindSetting<T>(string key, Dictionary<string, string> allSettings)
        {
            if (!allSettings.ContainsKey(key)) return default;
            var setting = allSettings[key];

            if (string.IsNullOrWhiteSpace(setting)) return default;

            var value = _dbUnstructuredDataSerializer.Deserialize<T>(setting);

            return value;
        }

        public void SetSettingProperty<TSource, TProperty>(
            TSource source,
            Expression<Func<TSource, TProperty>> propertyLambda,
            Dictionary<string, string> allSettings)
        {
            Type type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            var propInfo = member.Member as PropertyInfo;

            var value = FindSetting<TProperty>(propInfo.Name, allSettings);
            propInfo.SetValue(source, value);
        }
    }
}
