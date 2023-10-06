using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;


namespace BasicCompany.Feature.MyComponents.Model
{
    public class HeaderModel
    {
        public Item HomeItem { get; set; }
        public string HomeUrl { get; set; }
        public IList<NavigationModel> NavigationItems { get; set; }
    }

}