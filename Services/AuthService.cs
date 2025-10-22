using GTU.Api.Data;
using GTU.Api.DTOs;
using GTU.Api.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace GTU.Api.Services
{
    public class AuthService
    {
        private AppDbContext _context;
        private TokenService _tokenServ;

        public AuthService(AppDbContext context, TokenService tokenService) { 
            _context = context;
            _tokenServ = tokenService;
        }

        public async Task<AuthDTO> Register(RegisterDTO dto) {

            //valida email registrado

            if (await _context.Usuarios.AnyAsync( u => u.Email == dto.Email))
            {
                // usuario ya registrado
                return new AuthDTO { Estado= false, Mensaje="Usuario ya registrado" };
            }

            HashService hashServ = new HashService();
            hashServ.CrearPasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var usr = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Usuarios.Add(usr);
            await _context.SaveChangesAsync();

            // generacion de token
            var token = _tokenServ.CrearToken(usr);

            return new AuthDTO { Id = usr.UsuarioId, Nombre= usr.Nombre, Email= usr.Email, Token= token, Estado=true };
        }

        public async Task<AuthDTO> Login(LoginDTO lgn) 
        {

            var usr = await _context.Usuarios.Where(u => u.Email == lgn.Email).FirstOrDefaultAsync();
            if (usr == null) {

                // usuario ya registrado
                return new AuthDTO { Estado = false, Mensaje = "Usuario o contraseña incorrecta" };
            }

            HashService hashServ = new HashService();
            var resp = hashServ.VerificarPasswordHash(lgn.Password, usr.PasswordHash, usr.PasswordSalt);
            if (resp)
            {

                var token = _tokenServ.CrearToken(usr);
                return new AuthDTO { Id = usr.UsuarioId, Nombre = usr.Nombre, Email = usr.Email, Token = token, Estado = true };

            }
            else {
                return new AuthDTO { Estado = false, Mensaje = "Usuario ya registrado" };
            }
        }
    }
}
