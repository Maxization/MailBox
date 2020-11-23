// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ChangeGroupName(Name, GroupID) {
    $.ajax({
        url: '/Groups/ChangeGroupName',
        data: { 'Name': Name, 'GroupID': GroupID },
        type: "POST",
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
        url: '/Groups/AddGroup',
        data: { 'Name': Name },
        type: "POST",
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
        url: '/Groups/DeleteGroup',
        data: { 'groupID': groupID },
        type: "POST",
        cache: true,
        success: function () {
            setTimeout(() => location.reload(), 250);
        }
    });
}

function AddUserToGroup(GroupMemberAddress, GroupID) {
    $.ajax({
        url: '/Groups/AddUserToGroup',
        data: { 'GroupMemberAddress': GroupMemberAddress, 'GroupID': GroupID },
        type: "POST",
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
        url: '/Groups/DeleteUserFromGroup',
        data: { 'GroupMemberAddress': GroupMemberAddress, 'GroupID': GroupID },
        type: "POST",
        cache: true,
        success: function () {
            setTimeout(() => location.reload(), 250);
        }
    });
}