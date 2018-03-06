using System;
using Microsoft.VisualBasic;

namespace Msr.Infrastructure.Helpers
{
    public class AuthenticationHelper
    {
        public static string PasswordEncrypt(string password)
        {
            var encryptPassword = string.Empty;

            for (var i = 1; i <= password.Length; i++)
            {
                encryptPassword = encryptPassword + Strings.Chr(77 + Strings.Asc(Strings.Mid(password, i, 1)) % 128);
            }

            return encryptPassword;
        }

        public static int GetPassword(int intLetters)
        {
            int uLim = 90; int lLim = 48, myInt = 0;
            Random Rnd = new Random();
            for (int i = 1; i <= intLetters; i++)
            {
                myInt = Convert.ToInt32((uLim - lLim + 1) * Convert.ToInt32(Rnd.Next(lLim, uLim)) + lLim);

                while ((myInt >= 48 && myInt <= 57) || (myInt >= 65 && myInt <= 90))
                {
                    myInt = Convert.ToInt32((uLim - lLim + 1) * Convert.ToInt32(Rnd.Next(intLetters) + lLim));
                }
                myInt = char.ToLower(Convert.ToChar(myInt));
            }

            return myInt;
        }
    }
}
