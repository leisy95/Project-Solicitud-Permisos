using ProyectoLogin.Models;

namespace ProyectoLogin.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario>GetUsuario(string correo, string password);
        Task<Usuario>SaveUsuario(Usuario modelo);

    }
}
