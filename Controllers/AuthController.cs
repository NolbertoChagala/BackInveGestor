using backend_gestorinv.Context;
using backend_gestorinv.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly JwtServices _jwtServices;

    public AuthController(AppDbContext context, JwtServices jwtServices)
    {
        _context = context;
        _jwtServices = jwtServices;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = await _context.Usuarios.Include(u => u.rol)
            .FirstOrDefaultAsync(u => u.correo == request.correo);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.contraseña, usuario.contraseña))
            return Unauthorized("Credenciales incorrectas");

        var token = _jwtServices.GenerateToken(usuario);
        return Ok(new { token, usuario });
    }


    [HttpGet("me")]
    [Authorize] // 🔒 Requiere autenticación con JWT
    public async Task<IActionResult> GetUserProfile()
    {
        var userid = User.FindFirst("usuario_id")?.Value;

        Console.WriteLine(userid);

        if (string.IsNullOrEmpty(userid))
            return Unauthorized(new { message = "Usuario no autenticado" });

        var usuario = await _context.Usuarios.Include(u => u.rol)
            .FirstOrDefaultAsync(u => u.id_usuario.ToString() == userid);

        if (usuario == null)
            return NotFound("Usuario no encontrado");

        return Ok(new
        {
            id = usuario.id_usuario,
            name = usuario.nombre,
            email = usuario.correo,
            role = usuario.rol.rol
        });
    }


    [HttpPost("register")]
    public async Task<IActionResult> CrearUsuario([FromBody] RegisterRequest request)
    {
        // Verificar si el correo ya está registrado
        var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == request.correo);
        if (existingUser != null)
            return BadRequest(new { message = "El correo ya está registrado" });

        // Buscar el rol seleccionado por el usuario
        var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.rol == request.rol);
        if (userRole == null)
            return BadRequest(new { message = "El rol seleccionado no es válido" });

        // Crear el nuevo usuario
        var newUser = new Usuario
        {
            nombre = request.nombre,
            correo = request.correo,
            contraseña = BCrypt.Net.BCrypt.HashPassword(request.contraseña),
            rol = userRole,
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();

        // Generar token JWT para el usuario creado
        var token = _jwtServices.GenerateToken(newUser);

        // Devolver la respuesta con el token
        return Ok(new
        {
            token,
            usuario = new
            {
                name = newUser.nombre,
                email = newUser.correo,
                contraseña = newUser.contraseña,
                role = userRole.rol
            }
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok(new { message = "Sesión cerrada correctamente" });
    }


}

public class RegisterRequest
{
    public string nombre { get; set; }
    public string correo { get; set; }
    public string contraseña { get; set; }
    public string rol { get; set; }
}


    public class LoginRequest
    {
        public string correo { get; set; }
        public string contraseña { get; set; }
    }

