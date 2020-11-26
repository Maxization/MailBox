// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var groups = new Array();


function GetGroupsOnLoad()
{
    $.getJSON("/Groups/GetUserGroupsListAsJson", function (result) {
    groups = [];
    $.each(result, function (i, g) {
        var GroupMembers = new Array();
        $.each(g.groupMembers, function (j, gm) {
            var member = { Name: gm.name, Surname: gm.surname, Address: gm.address };
            GroupMembers.push(member);
        })
        var Group = { GroupID: g.groupID, Name: g.name, GroupMembers: GroupMembers };
        groups.push(Group);
    });
    $("#groupsPanel").empty();
    $.each(groups, function (i, g) {
        var idd = "gms-" + g.GroupID;
        $("#groupsPanel").append(
            "<div class=\"container-group\">"
            + "<div class=\"container-group-title\">"
            + "<div class=\"row no-gutters\">"
            + "<div class=\"col-12 no-gutters\">"
            + "<h6>"
            + g.Name
            + "</h6>"
            + "</div>"
            + "</div>"
            + "<hr />"
            + "</div>"
            + "<div id=\"" + idd + "\" class=\"container-contacts overflow-auto\">"
            + "</div>"
            + "</div>");
        var div = document.getElementById(idd);
        $.each(g.GroupMembers, function (j, gm) {
            div.innerHTML += ("<button class=\"col-form-label-sm bg-blue-button\">" + gm.Address + "</button>");
        });
    });
    $("#foldersactions").empty();
    $("#foldersactions").append(
        "<div class=\"container-title\">"
        + "<h4>Folders</h4>"
        + "<hr />"
        + "</div>"
        + "<div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Inbox</button></div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Sent</button></div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Spam</button></div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Stared</button></div>"
        + "</div>"
        + "<div class=\"row no-gutters container-board-white container-new\" id=\"folders_container\">"
        + "<div class=\"col-10 no-gutters\">"
        + "<input type=\"text\" class=\"form-control input-new_folder border-0\" placeholder=\" New folder...\" aria-label=\"Recipient's username\" aria-describedby=\"basic-addon2\">"
        + "</div>"
        + "<div class=\"col-2 no-gutters\">"
        + "<button class=\"bg-black text-light text-center border-0 btn-new\" type=\"button\">+</button>"
        + "</div>"
        + "</div>"
        + "<div><br /></div>"
        + "<div class=\"container-title\">"
        + "<h4>Actions</h4>"
        + "<hr />"
        + "</div>"
        + "<div>"
        + "<div class=\"button-container\"><button onclick=\"GoToNewMail()\" type=\"button\" class=\"btn-folder\">New email</button></div>"
        + "<div class=\"button-container\"><button onclick=\"GoToGroupManagement()\" type=\"button\" class=\"btn-folder\">Manage groups</button></div>"
        + "</div>"
    );
    });
}

window.onload += GetGroupsOnLoad();

function GoToGroupManagement() {
    setTimeout(() => window.location.replace("/groups/managegroups"), 250);
}

function GoToNewMail() {
    setTimeout(() => window.location.replace("/mail/create"), 250);
}
