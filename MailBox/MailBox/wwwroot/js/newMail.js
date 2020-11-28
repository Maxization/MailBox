
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

function OnFilter(elem, startval) {
    var elemid = "globalList" + elem;
    $(document.getElementById(elemid)).children('option').each(function (index, el) {
        el.hidden = (el.innerHTML.startsWith(startval) ? false : true);
    });
}

$(document).ready(function () {
    $.getJSON("/user/globallist", function (data) {
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
