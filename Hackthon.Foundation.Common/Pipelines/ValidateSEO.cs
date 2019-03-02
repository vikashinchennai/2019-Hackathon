using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Foundation.Common.Pipelines
{
    public class ValidateSEO
    {
        public void Process(ValidateRequestArgument args)
        {
            string text = System.IO.File.ReadAllText(args.FileNmae);

            Model.ValidatorRequestModel model = new Model.ValidatorRequestModel(text);

            var errorsResult = ValidationContext.HtmlValidationContext.Validate(model);

            
        }
    }
}