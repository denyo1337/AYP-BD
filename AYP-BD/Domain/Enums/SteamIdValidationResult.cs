using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum SteamIdValidationResult
    {
        Ok = 0,
        ErrorOnPeriod = 1,
        SteamIDTaken = 2,
        DoestNotExist = 3,
        Error = 4
    }
}
