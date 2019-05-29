CREATE DATABASE innguz_ventas;
GO
USE innguz_ventas;
GO

CREATE TABLE Usuarios(
	Id int primary key identity(1,1) not null,
	Usuario varchar(50),
	Nombre varchar(100),
	Apellido varchar(100),
	Posicion varchar(100),
	Telefono varchar(20),
	Correo varchar(50),
	Fecha_registro date
)

CREATE TABLE Tipo_Producto(
	id int primary key identity(1,1) not null,
	Tipo varchar(50),

	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)
)

CREATE TABLE Categorias(
	id int primary key identity(1,1) not null,
	Nombre varchar(100),

	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)
)

CREATE TABLE Productos(
	id int primary key identity(1,1) not null,
	Nombre varchar(100),
	Descripción varchar(255),
	Precio decimal(4,2),
	Tipo_Producto_Id int,
	Categoria_Id int,

	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)

)

CREATE TABLE Estados_Periodo (
	id int primary key identity(1,1) not null,
	estado varchar(25)
)

CREATE TABLE Periodos(
	id_periodo int primary key identity(1,1) not null,
	mes int,
	año int,
	estado int,
	FechaCreacion date,
)

CREATE TABLE Ventas(
	Id int primary key identity(1,1) not null,
	Descripcion varchar(250),
	Cantidad int,
	Monto decimal(4, 2),
	IVA decimal(4,2),
	Total decimal(4,2),

	Usuario_id int,
	Producto_Servicio_id int,
	periodo_id int,
	
	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)
)
