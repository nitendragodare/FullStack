using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.ProductDetailMessage
{
    /// <summary>
    /// Live person provider
    /// </summary>
    public class ProductDetailMessagePlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public ProductDetailMessagePlugin(ISettingService settingService, IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "productdetails_bottom" };
        }

        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "ProductDetailMessage";
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/WidgetsProductDetailMessage/Configure";
        }
        
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new ProductDetailMessageSettings
            {
                Message = ""
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductDetailMessage.Message", "Message");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductDetailMessage.Message.Hint", "Enter your Message here.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<ProductDetailMessageSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.ProductDetailMessage.Message");
            this.DeletePluginLocaleResource("Plugins.Widgets.ProductDetailMessage.Message.Hint");

            base.Uninstall();
        }

        #endregion
    }
}
