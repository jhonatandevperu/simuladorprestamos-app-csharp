using System.Data;
using System.Data.SqlClient;
using Dominio.IdentidadAcceso;
using Dominio.IdentidadAcceso.Identidad;
using Infraestructura.Persistencia.Proxies;

namespace Infraestructura.Persistencia.Vanilla.IdentidadAcceso.Identidad
{
    public class UsuarioRepository : Repository, IUsuarioRepository
    {
        
        public Usuario UsuarioAPartirDeCredenciales(string nombreUsuario, string clave)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select u.id, u.nombre_usuario, u.clave, p.id, p.nombres, p.apellidos, p.dni, p.fecha_nacimiento from usuario u left join persona p on u.id = p.usuario_id where nombre_usuario = '{nombreUsuario}' and clave = '{clave}'";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            var usuario = new UsuarioProxy(
                                                           reader.GetInt32(0),
                                                           reader.GetString(1),
                                                           reader.GetString(2),
                                                           new Persona(
                                                                       reader.GetString(4),
                                                                       reader.GetString(5),
                                                                       reader.GetString(6),
                                                                       reader.GetDateTime(7)
                                                                       )
                                                           );
                            return usuario;
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
        public Usuario UsuarioConNombreUsuario(string nombreUsuario)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select u.id, u.nombre_usuario, u.clave, p.id, p.nombres, p.apellidos, p.dni, p.fecha_nacimiento from usuario u left join persona p on u.id = p.usuario_id where nombre_usuario = '{nombreUsuario}'";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            var usuario = new UsuarioProxy(
                                                           reader.GetInt32(0),
                                                           reader.GetString(1),
                                                           reader.GetString(2),
                                                           new Persona(
                                                                       reader.GetString(4),
                                                                       reader.GetString(5),
                                                                       reader.GetString(6),
                                                                       reader.GetDateTime(7)
                                                                      )
                                                          );
                            return usuario;
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
        public Usuario UsuarioConId(int id)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select u.id, u.nombre_usuario, u.clave, p.id, p.nombres, p.apellidos, p.dni, p.fecha_nacimiento from usuario u left join persona p on u.id = p.usuario_id where u.id = '{id}'";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            var usuario = new UsuarioProxy(
                                                           reader.GetInt32(0),
                                                           reader.GetString(1),
                                                           reader.GetString(2),
                                                           new Persona(
                                                                       reader.GetString(4),
                                                                       reader.GetString(5),
                                                                       reader.GetString(6),
                                                                       reader.GetDateTime(7)
                                                                      )
                                                          );
                            return usuario;
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

        public UsuarioRepository(string connectionString) : base(connectionString)
        {
        }
    }
}