using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PMSoftAPI;

public class AuthOptions
{
    public const string Issuer = "PmSoftServer";
    public const string Audience = "PmSoftClient";
    private const string Key = "mysupersecret_secretsecretsecretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}