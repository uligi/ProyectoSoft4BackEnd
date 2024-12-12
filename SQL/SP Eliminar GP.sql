USE [DB_GP]
GO
-- Eliminar un Equipo
CREATE PROCEDURE [dbo].[Eliminar_Equipo]
    @idEquipos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @idEquipos)
    BEGIN
        SELECT -1 AS Codigo, 'El equipo no existe' AS Mensaje;
        RETURN;
    END;

    -- Marcar el equipo como inactivo
    UPDATE Equipos
    SET Activo = 0
    WHERE idEquipos = @idEquipos;

    SELECT 1 AS Codigo, 'Equipo eliminado exitosamente (borrado lógico)' AS Mensaje;
END;
GO



-- Eliminar un Portafolio
CREATE PROCEDURE [dbo].[Eliminar_Portafolio]
    @idPortafolio INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Portafolio WHERE idPortafolio = @idPortafolio)
    BEGIN
        SELECT -1 AS Codigo, 'El portafolio no existe' AS Mensaje;
        RETURN;
    END;

    UPDATE Portafolio
    SET Activo = 0
    WHERE idPortafolio = @idPortafolio;

    SELECT 1 AS Codigo, 'Portafolio eliminado lógicamente' AS Mensaje;
END;
GO




CREATE PROCEDURE [dbo].[Eliminar_Proyecto]
    @idProyectos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si el proyecto existe
    IF NOT EXISTS (SELECT 1 FROM Proyectos WHERE idProyectos = @idProyectos)
    BEGIN
        SELECT -1 AS Codigo, 'El proyecto no existe' AS Mensaje;
        RETURN;
    END;

    -- Realizar borrado lógico
    UPDATE Proyectos
    SET Activo = 0
    WHERE idProyectos = @idProyectos;

    SELECT 1 AS Codigo, 'Proyecto eliminado lógicamente' AS Mensaje;
END;
GO


CREATE PROCEDURE Eliminar_Tarea

    @idTareas INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si el proyecto existe
    IF NOT EXISTS (SELECT 1 FROM Tareas WHERE idTareas = @idTareas)
    BEGIN
        SELECT -1 AS Codigo, 'El proyecto no existe' AS Mensaje;
        RETURN;
    END;

    -- Realizar borrado lógico
    UPDATE Tareas
    SET Activo = 0
    WHERE idTareas = @idTareas;

    SELECT 1 AS Codigo, 'Proyecto eliminado lógicamente' AS Mensaje;
END;
GO


-- Eliminar una Subtarea
Create PROCEDURE Eliminar_Subtarea
    @idSubtareas INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Subtareas
    SET Activo = 0
    WHERE idSubtareas = @idSubtareas;

    SELECT 
        @idSubtareas AS idSubtareas,
        1 AS Codigo,
        'Subtarea Eliminada exitosamente.' AS Mensaje;
END;
GO



CREATE PROCEDURE Eliminar_Comentario
    @idComentarios INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Comentarios
    SET Activo = 0
    WHERE idComentarios = @idComentarios;

    SELECT @idComentarios AS idComentarios, 'Comentario eliminado lógicamente' AS Mensaje;
END;
GO





-- Eliminar un Permiso
CREATE PROCEDURE Eliminar_Permiso
    @idPermisos INT
AS
BEGIN
    DELETE FROM Permisos
    WHERE idPermisos = @idPermisos;

    SELECT 1 AS Codigo, 'Permiso eliminado exitosamente' AS Mensaje;
END;
GO


-- Eliminar un Rol
CREATE PROCEDURE Eliminar_Rol
    @idRoles INT
AS
BEGIN
    DELETE FROM Roles
    WHERE idRoles = @idRoles;

    SELECT 1 AS Codigo, 'Rol eliminado exitosamente' AS Mensaje;
END;
GO




-- Eliminar un Usuario
Create PROCEDURE Eliminar_Usuario
    @idUsuarios INT
AS
BEGIN
    UPDATE Usuarios
    SET Activo = 0
    WHERE idUsuarios = @idUsuarios;

    SELECT 1 AS Codigo, 'Usuario marcado como inactivo exitosamente' AS Mensaje;
END;
GO


-- Eliminar un Miembro de Equipo
CREATE PROCEDURE Eliminar_Miembro_Equipo
    @idMiembros_de_equipos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el miembro existe
    IF NOT EXISTS (SELECT 1 FROM Miembros_de_equipos WHERE idMiembros_de_equipos = @idMiembros_de_equipos)
    BEGIN
        SELECT -1 AS Codigo, 'El miembro no existe' AS Mensaje;
        RETURN;
    END;

    -- Eliminar el miembro del equipo
    DELETE FROM Miembros_de_equipos
    WHERE idMiembros_de_equipos = @idMiembros_de_equipos;

    SELECT 1 AS Codigo, 'Miembro eliminado exitosamente' AS Mensaje;
END;
GO



-- Eliminar un Historial de Cambios
CREATE PROCEDURE Eliminar_Historial_Cambio
    @idHistorial_de_cambios INT
AS
BEGIN
    DELETE FROM Historial_de_cambios
    WHERE idHistorial_de_cambios = @idHistorial_de_cambios;
END;
GO

Create PROCEDURE Eliminar_Comentario_Proyectos
    @idComentario INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Comentarios_Proyectos
        SET Activo = 0
        WHERE idComentario = @idComentario;

        SELECT 1 AS Codigo, 'Comentario eliminado correctamente.' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Codigo, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO



CREATE PROCEDURE Eliminar_Comentario_Tarea
    @idComentario INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Comentarios_Tareas
        SET Activo = 0
        WHERE idComentario = @idComentario;

        IF @@ROWCOUNT = 0
            THROW 50000, 'El comentario no existe.', 1;

        SELECT 1 AS Codigo, 'Comentario eliminado correctamente.' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Codigo, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO


CREATE PROCEDURE Eliminar_Comentario_Subtarea
    @idComentario INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Comentarios_Subtareas
        SET Activo = 0
        WHERE idComentario = @idComentario;

        IF @@ROWCOUNT = 0
            THROW 50000, 'El comentario no existe.', 1;

        SELECT 1 AS Codigo, 'Comentario eliminado correctamente.' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Codigo, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
