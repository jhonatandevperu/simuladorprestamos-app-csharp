#region Header

// Creado por: Christian
// Fecha: 29/05/2018 05:04
// Actualizado por ultima vez: 29/05/2018 05:04

#endregion

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;
using Dominio.Prestamos.Trabajadores;
using Infraestructura.Persistencia.Proxies;
using Infraestructura.Servicios.Prestamos;

namespace Infraestructura.Persistencia.Vanilla.Prestamos
{
    public class PrestamoRepository : Repository, IPrestamoRepository
    {
        private readonly ITrabajadorPrestamoService _traductorTrabajadorPrestamoService;
        private readonly IPrestatarioService _traductorPrestatarioService;

        public PrestamoRepository(string connectionString,
                                  IPrestatarioService traductorPrestatarioService,
                                  ITrabajadorPrestamoService traductorTrabajadorPrestamoService) :
            base(connectionString)
        {
            this._traductorPrestatarioService = traductorPrestatarioService;
            this._traductorTrabajadorPrestamoService = traductorTrabajadorPrestamoService;
        }

        public void RegistrarPrestamo(Prestamo prestamo)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                db.Open();
                using(var ts = db.BeginTransaction())
                {
                    const string queryPrestamo = "insert into prestamo" +
                                                 "(codigo, tipo_prestamo, nombre_del_prestamo, nombre_del_bien, cuota_fija_mensual, dias_por_anio, dias_por_mes, fecha_desembolso, dia_de_pago, tea, tdes, importe, numero_cuotas, interes_moratorio, costo_envio_a_casa, tasa_seguro_del_bien, valor_del_bien, prestatario_id, analista_financiero_id) " +
                                                 "output inserted.id values " +
                                                 "(@codigo, @tipoPrestamo, @nombreDelPrestamo, @nombreDelBien, @cuotaFijaMensual, @diasPorAnio, @diasPorMes, @fechaDesembolso, @diaDePago, @tea, @tdes, @importe, @numeroCuotas, @interesMoratorio, @costoEnvioACasa, @tasaSeguroDelBien, @valorDelBien, @prestatarioId, @analistaFinancieroId)";

                    const string queryCuota = "insert into cuota" +
                                              "(numero_cuota, fecha_vencimiento, saldo, amortizacion, interes_mensual, seguro_desgravamen_mensual, cuota_fija, seguro_del_bien, interes_moratorio, prestamo_id) " +
                                              "values " +
                                              "(@numeroCuota, @fechaVencimiento, @saldo, @amortizacion, @interesMensual, @seguroDesgravamenMensual, @cuotaFija, @seguroDelBien, @interesMoratorio, @prestamoId)";

                    var prestamoId = db.QuerySingle<int>(queryPrestamo,
                                                         new
                                                         {
                                                             codigo = prestamo.Codigo,
                                                             tipoPrestamo = prestamo.TipoPrestamo,
                                                             nombreDelPrestamo = prestamo.NombreDelPrestamo,
                                                             nombreDelBien = prestamo.NombreDelBien,
                                                             cuotaFijaMensual = prestamo.ObtenerCuotaFijaMensual(),
                                                             diasPorAnio = prestamo.DiasPorAnio,
                                                             diasPorMes = prestamo.DiasPorMes,
                                                             fechaDesembolso = prestamo.FechaDesembolso,
                                                             diaDePago = prestamo.DiaDePago,
                                                             tea = prestamo.Tea,
                                                             tdes = prestamo.Tdes,
                                                             importe = prestamo.Importe,
                                                             numeroCuotas = prestamo.NumeroCuotas,
                                                             interesMoratorio = prestamo.InteresMoratorio,
                                                             costoEnvioACasa = prestamo.CostoEnvioACasa,
                                                             tasaSeguroDelBien = prestamo.TasaSeguroDelBien,
                                                             valorDelBien = prestamo.ValorDelBien,
                                                             prestatarioId = prestamo.Prestatario.Id,
                                                             analistaFinancieroId = prestamo.AnalistaFinanciero.Id
                                                         },
                                                         ts);

                    var cronograma = prestamo.Cuotas;

                    foreach(var cuota in cronograma)
                    {
                        db.Execute(queryCuota,
                                   new
                                   {
                                       numeroCuota = cuota.NumeroCuota,
                                       fechaVencimiento = cuota.FechaVencimiento,
                                       saldo = cuota.SaldoInicial,
                                       amortizacion = cuota.Amortizacion,
                                       interesMensual = cuota.InteresMensual,
                                       seguroDesgravamenMensual = cuota.SeguroDesgravamenMensual,
                                       cuotaFija = cuota.CuotaFija,
                                       seguroDelBien = cuota.SeguroDelBien,
                                       interesMoratorio = cuota.InteresMoratorio,
                                       prestamoId = prestamoId,
                                   },
                                   ts);
                    }

                    ts.Commit();
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<Prestamo> PrestamosActivosDelPrestatario(int prestatarioId)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                const string queryPrestamos =
                    "select p.id, p.codigo, p.tipo_prestamo, p.nombre_del_prestamo, p.nombre_del_bien, p.cuota_fija_mensual, p.dias_por_anio, p.dias_por_mes, p.fecha_desembolso, p.dia_de_pago, p.tea, p.tdes, p.importe, p.numero_cuotas, p.interes_moratorio, p.costo_envio_a_casa, p.tasa_seguro_del_bien, p.valor_del_bien, p.prestatario_id, p.analista_financiero_id " +
                    "from prestamo p where p.pagado = 0 and p.prestatario_id = @PrestatarioId";

                const string queryCuotas =
                    "select c.id, c.interes_moratorio, c.numero_cuota, c.fecha_vencimiento, c.saldo, c.amortizacion, c.interes_mensual, c.seguro_desgravamen_mensual, c.cuota_fija, c.seguro_del_bien, c.prestamo_id, c.pago_id, p.id, p.codigo, p.fecha, p.monto_mora, p.cajero_id " +
                    "from cuota c left join pago p on c.pago_id = p.id where c.prestamo_id in @IdsPrestamo " +
                    "order by c.prestamo_id, c.numero_cuota";

                //Obtener los prestamos pendientes

                var prestamosDb = db.Query<PrestamoModel>(queryPrestamos, new {PrestatarioId = prestatarioId}).ToList();

                var idsPrestamos = from prestamo in prestamosDb select prestamo.id;

                //Obtener los cronogramas de esos prestamos

                var cuotas = db.Query<CuotaModel, PagoModel, CuotaModel>(queryCuotas,
                                                                         (cuota, pago) =>
                                                                         {
                                                                             cuota.pago = pago;
                                                                             return cuota;
                                                                         },
                                                                         new {IdsPrestamo = idsPrestamos}).ToList();

                var prestamos = new Dictionary<int, PrestamoProxy>();

                foreach(var cuota in cuotas)
                {
                    var prestamoId = cuota.prestamo_id;
                    if(!prestamos.ContainsKey(prestamoId))
                    {
                        var prestamoDb = prestamosDb.Find(p => p.id == prestamoId);
                        var reglas = new ReglasPrestamoProxy(prestamoDb.tipo_prestamo, prestamoDb.nombre_del_prestamo, prestamoDb.cuota_fija_mensual, prestamoDb.tea, prestamoDb.tdes, prestamoDb.interes_moratorio, prestamoDb.costo_envio_a_casa, prestamoDb.nombre_del_bien, prestamoDb.tasa_seguro_del_bien);
                        prestamos[prestamoId] = new PrestamoProxy(reglas,
                                                                  prestamoDb.id,
                                                                  prestamoDb.codigo,
                                                                  this._traductorPrestatarioService
                                                                      .PrestatarioDesde(prestamoDb.prestatario_id),
                                                                  this._traductorTrabajadorPrestamoService
                                                                      .AnalistaFinancieroDesde(prestamoDb
                                                                                                   .analista_financiero_id),
                                                                  prestamoDb.dias_por_anio,
                                                                  prestamoDb.dias_por_mes,
                                                                  prestamoDb.fecha_desembolso,
                                                                  prestamoDb.dia_de_pago,
                                                                  prestamoDb.importe,
                                                                  prestamoDb.numero_cuotas,
                                                                  prestamoDb.valor_del_bien);
                    }

                    var prestamo = prestamos[prestamoId];

                    var cuotaTemp = prestamo.AgregarCuota(cuota.id,
                                                          cuota.interes_moratorio,
                                                          cuota.numero_cuota,
                                                          cuota.fecha_vencimiento,
                                                          cuota.saldo,
                                                          cuota.amortizacion,
                                                          cuota.interes_mensual,
                                                          cuota.seguro_desgravamen_mensual,
                                                          cuota.cuota_fija,
                                                          cuota.seguro_del_bien);
                    if(cuota.pago_id != null)
                    {
                        cuotaTemp.AgregarPago(this._traductorTrabajadorPrestamoService
                                                  .CajeroDesde(cuota.pago.cajero_id),
                                              cuota.pago.fecha,
                                              cuota.pago.codigo,
                                              cuota.pago.monto_mora);
                    }
                }

                var prestamosProcesados = new List<Prestamo>();
                foreach(var pair in prestamos)
                {
                    prestamosProcesados.Add(pair.Value);
                }

                return prestamosProcesados;
            }
        }

        /// <inheritdoc />
        public Prestamo RecuperarPrestamoDelPrestatarioDeCodigo(int prestatarioId, string codigo)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                const string queryPrestamos =
                    "select p.id, p.codigo, p.tipo_prestamo, p.nombre_del_prestamo, p.nombre_del_bien, p.cuota_fija_mensual, p.dias_por_anio, p.dias_por_mes, p.fecha_desembolso, p.dia_de_pago, p.tea, p.tdes, p.importe, p.numero_cuotas, p.interes_moratorio, p.costo_envio_a_casa, p.tasa_seguro_del_bien, p.valor_del_bien, p.prestatario_id, p.analista_financiero_id " +
                    "from prestamo p where p.codigo = @codigo and p.prestatario_id = @prestatarioId";

                const string queryCuotas =
                    "select c.id, c.interes_moratorio, c.numero_cuota, c.fecha_vencimiento, c.saldo, c.amortizacion, c.interes_mensual, c.seguro_desgravamen_mensual, c.cuota_fija, c.seguro_del_bien, c.prestamo_id, c.pago_id, p.id, p.codigo, p.fecha, p.monto_mora, p.cajero_id " +
                    "from cuota c left join pago p on c.pago_id = p.id where c.prestamo_id = @prestamoId order by c.numero_cuota asc";

                var prestamoDb = db.QuerySingleOrDefault<PrestamoModel>(queryPrestamos, new {codigo, prestatarioId});


                if(prestamoDb == null)
                {
                    return null;
                }

                var reglas = new ReglasPrestamoProxy(prestamoDb.tipo_prestamo, prestamoDb.nombre_del_prestamo, prestamoDb.cuota_fija_mensual, prestamoDb.tea, prestamoDb.tdes, prestamoDb.interes_moratorio, prestamoDb.costo_envio_a_casa, prestamoDb.nombre_del_bien, prestamoDb.tasa_seguro_del_bien);
                var prestamo = new PrestamoProxy(reglas,
                                                          prestamoDb.id,
                                                          prestamoDb.codigo,
                                                          this._traductorPrestatarioService
                                                              .PrestatarioDesde(prestamoDb.prestatario_id),
                                                          this._traductorTrabajadorPrestamoService
                                                              .AnalistaFinancieroDesde(prestamoDb
                                                                                           .analista_financiero_id),
                                                          prestamoDb.dias_por_anio,
                                                          prestamoDb.dias_por_mes,
                                                          prestamoDb.fecha_desembolso,
                                                          prestamoDb.dia_de_pago,
                                                          prestamoDb.importe,
                                                          prestamoDb.numero_cuotas,
                                                          prestamoDb.valor_del_bien);

                var cuotas = db.Query<CuotaModel, PagoModel, CuotaModel>(queryCuotas,
                                                                         (cuota, pago) =>
                                                                         {
                                                                             cuota.pago = pago;
                                                                             return cuota;
                                                                         },
                                                                         new {prestamoId = prestamo.Id}).ToList();

                foreach(var cuota in cuotas)
                {
                    var cuotaTemp = prestamo.AgregarCuota(cuota.id,
                                                          cuota.interes_moratorio,
                                                          cuota.numero_cuota,
                                                          cuota.fecha_vencimiento,
                                                          cuota.saldo,
                                                          cuota.amortizacion,
                                                          cuota.interes_mensual,
                                                          cuota.seguro_desgravamen_mensual,
                                                          cuota.cuota_fija,
                                                          cuota.seguro_del_bien);
                    if(cuota.pago_id != null)
                    {
                        cuotaTemp.AgregarPago(this._traductorTrabajadorPrestamoService
                                                  .CajeroDesde(cuota.pago.cajero_id),
                                              cuota.pago.fecha,
                                              cuota.pago.codigo,
                                              cuota.pago.monto_mora);
                    }
                }

                return prestamo;
            }
        }

        /// <inheritdoc />
        public void RegistrarPago(Prestamo prestamo, int numeroCuota)
        {
            using(IDbConnection db = new SqlConnection(this.ConnectionString))
            {
                db.Open();
                using(var ts = db.BeginTransaction())
                {
                    const string queryPago = "insert into pago" +
                                             "(codigo, importe, monto_cuota, monto_mora, cajero_id, fecha) " +
                                             "output inserted.id values " +
                                             "(@codigo, @importe, @montoCuota, @montoMora, @cajeroId, @fecha)";

                    const string queryCuota =
                        "update cuota set pago_id = @pagoId where prestamo_id = @prestamoId and numero_cuota = @numeroCuota";

                    var pago = prestamo.ObtenerCuota(numeroCuota).Pago;

                    var pagoId = db.QuerySingle<int>(queryPago,
                                                     new
                                                     {
                                                         codigo = pago.Codigo,
                                                         importe = pago.Importe,
                                                         montoCuota = pago.MontoCuota,
                                                         montoMora = pago.MontoMora,
                                                         cajeroId = pago.Cajero.Id,
                                                         fecha = pago.Fecha
                                                     },
                                                     ts);

                    db.Execute(queryCuota, new {pagoId, numeroCuota, prestamoId = prestamo.Id}, ts);

                    //Añadido para actualizar el préstamo si su última cuota ha sido pagada
                    const string queryActualizarPrestamo =
                        "update prestamo set pagado = 1 where id = @prestamoId";

                    if(numeroCuota == prestamo.NumeroCuotas)
                        db.Execute(queryActualizarPrestamo, new {prestamoId = prestamo.Id}, ts);

                    ts.Commit();
                }
            }
        }
    }
}