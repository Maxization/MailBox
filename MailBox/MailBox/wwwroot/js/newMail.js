function OnClickBCC(id) {
    var inp = $('input[name=BCC]');
    var email = inp.val() + document.getElementById(id).innerHTML + "; ";
    inp.val(email);
}

function OnClickCC(id) {
    var inp = $('input[name=CC]');
    var email = inp.val() + document.getElementById(id).innerHTML + "; ";
    inp.val(email);
}

function OnClickSend() {
    var BCC = $('input[name=BCC]').val().replace(/\s/g, '').split(';').filter((el) => el);
    var CC = $('input[name=CC]').val().replace(/\s/g, '').split(';').filter((el) => el);
    var dataToSend =
    {
        Topic: $('input[name=Topic]').val(),
        Text: $('textarea[name=Text]').val(),
        CCRecipientsAddresses: CC,
        BCCRecipientsAddresses: BCC
    };
    $.ajax({
        url: '/api/mailapi/create',
        type: "POST",
        data: JSON.stringify(dataToSend),
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            document.getElementById("CCError").innerHTML = "";
            document.getElementById("BCCError").innerHTML = "";
            document.getElementById("TopicError").innerHTML = "";
            document.getElementById("TextError").innerHTML = "";
            xhr.responseJSON.errors.forEach(function (item, index) {
                if (item.fieldName.includes('BCCRecipientsAddresses'))
                    item.fieldName = 'BCCRecipientsAddresses';
                else if (item.fieldName.includes('CCRecipientsAddresses'))
                    item.fieldName = 'CCRecipientsAddresses';
                switch (item.fieldName) {
                    case 'CCRecipientsAddresses':
                        document.getElementById("CCError").innerHTML += item.message + "</br>";
                        break;
                    case 'BCCRecipientsAddresses':
                        document.getElementById("BCCError").innerHTML += item.message + "</br>";
                        break;
                    case 'Topic':
                        document.getElementById("TopicError").innerHTML += item.message + "</br>";
                        break;
                    case 'Text':
                        document.getElementById("TextError").innerHTML += item.message + "</br>";
                        break;
                    default:
                        break;
                }
            })
        },
        success: function () {
            window.location = '/mail';
        }
    });
}

function OnFilter(elem, startval) {
    var elemid = "globalList" + elem;
    $(document.getElementById(elemid)).children('option').each(function (index, el) {
        el.hidden = (el.innerHTML.startsWith(startval) ? false : true);
    });
}

$(document).ready(function () {
    $.getJSON("/api/userapi/globallist", function (data) {
        $.each(data, function (key, val) {
            var item1 = "<option onClick=\"OnClickBCC(this.id)\" class=\"btn btn-light dropdown-item\" id='" + key + "'>" + val.address + "</option>";
            var item2 = "<option onClick=\"OnClickCC(this.id)\" class=\"btn btn-light dropdown-item\" id='" + key + "'>" + val.address + "</option>";
            document.getElementById("globalListBCC").innerHTML += item1;
            document.getElementById("globalListCC").innerHTML += item2;
        });
    });
    $('input').keypress(function (event) {
        if (event.keyCode == 13)
            event.preventDefault();
    });
});
