// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var groupsToManage = new Array();

window.onload += ShowGroupsManagementInterfaceOnLoad();

function ShowGroupsManagementInterfaceOnLoad() {
    groupsToManage = new Array();
    $.getJSON("/api/GroupsApi/GetUserGroupsListAsJson", function (result) {
        $.each(result, function (i, g) {
            var GroupMembers = new Array();
            $.each(g.groupMembers, function (j, gm) {
                var member = { Name: gm.name, Surname: gm.surname, Address: gm.address };
                GroupMembers.push(member);
            })
            var Group = { GroupID: g.groupID, Name: g.name, GroupMembers: GroupMembers };
            groupsToManage.push(Group);
        });
        ShowGroupsManagementInterface();
    });
}

function ShowGroupsManagementInterface() {
    $("#managementinterface").empty();
    groupsToManage.forEach(function (g, i) {
        var gmid = "gm-" + g.GroupID;
        $("#managementinterface").append(
            "<div class=\"container-group no-gutters\" style=\"max-height: 200px\">"
            + "<div class=\"row no-gutters\">"
            + "<div class=\"col-3\">"
            + "<h5>"
            + g.Name
            + "</h5>"
            + "</div>"
            + "<div class=\"col-1\"></div>"
            + "<div class=\"col-3\">"
            + "<input id=\"NewGroupName-" + g.GroupID + "\" type=\"text\" class=\"form-control input-new_folder\" value=\"" + g.Name + "\" aria-label=\"Recipient's username\" aria-describedby=\"basic-addon2\">"
            + "</div>"
            + "<div class=\"col-2\">"
            + "<button onclick=\"ChangeGroupName(document.getElementById('NewGroupName-" + g.GroupID + "').value, " + g.GroupID + ") \" class=\"add-button\">change name</button>"
            + "</div>"
            + "<div class=\"col-2\"></div>"
            + "<div class=\"col-1\">"
            + "<button onclick=\"DeleteGroup(" + g.GroupID + ") \" class=\"delete-button\">X</button>"
            + "</div>"
            + "</div>"
            + "<hr />"
            + "<div id=\"" + gmid + "\" class=\"container-contacts overflow-auto\" style=\"max-height: 100px\"></div>"
            + "<div class=\"row no-gutters\">"
            + "<div class=\"col-8 no-gutters\">"
            + "<input id=\"GroupMemberAddress-" + g.GroupID + "\" type=\"text\" class=\"form-control input-new_folder\" placeholder=\" Type new member...\" aria-label=\"Recipient's username\" aria-describedby=\"basic-addon2\">"
            + "</div>"
            + "<div class=\"col-1 no-gutters\">"
            + "<button onclick=\"AddUserToGroup(document.getElementById('GroupMemberAddress-" + g.GroupID + "').value, " + g.GroupID + ") \" class=\"add-button\">+</button>"
            + "</div>"
            + "</div>"
            + "</div>"
        );
        var div = document.getElementById(gmid);
        g.GroupMembers.forEach(function (gm, j) {
            div.innerHTML += (
                "<div class=\"container-group-elem\">"
                + "<div class=\"row no-gutters\">"
                + "<div class=\"col-11 no-gutters\">"
                + "<div class=\"row no-gutters\">"
                + "<div class=\"col-3\">"
                + "<span class=\"span-data\">"
                + gm.Name
                + "</span>"
                + "</div>"
                + "<div class=\"col-3\">"
                + "<span class=\"span-data\">"
                + gm.Surname
                + "</span >"
                + "</div>"
                + "<div class=\"col-6\">"
                + "<span id=\"" + g.GroupID + "-" + gm.Address + "\" class=\"span-data\">"
                + gm.Address
                + "</span>"
                + "</div>"
                + "</div>"
                + "</div>"
                + "<div class=\"col-1 no-gutters\">"
                + "<button onclick=\"DeleteUserFromGroup(document.getElementById('" + g.GroupID + "-" + gm.Address + "').textContent, " + g.GroupID + ")\" class=\"delete-button\">X</button>"
                + "</div>"
                + "</div>"
                + "</div>"
            );
        });
    });
}

function ChangeGroupName(Name, GroupID) {
    $.ajax({
        url: '/api/GroupsApi/ChangeGroupName',
        type: "PUT",
        data: JSON.stringify({ Name: Name, GroupID: GroupID }),
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
            GetGroupsOnLoad();
            ShowGroupsManagementInterfaceOnLoad();
        }
    });
}

function AddGroup(Name) {
    $.ajax({
        url: '/api/GroupsApi/AddGroup',
        type: "POST",
        data: JSON.stringify({ Name: Name }),
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            document.getElementById("newGroupError").innerHTML = "";
            xhr.responseJSON.errors.forEach(function (item, index) {
                document.getElementById("newGroupError").innerHTML += (item.fieldName + ": " + item.message);
            });
        },
        success: function () {
            GetGroupsOnLoad();
            ShowGroupsManagementInterfaceOnLoad();
            var input = document.getElementById("newGroupName");
            input.value = "";
        }
    });
}

function DeleteGroup(groupID) {
    $.ajax({
        url: '/api/GroupsApi/DeleteGroup/' + groupID,
        type: "DELETE",
        cache: true,
        success: function () {
            GetGroupsOnLoad();
            ShowGroupsManagementInterfaceOnLoad();
        }
    });
}

function AddUserToGroup(GroupMemberAddress, GroupID) {
    $.ajax({
        url: '/api/GroupsApi/AddUserToGroup',
        type: "POST",
        data: JSON.stringify({ GroupMemberAddress: GroupMemberAddress, GroupID: GroupID }),
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
            GetGroupsOnLoad();
            ShowGroupsManagementInterfaceOnLoad();
        }
    });
}

function DeleteUserFromGroup(GroupMemberAddress, GroupID) {
    $.ajax({
        url: '/api/GroupsApi/DeleteUserFromGroup',
        type: "DELETE",
        data: JSON.stringify({ GroupMemberAddress: GroupMemberAddress, GroupID: GroupID }),
        contentType: 'application/json',
        cache: true,
        success: function () {
            GetGroupsOnLoad();
            ShowGroupsManagementInterfaceOnLoad();
        }
    });
}
