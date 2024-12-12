use DB_GP
GO
---------------------Permisos------------------------------

EXEC Crear_Permiso @Nombre_Permisos = 'Nivel 1', @Activo = 1;
EXEC Crear_Permiso @Nombre_Permisos = 'Nivel 2', @Activo = 1;
EXEC Crear_Permiso @Nombre_Permisos = 'Nivel 3', @Activo = 1;

---------------------Roles------------------------------
use DB_GP
GO
EXEC Crear_Rol @Nombre_Roles = 'Administrador', @Activo = 1, @idPermisos = 1;
GO
use DB_GP
GO
EXEC Crear_Rol @Nombre_Roles = 'Gerente', @Activo = 1, @idPermisos = 2;
GO
EXEC Crear_Rol @Nombre_Roles = 'Recursos Humanos', @Activo = 1, @idPermisos = 2;
GO
EXEC Crear_Rol @Nombre_Roles = 'Empleado', @Activo = 1, @idPermisos = 3;
GO

---------------------Usuarios------------------------------

-- Funci�n para encriptar una contrase�a con SHA-256
SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'carlos123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Carlos Fern�ndez', 
    @Email = 'carlos.fernandez@example.com', 
    @Contrasena = 'ac9c2c34c9f7ad52528c3422af40a66e2e24aaf2a727831255413c9470158984', 
    @idRoles = 1;
GO

use DB_GP
GO
SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'ana123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Ana Mar�a L�pez', 
    @Email = 'ana.lopez@example.com', 
    @Contrasena = 'e82827b00b2ca8620beb37f879778c082b292a52270390cff35b6fe3157f4e8b', 
    @idRoles = 2;
GO

SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'luis123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Luis Alberto', 
    @Email = 'luis.alberto@example.com', 
    @Contrasena = 'ec7908dc8241f0e4340266990dfe6001b1757084d891c6758bfaac826750009a', 
    @idRoles = 3;
GO

SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'maria123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Mar�a Garc�a', 
    @Email = 'maria.garcia@example.com', 
    @Contrasena = '626e3c805e77eeb472c42c6be607be2af7ac5c08fd7050f278e0330fe81abf57', 
    @idRoles = 3;
GO

SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'jorge123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Jorge Mart�nez', 
    @Email = 'jorge.martinez@example.com', 
    @Contrasena = 'f69ed3a744195d7d1429b167f449ed9a76f36ba6a74f997a7bfe179649de32c4', 
    @idRoles = 4;
GO


---------------------Portafolio------------------------------

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Infraestructura P�blica Nacional', 
    @Descripcion = 'Proyectos relacionados con la construcci�n y mantenimiento de infraestructura p�blica, como carreteras, puentes y hospitales.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Tecnolog�a y Comunicaci�n', 
    @Descripcion = 'Proyectos gubernamentales enfocados en la modernizaci�n tecnol�gica y la mejora de las comunicaciones.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Desarrollo Social', 
    @Descripcion = 'Iniciativas para mejorar la calidad de vida, como programas educativos y de salud.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Energ�as Renovables', 
    @Descripcion = 'Proyectos para fomentar el uso de energ�as limpias y sostenibles en el pa�s.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Seguridad Nacional', 
    @Descripcion = 'Proyectos relacionados con la mejora de la seguridad p�blica y la protecci�n de la ciudadan�a.', 
    @Activo = 1;
GO

---------------------Equipos------------------------------

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de Desarrollo de Software', 
    @Activo = 1;
GO

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de An�lisis de Datos', 
    @Activo = 1;
GO

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de Infraestructura', 
    @Activo = 1;
GO

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de Proyectos Sociales', 
    @Activo = 1;
GO

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de Seguridad Inform�tica', 
    @Activo = 1;
GO


---------------------Proyectos------------------------------

EXEC Crear_Proyecto 
    @NombreProyecto = 'Modernizaci�n del Sistema de Transporte P�blico', 
    @Descripcion = 'Proyecto enfocado en la mejora del sistema de transporte p�blico mediante el uso de tecnolog�as avanzadas.', 
    @FechaEstimada = '2025-12-31', 
    @FechaInicio = '2024-01-15', 
    @FechaFinal = '2025-12-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 1, -- Infraestructura P�blica Nacional
    @Equipos_idEquipos = 1, -- Equipo de Desarrollo de Software
    @Estado = 'En Progreso';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Implementaci�n de Redes de Banda Ancha', 
    @Descripcion = 'Ampliaci�n de la cobertura de internet de alta velocidad a zonas rurales.', 
    @FechaEstimada = '2024-11-30', 
    @FechaInicio = '2024-02-01', 
    @FechaFinal = '2024-11-15', 
    @Prioridad = 'Media', 
    @idPortafolio = 2, -- Tecnolog�a y Comunicaci�n
    @Equipos_idEquipos = 3, -- Equipo de Infraestructura
    @Estado = 'Planificaci�n';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Programa Nacional de Alfabetizaci�n Digital', 
    @Descripcion = 'Iniciativa para equipar a la poblaci�n con habilidades b�sicas en tecnolog�as de la informaci�n.', 
    @FechaEstimada = '2026-06-30', 
    @FechaInicio = '2024-07-01', 
    @FechaFinal = '2026-06-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 3, -- Desarrollo Social
    @Equipos_idEquipos = 2, -- Equipo de An�lisis de Datos
    @Estado = 'Planificaci�n';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Implementaci�n de Energ�a Solar en Edificios Gubernamentales', 
    @Descripcion = 'Adopci�n de paneles solares en instituciones gubernamentales para reducir costos y fomentar la sostenibilidad.', 
    @FechaEstimada = '2025-09-30', 
    @FechaInicio = '2024-03-01', 
    @FechaFinal = '2025-09-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 4, -- Energ�as Renovables
    @Equipos_idEquipos = 4, -- Equipo de Proyectos Sociales
    @Estado = 'En Progreso';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Fortalecimiento de la Seguridad Cibern�tica Nacional', 
    @Descripcion = 'Creaci�n de una estrategia integral para proteger activos digitales y datos del gobierno.', 
    @FechaEstimada = '2026-01-31', 
    @FechaInicio = '2024-05-01', 
    @FechaFinal = '2026-01-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 5, -- Seguridad Nacional
    @Equipos_idEquipos = 5, -- Equipo de Seguridad Inform�tica
    @Estado = 'Planificaci�n';
GO


---------------------Tareas------------------------------

EXEC Crear_Tarea 
    @NombreTareas = 'Dise�ar arquitectura del sistema de transporte', 
    @Descripcion = 'Crear un esquema de la arquitectura del sistema para el proyecto de modernizaci�n de transporte.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-01-16', 
    @FechaFinal = '2024-02-15', 
    @idProyectos = 1, -- Modernizaci�n del Sistema de Transporte P�blico
    @idUsuarios = 1, -- Asignado a Carlos Fern�ndez
    @Estado = 'En Progreso';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Configurar servidores de banda ancha', 
    @Descripcion = 'Instalaci�n y configuraci�n de servidores para soportar redes de banda ancha.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-02-02', 
    @FechaFinal = '2024-04-01', 
    @idProyectos = 2, -- Implementaci�n de Redes de Banda Ancha
    @idUsuarios = 2, -- Asignado a Ana Mar�a L�pez
    @Estado = 'Planificaci�n';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Crear material educativo digital', 
    @Descripcion = 'Desarrollar gu�as y recursos interactivos para el programa de alfabetizaci�n digital.', 
    @Prioridad = 'Media', 
    @FechaInicio = '2024-07-02', 
    @FechaFinal = '2024-08-15', 
    @idProyectos = 3, -- Programa Nacional de Alfabetizaci�n Digital
    @idUsuarios = 3, -- Asignado a Luis Alberto
    @Estado = 'Pendiente';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Instalar paneles solares', 
    @Descripcion = 'Supervisar la instalaci�n de paneles solares en edificios seleccionados.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-03-02', 
    @FechaFinal = '2024-06-01', 
    @idProyectos = 4, -- Implementaci�n de Energ�a Solar en Edificios Gubernamentales
    @idUsuarios = 4, -- Asignado a Mar�a Garc�a
    @Estado = 'En Progreso';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Auditar infraestructura de seguridad digital', 
    @Descripcion = 'Evaluar y auditar la infraestructura actual para identificar vulnerabilidades.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-05-02', 
    @FechaFinal = '2024-07-01', 
    @idProyectos = 5, -- Fortalecimiento de la Seguridad Cibern�tica Nacional
    @idUsuarios = 5, -- Asignado a Jorge Mart�nez
    @Estado = 'Planificaci�n';
GO

---------------------Sub-Tareas------------------------------

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Definir requisitos del sistema', 
    @Descripcion = 'Identificar y documentar los requisitos necesarios para el sistema de transporte.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-01-17', 
    @FechaFinal = '2024-01-31', 
    @idTareas = 1, -- Dise�ar arquitectura del sistema de transporte
    @Estado = 'En Progreso';
GO

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Configurar red de prueba', 
    @Descripcion = 'Implementar una red de prueba para validar el rendimiento de los servidores.', 
    @Prioridad = 'Media', 
    @FechaInicio = '2024-02-05', 
    @FechaFinal = '2024-02-20', 
    @idTareas = 2, -- Configurar servidores de banda ancha
    @Estado = 'Pendiente';
GO

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Dise�ar interfaz interactiva', 
    @Descripcion = 'Crear una interfaz gr�fica para las gu�as de aprendizaje digital.', 
    @Prioridad = 'Media', 
    @FechaInicio = '2024-07-03', 
    @FechaFinal = '2024-07-15', 
    @idTareas = 3, -- Crear material educativo digital
    @Estado = 'Planificaci�n';
GO

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Supervisar instalaci�n el�ctrica', 
    @Descripcion = 'Revisar las conexiones el�ctricas previas a la instalaci�n de los paneles.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-03-05', 
    @FechaFinal = '2024-03-20', 
    @idTareas = 4, -- Instalar paneles solares
    @Estado = 'Pendiente';
GO

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Generar informe de vulnerabilidades', 
    @Descripcion = 'Documentar los hallazgos tras la auditor�a de seguridad.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-05-10', 
    @FechaFinal = '2024-05-25', 
    @idTareas = 5, -- Auditar infraestructura de seguridad digital
    @Estado = 'Planificaci�n';
GO

---------------------Comentarios proyectos------------------------------

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'El dise�o inicial del sistema est� alineado con los objetivos del proyecto.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 1, -- Modernizaci�n del Sistema de Transporte P�blico
    @idUsuario = 1; -- Carlos Fern�ndez
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'Es necesario ampliar la cobertura en regiones con menos acceso a la banda ancha.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 2, -- Implementaci�n de Redes de Banda Ancha
    @idUsuario = 2; -- Ana Mar�a L�pez
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'El material educativo est� enfocado en el p�blico objetivo y cumple con los est�ndares requeridos.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 3, -- Programa Nacional de Alfabetizaci�n Digital
    @idUsuario = 3; -- Luis Alberto
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'La instalaci�n de paneles solares est� avanzando seg�n lo planeado.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 4, -- Implementaci�n de Energ�a Solar en Edificios Gubernamentales
    @idUsuario = 4; -- Mar�a Garc�a
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'Es importante considerar medidas adicionales para proteger contra amenazas emergentes.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 5, -- Fortalecimiento de la Seguridad Cibern�tica Nacional
    @idUsuario = 5; -- Jorge Mart�nez
GO

---------------------Comentarios tareas------------------------------

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'Los requisitos est�n bien definidos, pero es necesario validar con todas las partes interesadas.', 
    @FechaCreacion = '2024-01-18', 
    @idTarea = 1, -- Dise�ar arquitectura del sistema de transporte
    @idUsuario = 1; -- Carlos Fern�ndez
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'La configuraci�n de los servidores requiere ajustes en la seguridad de la red.', 
    @FechaCreacion = '2024-02-06', 
    @idTarea = 2, -- Configurar servidores de banda ancha
    @idUsuario = 2; -- Ana Mar�a L�pez
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'El dise�o de la interfaz est� en progreso y luce prometedor.', 
    @FechaCreacion = '2024-07-05', 
    @idTarea = 3, -- Crear material educativo digital
    @idUsuario = 3; -- Luis Alberto
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'La supervisi�n el�ctrica inicial est� completa; no se encontraron problemas graves.', 
    @FechaCreacion = '2024-03-06', 
    @idTarea = 4, -- Instalar paneles solares
    @idUsuario = 4; -- Mar�a Garc�a
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'La auditor�a inicial revel� vulnerabilidades cr�ticas; se recomienda actuar con urgencia.', 
    @FechaCreacion = '2024-05-12', 
    @idTarea = 5, -- Auditar infraestructura de seguridad digital
    @idUsuario = 5; -- Jorge Mart�nez
GO

---------------------Comentarios Sub-tareas------------------------------

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'Los requisitos del sistema han sido revisados y aprobados por el equipo.', 
    @FechaCreacion = '2024-01-20', 
    @idSubtarea = 1, -- Definir requisitos del sistema
    @idUsuario = 1; -- Carlos Fern�ndez
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'La red de prueba est� configurada correctamente, pero necesita ajustes en la seguridad.', 
    @FechaCreacion = '2024-02-10', 
    @idSubtarea = 2, -- Configurar red de prueba
    @idUsuario = 2; -- Ana Mar�a L�pez
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'El dise�o de la interfaz gr�fica est� completo y listo para revisi�n.', 
    @FechaCreacion = '2024-07-10', 
    @idSubtarea = 3, -- Dise�ar interfaz interactiva
    @idUsuario = 3; -- Luis Alberto
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'La instalaci�n el�ctrica se realiz� seg�n los est�ndares de seguridad.', 
    @FechaCreacion = '2024-03-15', 
    @idSubtarea = 4, -- Supervisar instalaci�n el�ctrica
    @idUsuario = 4; -- Mar�a Garc�a
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'El informe de vulnerabilidades est� en proceso; se espera completarlo en dos d�as.', 
    @FechaCreacion = '2024-05-15', 
    @idSubtarea = 5, -- Generar informe de vulnerabilidades
    @idUsuario = 5; -- Jorge Mart�nez
