use DB_GP
go

CREATE TRIGGER trg_Tareas_AfterUpdate
ON Tareas
AFTER UPDATE
AS
BEGIN
    INSERT INTO Historial_de_cambios (idTareas, Descripcioncambio, FechaCambio, idProyectos, idUsuarios)
    SELECT 
        i.idTareas, 
        'Actualización en Tareas', 
        GETDATE(), 
        i.idProyectos,
        i.idUsuarios
    FROM inserted i;
END;
GO

CREATE TRIGGER trg_Proyectos_AfterUpdate
ON Proyectos
AFTER UPDATE
AS
BEGIN
    INSERT INTO Historial_de_cambios (idProyectos, Descripcioncambio, FechaCambio, idPortafolio)
    SELECT 
        i.idProyectos, 
        'Actualización en Proyectos', 
        GETDATE(), 
        i.idPortafolio
    FROM inserted i;
END;
GO

CREATE TRIGGER trg_Portafolio_AfterUpdate
ON Portafolio
AFTER UPDATE
AS
BEGIN
    INSERT INTO Historial_de_cambios (Portafolio_idPortafolio, Descripcioncambio, FechaCambio)
    SELECT 
        i.idPortafolio, 
        'Actualización en Portafolio', 
        GETDATE()
    FROM inserted i;
END;
GO

CREATE TRIGGER trg_Subtareas_AfterUpdate
ON Subtareas
AFTER UPDATE
AS
BEGIN
    INSERT INTO Historial_de_cambios (idSubtareas, Descripcioncambio, FechaCambio, idTareas)
    SELECT 
        i.idSubtareas, 
        'Actualización en Subtareas', 
        GETDATE(), 
        i.idTareas
    FROM inserted i;
END;
GO

CREATE TRIGGER trg_Usuarios_AfterUpdate
ON Usuarios
AFTER UPDATE
AS
BEGIN
    INSERT INTO Historial_de_cambios (idUsuarios, Descripcioncambio, FechaCambio)
    SELECT 
        i.idUsuarios, 
        'Actualización en Usuarios', 
        GETDATE()
    FROM inserted i;
END;
GO

ALTER TABLE Historial_de_cambios
ALTER COLUMN idPortafolio INT NULL;

ALTER TABLE Historial_de_cambios
ALTER COLUMN idProyectos INT NULL;

ALTER TABLE Historial_de_cambios
ALTER COLUMN idTareas INT NULL;

ALTER TABLE Historial_de_cambios
ALTER COLUMN idSubtareas INT NULL;
