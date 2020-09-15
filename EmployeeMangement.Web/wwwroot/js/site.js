// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ListToForm(Did, DepartmentName) {
    document.getElementById("Did").value = Did;
    document.getElementById("DepartmentName").value = DepartmentName;
}

function ListToRoleForm(RoleId, Name) {
    document.getElementById("RoleId").value = RoleId;
    document.getElementById("Name").value = Name;
}

function CheckPassword() {
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirmPassword").value;

    if (confirmPassword != password) {
        document.getElementById("button").disabled = "true";
        document.getElementById("Error").hidden = "";
    }
    else {
        document.getElementById("button").disabled = "";
        document.getElementById("Error").hidden = "true";
    }
}

function Enabled() {
    $("#Enable :input").prop("disabled", false); 
}


$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover({
        placement: 'bottom',
        content: function () {
            return $("#notification-content").html();
        },
        html: true
    });

    $('body').append(`<div id="notification-content" class="hide"></div>`)


    function getNotification() {
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/Home/Notification",
            method: "GET",
            success: function (result) {

                    $("#notificationCount").html(result.count);
                

                var notifications = result.userNotification;
                notifications.forEach(element => {
                    res = res + "<li class='list-group-item  notification'  id='" + element.nid + "'>" + element.action + " " + element.name + " on " + element.date + "</li>";
                });

                res = res + "</ul>";

                $("#notification-content").html(res);

                console.log(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    $(document).on('click', '.notification', function (e) {
        console.log(this.id);
        var target = e.target;
        readNotification(this.id, target);
    })

    function readNotification(id, target) {
        $.ajax({
            url: "/Home/ReadNotification",
            method: "POST",
            data: { Nid: id },
            success: function (result) {
                getNotification();
                $(target).fadeOut('slow');
            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    getNotification();


    


    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalServer")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("displayNotification", () => {
        getNotification();
    });

    connection.start();

});