
var mailsList = new Array();
var Senders = new Array();
var MailSortFunc = NoMailsSorting;
var MailFiltrFunc = NoMailFiltering;
var responseMails;

function ChangeReadStatus(MailID, Read) {
    var dataToSend =
    {
        MailID: MailID,
        Read: Read
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
        },
    });
    $('input[name=' + MailID + ']').prop('checked', Read);
}

function DisplayMailsList() {
    $("#mail_container").empty();
    MailSortFunc([...mailsList]).forEach(function (mail) {
        if (MailFiltrFunc(mail)) {
            if (!document.getElementById("mails" + mail.date)) {
                $("#mail_container").append(
                    "<div class=\"row no-gutters\">" +
                    "<div class=\"col-2\">" +
                    "<button class=\"bg-blue-button\" type=\"button\" data-toggle=\"collapse\" data-target=\"#mails" + mail.date + "\" aria-expanded=\"false\" aria-controls=\"mails" + mail.date + "\">" +
                    mail.date +
                    "</button>" +
                    "</div>" +
                    "<div class=\"col-10\">" +
                    "<div class=\"row no-gutters collapse bg-dark card-body\" style=\"padding: 28px 0 0 0\" id=\"mails" + mail.date + "\">" +
                    "</div>" +
                    "</div >" +
                    "</div >")
            }
            $("#mails" + mail.date).append(
                "<a href=\"/mail/details/" + mail.id + "\" onclick=\"ChangeReadStatus(" + mail.id + ", " + true + ")\" class=\"col-12 no-gutters btn btn-secondary active\" style = \"margin-bottom: 1px; text-decoration: none\" role = \"button\" aria-pressed=\"true\" >" +
                "<div class=\"row no-gutters\" style=\"border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: gray\">" +
                "<div class=\"col-1 no-gutters\">" +
                "<div class=\"form-check\">" +
                "<input name=\"" + mail.id + "\" onchange=\"ChangeReadStatus(" + mail.id + ", " + (!mail.read) + ")\" class=\"form-check-input position-static\" type=\"checkbox\" id=\"blankCheckbox\" value=\"option1\" aria-label=\"...\" " + (mail.read ? "checked" : "") + " )>" +
                "</div>" +
                "</div>" +
                "<div class=\"col-3\"><span>" + mail.sender.name + " " + mail.sender.surname + "</span></div>" +
                "<div class=\"col-8\"><span>" + mail.topic + "</span></div>" +
                "</div>" +
                "</a >"
            )
        }
    });
}

function GetMailsOnLoad() {
    $.getJSON("/api/mailapi/getmails", function (result) {
        $.each(result, function (i, field) {
            var user = { name: field.sender.name, surname: field.sender.surname, address: field.sender.address };
            var recipients = new Array();
            field.recipientsAddresses.forEach(function (recipient) {
                recipients.push(recipient);
            });
            var messDate = new Date(Date.parse(field.date));

            var mail = { id: field.mailID, read: field.read, sender: user, recipients: recipients, topic: field.topic, text: field.text, date: messDate.toISOString().split('T')[0] };
            mailsList.push(mail);
        });
        DisplayMailsList();
    });
}

$('document').ready(GetMailsOnLoad());

// Set Filtering

function SetNoMailFiltering() {
    MailFiltrFunc = NoMailFiltering;
    DisplayMailsList();
    document.getElementById("no_mail_filtering").style.backgroundColor = "gray";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "white";
}

function SetMailTopicFiltering() {
    MailFiltrFunc = FiltrTopic;
    DisplayMailsList();
    document.getElementById("no_mail_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "gray";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "white";
}

function SetMailSenderNameFiltering() {
    MailFiltrFunc = FiltrSenderName;
    DisplayMailsList();
    document.getElementById("no_mail_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "gray";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "white";
}

function SetMailSenderSurnameFiltering() {
    MailFiltrFunc = FiltrSenderSurname;
    DisplayMailsList();
    document.getElementById("no_mail_filtering").style.backgroundColor = "white";
    document.getElementById("filtering_mail_topic").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_name").style.backgroundColor = "white";
    document.getElementById("filtering_mail_sender_surname").style.backgroundColor = "gray";
}

// Filtering

function NoMailFiltering(item) {
    return true;
}

function FiltrTopic(item) {
    return item.topic.startsWith(document.getElementById("mail_filter").value);
}

function FiltrSenderName(item) {
    return item.sender.name.startsWith(document.getElementById("mail_filter").value);
}

function FiltrSenderSurname(item) {
    return item.sender.surname.startsWith(document.getElementById("mail_filter").value);
}

// Set Sorting

function SetSortingMailsByDateFromNew() {
    MailSortFunc = SortMailsByDateFromNew;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsByDateFromOld() {
    MailSortFunc = SortMailsByDateFromOld;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsBySenderAZ() {
    MailSortFunc = SortMailsBySenderAZ;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsBySenderZA() {
    MailSortFunc = SortMailsBySenderZA;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsByTopicAZ() {
    MailSortFunc = SortMailsByTopicAZ;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

function SetSortingMailsByTopicZA() {
    MailSortFunc = SortMailsByTopicZA;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "gray";
}

function SetNoMailsSorting() {
    MailSortFunc = NoMailsSorting;
    DisplayMailsList();
    document.getElementById("no_mails_sorting").style.backgroundColor = "gray";
    document.getElementById("sort_mails_by_date_from_old").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_date_from_new").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_sender_za").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_az").style.backgroundColor = "white";
    document.getElementById("sort_mails_by_topic_za").style.backgroundColor = "white";
}

// Sorting

function SortMailsByDateFromOld(list) {
    return list.sort(compareDatesFromOld);
}

function SortMailsByDateFromNew(list) {
    return list.sort(compareDatesFromNew);
}

function SortMailsBySenderAZ(list) {
    return list.sort(compareSenderAZ);
}

function SortMailsBySenderZA(list) {
    return list.sort(compareSenderZA);
}

function SortMailsByTopicAZ(list) {
    return list.sort(compareTopicAZ);
}

function SortMailsByTopicZA(list) {
    return list.sort(compareTopicZA);
}

function NoMailsSorting(list) {
    return list;
}

//Comparing

function compareSenderAZ(a, b) {
    if (a.sender.name < b.sender.name) {
        return -1;
    }
    if (a.sender.name > b.sender.name) {
        return 1;
    }
    if (a.sender.surname < b.sender.surname) {
        return -1;
    }
    if (a.sender.surname > b.sender.surname) {
        return 1;
    }
    return 0;
}

function compareSenderZA(a, b) {
    if (a.sender.name < b.sender.name) {
        return 1;
    }
    if (a.sender.name > b.sender.name) {
        return -1;
    }
    if (a.sender.surname < b.sender.surname) {
        return 1;
    }
    if (a.sender.surname > b.sender.surname) {
        return -1;
    }
    return 0;
}

function compareDatesFromOld(a, b) {
    if (a.date < b.date) {
        return -1;
    }
    if (a.date > b.date) {
        return 1;
    }
    return 0;
}

function compareDatesFromNew(a, b) {
    if (a.date < b.date) {
        return 1;
    }
    if (a.date > b.date) {
        return -1;
    }
    return 0;
}

function compareTopicAZ(a, b) {
    if (a.topic < b.topic) {
        return -1;
    }
    if (a.topic > b.topic) {
        return 1;
    }
    return 0;
}

function compareTopicZA(a, b) {
    if (a.topic < b.topic) {
        return 1;
    }
    if (a.topic > b.topic) {
        return -1;
    }
    return 0;
}