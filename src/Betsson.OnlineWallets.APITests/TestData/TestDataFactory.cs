namespace Betsson.OnlineWallets.APITests.TestData;

public class TestDataFactory
{
    public static double GetRandomDouble()
    {
        Random random = new Random();
        return random.NextDouble() * 999999;
    }
}