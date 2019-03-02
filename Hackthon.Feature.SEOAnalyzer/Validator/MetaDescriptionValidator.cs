
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackahton.Validator
{
    public class MetaDescriptionValidator : ValidatorBase
    {
        public override List<Model.ValidationErrors> HandleValidation(Model.ValidatorRequestModel model)
        {
            string Section = "Meta Description";
            HtmlAgilityPack.HtmlNodeCollection meta = model.Documenthead.DocumentNode.SelectNodes("//meta");
            if (meta == null || meta.Count == 0)
            {
                AddErrorResult(Section, Model.SEOLevel.NeedFix, Section + "Tag not Found");
            }
            else
            {
                bool isFound = false;
                //<meta name="description" content="A "/>
                foreach (HtmlAgilityPack.HtmlNode eachMeta in meta)
                {
                    if (eachMeta.HasAttributes && eachMeta.Attributes["name"] != null && Convert.ToString(eachMeta.Attributes["name"].Value).ToLower() == "description")
                    {
                        if (isFound)
                        {
                            AddErrorResult(Section, Model.SEOLevel.Warning, "Multiple meta Description Tags Found");
                            break;
                        }
                        isFound = true;
                        if (eachMeta.Attributes["content"] != null)
                        {
                            var metaContent = Convert.ToString(eachMeta.Attributes["content"].Value);
                            var len = metaContent.Length;
                            if (len > 300)
                                AddErrorResult(Section, Model.SEOLevel.Warning, "Meta Description is Very Large. Character Count is " + len);
                            if (len < 160)
                                AddErrorResult(Section, Model.SEOLevel.Warning, "Meta Description content is small. Character Count is " + len);

                            if (metaContent.Contains("\""))
                                AddErrorResult(Section, Model.SEOLevel.Warning, "Meta Description contains quotation, Avoid using the same");

                            List<string> metaContentSplit = metaContent.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var counterString = string.Empty;
                            if (model.Keywords != null)
                            {
                                foreach (var keyword in model.Keywords)
                                {
                                    if (metaContentSplit.Any(a => a == keyword))
                                    {
                                        var cnt = metaContentSplit.Count(w => w == keyword);
                                        counterString = string.Format("{0}Keyword: {1} present {2} times.<br/>", counterString, keyword, cnt);
                                    }
                                }
                                AddErrorResult(Section, Model.SEOLevel.Warning, counterString);
                            }
                        }
                    }

                }

                if (!isFound)
                    AddErrorResult(Section, Model.SEOLevel.NeedFix, Section + "Tag not Found");
            }

            SetSuccessMessage(Section);
            model.ValidationErrors.AddRange(ErrorsResult);
            if (Successor != null)
                return Successor.HandleValidation(model);

            return model.ValidationErrors;
        }
    }
}
