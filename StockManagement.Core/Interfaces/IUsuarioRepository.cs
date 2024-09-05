using StockManagement.Core.Entities;

namespace StockManagement.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByNombreAsync(string nombreUsuario);
        Task<Usuario> ValidateUsuarioAsync(string nombreUsuario, string contraseña);
        Task UpdateUsuarioAsync(Usuario usuario);
    }
}