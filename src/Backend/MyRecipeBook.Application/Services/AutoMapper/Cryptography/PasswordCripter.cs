using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.AutoMapper.Cryptography
{
    public class PasswordCripter
    {
        public string Emcrypt(string password)
        {
            var newPassword = password + "MyRecipeBook2025@";

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA512.HashData(bytes);
            return ConvertToHexString(hashBytes);
        }

        //Converte um array de bytes em uma string hexadecimal
        private static string ConvertToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
