USE [DB_GP]
GO
-- Eliminar un Equipo
CREATE PROCEDURE Eliminar_Equipo
    @idEquipos INT
AS
BEGIN
    DELETE FROM Equipos WHERE idEquipos = @idEquipos;
END;
GO

-- Eliminar un Portafolio
CREATE PROCEDURE Eliminar_Portafolio
    @idPortafolio INT
AS
BEGIN
    DELETE FROM Portafolio WHERE idPortafolio = @idPortafolio;
END;
GO

-- Eliminar un Proyecto
CREATE PROCEDURE Eliminar_Proyecto
    @idProyectos INT
AS
BEGIN
    DELETE FROM Proyectos WHERE idProyectos = @idProyectos;
END;
GO

-- Eliminar una Subtarea
CREATE PROCEDURE Eliminar_Subtarea
    @idSubtareas INT
AS
BEGIN
    DELETE FROM Subtareas WHERE idSubtareas = @idSubtareas;
END;
GO

-- Eliminar un Comentario
CREATE PROCEDURE Eliminar_Comentario
    @idComentarios INT
AS
BEGIN
    DELETE FROM Comentarios WHERE idComentarios = @idComentarios;
END;
GO

-- Eliminar una Tarea
CREATE PROCEDURE Eliminar_Tarea
    @idTareas INT
AS
BEGIN
    DELETE FROM Tareas WHERE idTareas = @idTareas;
END;
GO

-- Eliminar un Permiso
CREATE PROCEDURE Eliminar_Permiso
    @idPermisos INT
AS
BEGIN
    DELETE FROM Permisos WHERE idPermisos = @idPermisos;
END;
GO

-- Eliminar un Rol
CREATE PROCEDURE Eliminar_Rol
    @idRoles INT
AS
BEGIN
    DELETE FROM Roles WHERE idRoles = @idRoles;
END;
GO


-- Eliminar un Usuario
CREATE PROCEDURE Eliminar_Usuario
    @idUsuarios INT
AS
BEGIN
    DELETE FROM Usuarios WHERE idUsuarios = @idUsuarios;
END;
GO

-- Eliminar un Miembro de Equipo
CREATE PROCEDURE Eliminar_Miembro_Equipo
    @idMiembros_de_equipos INT
AS
BEGIN
    DELETE FROM Miembros_de_equipos WHERE idMiembros_de_equipos = @idMiembros_de_equipos;
END;
GO

-- Eliminar un Historial de Cambios
CREATE PROCEDURE Eliminar_Historial_Cambio
    @idHistorial_de_cambios INT
AS
BEGIN
    DELETE FROM Historial_de_cambios WHERE idHistorial_de_cambios = @idHistorial_de_cambios;
END;
GO
