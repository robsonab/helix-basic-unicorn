using System.Diagnostics;
using System.Web.Mvc;
using BasicCompany.Feature.MyComponents.Services;
using Sitecore.Mvc.Presentation;
using BasicCompany.Feature.MyComponents.Model;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;

namespace BasicCompany.Feature.MyComponents.Controllers
{
    public class ProductsController : Controller
    {
        
        public ProductsController()
        {            
        
        }

        public ActionResult HeroBanner()
        {
            var heroBannerItem = RenderingContext.CurrentOrNull.Rendering.Item;
            var result = new HeroBannerModel();

            ImageField imageField = heroBannerItem.Fields["Body Image"];
            if (imageField == null || imageField.MediaItem == null)
            {
                result.ImageBackground = "";
            }
            else
            {
                result.ImageBackground = MediaManager.GetMediaUrl(imageField.MediaItem);
            }
            return View(result);
        }
    }
}