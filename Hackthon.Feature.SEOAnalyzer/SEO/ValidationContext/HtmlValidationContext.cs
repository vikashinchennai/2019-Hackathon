using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Feature.SEOAnalyzer.SEO.ValidationContext
{
    /// <summary>
    ///  Validate Request Model
    /// </summary>
    public class HtmlValidationContext
    {
        public static List<Model.ValidationErrors> Validate(ValidatorRequestModel model)
        {
            OgTagsValidator ogTagsValidator = new OgTagsValidator();

            MetaDescriptionValidator metaDescriptionValidator = new MetaDescriptionValidator();
            metaDescriptionValidator.SetSuccessor(ogTagsValidator);

            KeywordValidator keywordValidator = new KeywordValidator();
            keywordValidator.SetSuccessor(metaDescriptionValidator);

            TitleValidator titlevalidator = new TitleValidator();
            titlevalidator.SetSuccessor(keywordValidator);


            return titlevalidator.HandleValidation(model);
        }
    }
}