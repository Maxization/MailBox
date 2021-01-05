// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var groups = new Array();

window.onload += GetGroupsOnLoad();

$('document').ready(function () {
    FillFoldersActions();
});

function GetGroupsOnLoad() {
    groups = new Array();
    $.getJSON("/api/GroupsApi/GetUserGroupsListAsJson", function (result) {
        $.each(result, function (i, g) {
            var GroupMembers = new Array();
            $.each(g.groupMembers, function (j, gm) {
                var member = { Name: gm.name, Surname: gm.surname, Address: gm.address };
                GroupMembers.push(member);
            })
            var Group = { GroupID: g.groupID, Name: g.name, GroupMembers: GroupMembers };
            groups.push(Group);
        });
        FillGroupsPanel();
    });
}

function FillGroupsPanel() {
    $("#groupspanel").empty();
    groups.forEach(function (g, i) {
        var idd = "gms-" + g.GroupID;
        $("#groupspanel").append(
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
            + "<div id=\"" + idd + "\" class=\"container-contacts overflow-auto\" style=\"max-height: 120px\">"
            + "</div>"
            + "</div>");
        var div = document.getElementById(idd);
        g.GroupMembers.forEach(function (gm, j) {
            div.innerHTML += ("<button class=\"col-form-label-sm bg-blue-button\">" + gm.Address + "</button>");
        });
    });
}

function FillFoldersActions() {
    $("#foldersactions").empty();
    $("#foldersactions").append(
        "<div class=\"container-title\">"
        + "<h4>Folders</h4>"
        + "<hr />"
        + "</div>"
        + "<div>"
        + "<div class=\"button-container\"><button onclick=\"GoToInbox()\" type=\"button\" class=\"btn-folder\">Inbox</button></div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Sent</button></div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Spam</button></div>"
        + "<div class=\"button-container\"><button type=\"button\" class=\"btn-folder\">Stared</button></div>"
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
}

function GoToInbox() {
    window.location.replace("/mail/inbox");
}

function GoToGroupManagement() {
    window.location.replace("/groups/managegroups");
}

function GoToNewMail() {
    window.location.replace("/mail/create");
}
