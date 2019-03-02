using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Feature.SEOAnalyzer.Models
{
    public class ItemModel
    {
        public string GUID { get; set; }

        public string language { get; set; }
        public string version { get; set; }
        public string site { get; set; }

        public partial class Menu_Item
        {
            public bool IsActive { get; set; }
        }

    }
}