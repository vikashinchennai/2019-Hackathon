using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackahton.Model
{
   public class ValidationErrors
    {
        public ValidationErrors(string section, SEOLevel level, string message)
        {
            Section = section;
            Level = level;
            Message = message;
        }
        public string Section { get; set; }
        public string Message { get; set;}
        public SEOLevel Level { get; set; }
    }

    public enum SEOLevel
    {
        Good,
        Warning,
        NeedFix
    }
}
