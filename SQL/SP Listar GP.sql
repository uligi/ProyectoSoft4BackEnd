USE DB_GP;
GO

-- Stored Procedure para la tabla Proyectos con JOIN a Portafolio y Equipos
CREATE or alter PROCEDURE [dbo].[sp_Listar_Proyectos]
AS
BEGIN
SELECT 
    p.idProyectos,
    p.NombreProyecto,
    p.Descripcion,
    p.FechaEstimada,
    p.FechaInicio,
    p.FechaFinal,
    p.Prioridad,
	p.Estado,
    pf.NombrePortafolio,
    e.NombreEquipos,
    p.Activo
FROM Proyectos p
INNER JOIN Portafolio pf ON p.idPortafolio = pf.idPortafolio
INNER JOIN Equipos e ON p.Equipos_idEquipos = e.idEquipos;

END;

GO

-- Stored Procedure para la tabla Tareas con JOIN a Proyectos y Usuarios
CREATE or alter PROCEDURE Listar_Tareas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.idTareas,
        t.NombreTareas,
        t.Descripcion,
        t.Prioridad,
        t.FechaInicio,
        t.FechaFinal,
        t.Activo,
        t.idProyectos,
        t.idUsuarios,
		t.Estado,
        p.NombreProyecto,
        u.Nombre AS NombreUsuario
    FROM Tareas t
    LEFT JOIN Proyectos p ON t.idProyectos = p.idProyectos
    LEFT JOIN Usuarios u ON t.idUsuarios = u.idUsuarios
	WHERE t.Activo = 1; -- Solo tareas activas;
END;
GO


-- Stored Procedure para la tabla Subtareas con JOIN a Tareas
CREATE or alter PROCEDURE Listar_Subtareas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ST.idSubtareas,
        ST.NombreSubtareas,
        ST.Descripcion,
        ST.Prioridad,
        ST.FechaInicio,
        ST.FechaFinal,
        ST.idTareas,
		st.Estado,
		t.Activo,
        T.NombreTareas AS NombreTarea
    FROM Subtareas ST
    INNER JOIN Tareas T ON ST.idTareas = T.idTareas
	WHERE t.Activo = 1; -- Solo tareas activas;
END;
GO

-- Stored Procedure para la tabla Miembros_de_equipos con JOIN a Equipos, Usuarios y Roles
CREATE or alter PROCEDURE Listar_Todos_Los_Miembros
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        me.idMiembros_de_equipos, 
        me.idEquipos, 
        me.idUsuarios, 
        u.Nombre AS NombreUsuario, 
        e.NombreEquipos

    FROM Miembros_de_equipos me
    INNER JOIN Usuarios u ON me.idUsuarios = u.idUsuarios
    INNER JOIN Equipos e ON me.idEquipos = e.idEquipos;

END;
GO




-- Stored Procedure para la tabla Historial_de_cambios con JOIN a Tareas, Proyectos y Portafolio
CREATE or alter PROCEDURE [dbo].[sp_Listar_Historial_Cambios]
AS
BEGIN
    SELECT 
        hc.idHistorial_de_cambios,
        hc.idTareas,
        t.NombreTareas AS NombreTareas,
        hc.idProyectos,
        p.NombreProyecto AS NombreProyecto,
        hc.Portafolio_idPortafolio,
        pf.NombrePortafolio AS NombrePortafolio,
        hc.Descripcioncambio,
        hc.FechaCambio
    FROM Historial_de_cambios hc
    LEFT JOIN Tareas t ON hc.idTareas = t.idTareas
    LEFT JOIN Proyectos p ON hc.idProyectos = p.idProyectos
    LEFT JOIN Portafolio pf ON hc.Portafolio_idPortafolio = pf.idPortafolio;
END;
GO

CREATE or alter PROCEDURE [dbo].[sp_Listar_Permisos]
AS
BEGIN
    SELECT 
        idPermisos,
        Nombre_Permisos,
        Activo
    FROM Permisos;
END;
GO

CREATE or alter PROCEDURE [dbo].[sp_Listar_Roles]
AS
BEGIN
    SELECT 
        r.idRoles,
        r.Nombre_Roles,
        r.Activo,
        r.idPermisos,
        p.Nombre_Permisos AS NombrePermisos
    FROM Roles r
    LEFT JOIN Permisos p ON r.idPermisos = p.idPermisos;
END;
GO



CREATE or alter PROCEDURE [dbo].[sp_Listar_Usuarios]
AS
BEGIN
    SELECT 
        u.idUsuarios,
        u.Nombre,
        u.Email,
        u.Activo,
        u.FechaRegistro,
        u.RestablecerContrasena,
        u.idRoles,
        r.Nombre_Roles AS Nombre_Roles,
        p.Nombre_Permisos AS PermisoRelacionado,
		u.contrasena
    FROM Usuarios u
    LEFT JOIN Roles r ON u.idRoles = r.idRoles
    LEFT JOIN Permisos p ON r.idPermisos = p.idPermisos; -- Cambiado a idPermisos
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_Listar_Usuarios_Activos]
AS
BEGIN
    SET NOCOUNT ON; -- Evita mensajes adicionales que puedan interferir con los resultados.

    SELECT 
        u.idUsuarios,
        u.Nombre,
        u.Email,
        u.Activo,
        u.FechaRegistro,
        u.RestablecerContrasena,
        u.idRoles,
        r.Nombre_Roles AS Nombre_Roles,
        p.Nombre_Permisos AS PermisoRelacionado,
        u.contrasena
    FROM Usuarios u
    LEFT JOIN Roles r ON u.idRoles = r.idRoles
    LEFT JOIN Permisos p ON r.idPermisos = p.idPermisos
    WHERE u.Activo = 1; -- Filtra solo los usuarios activos.
END;
GO



CREATE or alter PROCEDURE [dbo].[Listar_Portafolios]
AS
BEGIN
    SELECT idPortafolio, NombrePortafolio, Descripcion, Activo, FechaCreacion
    FROM Portafolio
    ORDER BY FechaCreacion DESC;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[Listar_Portafolios_Activos]
AS
BEGIN
    SELECT idPortafolio, NombrePortafolio, Descripcion, Activo, FechaCreacion
    FROM Portafolio
    WHERE Activo = 1
    ORDER BY FechaCreacion DESC;
END;
GO


CREATE or alter PROCEDURE [dbo].[Listar_Equipos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idEquipos, NombreEquipos, Activo, Fecha_Registro
    FROM Equipos
   
END;
GO

CREATE or alter PROCEDURE [dbo].[Listar_Equipos_Activos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idEquipos, NombreEquipos, Activo, Fecha_Registro
    FROM Equipos
    WHERE Activo = 1;
END;
GO
CREATE or alter PROCEDURE [dbo].[Listar_Comentarios_Proyectos]
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            cp.idComentario, 
            ISNULL(cp.Comentario, 'Sin Comentario') AS Comentario, 
            ISNULL(cp.FechaCreacion, GETDATE()) AS FechaCreacion, 
            ISNULL(cp.Activo, 0) AS Activo, 
            cp.idProyecto, -- Agregamos esta columna
            ISNULL(p.NombreProyecto, 'Sin Proyecto') AS NombreProyecto, 
            cp.idUsuario, -- Agregamos esta columna
            ISNULL(u.Nombre, 'Sin Usuario') AS NombreUsuario
        FROM Comentarios_Proyectos cp
        LEFT JOIN Proyectos p ON cp.idProyecto = p.idProyectos
        LEFT JOIN Usuarios u ON cp.idUsuario = u.idUsuarios;
    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_MESSAGE() AS ErrorMessage, 
            ERROR_NUMBER() AS ErrorNumber, 
            ERROR_STATE() AS ErrorState;
    END CATCH
END;
GO

CREATE or alter PROCEDURE Listar_Comentarios_Tareas
AS
BEGIN
    SELECT 
        c.idComentario,
        c.Comentario,
        c.FechaCreacion,
        c.Activo,
        t.NombreTareas AS NombreTarea,
        u.Nombre AS NombreUsuario
    FROM Comentarios_Tareas c
    INNER JOIN Tareas t ON c.idTarea = t.idTareas
    INNER JOIN Usuarios u ON c.idUsuario = u.idUsuarios
    WHERE c.Activo = 1;
END;
GO


CREATE or alter PROCEDURE Listar_Comentarios_Subtareas
AS
BEGIN
    SELECT 
        c.idComentario,
        c.Comentario,
        c.FechaCreacion,
        c.Activo,
        s.NombreSubtareas AS NombreSubtarea,
        u.Nombre AS NombreUsuario
    FROM Comentarios_Subtareas c
    INNER JOIN Subtareas s ON c.idSubtarea = s.idSubtareas
    INNER JOIN Usuarios u ON c.idUsuario = u.idUsuarios
    WHERE c.Activo = 1;
END;
GO


CREATE or alter PROCEDURE Listar_Comentarios_Por_Proyecto
    @idProyecto INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            c.idComentario,
            c.Comentario,
            c.FechaCreacion,
            c.Activo,
            c.idProyecto, -- Campo requerido por el modelo
            u.idUsuarios AS idUsuario, -- Alias ajustado para coincidir con el modelo
            u.Nombre AS NombreUsuario,
            p.NombreProyecto
        FROM Comentarios_Proyectos c
        INNER JOIN Usuarios u ON c.idUsuario = u.idUsuarios
        INNER JOIN Proyectos p ON c.idProyecto = p.idProyectos
        WHERE c.idProyecto = @idProyecto AND c.Activo = 1;

    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS CodigoError,
            ERROR_MESSAGE() AS MensajeError;
    END CATCH
END;
GO

CREATE or alter PROCEDURE Listar_Comentarios_Por_Tarea
    @idTarea INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            c.idComentario,
            c.Comentario,
            c.FechaCreacion,
            c.Activo,
            c.idTarea AS idTareas, -- Campo requerido por el modelo
            u.idUsuarios AS idUsuario, -- Alias ajustado para coincidir con el modelo
            u.Nombre AS NombreUsuario,
            p.NombreTareas AS NombreTarea
        FROM Comentarios_Tareas c
        INNER JOIN Usuarios u ON c.idUsuario = u.idUsuarios
        INNER JOIN Tareas p ON c.idTarea = p.idTareas
        WHERE c.idTarea = @idTarea AND c.Activo = 1;

    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS CodigoError,
            ERROR_MESSAGE() AS MensajeError;
    END CATCH
END;
GO

CREATE or alter PROCEDURE Listar_Comentarios_Por_SubTarea
    @idSubTarea INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
      SELECT 
			c.idComentario,
			c.Comentario,
			c.FechaCreacion,
			c.Activo,
			c.idSubtarea AS idSubtareas, -- Debe coincidir con el modelo
			u.idUsuarios AS idUsuario,   -- Alias ajustado
			u.Nombre AS NombreUsuario,
			p.NombreSubtareas AS NombreSubtarea -- Alias correcto
		FROM Comentarios_SubTareas c
		INNER JOIN Usuarios u ON c.idUsuario = u.idUsuarios
		INNER JOIN Subtareas p ON c.idSubtarea = p.idSubtareas
		WHERE c.idSubtarea = @idSubTarea AND c.Activo = 1;


    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS CodigoError,
            ERROR_MESSAGE() AS MensajeError;
    END CATCH
END;
GO

CREATE or alter PROCEDURE [dbo].[ListarProyectosPorUsuario]
    @idUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        p.idProyectos,
        p.NombreProyecto,
        p.Descripcion,
        p.FechaEstimada,
        p.FechaInicio,
        p.FechaFinal,
        p.Prioridad,
		e.idEquipos,
		p.idPortafolio,
        p.Estado,
		p.Activo,
		p.Equipos_idEquipos,
        pf.NombrePortafolio,
        e.NombreEquipos
    FROM Proyectos p
    INNER JOIN Equipos e ON p.Equipos_idEquipos = e.idEquipos
    INNER JOIN Miembros_de_equipos me ON e.idEquipos = me.idEquipos
    INNER JOIN Portafolio pf ON p.idPortafolio = pf.idPortafolio
    WHERE me.idUsuarios = @idUsuario
        AND p.Activo = 1;

    -- Gerentes podrían tener lógica adicional si los roles necesitan más datos
END;
GO

CREATE OR ALTER PROCEDURE Listar_Tareas_Por_Proyecto
    @idProyectos INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.idTareas,
        t.NombreTareas,
        t.Descripcion,
        t.Prioridad,
        t.FechaInicio,
        t.FechaFinal,
        t.Estado,
        t.Activo,
        t.idProyectos,
        p.NombreProyecto, -- Añadir esta columna
        u.Nombre AS NombreUsuario
    FROM Tareas t
    LEFT JOIN Proyectos p ON t.idProyectos = p.idProyectos
    LEFT JOIN Usuarios u ON t.idUsuarios = u.idUsuarios
    WHERE t.idProyectos = @idProyectos
      AND t.Activo = 1; -- Solo tareas activas
END;
GO

CREATE OR ALTER PROCEDURE sp_ObtenerTareaPorID
    @idTarea INT
AS
BEGIN
    SELECT 
        T.idTareas,
        T.NombreTareas,
        T.Descripcion,
        T.Prioridad,
        T.FechaInicio,
        T.FechaFinal,
        T.Estado,
        T.Activo,
        U.Nombre AS NombreUsuario, -- Corregir el alias aquí
        P.NombreProyecto
    FROM Tareas T
    LEFT JOIN Usuarios U ON T.idUsuarios = U.idUsuarios
    LEFT JOIN Proyectos P ON T.idProyectos = P.idProyectos
    WHERE T.idTareas = @idTarea;
END;
GO



CREATE OR ALTER PROCEDURE sp_ObtenerSubtareasPorIDTarea
    @idTarea INT
AS
BEGIN
    SELECT 
        ST.idSubtareas,
        ST.NombreSubtareas,
        ST.Descripcion,
        ST.Prioridad,
        ST.FechaInicio,
        ST.FechaFinal,
        ST.Estado,
        ST.Activo,
        ST.idTareas,
        T.NombreTareas AS NombreTarea -- Se añade el nombre de la tarea asociada
    FROM Subtareas ST
    INNER JOIN Tareas T ON ST.idTareas = T.idTareas
    WHERE ST.idTareas = @idTarea;
END;
GO



CREATE OR ALTER PROCEDURE ListarTareasPorUsuario
    @idUsuario INT
AS
BEGIN
    SELECT 
        t.idTareas,
        t.NombreTareas,
        t.Descripcion,
        t.Prioridad,
        t.FechaInicio,
        t.FechaFinal,
        t.Estado,
        t.Activo,
        t.idProyectos,
        p.NombreProyecto,
        u.Nombre AS NombreUsuario
    FROM Tareas t
    INNER JOIN Proyectos p ON t.idProyectos = p.idProyectos
    INNER JOIN Usuarios u ON t.idUsuarios = u.idUsuarios
    WHERE t.idUsuarios = @idUsuario AND t.Activo = 1; -- Solo tareas activas
END;
GO
