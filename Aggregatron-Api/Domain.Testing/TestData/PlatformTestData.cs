using System.Globalization;
using Domain.Platforms;

namespace Domain.Testing.TestData;

public class PlatformTestData
{
    public Platform Platform(int id = 0, string identifier = "Platform1")
    {
        return new Platform(
            id: id,
            identifier: identifier,
            name: identifier,
            validFrom: new DateTime(2024, 05, 01).ToUniversalTime(),
            validTo: null,
            requiresApiKey: true,
            requiresApiSecret: true,
            requiresApiPassword: true,
            hasCryptoApi: true,
            hasEquitiesApi: true,
            hasUserAccounts: true,
            allowsTrading: true
        );
    }

    public Platform Platform1Modified(int id = 0, string identifier = "Platform1")
    {
        return new Platform(
            id: id,
            identifier: identifier,
            name: identifier,
            validFrom: new DateTime(2024, 04, 01).ToUniversalTime(),
            validTo: new DateTime(2027, 12, 31).ToUniversalTime(),
            requiresApiKey: false,
            requiresApiSecret: false,
            requiresApiPassword: false,
            hasCryptoApi: false,
            hasEquitiesApi: false,
            hasUserAccounts: false,
            allowsTrading: false
        );
    }
}