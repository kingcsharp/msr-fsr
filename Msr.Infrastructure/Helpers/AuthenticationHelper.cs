using System;
using Microsoft.VisualBasic;

namespace Msr.Infrastructure.Helpers
{
    public class AuthenticationHelper
    {
        public static string PassWordEncrypt(string password)
        {
            var encryptPassword = string.Empty;

            for (var i = 1; i <= password.Length; i++)
            {
               encryptPassword = encryptPassword + Strings.Chr(77 + Strings.Asc(Strings.Mid(password, i, 1)) % 128);
            }

            return encryptPassword;
        }
        
}
}
