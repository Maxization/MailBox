// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var groups = new Array();

window.onload += $.getJSON("/Groups/GetUserGroupsListAsJson", function (result) {
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
});

function GoToGroupManagement() {
    setTimeout(() => window.location.replace("/groups/managegroups"), 250);
}
