
var usersGlobalList = new Array();
var SortFunc = NoSorting;
var FiltrFunc = NoFiltering;

window.onload += $.getJSON("user/globallist", function (result) {
    $.each(result, function (i, field) {
        //$("#container").append("<li>" + field.name + " " + field.surname + " " + field.address + "</li><br/>");
        var user = { name: field.name, surname: field.surname, address: field.address };
        usersGlobalList.push(user);
    });

    Show(usersGlobalList);
});

function DisplayList() {
    Show(SortFunc([...usersGlobalList]));
}

function Show(userList) {
    $("#user_global_list").empty();
    $("#user_global_list").append("<tr style=\"background-color: black\"><td>" + "Name" + "</td><td>" + "Surname" + "</td><td>" + "Email" + "</td></tr>");
    userList.forEach(function (item, index) {
        if (FiltrFunc(item)) {
            $("#user_global_list").append("<tr><td>" + item.name + "</td><td>" + item.surname + "</td><td>" + item.address + "</td></tr>");
        }
    });
}

// Set Filtering

function SetNoFiltering() {
    FiltrFunc = NoFiltering;
    DisplayList();
    document.getElementById("no_filtering").style.backgroundColor = "gray";
    document.getElementById("filtering_name").style.backgroundColor = "white";
    document.getElementById("filtering_surname").style.backgroundColor = "white";
    document.getElementById("filtering_address").style.backgroundColor = "white";
}

function SetNameFiltering() {
    FiltrFunc = FiltrName;
    DisplayList();
    document.getElementById("no_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_name").style.backgroundColor = "gray";
    document.getElementById("filtering_surname").style.backgroundColor = "white";
    document.getElementById("filtering_address").style.backgroundColor = "white";
}

function SetSurnameFiltering() {
    FiltrFunc = FiltrSurame;
    DisplayList();
    document.getElementById("no_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_name").style.backgroundColor = "white";
    document.getElementById("filtering_surname").style.backgroundColor = "gray";
    document.getElementById("filtering_address").style.backgroundColor = "white";
}

function SetAddressFiltering() {
    FiltrFunc = FiltrAddress;
    DisplayList();
    document.getElementById("no_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_name").style.backgroundColor = "white";
    document.getElementById("filtering_surname").style.backgroundColor = "white";
    document.getElementById("filtering_address").style.backgroundColor = "gray";
}

// Filtering

function NoFiltering(item) {
    return true;
}

function FiltrName(item) {
    return item.name.startsWith(document.getElementById("filter").value);
}

function FiltrSurame(item) {
    return item.surname.startsWith(document.getElementById("filter").value);
}

function FiltrAddress(item) {
    return item.address.startsWith(document.getElementById("filter").value);
}

// Set Sorting

function SetSortingByName() {
    SortFunc = SortByName;
    DisplayList();
    document.getElementById("no_sorting").style.backgroundColor = "white";
    document.getElementById("sort_by_name").style.backgroundColor = "gray";
    document.getElementById("sort_by_surname").style.backgroundColor = "white";
    document.getElementById("sort_by_address").style.backgroundColor = "white";
}

function SetSortingBySurname() {
    SortFunc = SortBySurname;
    DisplayList();
    document.getElementById("no_sorting").style.backgroundColor = "white";
    document.getElementById("sort_by_name").style.backgroundColor = "white";
    document.getElementById("sort_by_surname").style.backgroundColor = "gray";
    document.getElementById("sort_by_address").style.backgroundColor = "white";
}

function SetSortingByAddress() {
    SortFunc = SortByAddress;
    DisplayList();
    document.getElementById("no_sorting").style.backgroundColor = "white";
    document.getElementById("sort_by_name").style.backgroundColor = "white";
    document.getElementById("sort_by_surname").style.backgroundColor = "white";
    document.getElementById("sort_by_address").style.backgroundColor = "gray";
}

function SetNoSorting() {
    SortFunc = NoSorting;
    DisplayList();
    document.getElementById("no_sorting").style.backgroundColor = "gray";
    document.getElementById("sort_by_name").style.backgroundColor = "white";
    document.getElementById("sort_by_surname").style.backgroundColor = "white";
    document.getElementById("sort_by_address").style.backgroundColor = "white";
}

// Sorting

function SortByName(list) {
    return list.sort(compareNames);
}

function SortBySurname(list) {
    return list.sort(compareSurnames);
}

function SortByAddress(list) {
    return list.sort(compareAddress);
}

function NoSorting(list) {
    return list;
}

//Comparing

function compareNames(a, b) {
    if (a.name < b.name) {
        return -1;
    }
    if (a.name > b.name) {
        return 1;
    }
    return 0;
}

function compareSurnames(a, b) {
    if (a.surname < b.surname) {
        return -1;
    }
    if (a.surname > b.surname) {
        return 1;
    }
    return 0;
}

function compareAddress(a, b) {
    if (a.address < b.address) {
        return -1;
    }
    if (a.address > b.address) {
        return 1;
    }
    return 0;
}