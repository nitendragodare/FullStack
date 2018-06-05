using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductDetailMessage.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ProductDetailMessage.Components
{
    [ViewComponent(Name = "ProductDetailMessage")]
    public class WidgetsProductDetailMessageViewComponent : NopViewComponent
    {
        private readonly ProductDetailMessageSettings _productDetailMessageSettings;

        public WidgetsProductDetailMessageViewComponent(ProductDetailMessageSettings productDetailMessageSettings)
        {
            this._productDetailMessageSettings = productDetailMessageSettings;
        }

        public IViewComponentResult Invoke()
        {
            var model = new PublicInfoModel
            {
                Message = _productDetailMessageSettings.Message
            };

            return View("~/Plugins/Widgets.ProductDetailMessage/Views/PublicInfo.cshtml", model);
        }
    }
}
