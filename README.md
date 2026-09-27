Tarea 4 – Verificación de email con ASP.NET Core SignalR

Taller .NET 2026 – UTEC

¿Qué hace?

Simula el registro de un usuario con verificación de email, pero sin que el usuario tenga que volver a loguearse después de verificar:

El usuario se registra con su email.
La aplicación le muestra una página que dice "Revisá tu correo".
El usuario hace click en el link de verificación (en esta tarea no se envía un email real: el link se simula con la URL /verificar?email=...).
La novedad: la página "Revisá tu correo" se entera sola de que el email fue verificado y redirige automáticamente a la página de Bienvenida, sin que el usuario haga nada.

Esto funciona porque la página mantiene una conexión abierta con el servidor mediante SignalR, y es el servidor quien le envía el aviso a la página (comunicación servidor → cliente).

Estructura y piezas principales

Hubs/VerificacionHub.cs:	El Hub de SignalR. Cuando una página se conecta, llama al método EsperarVerificacion(email), que agrega esa conexión a un grupo con el nombre del email.

Services/UsuarioStore.cs:	Guarda los usuarios en memoria (email → verificado sí/no). Reemplaza a una base de datos para simplificar.

Program.cs: 	Registra SignalR (AddSignalR), el UsuarioStore, mapea el Hub en /verificacionHub y define el endpoint /verificar.
Endpoint /verificar (en Program.cs)	Simula el click en el link del email. Marca al usuario como verificado y, usando IHubContext<VerificacionHub>, envía el evento EmailVerificado solo al grupo de ese email.

Pages/Registro.cshtml:	Formulario de registro. Al enviarlo redirige a la página de espera.

Pages/Esperando.cshtml: Página "Revisá tu correo". Carga la librería cliente de SignalR y el script esperando.js.

wwwroot/js/esperando.js: Se conecta al Hub, se anota para esperar su email y escucha el evento EmailVerificado. Cuando llega, redirige a Bienvenido.

Pages/Bienvenido.cshtml: Página de destino luego de verificar.