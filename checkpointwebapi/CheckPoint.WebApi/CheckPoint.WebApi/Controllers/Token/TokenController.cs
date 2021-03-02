using Bearer.Tokens.Security.Metadata;
using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Security.BAL.Interface.DomainUoW;
using Security.Business.Model;
using Security.DAL.Repository.DomainUoW;
using Security.EntityFramework.Edbm;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;


namespace CheckPoint.WebApi.Controllers.Token
{
    [Produces("application/json")]
    //[Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly TokenAuthOptions tokenOptions;

        private SecurityDbContext _securityDbContext;
        private IOptions<DatabaseSettings> _dbSettings;
        private IOptions<SecuritySettings> _securitySettings;
        private ISecurityUnitOfWork _securityUoW;

        public SecurityDbContext ErpDbContext
        {
            get { return _securityDbContext; }
        }
        public IOptions<DatabaseSettings> DbSettings
        {
            get { return _dbSettings; }
            set { _dbSettings = value; }
        }
        public IOptions<SecuritySettings> SecuritySettings
        {
            get { return _securitySettings; }
        }
        public ISecurityUnitOfWork SecurityUoW
        {
            get
            {
                return _securityUoW;
            }
        }


        public TokenController(IOptions<DatabaseSettings> dbSettings, IOptions<SecuritySettings> securitySettings, TokenAuthOptions tokenOptions)
        {
            _dbSettings = dbSettings;
            _securitySettings = securitySettings;
            _securityDbContext = new SecurityDbContext(_dbSettings.Value.ConnString);
            _securityUoW = new SecurityUnitOfWork(_securityDbContext);
            this.tokenOptions = tokenOptions;
        }


        //**************************************************
        //*                     P O S T 
        //**************************************************        

        [HttpPost]
        [Route("GetUserToken")]
        public dynamic GetUserToken([FromBody] AuthRequest data)
        {
            // Check for Authentication 
            Tuple<bool, AuthRequest> login = IsAuthenticated(data);
            if (login.Item1)
            {
                double duration = 20;
                double.TryParse(SecuritySettings.Value.TokenDuration, out duration);
                DateTime? expires = DateTime.UtcNow.AddMinutes(duration);
                var token = GetToken(login.Item2, expires);
                return new { authenticated = true, token = token, tokenExpires = expires };
            }
            return new { authenticated = false };
        }


        private Tuple<bool, AuthRequest> IsAuthenticated(AuthRequest data)
        {
            if (data != null)
            {
                Usuarios usuario = SecurityUoW.UsuariosRepository.GetUsuarioByUserName(data.User);
                AuthRequest login = new AuthRequest
                {
                    User = usuario.NombreUsuario,
                    Pwd = usuario.Contraseña
                };
                return new Tuple<bool, AuthRequest>(true, login);

            }
            return new Tuple<bool, AuthRequest>(false, null);
        }

        private string GetToken(AuthRequest user, DateTime? expires)
        {
            var handler = new JwtSecurityTokenHandler();
            // Here, you should create or look up an identity for the user which is being authenticated.
            // For now, just creating a simple generic identity.
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.User, "TokenAuth"), new[] { new Claim(user.User, user.User.ToString(), ClaimValueTypes.Integer) });

            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                SigningCredentials = tokenOptions.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }

    }
}