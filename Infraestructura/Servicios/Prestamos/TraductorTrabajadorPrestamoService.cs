#region Header

// Creado por: Christian
// Fecha: 18/05/2018 23:46
// Actualizado por ultima vez: 18/05/2018 23:46

#endregion

using System;
using Dominio.IdentidadAcceso.Acceso;
using Dominio.IdentidadAcceso.Identidad;
using Dominio.Prestamos.Trabajadores;

namespace Infraestructura.Servicios.Prestamos
{
    public class TraductorTrabajadorPrestamoService : ITrabajadorPrestamoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AutorizacionService _autorizacionService;
        private readonly TraductorTrabajadorPrestamo _traductorTrabajadorPrestamo;

        public TraductorTrabajadorPrestamoService(IUsuarioRepository usuarioRepository,
                                                  AutorizacionService autenticacionService,
                                                  TraductorTrabajadorPrestamo traductorTrabajadorPrestamo)
        {
            this._usuarioRepository = usuarioRepository;
            this._autorizacionService = autenticacionService;
            this._traductorTrabajadorPrestamo = traductorTrabajadorPrestamo;
        }

        public AnalistaFinanciero AnalistaFinancieroDesde(string identidad)
        {
            var usuario = this._usuarioRepository.UsuarioConNombreUsuario(identidad);
            if(usuario != null && this._autorizacionService.EstaElUsuarioEnElRol(identidad, "Analista Financiero"))
            {
                return this._traductorTrabajadorPrestamo.AAnalistaFinancieroDesdeRepresentacion(usuario);
            }
            else
            {
                throw new Exception("El usuario no pertenece al rol.");
            }
        }
        
        public AnalistaFinanciero AnalistaFinancieroDesde(int id)
        {
            var usuario = this._usuarioRepository.UsuarioConId(id);
            if(usuario != null && this._autorizacionService.EstaElUsuarioEnElRol(usuario, "Analista Financiero"))
            {
                return this._traductorTrabajadorPrestamo.AAnalistaFinancieroDesdeRepresentacion(usuario);
            }
            else
            {
                throw new Exception("El usuario no pertenece al rol.");
            }
        }

        public Cajero CajeroDesde(string identidad)
        {
            var usuario = this._usuarioRepository.UsuarioConNombreUsuario(identidad);
            if(usuario != null && this._autorizacionService.EstaElUsuarioEnElRol(identidad, "Cajero"))
            {
                return this._traductorTrabajadorPrestamo.ACajeroDesdeRepresentacion(usuario);
            }
            else
            {
                throw new Exception("El usuario no pertenece al rol.");
            }
        }
        
        public Cajero CajeroDesde(int id)
        {
            var usuario = this._usuarioRepository.UsuarioConId(id);
            if(usuario != null && this._autorizacionService.EstaElUsuarioEnElRol(usuario, "Cajero"))
            {
                return this._traductorTrabajadorPrestamo.ACajeroDesdeRepresentacion(usuario);
            }
            else
            {
                throw new Exception("El usuario no pertenece al rol.");
            }
        }
    }
}