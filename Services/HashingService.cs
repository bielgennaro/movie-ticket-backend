// HashingService.cs

using System.Security.Cryptography;
using System.Text;

public class HashingService
{
    public byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[32];
            rng.GetBytes(salt);
            return salt;
        }
    }

    public byte[] CombinePasswordAndSalt(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

        return combinedBytes;
    }

    public byte[] ComputeHash(byte[] data)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(data);
        }
    }
}
