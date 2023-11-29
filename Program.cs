namespace DigitalSignatureAlgorithmImplementation;

public static class Program
{
    public static void Main()
    {
        // Генерація ключової пари
        var keys = RSASignature.KeyGen();
        var privateKey = keys.privateKey;
        var publicKey = keys.publicKey;

        // Повідомлення для підпису
        string message = "Hello, RSA!";
        Console.WriteLine("Original Message: " + message);

        // Підписуємо повідомлення
        var signature = RSASignature.Sign(message, privateKey);
        Console.WriteLine($"Signature: {signature}");

        // Перевіряємо підпис
        bool isSignatureValid = RSASignature.Verify(message, signature, publicKey);
        Console.WriteLine("Is Signature Valid? " + isSignatureValid);
    }
}
