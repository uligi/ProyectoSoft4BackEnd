use DB_GP
GO

CREATE PROCEDURE sp_GetProyectos
    @FechaInicio DATE,
    @Estado VARCHAR(45)
AS
BEGIN
    SELECT 
        p.idProyectos,
        p.NombreProyecto,
        p.Descripcion,
        p.Estado,
        p.FechaInicio,
        p.FechaFinal
    FROM Proyectos p
    WHERE p.FechaInicio >= @FechaInicio
      AND p.Estado = @Estado
      AND p.Activo = 1;
END;
GO
CREATE PROCEDURE sp_GetTareas
    @IdUsuario INT,
    @Prioridad VARCHAR(45)
AS
BEGIN
    SELECT 
        t.idTareas,
        t.NombreTareas,
        t.Descripcion,
        t.Prioridad,
        t.FechaInicio,
        t.FechaFinal
    FROM Tareas t
    WHERE t.idUsuarios = @IdUsuario
      AND t.Prioridad = @Prioridad
      AND t.Activo = 1;
END;
GO
