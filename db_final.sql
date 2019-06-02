CREATE DATABASE innguz_ventas;
GO
USE innguz_ventas;
GO



CREATE TABLE Usuarios(
	Id int primary key identity(1,1) not null,
	Usuario varchar(50) not null,
	Nombre varchar(100) not null,
	Apellido varchar(100) not null,
	Posicion varchar(100) not null,
	Telefono varchar(20) not null,
	Correo varchar(50) not null,
	Clave varchar(30) not null,
	Foto varbinary(max) not null,
	Fecha_registro date not null
)

CREATE TABLE Tipo_Producto(
	id int primary key identity(1,1) not null,
	Tipo varchar(50) not null,

	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)
)

CREATE TABLE Categorias(
	id int primary key identity(1,1) not null,
	Nombre varchar(100) not null,

	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)
)

CREATE TABLE Productos(
	id int primary key identity(1,1) not null,
	Nombre varchar(100) not null,
	Descripción varchar(255) not null,
	Precio decimal(4,2) not null,
	Tipo_Producto_Id int not null,
	Categoria_Id int not null,

	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)

)


CREATE TABLE Estados_Periodo (
	id int primary key identity(1,1) not null,
	estado varchar(25) not null
)

CREATE TABLE Periodos(
	id_periodo int primary key identity(1,1) not null,
	mes int not null,
	año int not null,
	estado int not null,
	FechaCreacion date,
)


CREATE TABLE Ventas(
	Id int primary key identity(1,1) not null,
	Descripcion varchar(250) not null,
	Cantidad int not null,
	Monto decimal(4, 2) not null,
	IVA decimal(4,2) not null, 
	Total decimal(4,2) not null,

	Usuario_id int,
	Producto_Servicio_id int,
	periodo_id int,
	
	FechaInserta date,
	UsuarioInserta varchar(50),
	FechaActualiza date,
	UsuarioActualiza varchar(50)
)



CREATE PROCEDURE SP_modificar_usuario 
	@id int,
	@usuario varchar(50),
	@nombre varchar(100),
	@apellido varchar(100),
	@posicion varchar(100),
	@telefono varchar(20),
	@correo varchar(50),
	@clave varchar(30),
	@foto varbinary(max)
AS
BEGIN
	update Usuarios set Usuario=@usuario, Nombre=@nombre, Apellido=@apellido, Posicion=@posicion, Telefono=@telefono, Correo=@correo, Clave=@clave,Foto=@foto
	where Id=@id;

    -- Insert statements for procedure here
	SELECT 'Datos actualizados exitosamente' Mensaje
END
GO

CREATE PROCEDURE SP_ActualizarTipoProdcuto
	@id int,
	@tipo varchar(50),
	@usuarioActualiza varchar(50)
AS
BEGIN
	UPDATE Tipo_Producto SET tipo = @tipo, UsuarioActualiza = @usuarioActualiza, FechaActualiza = GETDATE() WHERE id = @id
END

CREATE PROCEDURE SP_ActualizarCategorias
	@id int,
	@nombre varchar(50),
	@usuarioActualiza varchar(50)
AS
BEGIN
	UPDATE Categorias SET Nombre = @nombre, UsuarioActualiza = @usuarioActualiza, FechaActualiza = GETDATE() WHERE id = @id
END

CREATE PROCEDURE SP_Actualizar_Producto
@id int,
@nombre varchar(100),
@descripcion varchar(255),
@precio decimal(8,2),
@tipo_producto int,
@categoria int,
@usuarioActualiza varchar(50)
AS
BEGIN
	UPDATE Productos set Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, Tipo_Producto_Id = @tipo_producto, Categoria_Id = @categoria, UsuarioActualiza = @usuarioActualiza, FechaActualiza = GETDATE()
	WHERE id = @id
END 

CREATE PROCEDURE SP_Actualizar_Periodo
@id int,
@estado int
AS
BEGIN
	UPDATE Periodos set estado = @estado where id_periodo = @id
END

CREATE PROCEDURE SP_Actualizar_Venta
@id int,
@descripcion varchar(255),
@cantidad int,
@monto decimal(8,2),
@iva decimal(8,2),
@total decimal(8,2),
@producto int,
@usuarioActualiza varchar(50)
AS
BEGIN
	UPDATE Ventas SET Descripcion = @descripcion, Cantidad = @cantidad, Monto = @monto, IVA = @iva, total = @total, Producto_Servicio_id = @producto, UsuarioActualiza = @usuarioActualiza, FechaActualiza = GETDATE()
	WHERE id = @id
END


	

CREATE VIEW Productos_view
AS
	SELECT p.id, p.Nombre, p.Descripcion, p.Precio, tp.Tipo, c.Nombre AS Categoria, p.UsuarioInserta, p.FechaInserta, p.UsuarioActualiza, p.FechaActualiza 
	FROM Productos AS p INNER JOIN Tipo_producto AS tp ON tp.id = p.Tipo_Producto_Id
	INNER JOIN Categorias AS c ON c.id = p.Categoria_ID

create VIEW Ventas_view

AS
	SELECT v.Id, p.Nombre AS Producto, v.Descripcion, v.Cantidad, v.Monto, V.IVA, v.Total, u.Usuario, per.mes, per.año, v.FechaInserta, v.UsuarioInserta, v.FechaActualiza, v.UsuarioActualiza
	FROM Ventas AS v INNER JOIN Usuarios AS u ON v.Usuario_id = u.Id
	INNER JOIN Periodos AS per ON v.periodo_id = per.id_periodo
	INNER JOIN productos AS p ON v.producto_Servicio_id = p.id

select * from Ventas

