
﻿var globalList;

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
        globalList = [];
        $.each(data, function (key, val) {
            var item1 = "<button type=\"button\" onClick=\"OnClickBCC(this.id)\" class=\"btn btn-light\" id='" + key + "'>" + val.address + "</button>";
            var item2 = "<button type=\"button\" onClick=\"OnClickCC(this.id)\" class=\"btn btn-light\" id='" + key + "'>" + val.address + "</button>";
            globalList.push(val.address);
            document.getElementById("globalListBCC").innerHTML += item1;
            document.getElementById("globalListCC").innerHTML += item2;
        });
    });
});
