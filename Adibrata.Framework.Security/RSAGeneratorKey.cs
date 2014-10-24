using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Adibrata.Framework.Security
{
    public static class RSAGeneratorKey
    {
        public static string RSAPrivateKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string privateKey = rsa.ToXmlString(true);
            return privateKey;
        }

        public static string RSAPublicKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string publicKey = rsa.ToXmlString(false);
            return publicKey;
        }
    }
}
