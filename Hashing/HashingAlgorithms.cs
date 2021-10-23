using System;

namespace LicentaApi.Hashing
{
    public class HashingAlgorithms
    {
         public void CreateHash(String Password, out byte[] Hash, out byte[] Salt)
        //Creates a hash and a salt from password using sha1 algorithm
        {
            var hmac = new System.Security.Cryptography.HMACSHA1();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            hmac.Dispose();
        }

        public bool VerifyHash(String Password, byte[] Hash, byte[] Salt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA1(Salt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != Hash[i])
                {
                    return false;
                }

            }
            return true;
        }
    }
}