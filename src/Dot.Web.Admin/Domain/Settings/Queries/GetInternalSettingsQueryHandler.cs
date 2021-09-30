﻿using Cofoundry.Domain.CQS;
using Dot.Web.Admin.Domain.Setup.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class GetInternalSettingsQueryHandler
     : IRequestHandler<GetSettingsQuery<InternalSettings>, InternalSettings>
    {
        #region constructor

        private readonly SettingQueryHelper _getSettingQueryHelper;
        private readonly IInternalSettingsRepository _internalSettingsRepository;

        public GetInternalSettingsQueryHandler(
            SettingQueryHelper getSettingQueryHelper,
            IInternalSettingsRepository internalSettingsRepository
            )
        {
            _getSettingQueryHelper = getSettingQueryHelper;
            _internalSettingsRepository = internalSettingsRepository;
        }

        #endregion

        #region execution

        public async Task<InternalSettings> ExecuteAsync(GetSettingsQuery<InternalSettings> query, IExecutionContext executionContext)
        {
            var allSettings = await _internalSettingsRepository.GetAllSettingsAsync();
            return MapSettings(allSettings);
        }

        #endregion

        #region helpers

        private InternalSettings MapSettings(Dictionary<string, string> allSettings)
        {
            var settings = new InternalSettings();

            _getSettingQueryHelper.SetSettingProperty(settings, s => s.IsSetup, allSettings);

            return settings;
        }

        #endregion
    }
}
