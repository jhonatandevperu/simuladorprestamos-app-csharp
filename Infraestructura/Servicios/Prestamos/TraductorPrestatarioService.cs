#region Header
// Creado por: Christian
// Fecha: 21/05/2018 13:32
// Actualizado por ultima vez: 21/05/2018 13:32
#endregion

using System;
using Dominio.Clientes;
using Dominio.Prestamos.Prestatario;

namespace Infraestructura.Servicios.Prestamos
{
    public class TraductorPrestatarioService : IPrestatarioService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly TraductorPrestatario _traductorPrestatario;
        
        public TraductorPrestatarioService(IClienteRepository clienteRepository, TraductorPrestatario traductorPrestatario)
        {
            this._clienteRepository = clienteRepository;
            this._traductorPrestatario = traductorPrestatario;
        }
        
        public Prestatario PrestatarioDesde(string dni)
        {
            var cliente = this._clienteRepository.ClienteConDni(dni);
            if(cliente != null)
            {
                return this._traductorPrestatario.APrestatarioDesdeRepresentacion(cliente);
            }
            else
            {
                throw new Exception("No existe con cliente con ese dni.");
            }
        }
        
        public Prestatario PrestatarioDesde(int id)
        {
            var cliente = this._clienteRepository.ClienteConId(id);
            if(cliente != null)
            {
                return this._traductorPrestatario.APrestatarioDesdeRepresentacion(cliente);
            }
            else
            {
                throw new Exception("No existe con cliente con ese dni.");
            }
        }
    }
}