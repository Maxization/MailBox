function ChangeReadStatus(MailID, Read) {
    $.ajax({
        url: '/Mail/UpdateRead',
        data: { 'MailID': MailID, 'Read': Read },
        type: "PUT",
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

function GetFilterInput() {
    return document.getElementById("topic_filter").value;
}