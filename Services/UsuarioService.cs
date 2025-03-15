using backend_gestorinv.Context;
using backend_gestorinv.DTOs;
using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los usuarios
        public List<Usuario> GetUsuarios()
        {
            return _context.Usuarios.Include(u => u.rol).ToList();
        }

        // Obtener usuario por ID
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios.Include(u => u.rol).FirstOrDefaultAsync(u => u.id_usuario == id);
        }

        // Crear usuario con DTO
        public async Task<bool> CreateUsuario(UsuarioCreateDTO request)
        {
            try
            {
                // Convertir DTO en objeto Usuario
                var usuario = new Usuario
                {
                    nombre = request.nombre,
                    correo = request.correo,
                    contraseña = BCrypt.Net.BCrypt.HashPassword(request.contraseña),
                    rol_id = request.rol_id
                };

                _context.Usuarios.Add(usuario);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message, ex);
            }
        }

        // Editar usuario con DTO
        public async Task<bool> EditUsuario(int id_usuario, UsuarioEditDTO request)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id_usuario);
                if (usuario == null) return false;

                // Actualizar los campos que pueden cambiar
                usuario.nombre = request.nombre ?? usuario.nombre;
                usuario.correo = request.correo ?? usuario.correo;

                // Si hay una nueva contraseña, la actualizamos
                if (!string.IsNullOrEmpty(request.contraseña))
                {
                    usuario.contraseña = BCrypt.Net.BCrypt.HashPassword(request.contraseña);
                }

                usuario.rol_id = request.rol_id;

                _context.Usuarios.Update(usuario);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message, ex);
            }
        }

        // Eliminar usuario
        public async Task<bool> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
