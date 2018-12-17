var connection = new signalR.HubConnectionBuilder().withUrl("/message").build();

connection.on("ReceiveMessage",
    function (message) {
        let chatInfo = "";
        if (message.receiverEmail !== "nikolay.danev16@gmail.com") {
            chatInfo = "<li class=\"message left appeared\"><div class=\"text_wrapper\">" +
                message.content +
                "</div ></li>";
        } else {
            chatInfo = "<li class=\"message right appeared\"><div class=\"text_wrapper\">" +
                message.content +
                "</div ></li>";
        }
        $("#messagesList").append(chatInfo);
        $(".messages").animate({ scrollTop: $('.messages')[0].scrollHeight }, 1);
        $("#chatSound").play();
    });

connection.on("AdminReceiveMessage",
    function (message) {
        let chatInfo = "";
        if (message.receiverEmail !== "nikolay.danev16@gmail.com") {
            chatInfo = "<li class=\"message right appeared\"><div class=\"text_wrapper\">" +
                message.content +
                "</div ></li>";
        } else {
            chatInfo = "<li class=\"message left appeared\"><div class=\"text_wrapper\">" +
                message.content +
                "</div ></li>";
        }
        $("#adminMessages").append(chatInfo);
        $(".messages").animate({ scrollTop: $('.messages')[0].scrollHeight }, 1);
        $("#chatSound").play();
    });

let isExecuted = false;
connection.on("getAll",
    function (listOfMessages) {
            if (isExecuted === false) {
                for (var i = 0; i < listOfMessages.length; i++) {
                    let chatInfo = "";
                    if (listOfMessages[i].receiverEmail !== "nikolay.danev16@gmail.com") {
                        chatInfo = "<li class=\"message left appeared\"><div class=\"text_wrapper\">" +
                            listOfMessages[i].content +
                            "</div ></li>";
                    } else {
                        chatInfo = "<li class=\"message right appeared\"><div class=\"text_wrapper\">" +
                            listOfMessages[i].content +
                            "</div ></li>";
                    }
                    $("#messagesList").append(chatInfo);
                    $(".messages").animate({ scrollTop: $('.messages')[0].scrollHeight }, 1);
                    $("#chatSound").play();
                }
                isExecuted = true;
            }
    });

$("#adminSendMessage").keyup(function (e) {
    if (e.keyCode === 13) {  //checks whether the pressed key is "Enter"
        //$("#sendNewMessage").click(function () {
        let senderEmail = $("#senderEmail").val();
        let receiverEmail = $("#receiverEmail").val();
        let message = $("#adminSendMessage").val();
        if (message !== "") {
            connection.invoke("AdminSendMessage", senderEmail, receiverEmail, message);
            $("#adminSendMessage").val('');
            $(".messages").animate({ scrollTop: $('.messages')[0].scrollHeight }, 1);
            $("#chatSound").play();
        }
        //});
    }
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
            $(".messages").animate({ scrollTop: $('.messages')[0].scrollHeight }, 1);
            var sound = new Audio('~/sounds/chatSound.mp3');
            sound.play();
        }
        //});
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

$("#live-chat").click(function () {
    connection.invoke("GetMessages");
    $(".messages").animate({ scrollTop: $('.messages')[0].scrollHeight }, 1);
});
