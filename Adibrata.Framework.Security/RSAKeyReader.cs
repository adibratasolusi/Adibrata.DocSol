
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.Logging;


namespace Adibrata.Framework.Security
{
    [Serializable]    
   public class RSAKeyReader
    {

        private string encryptKey, decryptKey, _bitstrength;
        public int eBitStrength;
        private int dBitStrength;
        
        public string EncryptKey
        {
            get { return encryptKey; }
        }

        public int EncryptBitStrength
        {
            get { return eBitStrength; }
        }

        public string DecryptKey
        {
            get { return decryptKey; }
        }

        public int DecryptBitStrength
        {
            get { return dBitStrength; }
        }
        
        private const String decryptorKey = "<BitStrength>1024</BitStrength><RSAKeyValue><Modulus>uJpKN2M7t154Gq9iMKRX+wyHfM4gChbfxS2dMkH35Q0mHre/G16z2q3py39U2ucGO53V07r+B6VgAUIX05zfKcUrwGuvpxNL0mFLf/+5Mn/umZd34vSphJu/l/NKs4wHTaEbCB8o3iYsIJjDzuvomX0wvlTJDW3VShCyAWvIgIs=</Modulus><Exponent>AQAB</Exponent><P>9AJly5ERgtGc3eePGvxEyPZgkTuannfD4fApOmLcrC8rP9HE3AchpXOiSdn1+X1VOHuoe9qRZzh1wOsAabBjsw==</P><Q>wayQoszqLmxFoBOebcCaZdA4I/iWxkEjykcVdYUXlSNU+I7kSRCJfbnONx5sYFPcrNmaG6CBJIw8zdAh8VRjyQ==</Q><DP>GgZ/cPziDz/oKUrfWpN5iq2skxD2ZtyDSf/hhdaxjrPhOwdWpdkk3467yoCy2Y9inYmi6MLhK088T/1AqAUmPQ==</DP><DQ>J6KNJwQQmNeHmC7rqUJVVqi6FIJ3OLN0A51wgtBt9xN3/DYh+eHsgVJZJWaQf1YqPyV5KFY8l6Irf2MszxqaIQ==</DQ><InverseQ>b136Kld6Il1Dz3Xriv9WIyD6On6z1CK/dguhcokQ2fMtwoZ3444hFANCUOzwi7vNGLzMbqRzP6VM0rKEhgehOg==</InverseQ><D>Aj/LALLCUwEhKH4TjbEq60GjUvd193mA33enTynzmcRXvw8REoXMXjE5RIP4JjSjZE2PgeVo2/H9YfaKsMbcBzJtHE7sVFC9B7u2ylJtz5wRBzW6imrNDTRz7lZ+WQNRgaExiB+23u696KpZjaiYBWiZwX+0eAaSGi54sg4TvGk=</D></RSAKeyValue>";
        private const String encryptorKey = "<BitStrength>1024</BitStrength><RSAKeyValue><Modulus>uJpKN2M7t154Gq9iMKRX+wyHfM4gChbfxS2dMkH35Q0mHre/G16z2q3py39U2ucGO53V07r+B6VgAUIX05zfKcUrwGuvpxNL0mFLf/+5Mn/umZd34vSphJu/l/NKs4wHTaEbCB8o3iYsIJjDzuvomX0wvlTJDW3VShCyAWvIgIs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        
        public void ReadEncryptKey()
        {
            try
            {
                String fileString = encryptorKey;
                eBitStrength = getBitStrengthDigit(getBitStrengthString(fileString));
                encryptKey = getRSAKeyValue(fileString);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "RSAKeyReader",
                    FunctionName = "ReadEncryptKey",
                    ExceptionNumber = 1,
                    EventSource = "Encryption",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(true, _errent);
            }
        }

        public void ReadDecryptKey()
        {
            try
            {
                String fileString = decryptorKey;
                dBitStrength = getBitStrengthDigit(getBitStrengthString(fileString));
                decryptKey = getRSAKeyValue(fileString);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "RSAKeyReader",
                    FunctionName = "ReadDecryptKey",
                    ExceptionNumber = 1,
                    EventSource = "Encryption",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(true, _errent);
            }
        }

        private string getBitStrengthString(String fileString)
        {
            try
            {
                _bitstrength = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "Encryption",
                    NameSpace = "Adibrata.Framework.Security",
                    ClassName = "RSAKeyReader",
                    FunctionName = "getBitStrengthString",
                    ExceptionNumber = 1,
                    EventSource = "Encryption",
                    ExceptionObject = _exp,
                    EventID = 1,
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(true, _errent);
            }
            return _bitstrength;
        }

        private int getBitStrengthDigit(String BitStrengthString)
        {
            return int.Parse(getBitStrengthString(BitStrengthString).Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
        }

        private string getRSAKeyValue(String fileString)
        {
            return fileString.Substring(fileString.IndexOf("<RSAKeyValue>"));
        }
    }
}
