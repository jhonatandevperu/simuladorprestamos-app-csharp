namespace Dominio.Clientes
{
    public interface IClienteRepository
    {
        Cliente ClienteConDni(string dni);
        Cliente ClienteConId(int id);
    }
}