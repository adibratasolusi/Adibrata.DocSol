using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.Logging;
using System.Security.Cryptography;
using System.Collections;
using HashLib;

namespace Adibrata.Framework.Security
{
    public static class Encryption
    {
        private static string _decryption;
        private static string _encryption;
        public static string EncryptToSHA3 (string _value) // encrypt on way. Use for password only
        {
            try
            {
                IHash hash;
                hash = HashFactory.Crypto.SHA3.CreateKeccak(HashSize.HashSize224);
                _encryption = hash.ComputeString(_value, Encoding.ASCII).ToString();
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "Encryption",
                    FunctionName = "EncryptToSHA3",
                    ExceptionNumber = 1,
                    EventSource = "Encryption",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(true, _errent);
            }
            return _encryption;
        } 
        
        public static string EncryptToRSA(string _value)
        {
          
            try
            {
                RSAKeyReader kr = new RSAKeyReader();
                kr.ReadEncryptKey();
                int _strengthkey = kr.EncryptBitStrength;
                string _encryptkey = kr.EncryptKey;
                _encryption = EncryptString(_value, _strengthkey, _encryptkey);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "Encryption",
                    FunctionName = "EncryptToRCA",
                    ExceptionNumber = 1,
                    EventSource = "Encryption",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(true, _errent);


            }
            return _encryption;
        }

        public static string DecryptFromRSA (string _value)
        {
            RSAKeyReader kr = new RSAKeyReader();
            try
            {
                kr.ReadDecryptKey();
                _decryption=  DecryptString(_value, kr.DecryptBitStrength, kr.DecryptKey);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "Encryption",
                    FunctionName = "DecryptFromRSA",
                    ExceptionNumber = 1,
                    EventSource = "Security",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);

            }
            return _decryption;

        }

        private static string EncryptString(string inputString, int dwKeySize, string xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                RSACryptoServiceProvider rsaCryptoServiceProvider =
                                              new RSACryptoServiceProvider(dwKeySize);
                rsaCryptoServiceProvider.FromXmlString(xmlString);
                int keySize = dwKeySize / 8;
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                // The hash function in use by the .NET RSACryptoServiceProvider here 
                // is SHA1
                // int maxLength = ( keySize ) - 2 - 
                //              ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
                int maxLength = keySize - 42;
                int dataLength = bytes.Length;
                int iterations = dataLength / maxLength;
                
                for (int i = 0; i <= iterations; i++)
                {
                    byte[] tempBytes = new byte[
                            (dataLength - maxLength * i > maxLength) ? maxLength :
                                                          dataLength - maxLength * i];
                    Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                      tempBytes.Length);
                    byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                              true);
                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes. It does this after encryption and before 
                    // decryption. If you do not require compatibility with Microsoft 
                    // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                    // next line and the corresponding one in the DecryptString function.
                    Array.Reverse(encryptedBytes);
                    // Why convert to base 64?
                    // Because it is the largest power-of-two base printable using only 
                    // ASCII characters
                    stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "Encryption",
                    FunctionName = "EncryptString",
                    ExceptionNumber = 1,
                    EventSource = "Encryption",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return stringBuilder.ToString();
        }

        private static string DecryptString(string inputString, int dwKeySize, string xmlString)
        {
            // TODO: Add Proper Exception Handlers
            ArrayList arrayList = new ArrayList();
            try
            {
                RSACryptoServiceProvider rsaCryptoServiceProvider
                                         = new RSACryptoServiceProvider(dwKeySize);
                rsaCryptoServiceProvider.FromXmlString(xmlString);
                int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ?
                  (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
                int iterations = inputString.Length / base64BlockSize;
                
                for (int i = 0; i < iterations; i++)
                {
                    byte[] encryptedBytes = Convert.FromBase64String(
                         inputString.Substring(base64BlockSize * i, base64BlockSize));
                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic 
                    // API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the 
                    // EncryptString function.
                    Array.Reverse(encryptedBytes);
                    arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                        encryptedBytes, true));
                }
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "Encryption",
                    FunctionName = "DecryptString",
                    ExceptionNumber = 1,
                    EventSource = "Security",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return Encoding.UTF8.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }
    }
}
