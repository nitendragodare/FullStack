using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductDetailMessage.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ProductDetailMessage.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class WidgetsProductDetailMessageController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly ProductDetailMessageSettings _productDetailMessageSettings;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Ctor

        public WidgetsProductDetailMessageController(ISettingService settingService, 
            ILocalizationService localizationService, 
            ProductDetailMessageSettings productDetailMessageSettings,
            IPermissionService permissionService)
        {
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._productDetailMessageSettings = productDetailMessageSettings;
            this._permissionService = permissionService;
        }

        #endregion

        #region Methods
        
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new ConfigurationModel
            {
                Message = _productDetailMessageSettings.Message
            };

            return View("~/Plugins/Widgets.ProductDetailMessage/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            //save settings
            _productDetailMessageSettings.Message = model.Message;

            _settingService.SaveSetting(_productDetailMessageSettings);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
        
        #endregion
    }
}