using System.Numerics;

namespace DigitalSignatureAlgorithmImplementation;

// Структура для збереження приватного ключа
public struct PrivateKey
{
    public BigInteger d;
    public BigInteger n;
}