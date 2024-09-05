using StockManagement.Core.Entities;
using StockManagement.Core.Interfaces;
using StockManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly StockContext _context;

        public UsuarioRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombreUsuario)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario> ValidateUsuarioAsync(string nombreUsuario, string contraseña)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario && u.Contraseña == contraseña);
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

    }


}
