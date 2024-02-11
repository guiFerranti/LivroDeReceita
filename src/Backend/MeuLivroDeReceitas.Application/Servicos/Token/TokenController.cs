using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeuLivroDeReceitas.Application.Servicos.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tempoDeVidaTokenMin;
    private readonly string _chaveSeguranca;

    public TokenController(double tempoDeVidaTokenMin, string chaveSeguranca)
    {
        _tempoDeVidaTokenMin = tempoDeVidaTokenMin;
        _chaveSeguranca = chaveSeguranca;
    }

    public string GerarToken(string emailDoUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, emailDoUsuario)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescricao = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaTokenMin),
            SigningCredentials = new SigningCredentials(SymmetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescricao);

        return tokenHandler.WriteToken(securityToken);

    }

    public void ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var paramsValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SymmetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        tokenHandler.ValidateToken(token, paramsValidacao, out _);
    }

    private SymmetricSecurityKey SymmetricKey()
    {
        var symmetricKey = Convert.FromBase64String( _chaveSeguranca );
        return new SymmetricSecurityKey( symmetricKey );
    }
}
