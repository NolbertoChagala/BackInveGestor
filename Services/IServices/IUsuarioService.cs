using backend_gestorinv.DTOs;
using backend_gestorinv.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Services.IServices
{
    public interface IUsuarioService
    {
        public List<Usuario> GetUsuarios();
        public Task<Usuario> GetUsuarioById(int id);
        public Task<bool> CreateUsuario(UsuarioCreateDTO request);

        public Task<bool> EditUsuario(int id_usuario, UsuarioEditDTO request);
        public Task<bool> DeleteUsuario(int id);

    }
}  