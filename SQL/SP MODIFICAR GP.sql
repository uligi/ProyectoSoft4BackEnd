use DB_GP
GO

CREATE or alter PROCEDURE Actualizar_Comentario
    @idComentarios INT,
    @Comentario NVARCHAR(MAX),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Comentarios
    SET Comentario = @Comentario,
        Activo = @Activo
    WHERE idComentarios = @idComentarios;

    SELECT @idComentarios AS idComentarios, 'Comentario actualizado' AS Mensaje;
END;
GO


CREATE or alter PROCEDURE [dbo].[Actualizar_Equipo]
    @idEquipos INT,
    @NombreEquipos VARCHAR(45)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @idEquipos)
    BEGIN
        SELECT -1 AS Codigo, 'El equipo no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el equipo
    UPDATE Equipos
    SET NombreEquipos = @NombreEquipos
    WHERE idEquipos = @idEquipos;

    SELECT 1 AS Codigo, 'Equipo actualizado exitosamente' AS Mensaje;
END;
GO


CREATE OR ALTER PROCEDURE Modificar_Miembro_Equipo
    @idMiembros_de_equipos INT,
    @idEquipos INT,
    @idUsuarios INT,
    @forzar BIT = 0 -- Parámetro para forzar la modificación
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el miembro existe
    IF NOT EXISTS (SELECT 1 FROM Miembros_de_equipos WHERE idMiembros_de_equipos = @idMiembros_de_equipos)
    BEGIN
        SELECT -1 AS Codigo, 'El miembro no existe' AS Mensaje;
        RETURN;
    END;

    -- Verificar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @idEquipos)
    BEGIN
        SELECT -2 AS Codigo, 'El equipo no existe' AS Mensaje;
        RETURN;
    END;

    -- Verificar si el usuario existe
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE idUsuarios = @idUsuarios)
    BEGIN
        SELECT -3 AS Codigo, 'El usuario no existe' AS Mensaje;
        RETURN;
    END;

    -- Verificar si el miembro ya está en el mismo equipo
    IF EXISTS (
        SELECT 1
        FROM Miembros_de_equipos
        WHERE idUsuarios = @idUsuarios AND idEquipos = @idEquipos
          AND idMiembros_de_equipos <> @idMiembros_de_equipos
    )
    BEGIN
        SELECT -4 AS Codigo, 'El usuario ya pertenece a este equipo' AS Mensaje;
        RETURN;
    END;

    -- Verificar si el usuario ya pertenece a otro equipo (no forzado)
    IF @forzar = 0 AND EXISTS (
        SELECT 1
        FROM Miembros_de_equipos
        WHERE idUsuarios = @idUsuarios AND idEquipos <> @idEquipos
          AND idMiembros_de_equipos <> @idMiembros_de_equipos
    )
    BEGIN
        SELECT -5 AS Codigo, 'El usuario ya pertenece a otro equipo. ¿Desea forzar la actualización?' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el miembro del equipo
    UPDATE Miembros_de_equipos
    SET idEquipos = @idEquipos,
        idUsuarios = @idUsuarios
    WHERE idMiembros_de_equipos = @idMiembros_de_equipos;

    SELECT 1 AS Codigo, 'Miembro del equipo modificado exitosamente' AS Mensaje;
END;
GO



CREATE or alter PROCEDURE [dbo].[Actualizar_Portafolio]
    @idPortafolio INT,
    @NombrePortafolio VARCHAR(300),
    @Descripcion NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Portafolio WHERE idPortafolio = @idPortafolio)
    BEGIN
        SELECT -1 AS Codigo, 'El portafolio no existe' AS Mensaje;
        RETURN;
    END;

    UPDATE Portafolio
    SET NombrePortafolio = @NombrePortafolio,
        Descripcion = @Descripcion
    WHERE idPortafolio = @idPortafolio;

    SELECT 1 AS Codigo, 'Portafolio actualizado exitosamente' AS Mensaje;
END;
GO



CREATE or alter PROCEDURE [dbo].[Actualizar_Proyecto]
    @idProyectos INT,
    @NombreProyecto VARCHAR(500),
    @Descripcion NVARCHAR(MAX),
    @FechaEstimada DATE,
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Prioridad VARCHAR(45),
    @idPortafolio INT,
    @Equipos_idEquipos INT,
	@Estado VARCHAR(45)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si el proyecto existe
    IF NOT EXISTS (SELECT 1 FROM Proyectos WHERE idProyectos = @idProyectos)
    BEGIN
        SELECT -1 AS Codigo, 'El proyecto no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el portafolio existe
    IF NOT EXISTS (SELECT 1 FROM Portafolio WHERE idPortafolio = @idPortafolio)
    BEGIN
        SELECT -2 AS Codigo, 'El portafolio asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @Equipos_idEquipos)
    BEGIN
        SELECT -3 AS Codigo, 'El equipo asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el proyecto
    UPDATE Proyectos
    SET NombreProyecto = @NombreProyecto,
        Descripcion = @Descripcion,
        FechaEstimada = @FechaEstimada,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Prioridad = @Prioridad,
        idPortafolio = @idPortafolio,
        Equipos_idEquipos = @Equipos_idEquipos,
		Estado = @Estado
    WHERE idProyectos = @idProyectos;

    SELECT 1 AS Codigo, 'Proyecto actualizado exitosamente' AS Mensaje;
END;
GO


CREATE or alter PROCEDURE Actualizar_Tarea
    @idTareas INT,
    @NombreTareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @idProyectos INT,
    @idUsuarios INT = NULL,
	@Estado VARCHAR(45)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tareas
    SET NombreTareas = @NombreTareas,
        Descripcion = @Descripcion,
        Prioridad = @Prioridad,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Activo = 1,
        idProyectos = @idProyectos,
        idUsuarios = @idUsuarios,
		Estado = @Estado
    WHERE idTareas = @idTareas;

    SELECT 
        idTareas,
        NombreTareas,
        Descripcion,
        Prioridad,
        FechaInicio,
        FechaFinal,
        Activo,
        idProyectos,
        idUsuarios,
		Estado,
        1 AS Codigo,
        'Tarea modificada exitosamente' AS Mensaje
    FROM Tareas
    WHERE idTareas = @idTareas;
END;
GO



Create or alter PROCEDURE [dbo].[Modificar_Usuario]
    @idUsuarios INT,
    @Nombre VARCHAR(200),
    @Email VARCHAR(200),
    @idRoles INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE idUsuarios = @idUsuarios)
    BEGIN
        SELECT -1 AS Codigo, 'Usuario no encontrado' AS Mensaje;
        RETURN;
    END;

    UPDATE Usuarios
    SET Nombre = @Nombre,
        Email = @Email,
        idRoles = @idRoles
    WHERE idUsuarios = @idUsuarios;

    SELECT 1 AS Codigo, 'Usuario modificado exitosamente' AS Mensaje;
END;
GO


CREATE or alter PROCEDURE [dbo].[Modificar_Permiso]
    @idPermisos INT,
    @Nombre_Permisos VARCHAR(100),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el permiso existe
    IF NOT EXISTS (SELECT 1 FROM Permisos WHERE idPermisos = @idPermisos)
    BEGIN
        SELECT -1 AS Codigo, 'El permiso no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el permiso
    UPDATE Permisos
    SET Nombre_Permisos = @Nombre_Permisos,
        Activo = @Activo
    WHERE idPermisos = @idPermisos;

    -- Respuesta
    SELECT 1 AS Codigo, 'Permiso modificado exitosamente' AS Mensaje;
END;
GO

CREATE or alter PROCEDURE [dbo].[Modificar_Rol]
    @idRoles INT,
    @Nombre_Roles VARCHAR(100),
    @Activo BIT,
    @idPermisos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el rol existe
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE idRoles = @idRoles)
    BEGIN
        SELECT -1 AS Codigo, 'El rol no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el permiso asociado existe
    IF NOT EXISTS (SELECT 1 FROM Permisos WHERE idPermisos = @idPermisos)
    BEGIN
        SELECT -2 AS Codigo, 'El permiso asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el rol
    UPDATE Roles
    SET Nombre_Roles = @Nombre_Roles,
        Activo = @Activo,
        idPermisos = @idPermisos
    WHERE idRoles = @idRoles;

    -- Respuesta
    SELECT 1 AS Codigo, 'Rol modificado exitosamente' AS Mensaje;
END;
GO

CREATE or alter PROCEDURE Actualizar_Subtarea
    @idSubtareas INT,
    @NombreSubtareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @idTareas INT,
	@Estado VARCHAR(45)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Subtareas
    SET 
        NombreSubtareas = @NombreSubtareas,
        Descripcion = @Descripcion,
        Prioridad = @Prioridad,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        idTareas = @idTareas,
		Estado = @Estado
    WHERE idSubtareas = @idSubtareas;

    SELECT 
        @idSubtareas AS idSubtareas,
        1 AS Codigo,
        'Subtarea actualizada exitosamente.' AS Mensaje;
		END;
GO

CREATE or alter PROCEDURE Actualizar_Comentario_Proyectos
    @idComentario INT,
    @Comentario NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Comentarios_Proyectos
        SET Comentario = @Comentario,
            Activo = 1
        WHERE idComentario = @idComentario;

        SELECT 1 AS Codigo, 'Comentario actualizado correctamente.' AS Mensaje; -- Cambia 'Success' por 'Codigo'
    END TRY
    BEGIN CATCH
        SELECT 0 AS Codigo, ERROR_MESSAGE() AS Mensaje; -- Cambia 'Success' por 'Codigo'
    END CATCH
END;
go

CREATE or alter PROCEDURE Actualizar_Comentario_Tarea
    @idComentario INT,
    @Comentario NVARCHAR(MAX),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Comentarios_Tareas
        SET Comentario = @Comentario,
            Activo = 1
        WHERE idComentario = @idComentario;

        IF @@ROWCOUNT = 0
            THROW 50000, 'El comentario no existe.', 1;

        SELECT 1 AS Codigo, 'Comentario actualizado correctamente.' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Codigo, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO


CREATE or alter PROCEDURE Actualizar_Comentario_Subtarea
    @idComentario INT,
    @Comentario NVARCHAR(MAX),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Comentarios_Subtareas
        SET Comentario = @Comentario,
            Activo = 1
        WHERE idComentario = @idComentario;

        IF @@ROWCOUNT = 0
            THROW 50000, 'El comentario no existe.', 1;

        SELECT 1 AS Codigo, 'Comentario actualizado correctamente.' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Codigo, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
