using System.Text;

namespace MSR.Infrastructure.Tests
{
    public static class Constants
    {
        public static string GoodPassword => "GoodPassword";
        public static string FailPassword => "FailPassword";
        public static string ExceptionPassword => "ExceptionPassword";

        public static string GoodUserName => "GoodUserName";
        public static string FailUserName => "FailUserName";
        public static string ExceptionUserName => "ExceptionUserName";

        public static byte[] GoodHash => Encoding.UTF8.GetBytes(@"$���^3^Ⱦ����Cn�~�e-詬��2�r���G�8��E�S0[������v��Re,Ê");
        public static string FailHash => "";
        public static string ExceptionHash => "";


        public static byte[] GoodSalt => Encoding.UTF8.GetBytes(@"°����$k�k��l�Oc⊇nP{��߲!+�Y�y��}﫥Y���/Uyq�<$�H��C�Ƌ��=��L(�+Gbv�4�ZE��K��");
        public static string FailSalt => "";
        public static string ExceptionSalt => "";

    }
}
