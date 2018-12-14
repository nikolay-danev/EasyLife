var connection = new signalR.HubConnectionBuilder().withUrl("/message").build();

connection.on("ReceiveMessage",
    function (message) {
        let chatInfo = "<div class=\"chat-message clearfix\" ><div class=\"chat-message-content clearfix\"><p>" + message.content + "</p></div></div ><hr>";
        $("#messagesList").append(chatInfo);
    });

$("#newMessage").keyup(function (e) {
    if (e.keyCode === 13) {  //checks whether the pressed key is "Enter"
        //$("#sendNewMessage").click(function () {
        let senderEmail = $("#senderEmail").val();
        let receiverEmail = $("#receiverEmail").val();
        let message = $("#newMessage").val();
        if (message !== "") {
            connection.invoke("SendMessage", senderEmail, receiverEmail, message);
            $("#newMessage").val('');
        }
        //});
    }
});

connection.on("ReceiveAllMessages", function (messages) {
    //var chatInfo = `<div>${messages}</div>`;
    //$("#messagesList").append(chatInfo);
    console.log(messages);
});

$("#getMessages").click(function () {
    connection.invoke("GetMessages");
});



connection.start().catch(function (err) {
    return console.error(err.toString());
});