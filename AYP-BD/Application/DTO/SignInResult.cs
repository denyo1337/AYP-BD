using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class SignInResultDto
    {
        public string Token { get; set; } = null;
        public LoginVerificationResult VerificationResult { get; set; }
        public SignInResultDto(string token, LoginVerificationResult verificationResult)
        {
            Token = token;
            VerificationResult = verificationResult;
        }
        public SignInResultDto(LoginVerificationResult result)
        {
            VerificationResult = result;
        }
    }
}
