var connection = new signalR.HubConnectionBuilder().withUrl("/message").build();

connection.on("ReceiveMessage",
    function (message) {
        var chatInfo = `<div>${message}</div>`;
        $("#messagesList").append(chatInfo);
    });

connection.on("ReceiveAllMessages", function (messages)
{
    var chatInfo = `<div>${messages}</div>`;
    $("#messagesList").append(chatInfo);
});

$("#getMessages").click(function() {
    connection.invoke("GetMessages");
});

$("#sendNewMessage").click(function () {
    var senderEmail = $("#senderEmail").val();
    var receiverEmail = $("#receiverEmail").val();
    var message = $("#newMessage").val();
    connection.invoke("SendMessage", senderEmail, receiverEmail, message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});