using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace USAA_Gen2Library
{
    public class RSACryption
    {
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            try
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                xmlKeys = cryptoServiceProvider.ToXmlString(true);
                xmlPublicKey = cryptoServiceProvider.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RSAEncrypt(string publicKey, string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
                return string.Empty;
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentException("Invalid Public Key");
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                cryptoServiceProvider.FromXmlString(publicKey);
                int count = cryptoServiceProvider.KeySize / 8 - 11;
                byte[] buffer1 = new byte[count];
                using (MemoryStream memoryStream1 = new MemoryStream(bytes))
                {
                    using (MemoryStream memoryStream2 = new MemoryStream())
                    {
                        while (true)
                        {
                            int length = memoryStream1.Read(buffer1, 0, count);
                            if (length > 0)
                            {
                                byte[] rgb = new byte[length];
                                Array.Copy((Array)buffer1, 0, (Array)rgb, 0, length);
                                byte[] buffer2 = cryptoServiceProvider.Encrypt(rgb, false);
                                memoryStream2.Write(buffer2, 0, buffer2.Length);
                            }
                            else
                                break;
                        }
                        return Convert.ToBase64String(memoryStream2.ToArray());
                    }
                }
            }
        }

        public string RSADecrypt(string privateKey, string ciphertext)
        {
            if (string.IsNullOrEmpty(ciphertext))
                return string.Empty;
            if (string.IsNullOrEmpty(privateKey))
                throw new ArgumentException("Invalid Private Key");
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                byte[] buffer1 = Convert.FromBase64String(ciphertext);
                cryptoServiceProvider.FromXmlString(privateKey);
                int count = cryptoServiceProvider.KeySize / 8;
                byte[] buffer2 = new byte[count];
                using (MemoryStream memoryStream1 = new MemoryStream(buffer1))
                {
                    using (MemoryStream memoryStream2 = new MemoryStream())
                    {
                        while (true)
                        {
                            int length = memoryStream1.Read(buffer2, 0, count);
                            if (length > 0)
                            {
                                byte[] rgb = new byte[length];
                                Array.Copy((Array)buffer2, 0, (Array)rgb, 0, length);
                                byte[] buffer3 = cryptoServiceProvider.Decrypt(rgb, false);
                                memoryStream2.Write(buffer3, 0, buffer3.Length);
                            }
                            else
                                break;
                        }
                        return Encoding.UTF8.GetString(memoryStream2.ToArray());
                    }
                }
            }
        }

        public bool GetHash(string strSource, ref string strHashData)
        {
            try
            {
                byte[] hash = HashAlgorithm.Create("SHA256").ComputeHash(Encoding.UTF8.GetBytes(strSource));
                strHashData = Convert.ToBase64String(hash);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SignatureFormatter(
          string strKeyPrivate,
          string strHashbyteSignature,
          ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(strHashbyteSignature);
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.FromXmlString(strKeyPrivate);
                RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter((AsymmetricAlgorithm)cryptoServiceProvider);
                signatureFormatter.SetHashAlgorithm("SHA256");
                byte[] signature = signatureFormatter.CreateSignature(rgbHash);
                strEncryptedSignatureData = Convert.ToBase64String(signature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SignatureDeformatter(
          string strKeyPublic,
          string strHashbyteDeformatter,
          string strDeformatterData)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(strHashbyteDeformatter);
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.FromXmlString(strKeyPublic);
                RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter((AsymmetricAlgorithm)cryptoServiceProvider);
                signatureDeformatter.SetHashAlgorithm("SHA256");
                byte[] rgbSignature = Convert.FromBase64String(strDeformatterData);
                return signatureDeformatter.VerifySignature(rgbHash, rgbSignature);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
