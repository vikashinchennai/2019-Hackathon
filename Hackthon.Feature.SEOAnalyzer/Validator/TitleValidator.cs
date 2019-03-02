using System.Collections.Generic;
using System.Linq;

namespace Hackahton.Validator
{
    public class TitleValidator : ValidatorBase
    {
        public override List<Model.ValidationErrors> HandleValidation(Model.ValidatorRequestModel model)
        {

            var title = model.Documenthead.DocumentNode.SelectNodes("//title");

            if (title == null || title.Count == 0)
            {
                AddErrorResult("Title", Model.SEOLevel.NeedFix, "Title Tag not Found");
            }
            else
            {
                if (title.Count > 1)
                {
                    AddErrorResult("Title", Model.SEOLevel.NeedFix, "Multiple Title Tags Found");
                }
                else
                {
                    var content = title.FirstOrDefault().InnerText;
                    if (string.IsNullOrEmpty(content))
                    {
                        AddErrorResult("Title", Model.SEOLevel.NeedFix, "Tile Tag is Empty");
                    }
                    else
                    {
                        if (content.Contains(" and ") || content.Contains(" or "))
                        {
                            AddErrorResult("Title", Model.SEOLevel.Warning, "Remember to use character savers, like the ampersand(&) symbol instead of \'and\', the slash (/) instead of ‘or\', etc.");
                        }

                        if (content.Length > 65)
                        {
                            AddErrorResult("Title", Model.SEOLevel.NeedFix, "Title Length >65 Characters");
                        }
                        if (content.Length < 35)
                        {
                            AddErrorResult("Title", Model.SEOLevel.Warning, "Title Length >65 Characters");
                        }

                        if (content.Any(char.IsUpper))
                        {
                            AddErrorResult("Title", Model.SEOLevel.Warning, "Avoid using Uppercase Characters");
                        }
                    }
                }
            }

            SetSuccessMessage("Title");
            model.ValidationErrors.AddRange(ErrorsResult);
            if (Successor != null)
                return Successor.HandleValidation(model);

            return model.ValidationErrors;
        }
    }
}
