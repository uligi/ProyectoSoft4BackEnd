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

-- Función para encriptar una contraseña con SHA-256
SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'carlos123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Carlos Fernández', 
    @Email = 'carlos.fernandez@example.com', 
    @Contrasena = 'ac9c2c34c9f7ad52528c3422af40a66e2e24aaf2a727831255413c9470158984', 
    @idRoles = 1;
GO

use DB_GP
GO
SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'ana123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Ana María López', 
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
    @Nombre = 'María García', 
    @Email = 'maria.garcia@example.com', 
    @Contrasena = '626e3c805e77eeb472c42c6be607be2af7ac5c08fd7050f278e0330fe81abf57', 
    @idRoles = 3;
GO

SELECT LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'jorge123'), 2)) AS ContrasenaHash;
GO
EXEC Crear_Usuario 
    @Nombre = 'Jorge Martínez', 
    @Email = 'jorge.martinez@example.com', 
    @Contrasena = 'f69ed3a744195d7d1429b167f449ed9a76f36ba6a74f997a7bfe179649de32c4', 
    @idRoles = 4;
GO


---------------------Portafolio------------------------------

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Infraestructura Pública Nacional', 
    @Descripcion = 'Proyectos relacionados con la construcción y mantenimiento de infraestructura pública, como carreteras, puentes y hospitales.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Tecnología y Comunicación', 
    @Descripcion = 'Proyectos gubernamentales enfocados en la modernización tecnológica y la mejora de las comunicaciones.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Desarrollo Social', 
    @Descripcion = 'Iniciativas para mejorar la calidad de vida, como programas educativos y de salud.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Energías Renovables', 
    @Descripcion = 'Proyectos para fomentar el uso de energías limpias y sostenibles en el país.', 
    @Activo = 1;
GO

EXEC Crear_Portafolio 
    @NombrePortafolio = 'Seguridad Nacional', 
    @Descripcion = 'Proyectos relacionados con la mejora de la seguridad pública y la protección de la ciudadanía.', 
    @Activo = 1;
GO

---------------------Equipos------------------------------

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de Desarrollo de Software', 
    @Activo = 1;
GO

EXEC Crear_Equipo 
    @NombreEquipos = 'Equipo de Análisis de Datos', 
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
    @NombreEquipos = 'Equipo de Seguridad Informática', 
    @Activo = 1;
GO


---------------------Proyectos------------------------------

EXEC Crear_Proyecto 
    @NombreProyecto = 'Modernización del Sistema de Transporte Público', 
    @Descripcion = 'Proyecto enfocado en la mejora del sistema de transporte público mediante el uso de tecnologías avanzadas.', 
    @FechaEstimada = '2025-12-31', 
    @FechaInicio = '2024-01-15', 
    @FechaFinal = '2025-12-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 1, -- Infraestructura Pública Nacional
    @Equipos_idEquipos = 1, -- Equipo de Desarrollo de Software
    @Estado = 'En Progreso';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Implementación de Redes de Banda Ancha', 
    @Descripcion = 'Ampliación de la cobertura de internet de alta velocidad a zonas rurales.', 
    @FechaEstimada = '2024-11-30', 
    @FechaInicio = '2024-02-01', 
    @FechaFinal = '2024-11-15', 
    @Prioridad = 'Media', 
    @idPortafolio = 2, -- Tecnología y Comunicación
    @Equipos_idEquipos = 3, -- Equipo de Infraestructura
    @Estado = 'Planificación';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Programa Nacional de Alfabetización Digital', 
    @Descripcion = 'Iniciativa para equipar a la población con habilidades básicas en tecnologías de la información.', 
    @FechaEstimada = '2026-06-30', 
    @FechaInicio = '2024-07-01', 
    @FechaFinal = '2026-06-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 3, -- Desarrollo Social
    @Equipos_idEquipos = 2, -- Equipo de Análisis de Datos
    @Estado = 'Planificación';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Implementación de Energía Solar en Edificios Gubernamentales', 
    @Descripcion = 'Adopción de paneles solares en instituciones gubernamentales para reducir costos y fomentar la sostenibilidad.', 
    @FechaEstimada = '2025-09-30', 
    @FechaInicio = '2024-03-01', 
    @FechaFinal = '2025-09-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 4, -- Energías Renovables
    @Equipos_idEquipos = 4, -- Equipo de Proyectos Sociales
    @Estado = 'En Progreso';
GO

EXEC Crear_Proyecto 
    @NombreProyecto = 'Fortalecimiento de la Seguridad Cibernética Nacional', 
    @Descripcion = 'Creación de una estrategia integral para proteger activos digitales y datos del gobierno.', 
    @FechaEstimada = '2026-01-31', 
    @FechaInicio = '2024-05-01', 
    @FechaFinal = '2026-01-15', 
    @Prioridad = 'Alta', 
    @idPortafolio = 5, -- Seguridad Nacional
    @Equipos_idEquipos = 5, -- Equipo de Seguridad Informática
    @Estado = 'Planificación';
GO


---------------------Tareas------------------------------

EXEC Crear_Tarea 
    @NombreTareas = 'Diseñar arquitectura del sistema de transporte', 
    @Descripcion = 'Crear un esquema de la arquitectura del sistema para el proyecto de modernización de transporte.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-01-16', 
    @FechaFinal = '2024-02-15', 
    @idProyectos = 1, -- Modernización del Sistema de Transporte Público
    @idUsuarios = 1, -- Asignado a Carlos Fernández
    @Estado = 'En Progreso';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Configurar servidores de banda ancha', 
    @Descripcion = 'Instalación y configuración de servidores para soportar redes de banda ancha.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-02-02', 
    @FechaFinal = '2024-04-01', 
    @idProyectos = 2, -- Implementación de Redes de Banda Ancha
    @idUsuarios = 2, -- Asignado a Ana María López
    @Estado = 'Planificación';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Crear material educativo digital', 
    @Descripcion = 'Desarrollar guías y recursos interactivos para el programa de alfabetización digital.', 
    @Prioridad = 'Media', 
    @FechaInicio = '2024-07-02', 
    @FechaFinal = '2024-08-15', 
    @idProyectos = 3, -- Programa Nacional de Alfabetización Digital
    @idUsuarios = 3, -- Asignado a Luis Alberto
    @Estado = 'Pendiente';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Instalar paneles solares', 
    @Descripcion = 'Supervisar la instalación de paneles solares en edificios seleccionados.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-03-02', 
    @FechaFinal = '2024-06-01', 
    @idProyectos = 4, -- Implementación de Energía Solar en Edificios Gubernamentales
    @idUsuarios = 4, -- Asignado a María García
    @Estado = 'En Progreso';
GO

EXEC Crear_Tarea 
    @NombreTareas = 'Auditar infraestructura de seguridad digital', 
    @Descripcion = 'Evaluar y auditar la infraestructura actual para identificar vulnerabilidades.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-05-02', 
    @FechaFinal = '2024-07-01', 
    @idProyectos = 5, -- Fortalecimiento de la Seguridad Cibernética Nacional
    @idUsuarios = 5, -- Asignado a Jorge Martínez
    @Estado = 'Planificación';
GO

---------------------Sub-Tareas------------------------------

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Definir requisitos del sistema', 
    @Descripcion = 'Identificar y documentar los requisitos necesarios para el sistema de transporte.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-01-17', 
    @FechaFinal = '2024-01-31', 
    @idTareas = 1, -- Diseñar arquitectura del sistema de transporte
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
    @NombreSubtareas = 'Diseñar interfaz interactiva', 
    @Descripcion = 'Crear una interfaz gráfica para las guías de aprendizaje digital.', 
    @Prioridad = 'Media', 
    @FechaInicio = '2024-07-03', 
    @FechaFinal = '2024-07-15', 
    @idTareas = 3, -- Crear material educativo digital
    @Estado = 'Planificación';
GO

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Supervisar instalación eléctrica', 
    @Descripcion = 'Revisar las conexiones eléctricas previas a la instalación de los paneles.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-03-05', 
    @FechaFinal = '2024-03-20', 
    @idTareas = 4, -- Instalar paneles solares
    @Estado = 'Pendiente';
GO

EXEC Crear_Subtarea 
    @NombreSubtareas = 'Generar informe de vulnerabilidades', 
    @Descripcion = 'Documentar los hallazgos tras la auditoría de seguridad.', 
    @Prioridad = 'Alta', 
    @FechaInicio = '2024-05-10', 
    @FechaFinal = '2024-05-25', 
    @idTareas = 5, -- Auditar infraestructura de seguridad digital
    @Estado = 'Planificación';
GO

---------------------Comentarios proyectos------------------------------

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'El diseño inicial del sistema está alineado con los objetivos del proyecto.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 1, -- Modernización del Sistema de Transporte Público
    @idUsuario = 1; -- Carlos Fernández
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'Es necesario ampliar la cobertura en regiones con menos acceso a la banda ancha.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 2, -- Implementación de Redes de Banda Ancha
    @idUsuario = 2; -- Ana María López
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'El material educativo está enfocado en el público objetivo y cumple con los estándares requeridos.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 3, -- Programa Nacional de Alfabetización Digital
    @idUsuario = 3; -- Luis Alberto
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'La instalación de paneles solares está avanzando según lo planeado.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 4, -- Implementación de Energía Solar en Edificios Gubernamentales
    @idUsuario = 4; -- María García
GO

EXEC Agregar_Comentario_Proyectos 
    @Comentario = 'Es importante considerar medidas adicionales para proteger contra amenazas emergentes.', 
    @FechaCreacion = '2024-05-10', 
    @idProyecto = 5, -- Fortalecimiento de la Seguridad Cibernética Nacional
    @idUsuario = 5; -- Jorge Martínez
GO

---------------------Comentarios tareas------------------------------

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'Los requisitos están bien definidos, pero es necesario validar con todas las partes interesadas.', 
    @FechaCreacion = '2024-01-18', 
    @idTarea = 1, -- Diseñar arquitectura del sistema de transporte
    @idUsuario = 1; -- Carlos Fernández
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'La configuración de los servidores requiere ajustes en la seguridad de la red.', 
    @FechaCreacion = '2024-02-06', 
    @idTarea = 2, -- Configurar servidores de banda ancha
    @idUsuario = 2; -- Ana María López
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'El diseño de la interfaz está en progreso y luce prometedor.', 
    @FechaCreacion = '2024-07-05', 
    @idTarea = 3, -- Crear material educativo digital
    @idUsuario = 3; -- Luis Alberto
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'La supervisión eléctrica inicial está completa; no se encontraron problemas graves.', 
    @FechaCreacion = '2024-03-06', 
    @idTarea = 4, -- Instalar paneles solares
    @idUsuario = 4; -- María García
GO

EXEC Agregar_Comentario_Tarea 
    @Comentario = 'La auditoría inicial reveló vulnerabilidades críticas; se recomienda actuar con urgencia.', 
    @FechaCreacion = '2024-05-12', 
    @idTarea = 5, -- Auditar infraestructura de seguridad digital
    @idUsuario = 5; -- Jorge Martínez
GO

---------------------Comentarios Sub-tareas------------------------------

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'Los requisitos del sistema han sido revisados y aprobados por el equipo.', 
    @FechaCreacion = '2024-01-20', 
    @idSubtarea = 1, -- Definir requisitos del sistema
    @idUsuario = 1; -- Carlos Fernández
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'La red de prueba está configurada correctamente, pero necesita ajustes en la seguridad.', 
    @FechaCreacion = '2024-02-10', 
    @idSubtarea = 2, -- Configurar red de prueba
    @idUsuario = 2; -- Ana María López
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'El diseño de la interfaz gráfica está completo y listo para revisión.', 
    @FechaCreacion = '2024-07-10', 
    @idSubtarea = 3, -- Diseñar interfaz interactiva
    @idUsuario = 3; -- Luis Alberto
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'La instalación eléctrica se realizó según los estándares de seguridad.', 
    @FechaCreacion = '2024-03-15', 
    @idSubtarea = 4, -- Supervisar instalación eléctrica
    @idUsuario = 4; -- María García
GO

EXEC Agregar_Comentario_Subtarea 
    @Comentario = 'El informe de vulnerabilidades está en proceso; se espera completarlo en dos días.', 
    @FechaCreacion = '2024-05-15', 
    @idSubtarea = 5, -- Generar informe de vulnerabilidades
    @idUsuario = 5; -- Jorge Martínez
