using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Feature.SEOAnalyzer.SEO.Validator
{
    public class OgTagsValidator : ValidatorBase
    {
        public override List<Model.ValidationErrors> HandleValidation(Model.ValidatorRequestModel model)
        {
            string Section = "OG Tags";
            HtmlAgilityPack.HtmlNodeCollection meta = model.Documenthead.DocumentNode.SelectNodes("//meta");
            if (meta == null || meta.Count == 0)
            {
                AddErrorResult(Section, Model.SEOLevel.NeedFix, Section + "Tag not Found");
            }
            else
            {
                bool isOgTitleFound = false, isOgtypeFound = false, isOgUrlFound = false;
                //<meta name="description" content="A "/>
                foreach (HtmlAgilityPack.HtmlNode eachMeta in meta)
                {
                    if (eachMeta.HasAttributes && eachMeta.Attributes["property"] != null)
                    {
                        var propValue = Convert.ToString(eachMeta.Attributes["property"].Value).ToLower();
                        if (propValue == "og:title")
                        {
                            if (isOgTitleFound)
                            {
                                AddErrorResult("OG Title", Model.SEOLevel.Warning, "Multiple OG:Title Tags Found");
                            }
                            else
                            {
                                isOgTitleFound = true;
                                if (eachMeta.Attributes["content"] != null)
                                {
                                    var metaContent = Convert.ToString(eachMeta.Attributes["content"].Value);
                                    var len = metaContent.Length;
                                    if (len > 90)
                                        AddErrorResult(Section, Model.SEOLevel.Warning, "OG:TITLE is Very Large. Character Count is " + len);
                                    if (len < 60)
                                        AddErrorResult(Section, Model.SEOLevel.Warning, "OG:Title content is small. Character Count is " + len);

                                }
                            }
                        }
                        else if (propValue == "og:type")
                        {
                            if (isOgtypeFound)
                            {
                                AddErrorResult("OG Type", Model.SEOLevel.Warning, "Multiple OG:type Tags Found");
                            }
                            else
                            {
                                isOgtypeFound = true;
                                if (eachMeta.Attributes["content"] != null)
                                {
                                    var metaContent = Convert.ToString(eachMeta.Attributes["content"].Value);
                                    var len = metaContent.Length;
                                    if (len < 1)
                                        AddErrorResult(Section, Model.SEOLevel.Warning, "OG:type content is small. Character Count is " + len);

                                }
                            }
                        }
                        else if (propValue == "og:url")
                        {
                            if (isOgUrlFound)
                            {
                                AddErrorResult("OG Url", Model.SEOLevel.Warning, "Multiple OG:url Tags Found");
                            }
                            else
                            {
                                isOgUrlFound = true;
                                if (eachMeta.Attributes["content"] != null)
                                {
                                    var metaContent = Convert.ToString(eachMeta.Attributes["content"].Value);
                                    var len = metaContent.Length;
                                    if (len < 1)
                                        AddErrorResult(Section, Model.SEOLevel.Warning, "OG:url content is small. Character Count is " + len);
                                }
                            }
                        }

                    }
                }

                if (!isOgTitleFound)
                    AddErrorResult("OG Title", Model.SEOLevel.NeedFix, "OG Title Tag not Found");
                if (!isOgtypeFound)
                    AddErrorResult("OG Type", Model.SEOLevel.NeedFix, "OG Type Tag not Found");
                if (!isOgUrlFound)
                    AddErrorResult("OG Url", Model.SEOLevel.NeedFix, "OG Url Tag not Found");
            }

            SetSuccessMessage(Section);
            model.ValidationErrors.AddRange(ErrorsResult);
            if (Successor != null)
                return Successor.HandleValidation(model);

            return model.ValidationErrors;
        }
    }
}