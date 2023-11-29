using System.Numerics;

namespace DigitalSignatureAlgorithmImplementation;

// Структура для збереження публічного ключа
public struct PublicKey
{
    public BigInteger e;
    public BigInteger n;
}