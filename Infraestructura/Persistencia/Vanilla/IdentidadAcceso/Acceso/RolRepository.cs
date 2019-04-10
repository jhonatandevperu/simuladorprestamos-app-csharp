using System.Data;
using System.Data.SqlClient;
using Dominio.IdentidadAcceso;
using Dominio.IdentidadAcceso.Acceso;
using Dominio.IdentidadAcceso.Identidad;
using Infraestructura.Persistencia.Proxies;

namespace Infraestructura.Persistencia.Vanilla.IdentidadAcceso.Acceso
{
    public class RolRepository : Repository, IRolRepository
    {        
        public Rol RolConNombre(string nombreRol)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select r.id, r.nombre from rol r where nombre = '{nombreRol}'";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return new RolProxy(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
                finally
                {
                    db.Close();
                }
            }
            return null;
        }

        /// <inheritdoc />
        public bool ExisteUsuarioEnRol(int usuarioId, int rolId)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select count(*) from rol_usuario where rol_id = {rolId} and usuario_id = {usuarioId}";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return reader.GetInt32(0) == 1;
                        }
                    }
                }
                finally
                {
                    db.Close();
                }
            }
            return false;
        }

        public RolRepository(string connectionString) : base(connectionString)
        {
        }
    }
}