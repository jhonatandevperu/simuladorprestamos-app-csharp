#region Header
// Creado por: Christian
// Fecha: 30/05/2018 05:56
// Actualizado por ultima vez: 30/05/2018 05:56
#endregion

using System;

namespace Dominio.Comun
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
            
        }
    }
}