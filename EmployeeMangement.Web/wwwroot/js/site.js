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
    $('[data-toggle="popover"]').popover();



    function getNotification() {
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/Home/Notification",
            method: "GET",
            success: function (result) {

                if (result.count != 0) {
                    $("#notificationCount").html(result.count);
                    $("#notificationCount").show('slow');
                } else {
                    $("#notificationCount").html();
                    $("#notificationCount").hide('slow');
                    $("#notificationCount").popover('hide');
                }

                var notifications = result.userNotification;
                notifications.forEach(element => {
                    res = res + "<li class='list-group-item notification-text' data-id='" + element.notification.id + "'>" + element.notification.text + "</li>";
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

    getNotification();

    let connection = new signalR.HubConnection("/signalServer");

    connection.on('displayNotification', () => {
        getNotification();
    });

    connection.start();

});