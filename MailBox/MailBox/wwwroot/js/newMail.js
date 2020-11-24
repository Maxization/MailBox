
function OnClickBCC(id) {
    var inp = $('input[name=BCC]');
    var email = inp.val() + document.getElementById(id).innerHTML + ", ";
    inp.val(email); 
    console.log(inp.val());
}

function OnClickCC(id) {
    var inp = $('input[name=CC]');
    var email = inp.val() + document.getElementById(id).innerHTML + ", ";
    inp.val(email);
    console.log(inp.val());
}

$(document).ready(function () {
    $.getJSON("/user/globallist", function (data) {
        $.each(data, function (key, val) {
            var item1 = "<button type=\"button\" onClick=\"OnClickBCC(this.id)\" class=\"btn btn-light\" id='" + key + "'>" + val.address + "</button>";
            var item2 = "<button type=\"button\" onClick=\"OnClickCC(this.id)\" class=\"btn btn-light\" id='" + key + "'>" + val.address + "</button>";
            document.getElementById("globalListBCC").innerHTML += item1;
            document.getElementById("globalListCC").innerHTML += item2;
        });
    });

})