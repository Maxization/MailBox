// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
            setTimeout(() => location.reload(), 250);
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
            setTimeout(() => location.reload(), 250);
        }
    });
}

function DeleteGroup(groupID) {
    $.ajax({
        url: '/api/GroupsApi/DeleteGroup/' + groupID,
        type: "DELETE",
        cache: true,
        success: function () {
            setTimeout(() => location.reload(), 250);
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
            setTimeout(() => location.reload(), 250);
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
            setTimeout(() => location.reload(), 250);
        }
    });
}