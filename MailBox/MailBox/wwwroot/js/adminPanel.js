
var usersList = new Array();

window.onload += GetAllUsersOnLoad();

function GetAllUsersOnLoad() {
    usersList = new Array();
    $.getJSON("/api/userapi/adminviewlist", function (result) {
        $.each(result, function (i, field) {
            //$("#container").append("<li>" + field.name + " " + field.surname + " " + field.address + "</li><br/>");
            var user = { name: field.name, surname: field.surname, address: field.address, role: field.roleName, enable: field.enable };
            usersList.push(user);
        });
        ShowUsers(usersList);
    });
}

function SetUserRole(address, roleName) {
    $.ajax({
        url:'/api/userapi/updateuserrole',
        type: "PUT",
        data: JSON.stringify({ Address: address, RoleName: roleName }),
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            var errMess = "";
            xhr.responseJSON.errors.forEach(function (item, index) {
                errMess += item.fieldName + ": " + item.message + "\n";
            });
            alert(errMess);
        },
        success: function () {
            GetAllUsersOnLoad();
        }
    });
}

function DeleteUser(address) {
    $.ajax({
        url: '/api/userapi/DeleteUser',
        type: "DELETE",
        data: JSON.stringify({ Address: address }),
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            var errMess = "";
            xhr.responseJSON.errors.forEach(function (item, index) {
                errMess += item.fieldName + ": " + item.message + "\n";
            });
            alert(errMess);
        },
        success: function () {
            GetAllUsersOnLoad();
        }
    });
}

function ShowUsers(list)
{
    $("#new_users").empty();
    $("#banned_users").empty();
    $("#active_users").empty();
    list.forEach(function (item) {
        if (item.role == "New") {
            ShowNewUser(item);
        }
        if (item.role == "Banned") {
            ShowBannedUser(item);
        }
        if (item.role == "User" || item.role == "Admin") {
            ShowActiveUser(item);
        }
    });
}

function ShowNewUser(item)
{
    $("#new_users").append("<tr><td>" + item.name + "</td><td>" + item.surname + "</td><td>" + item.address +
        "</td><td><button class=\"btn btn-success\" onclick=\"SetUserRole( '" + item.address + "', 'User' )\">Accept</button>" +
        "</td><td><button class=\"btn btn-danger\" onclick=\"DeleteUser( '" + item.address + "')\">Delete</button>" +
        "</td></tr>");
}

function ShowBannedUser(item)
{
    $("#banned_users").append("<tr><td>" + item.name + "</td><td>" + item.surname + "</td><td>" + item.address +
        "</td><td><button class=\"btn btn-success\" onclick=\"SetUserRole('" + item.address + "' , 'User' )\">Unlock</button>" +
        "</td><td><button class=\"btn btn-danger\" onclick=\"DeleteUser( '" + item.address + "' )\">Delete</button>" +
        "</td></tr>");
}

function ShowActiveUser(item)
{
    $("#active_users").append("<tr><td>" + item.name + "</td><td>" + item.surname + "</td><td>" + item.address +
        "<td><button class=\"btn dropdown-toggle content-center btn-info \" type=\"button\" id=\"dropdownMenuButton\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">" +
        item.role +
        "</button ><div class=\"dropdown-menu \" aria-labelledby=\"dropdownMenuButton\">" +
        "<button id=\"admin_role\" style=\"background-color: white\" onclick=\"SetUserRole( '" + item.address + "', 'User' )\" class=\"dropdown-item\" href=\"#\">User</button>" +
        "<button id=\"admin_role\" style=\"background-color: white\" onclick=\"SetUserRole( '" + item.address + "', 'Admin' )\" class=\"dropdown-item\" href=\"#\">Admin</button>" +
        "<button id=\"admin_role\" style=\"background-color: white\" onclick=\"SetUserRole( '" + item.address + "', 'New' )\" class=\"dropdown-item\" href=\"#\">New</button>" +
        "<button id=\"banned_role\" style=\"background-color: white\" onclick=\"SetUserRole( '" + item.address + "', 'Banned' )\" class=\"dropdown-item\" href=\"#\">Banned</button>" +
        "</div>" +
        "</td><td><button class=\"btn btn-danger\" onclick=\"DeleteUser( '" + item.address + "' )\">Delete</button>" +
        "</td></tr>");
}