using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Helpers.Abstractions
{
    public interface IAuthenticationHelper
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string CreateRandomPassword(int length = 15);
    }
}
