using System;
using System.Text;

namespace Email_Template.UTIL
{
    public static class CriptografarSenha
    {
        private readonly static string key = "passwordemailval";

        public static string EncodePassword(string senha)
        {
            senha += key;
            var passBytes = Encoding.UTF8.GetBytes(senha);

            return Convert.ToBase64String(passBytes);
        }

        public static string DecodePassword(string passEncoded)
        {
            var passEncodedBytes = Convert.FromBase64String(passEncoded);
            var password = Encoding.UTF8.GetString(passEncodedBytes);

            password = password.Substring(0, password.Length - key.Length);

            return password;
        }
    }
}
