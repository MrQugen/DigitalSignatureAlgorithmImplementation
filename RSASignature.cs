using System.Numerics;
using System.Text;

namespace DigitalSignatureAlgorithmImplementation;

public class RSASignature
{
    // Функція для генерації ключової пари
    public static KeyPair KeyGen()
    {
        Random rand = new Random();

        BigInteger p = GeneratePrime(rand);
        BigInteger q = GeneratePrime(rand);

        BigInteger n = p * q;
        BigInteger phi = (p - 1) * (q - 1);

        // Обираємо e, яке не є дільником n та 1 < e < phi
        BigInteger e;
        do
        {
            e = new BigInteger(rand.Next(2, (int)phi - 1));
        } while (BigInteger.GreatestCommonDivisor(e, phi) != 1);

        // Розрахунок d
        BigInteger d = ModInverse(e, phi);

        PublicKey publicKey;
        publicKey.e = e;
        publicKey.n = n;

        PrivateKey privateKey;
        privateKey.d = d;
        privateKey.n = n;

        KeyPair keyPair;
        keyPair.publicKey = publicKey;
        keyPair.privateKey = privateKey;

        return keyPair;
    }

    // Функція для обчислення геш-значення
    private static BigInteger Hash(string message)
    {
        // Простий приклад - сума кодів Unicode символів повідомлення
        byte[] bytes = Encoding.Unicode.GetBytes(message);
        BigInteger hashValue = 0;

        foreach (byte b in bytes)
        {
            hashValue += b;
        }

        return hashValue;
    }

    // Функція для підпису повідомлення
    public static BigInteger Sign(string message, PrivateKey privateKey)
    {
        BigInteger hashedMessage = Hash(message);
        return PowMod(hashedMessage, privateKey.d, privateKey.n);
    }

    // Функція для перевірки цифрового підпису
    public static bool Verify(string message, BigInteger signature, PublicKey publicKey)
    {
        BigInteger hashedMessage = Hash(message);
        BigInteger decryptedSignature = PowMod(signature, publicKey.e, publicKey.n);

        return hashedMessage == decryptedSignature;
    }

    // Функція для генерації великого простого числа
    private static BigInteger GeneratePrime(Random rand)
    {
        // Реалізація простого тесту та генерація великого простого числа
        // (для простоти, у реальному коді слід використовувати більш ефективні методи)
        BigInteger num;
        do
        {
            num = new BigInteger(rand.Next(1000, 5000));
        } while (!IsPrime(num));

        return num;
    }

    // Функція для обчислення квадратного кореня з BigInteger
    private static BigInteger Sqrt(BigInteger n)
    {
        if (n == 0 || n == 1)
            return n;

        BigInteger start = 0, end = n, ans = 0;

        while (start <= end)
        {
            BigInteger mid = (start + end) / 2;
            if (mid * mid == n)
                return mid;

            if (mid * mid < n)
            {
                start = mid + 1;
                ans = mid;
            }
            else
            {
                end = mid - 1;
            }
        }

        return ans;
    }

    // Функція для перевірки, чи є число простим
    private static bool IsPrime(BigInteger num)
    {
        if (num <= 1)
            return false;

        BigInteger sqrtNum = Sqrt(num);

        for (BigInteger i = 2; i <= sqrtNum; i++)
        {
            if (num % i == 0)
                return false;
        }

        return true;
    }


    // Функція для обчислення оберненого елемента в кільці по модулю
    private static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        BigInteger m0 = m;
        BigInteger x0 = 0;
        BigInteger x1 = 1;

        while (a > 1)
        {
            BigInteger q = a / m;
            BigInteger t = m;

            m = a % m;
            a = t;
            t = x0;

            x0 = x1 - q * x0;
            x1 = t;
        }

        if (x1 < 0)
            x1 += m0;

        return x1;
    }

    // Функція для обчислення a^b mod m
    private static BigInteger PowMod(BigInteger a, BigInteger b, BigInteger m)
    {
        BigInteger result = 1;
        a = a % m;

        while (b > 0)
        {
            if (b % 2 == 1)
                result = (result * a) % m;

            b = b >> 1;
            a = (a * a) % m;
        }

        return result;
    }
}