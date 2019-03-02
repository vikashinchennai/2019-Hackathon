using Hackahton.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackahton.Validator
{
    public class KeywordValidator : ValidatorBase
    {
        public override List<ValidationErrors> HandleValidation(Model.ValidatorRequestModel model)
        {
            string Section = "Keywords";
            HtmlAgilityPack.HtmlNodeCollection meta = model.Documenthead.DocumentNode.SelectNodes("//meta");
            if (meta == null || meta.Count == 0)
            {
                AddErrorResult(Section, Model.SEOLevel.NeedFix, Section + "Tag not Found");
            }
            else
            {
                bool isFound = false;
                // <meta name="keywords" content="C# (CSharp),code,examples,,,,HtmlAgilityPack,SelectNodes,0">
                foreach (HtmlAgilityPack.HtmlNode eachMeta in meta)
                {
                    if (eachMeta.HasAttributes && eachMeta.Attributes["name"] != null && Convert.ToString(eachMeta.Attributes["name"].Value).ToLower() == "keywords")
                    {
                        if (isFound)
                        {
                            AddErrorResult(Section, Model.SEOLevel.Warning, "Multiple meta Keywords Tags Found");
                            break;
                        }
                        isFound = true;
                        if (eachMeta.Attributes["content"] != null)
                        {
                            var metaContent = Convert.ToString(eachMeta.Attributes["content"].Value);
                            var Keywords = metaContent.Split(',');
                            if (Keywords.Length == 0)
                                AddErrorResult(Section, Model.SEOLevel.NeedFix, "Meta Keywords not found");
                            if (Keywords.Length < 5)
                                AddErrorResult(Section, Model.SEOLevel.Warning, "Meta Keywords are very Few. Please Add More. Found Only " + Keywords.Length);
                            if (Keywords.Any(f => string.IsNullOrEmpty(f)))
                            {
                                AddErrorResult(Section, Model.SEOLevel.Warning,
                                    "Meta Keywords Contains Empty Values too. Empty Keys" + Keywords.Count(f => string.IsNullOrEmpty(f)));
                            }
                            model.Keywords = Keywords;
                        }
                    }
                }
                if (!isFound)
                    AddErrorResult(Section, SEOLevel.NeedFix, Section + "Tag not Found");
            }
            SetSuccessMessage(Section);
            model.ValidationErrors.AddRange(ErrorsResult);
            if (Successor != null)
                return Successor.HandleValidation(model);

            return model.ValidationErrors;
        }
    }
}