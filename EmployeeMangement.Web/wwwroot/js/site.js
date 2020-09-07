// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ListToForm(RoleId, Did, DepartmentName) {
    document.getElementById("RoleId").value = RoleId;
    document.getElementById("Did").value = Did;
    document.getElementById("DepartmentName").value = DepartmentName;
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
