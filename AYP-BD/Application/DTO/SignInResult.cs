using Domain.Enums;

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
