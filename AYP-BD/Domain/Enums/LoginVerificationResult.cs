using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum LoginVerificationResult
    {
        Succes = 0,
        WrongPassword = 1,
        UserBanned = 2,
    }
}
