const email = document.getElementById("datos").dataset.email;

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/verificacionHub")
    .withAutomaticReconnect()
    .build();

connection.on("EmailVerificado", () => {
    document.getElementById("estado").textContent = "¡Verificado! Redirigiendo...";
    setTimeout(() => window.location.href = "/Bienvenido", 1500);
});


connection.onreconnected(() => connection.invoke("EsperarVerificacion", email));

connection.start()
    .then(() => connection.invoke("EsperarVerificacion", email))
    .catch(err => console.error(err));