using backend_gestorinv.Context;
using backend_gestorinv.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {

        var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == request.correo);
        if (existingUser != null)
            return BadRequest(new { message = "El correo ya está registrado" });

        var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.rol == "Usuario");
        if (adminRole == null)
            return BadRequest(new { message = "El rol de administrador no está configurado en la base de datos" });


        var newUser = new Usuario
        {
            nombre = request.nombre,
            correo = request.correo,
            contraseña = BCrypt.Net.BCrypt.HashPassword(request.contraseña),
            rol = adminRole,
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();

        // Generar token JWT
        var token = _jwtServices.GenerateToken(newUser);

        return Ok(new { token, usuario = new { id = newUser.id_usuario, name = newUser.nombre, email = newUser.correo, role = "Usuario" } });
    }


}



public class RegisterRequest
{
    public string nombre { get; set; }
    public string correo { get; set; }
    public string contraseña { get; set; }
}


public class LoginRequest
{
    public string correo { get; set; }
    public string contraseña { get; set; }
}

