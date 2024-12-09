use DB_GP
GO

CREATE OR ALTER PROCEDURE sp_GetProyectos
    @IdUsuario INT = NULL,
    @IdEquipo INT = NULL,
    @IdPortafolio INT = NULL
AS
BEGIN
    SELECT 
        p.idProyectos,
        p.NombreProyecto,
        p.Descripcion,
        p.Estado,
        p.FechaInicio,
        p.FechaFinal,
        e.NombreEquipos AS Equipo,
        pf.NombrePortafolio AS Portafolio
    FROM Proyectos p
    INNER JOIN Equipos e ON p.Equipos_idEquipos = e.idEquipos
    INNER JOIN Portafolio pf ON p.idPortafolio = pf.idPortafolio
    WHERE (@IdUsuario IS NULL OR p.Equipos_idEquipos = @IdEquipo)
      AND (@IdPortafolio IS NULL OR p.idPortafolio = @IdPortafolio)
      AND p.Activo = 1
    ORDER BY p.Prioridad DESC, p.FechaInicio ASC;
END;
GO





CREATE OR ALTER PROCEDURE sp_GetTareas
    @IdUsuario INT = NULL,
    @IdEquipo INT = NULL,
    @IdPortafolio INT = NULL
AS
BEGIN
    SELECT 
        t.idTareas,
        t.NombreTareas,
        t.Descripcion,
        t.Prioridad,
        t.FechaInicio,
        t.FechaFinal,
        p.NombreProyecto AS Proyecto,
        e.NombreEquipos AS Equipo,
        pf.NombrePortafolio AS Portafolio
    FROM Tareas t
    INNER JOIN Proyectos p ON t.idProyectos = p.idProyectos
    INNER JOIN Equipos e ON p.Equipos_idEquipos = e.idEquipos
    INNER JOIN Portafolio pf ON p.idPortafolio = pf.idPortafolio
    WHERE (@IdUsuario IS NULL OR t.idUsuarios = @IdUsuario)
      AND (@IdEquipo IS NULL OR p.Equipos_idEquipos = @IdEquipo)
      AND (@IdPortafolio IS NULL OR p.idPortafolio = @IdPortafolio)
      AND t.Activo = 1
    ORDER BY t.FechaInicio ASC, t.Prioridad DESC;
END;
GO



