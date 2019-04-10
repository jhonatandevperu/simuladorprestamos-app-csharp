using System;
using System.Data;
using System.Data.SqlClient;
using Dominio.Clientes;
using Infraestructura.Persistencia.Proxies;

namespace Infraestructura.Persistencia.Vanilla.Cliente
{
    public class ClienteRepository : Repository, IClienteRepository
    {
        public ClienteRepository(string connectionString) : base(connectionString)
        {
        }


        public Dominio.Clientes.Cliente ClienteConDni(string dni)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select c.id, c.ingresos_netos_mensuales, c.gastos_fijos_mensuales, c.tipo_cliente, p.id, p.nombres, p.apellidos, p.dni, p.fecha_nacimiento from cliente c left join persona p on c.persona_id = p.id where p.dni = '{dni}'";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return new ClienteProxy(reader.GetInt32(0),
                                                    reader.GetString(5),
                                                    reader.GetString(6),
                                                    reader.GetString(7),
                                                    reader.GetDecimal(1),
                                                    reader.GetDecimal(2),
                                                    (TipoCliente)reader.GetInt32(3),
                                                    reader.GetDateTime(8));
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
        
        public Dominio.Clientes.Cliente ClienteConId(int id)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    db.Open();
                    var query =
                        $"select c.id, c.ingresos_netos_mensuales, c.gastos_fijos_mensuales, c.tipo_cliente, p.id, p.nombres, p.apellidos, p.dni, p.fecha_nacimiento from cliente c left join persona p on c.persona_id = p.id where c.id = '{id}'";

                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return new ClienteProxy(reader.GetInt32(0),
                                                    reader.GetString(5),
                                                    reader.GetString(6),
                                                    reader.GetString(7),
                                                    reader.GetDecimal(1),
                                                    reader.GetDecimal(2),
                                                    (TipoCliente)reader.GetInt32(3),
                                                    reader.GetDateTime(8));
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
    }
}