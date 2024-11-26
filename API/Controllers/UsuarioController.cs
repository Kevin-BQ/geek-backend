using Data;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<UserAplication> _userManager;
        private readonly ITokenService _tokenServicio;
        private ApiResponse _apiResponse;
        private readonly RoleManager<RoleAplication> _roleManager;


        public UsuarioController(UserManager<UserAplication> userManager, ITokenService tokenServicio,
                                 RoleManager<RoleAplication> roleManager)
        {
            _userManager = userManager;
            _tokenServicio = tokenServicio;
            _apiResponse = new();
            _roleManager = roleManager;
        }

        /*
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsurios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id) 
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return Ok(usuario);
        }*/
        [Authorize(Policy ="AdminRol")]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Register(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.UserName)) return BadRequest("UserName ya Registrado");

            var usuario = new UserAplication
            {
                UserName = registroDto.UserName.ToLower(),
                Email = registroDto.Email,
                LastName = registroDto.LatName,
                Names = registroDto.Names
            };

            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password);

            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            var rolResultado = await _userManager.AddToRoleAsync(usuario, registroDto.Role);

            if (!rolResultado.Succeeded) return BadRequest("Error al Agregar un Rol");

            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = await _tokenServicio.CreateToken(usuario)
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);
            if (usuario == null) return Unauthorized("Usuario no Valido");

            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);

            if(!resultado) return Unauthorized("Password no Valido");

            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = await _tokenServicio.CreateToken(usuario)
            };
        }

        [Authorize(Policy ="AdminRol")]
        [HttpGet("ListadoRoles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => new { NombreRol = r.Name}).ToList();
            _apiResponse.Result = roles;
            _apiResponse.IsSuccessful = true;
            _apiResponse.statusCode = HttpStatusCode.OK;
            return Ok(_apiResponse);
        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
