
var mailsList = new Array();

const SortingEnum = Object.freeze({ "ByDateFromNewest": 0, "ByDateFromOldest": 1, "BySenderAZ": 2, "BySenderZA": 3, "ByTopicAZ": 4, "ByTopicZA": 5 });
const FilterEnum = Object.freeze({ "NoFilter": 0, "FilterTopic": 1, "FilterSenderName": 2, "FilterSenderSurname": 3 });

var currentFilterPhrase = "";
var currentSorting = SortingEnum.ByDateFromNewest;
var currentFilter = FilterEnum.NoFilter;
var currentPage = 1;

function ChangePage(number) {
    currentPage += number;
    GetProperMails();
}

function ChangeCurrentFilterPhrase() {
    currentFilterPhrase = document.getElementById('mail_filter').value;
    console.log(currentFilterPhrase);
    GetProperMails();
}

function GetProperMails() {
    $.ajax({
        url: '/api/mailapi/getmails',
        type: "GET",
        data: { page: currentPage, sorting: currentSorting, filter: currentFilter, filterPhrase: currentFilterPhrase },
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            var errMess = "";
            xhr.responseJSON.errors.forEach(function (item, index) {
                errMess += item.fieldName + ": " + item.message + "\n";
            });
            alert(errMess);
        },
        success: function (result) {
            document.getElementById('prev_page').disabled = result.firstPage;
            document.getElementById('next_page').disabled = result.lastPage;
            mailsList = [];
            (result.mails).forEach(function (field) {
                var user = { name: field.sender.name, surname: field.sender.surname, address: field.sender.address };
                var messDate = new Date(Date.parse(field.date));
                var mail = { id: field.mailID, read: field.read, sender: user, topic: field.topic, date: messDate.toISOString().split('T')[0] };
                mailsList.push(mail);
            });
            DisplayMails();
        }
    });
}

function DisplayMails() {
    $("#mail_container").empty();
    mailsList.forEach(function (mail) {
        if (currentSorting < 2)
            DisplayContainerContent("Date", "Sender", mail.date, mail.sender.name + " " + mail.sender.surname, mail);
        else if (currentSorting < 4)
            DisplayContainerContent("Sender", "Date", mail.sender.name + " " + mail.sender.surname, mail.date, mail);
        else
            DisplayContainerContent("Date", "Sender", mail.date, mail.sender.name + " " + mail.sender.surname, mail);
    });
}

function DisplayContainerContent(DateSenderValue1, DateSenderValue2, value1, value2, mail) {
    document.getElementById("DateSender1").innerHTML = DateSenderValue1;
    document.getElementById("DateSender2").innerHTML = DateSenderValue2;
    if (currentSorting < 4) {
        var key = GetKey(mail);
        if (!document.getElementById("mails_" + key)) {
            $("#mail_container").append(
                "<div class=\"row no-gutters\">" +
                "<div class=\"col-2\">" +
                "<button class=\"bg-blue-button\" type=\"button\" data-toggle=\"collapse\" data-target=\"#mails_" + key + "\" aria-expanded=\"false\" aria-controls=\"mails_" + key + "\">" +
                value1 +
                "</button>" +
                "</div>" +
                "<div class=\"col-10\">" +
                "<div class=\"row no-gutters collapse show bg-dark card-body\" style=\"padding: 28px 0 0 0\" id=\"mails_" + key + "\">" +
                "</div>" +
                "</div>" +
                "</div>");
        }
        $("#mails_" + key).append(
            "<a href=\"/mail/details/" + mail.id + "\" class=\"col-12 no-gutters btn btn-secondary active\" style = \"margin-bottom: 1px; text-decoration: none\" role = \"button\" aria-pressed=\"true\" >" +
            "<div class=\"row no-gutters\" style=\"border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: gray\">" +
            "<div class=\"col-1 no-gutters\">" +
            "<div class=\"form-check\">" +
            "<input name=\"" + mail.id + "\" onchange=\"ChangeReadStatus(" + mail.id + ")\" class=\"form-check-input position-static\" type=\"checkbox\" id=\"blankCheckbox\" value=\"option1\" aria-label=\"...\" " + (mail.read ? "checked" : "") + " )>" +
            "</div>" +
            "</div>" +
            "<div class=\"col-3\"><span>" + value2 + "</span></div>" +
            "<div class=\"col-8\"><span>" + mail.topic + "</span></div>" +
            "</div>" +
            "</a>"
        );
    }
    else {
        $("#mail_container").append(
            "<a href=\"/mail/details/" + mail.id + "\" class=\"col-12 no-gutters btn btn-secondary active\" style = \"margin-bottom: 1px; text-decoration: none\" role = \"button\" aria-pressed=\"true\" >" +
            "<div class=\"row no-gutters\" style=\"border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: gray\">" +
            "<div class=\"col-2\"><span>" + value1 + "</span></div>" +
            "<div class=\"col-10\">" +
            "<div class=\"row no-gutters\">" +
            "<div class=\"col-1 no-gutters\">" +
            "<div class=\"form-check\">" +
            "<input name=\"" + mail.id + "\" onchange=\"ChangeReadStatus(" + mail.id + ")\" class=\"form-check-input position-static\" type=\"checkbox\" id=\"blankCheckbox\" value=\"option1\" aria-label=\"...\" " + (mail.read ? "checked" : "") + " )>" +
            "</div>" +
            "</div>" +
            "<div class=\"col-3\"><span>" + value2 + "</span></div>" +
            "<div class=\"col-8\"><span>" + mail.topic + "</span></div>" +
            "</div>" +
            "</div>" +
            "</div>" +
            "</a>"
        );
    }
}

function GetKey(mail) {
    var key;
    if (currentSorting === 0 || currentSorting === 1)
        key = mail.date;
    if (currentSorting === 2 || currentSorting === 3)
        key = mail.sender.name + "_" + mail.sender.surname;
    if (currentSorting === 4 || currentSorting === 5)
        key = mail.topic;
    return key;
}

function ChangeReadStatus(MailID) {
    var dataToSend =
    {
        MailID: MailID,
        Read: $('input[name=' + MailID + ']').prop('checked')
    };
    $.ajax({
        url: '/api/mailapi/updateread',
        type: "PUT",
        data: JSON.stringify(dataToSend),
        contentType: 'application/json',
        cache: true,
        error: function (xhr) {
            var errMess = "";
            xhr.responseJSON.errors.forEach(function (item, index) {
                errMess += item.fieldName + ": " + item.message + "\n";
            });
            alert(errMess);
        }
    });
}

window.onload += GetProperMails();

// Set Filtering

function SetNoMailFiltering() {
    currentFilter = FilterEnum.NoFilter;
    currentPage = 1;
    GetProperMails();
    document.getElementById("no_mail_filtering").style.backgroundColor = "gray";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "white";
}

function SetMailTopicFiltering() {
    currentFilter = FilterEnum.FilterTopic;
    currentPage = 1;
    GetProperMails();
    document.getElementById("no_mail_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "gray";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "white";
}

function SetMailSenderNameFiltering() {
    currentFilter = FilterEnum.FilterSenderName;
    currentPage = 1;
    GetProperMails();
    document.getElementById("no_mail_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "gray";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "white";
}

function SetMailSenderSurnameFiltering() {
    currentFilter = FilterEnum.FilterSenderSurname;
    currentPage = 1;
    GetProperMails();
    document.getElementById("no_mail_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "gray";
}

// Set Sorting

function SetSortingMailsByDateFromNew() {
    currentSorting = SortingEnum.ByDateFromNewest;
    currentPage = 1;
    GetProperMails();
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsByDateFromOld() {
    currentSorting = SortingEnum.ByDateFromOldest;
    currentPage = 1;
    GetProperMails();
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsBySenderAZ() {
    currentSorting = SortingEnum.BySenderAZ;
    currentPage = 1;
    GetProperMails();
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsBySenderZA() {
    currentSorting = SortingEnum.BySenderZA;
    currentPage = 1;
    GetProperMails();
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsByTopicAZ() {
    currentSorting = SortingEnum.ByTopicAZ;
    currentPage = 1;
    GetProperMails();
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsByTopicZA() {
    currentSorting = SortingEnum.ByTopicZA;
    currentPage = 1;
    GetProperMails();
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "gray";
}
