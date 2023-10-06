using System.Diagnostics;
using System.Web.Mvc;
using BasicCompany.Feature.MyComponents.Services;
using Sitecore.Mvc.Presentation;
using BasicCompany.Feature.MyComponents.Model;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Mvc.Extensions;
using System;
using Sitecore.Globalization;
using System.Web.UI.WebControls;
using Sitecore.Links;
using BasicCompany.Feature.MyComponents.Model;

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

            Sitecore.Data.Fields.ImageField imageField = heroBannerItem.Fields["Body Image"];
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


        public ActionResult Carousel()
        {
            var model = new CarouselModel();
            List<Slide> slides = new List<Slide>();
            var dataSource = RenderingContext.Current?.Rendering.Item;
            if (dataSource == null) { return View(); }
            CheckboxField showLabelsField = dataSource.Fields["Show Labels"];

            model.Previous = Translate.TextByDomain("Company Dictionary", "Previous");
            model.Next = Translate.TextByDomain("Company Dictionary", "Next");
            model.ShowLabels = showLabelsField != null && showLabelsField.Checked;

            MultilistField slidesField = dataSource.Fields["Slides"];

            if (slidesField?.Count > 0)
            {
                var slideItems = slidesField.GetItems();
                foreach (var slideItem in slideItems)
                {
                    var title = new MvcHtmlString(FieldRenderer.Render(
                            slideItem, "Slide Title"));

                    var subTitle = new MvcHtmlString(FieldRenderer.Render(
                        slideItem, "Slide Body"));

                    var image = new MvcHtmlString(FieldRenderer.Render(
                            slideItem, "Slide Image", "class=img-fluid"));

                    var callToAction = new MvcHtmlString(FieldRenderer.Render(
                        slideItem, "Slide Link",
                        "class=btn animated fadeInUp"));

                    slides.Add(new Slide
                    {
                        Title = title,
                        SubTitle = subTitle,
                        Image = image,
                        CallToAction = callToAction
                    });
                }
                model.Slides = slides;
            }

            return View(model);
        }


        public ActionResult Header()
        {            
            var contextItem = RenderingContext.Current.ContextItem;
            var homeItem = contextItem.Database.GetItem("/sitecore/content/Basic Company/Home");
            var navigationId = new Sitecore.Data.ID("{11B89155-ECC0-416E-A327-D5D4AC92688B}");

            var navigationItems = homeItem.GetChildren()
                                            .Where(item => item.DescendsFrom(navigationId))
                                            .Select(item => new NavigationModel
                                            {
                                                Item = item,
                                                Url = LinkManager.GetItemUrl(item),
                                            })
                                            .ToList();

            var header = new HeaderModel
            {
                HomeItem = homeItem,
                HomeUrl = LinkManager.GetItemUrl(homeItem),
                NavigationItems = navigationItems
            };

            return View(header);
        }
    }
}
