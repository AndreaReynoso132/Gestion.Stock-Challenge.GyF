using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockManagement.Core.Entities;
using StockManagement.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManagement.WebAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones de autenticación de usuario.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor del controlador de usuario.
        /// </summary>
        /// <param name="usuarioRepository">Repositorio de usuario inyectado.</param>
        /// <param name="configuration">Configuración de la aplicación inyectada.</param>
        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Autentica un usuario en base a sus credenciales.
        /// </summary>
        /// <param name="request">Objeto de solicitud que contiene el nombre de usuario y la contraseña.</param>
        /// <returns>Un token JWT si la autenticación es exitosa; de lo contrario, una respuesta de no autorizado.</returns>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate([FromBody] UsuarioLoginRequest request)
        {
            var usuario = await _usuarioRepository.ValidateUsuarioAsync(request.NombreUsuario, request.Contraseña);
            if (usuario == null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            var token = GenerateJwtToken(usuario);
            return Ok(new { token });
        }

        /// <summary>
        /// Genera un token JWT para un usuario autenticado.
        /// </summary>
        /// <param name="usuario">Objeto usuario para el cual se generará el token.</param>
        /// <returns>Una cadena que representa el token JWT.</returns>
        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("k8B!p$3fG@zL9d#4uW&yQ*1JcX^tV6A7\r\n");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "https://localhost:7212",
                Audience = "https://localhost:7212",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    /// <summary>
    /// Clase para la solicitud de inicio de sesión del usuario.
    /// </summary>
    public class UsuarioLoginRequest
    {

        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }
}
