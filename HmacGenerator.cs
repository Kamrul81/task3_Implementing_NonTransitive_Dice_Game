using System.Security.Cryptography;
using System.Text;

public class HmacGenerator
{
    public string GenerateKey()
    {
        byte[] key = new byte[32]; // 256-bit key
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);
        return Convert.ToHexString(key);
    }

    public string CalculateHmac(string message, string keyHex)
    {
        byte[] key = Convert.FromHexString(keyHex);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);

        using var hmac = new HMACSHA256(key);
        byte[] hash = hmac.ComputeHash(messageBytes);
        return Convert.ToHexString(hash);
    }
}