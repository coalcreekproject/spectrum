using Microsoft.AspNet.Identity;

namespace Spectrum.Data.Core.Utilities
{
    public class SpectrumPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return PasswordUtility.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var result = PasswordUtility.ComparePasswordToHash(hashedPassword, providedPassword);

            if (result)
                return PasswordVerificationResult.Success;
            
            return PasswordVerificationResult.Failed;
        }
    }
}
