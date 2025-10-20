using GTU.Api.DTOs;
using GTU.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTU.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private AuthService _authSrv;

        public AuthController(AuthService authService)
        {
            _authSrv = authService;
        }

        // GET: UsuariosController
        [HttpGet]
        public async Task<ActionResult<UsuarioDTO>> get()
        {
            return Ok();
        }



        // POST: UsuariosController/Create
        [HttpPost("register")]

        public async Task<ActionResult<UserResponse>> Create([FromBody] RegisterDTO reg )
        {
            var result = await _authSrv.Register(reg);
            if (result.Estado)
            {
                return Ok(new UserResponse { Email = result.Email, Id = result.Id, Nombre = result.Nombre, Token = result.Token });
            }
            else
            {
                return BadRequest(result.Mensaje);
            }
        }


        // POST: UsuariosController/login
        [HttpPost("login")]

        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginDTO lg)
        {
            try
            {
                var result = await _authSrv.Login(lg);
                if (result.Estado)
                {

                    return Ok( new UserResponse { Email = result.Email, Id = result.Id, Nombre = result.Nombre, Token = result.Token  });

                }
                else
                {
                    return BadRequest(result.Mensaje);
                }

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
