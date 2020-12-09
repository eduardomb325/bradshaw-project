using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Objects
{
    public class RuleVerification
    {
        public bool IsValidationPass;

        public string ErrorMessage;

        public RuleVerification(bool isValidationPass, string message)
        {
            IsValidationPass = isValidationPass;
            ErrorMessage = message;
        }
    }
}
