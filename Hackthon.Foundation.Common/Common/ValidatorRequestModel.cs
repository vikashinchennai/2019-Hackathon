using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Foundation.Common.Common
{
    /// <summary>
    /// Validation Request Model
    /// </summary>
    public class ValidatorRequestModel
    {
        public ValidatorRequestModel(string htmlSource)
        {
            ValidationErrors = new List<ValidationErrors>();
            if (!string.IsNullOrEmpty(htmlSource))
            {
                try
                {
                    HtmlDocument = new HtmlDocument();
                    HtmlDocument.LoadHtml(htmlSource);

                    IsValidHtml = HtmlDocument != null;
                    if (IsValidHtml)
                    {
                        if (HtmlDocument.DocumentNode != null && HtmlDocument.DocumentNode.SelectSingleNode("//head") != null)
                        {
                            var innerHtml = HtmlDocument.DocumentNode.SelectSingleNode("//head").InnerHtml;
                            Documenthead = new HtmlDocument();
                            Documenthead.LoadHtml(innerHtml);
                        }
                        else
                        {
                            ValidationErrors.Add(new ValidationErrors("Html Structure", SEOLevel.Warning, "Html Head Section is Missing"));
                        }
                    }
                    else
                    {
                        ValidationErrors.Add(new ValidationErrors("Html Structure", SEOLevel.Warning, "Html Content is Missing"));
                    }
                }
                catch (Exception ex)
                {
                    ValidationErrors.Add(new ValidationErrors("Html Structure", SEOLevel.Warning, ex.Message));
                }
            }

        }
        public bool IsValidHtml { private get; set; }
        public HtmlDocument HtmlDocument { private set; get; }
        public String[] Keywords { get; set; }
        public HtmlDocument Documenthead { private set; get; }
        public List<ValidationErrors> ValidationErrors { get; set; }
    }
}