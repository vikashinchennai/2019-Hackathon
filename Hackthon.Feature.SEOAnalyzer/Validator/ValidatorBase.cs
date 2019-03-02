using Hackahton.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackahton.Validator
{
    public abstract class ValidatorBase
    {
        public ValidatorBase Successor { get; private set; }
        public List<ValidationErrors> ErrorsResult { get; set; }
        protected ValidatorBase()
        {
            ErrorsResult = new List<ValidationErrors>();
        }
        public abstract List<ValidationErrors> HandleValidation(Model.ValidatorRequestModel model);

        /// <summary>
        /// Set next validation
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(ValidatorBase successor)
        {
            this.Successor = successor;
        }
        public void SetSuccessMessage(String sectionName, bool ignoreEmptyCheck = false)
        {
            if (!ErrorsResult.Any(f => f.Section == sectionName) || (ignoreEmptyCheck))
                ErrorsResult.Add(new ValidationErrors(sectionName, SEOLevel.Good, sectionName + " is Good"));
        }

        public void AddErrorResult(string sectionName, SEOLevel level, string message)
        {
            ErrorsResult.Add(new ValidationErrors(sectionName, level, message));
        }        
    }
}
