using Hackahton.Model;
using Hackahton.Validator;
using System.Collections.Generic;

namespace Hackahton.ValidationContext
{
    public static class HtmlValidationContext
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