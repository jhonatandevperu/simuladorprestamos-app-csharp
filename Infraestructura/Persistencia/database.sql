create table usuario (
  id             int primary key identity (1, 1),
  nombre_usuario varchar(20)  not null unique,
  clave          varchar(100) not null
);
go

create table persona (
  id               int primary key identity (1, 1),
  nombres          varchar(100) not null,
  apellidos        varchar(100) not null,
  dni              char(8)      not null,
  fecha_nacimiento date         not null,
  usuario_id       int,
  foreign key (usuario_id) references usuario (id)
);
go
;

create table rol (
  id     int primary key identity (1, 1),
  nombre varchar(100) not null,
);
go
;

create table rol_usuario (
  rol_id     int not null,
  usuario_id int not null,
  unique (rol_id, usuario_id)
);
go
;

--TODO: Cambiar tipo_cliente de int a smallint
create table cliente (
  id                       int primary key identity (1, 1),
  ingresos_netos_mensuales decimal(7, 2),
  gastos_fijos_mensuales   decimal(7, 2),
  tipo_cliente             int not null,
  persona_id               int not null,
  foreign key (persona_id) references persona (id)
);
go
;

create table prestamo (
  id                     int primary key identity (1, 1),
  codigo                 varchar(100)  not null,
  tipo_prestamo          int           not null,
  nombre_del_prestamo    varchar (100) not null,
  nombre_del_bien        varchar (100),
  dias_por_anio          int           not null,
  dias_por_mes           int           not null,
  fecha_desembolso       date          not null,
  dia_de_pago            int           not null,
  tea                    decimal(5, 4),
  tdes                   decimal(5, 4),
  importe                decimal(9, 2),
  numero_cuotas          int           not null,
  interes_moratorio      decimal(5, 2) not null,
  pagado                 smallint        default 0,
  cuota_fija_mensual     decimal (9,2) not null,
  costo_envio_a_casa     decimal(5, 2) not null,
  tasa_seguro_del_bien   decimal(5, 2),
  valor_del_bien         decimal(9, 2),
  prestatario_id         int           not null,
  analista_financiero_id int           not null,
  foreign key (prestatario_id) references cliente (id),
  foreign key (analista_financiero_id) references usuario (id)
);
go
;

create table pago (
  id          int primary key identity (1, 1),
  codigo      varchar(36)   not null,
  importe     decimal(8, 2) not null,
  monto_cuota decimal(8, 2) not null,
  monto_mora  decimal(8, 2),
  cajero_id   int           not null,
  fecha       datetime      not null,
  foreign key (cajero_id) references usuario (id)
);
go
;

create table cuota (
  id                         int primary key identity (1, 1),
  numero_cuota               int           not null,
  fecha_vencimiento          date          not null,
  saldo                      decimal(9, 2) not null,
  amortizacion               decimal(8, 2) not null,
  interes_mensual            decimal(8, 2) not null,
  seguro_desgravamen_mensual decimal(8, 2) not null,
  cuota_fija                 decimal(8, 2) not null,
  seguro_del_bien            decimal(8, 2),
  interes_moratorio          decimal(8, 2) not null,
  prestamo_id                int           not null,
  pago_id                    int,
  foreign key (pago_id) references pago (id),
  foreign key (prestamo_id) references prestamo (id)
);
go
;

INSERT INTO rol (nombre) VALUES ('Analista Financiero');
INSERT INTO rol (nombre) VALUES ('Cajero');

INSERT INTO usuario (nombre_usuario, clave) VALUES ('juan.perez', '123456');
INSERT INTO usuario (nombre_usuario, clave) VALUES ('pedro.gomez', '789123');

INSERT INTO rol_usuario (rol_id, usuario_id) VALUES (1, 1);
INSERT INTO rol_usuario (rol_id, usuario_id) VALUES (2, 2);

INSERT INTO persona (nombres, apellidos, dni, fecha_nacimiento, usuario_id)
VALUES ('Juan', 'Perez', '12345678', '1990-05-09', 1);

INSERT INTO persona (nombres, apellidos, dni, fecha_nacimiento, usuario_id)
VALUES ('Pedro', 'Gomez', '87654321', '1980-11-29', 2);

INSERT INTO cliente (ingresos_netos_mensuales, tipo_cliente, persona_id, gastos_fijos_mensuales)
VALUES (3000.00, 0, 1, 10.00);
