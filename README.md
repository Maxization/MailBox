# Introduction 
The project is a simple mailbox for internal use.
 
User can:
- display global contact list,
- send emails to any user from the global contact list,
- attach many files in one email,
- display inbox containing mails that other users sent to him/her,
- sort and filter emails,
- open incoming emails,
- change email read status,
- download indicated attachment,
- manage his/her groups.

As a group owner user can:
- create and delete groups,
- add and remove users from group,
- change group name.

Admin (user with special permissions):
- approve new users,
- ban and unban user,
- change user role,
- delete user from database.

Used technologies:
- project management - Azure DevOps,
- web framework - ASP.NET Core 3.1.,
- authentication - Azure Active Directory B2C,
- UI - Bootstrap, CSS,
- dynamic user-server interaction - Razor, AJAX,
- validations - FluentValidation,
- unit tests - xUnit,
- storage files - Azure Blob Storage.

External API use:
When an email is sent by one user to another, the relevant data are sent to API and saved.
The user has a desktop application that runs in the background and periodically sends to API requests for notifications.
In case of receiving non-empty data, the application saves them in its memory and sends command to the API to delete the received notifications.
Then the user receives a notification via the desktop application that he/she has new emails in his/her Mailbox.

# Getting Started
The project builds on ASP.NET Core 3.1. Can be opened in Visual Studio 2019.

# Build and Test
The project is built and tested after committing. Can be opened and compiled in Visual Studio 2019.

# Contribute
All added functionalities have to be tested. Changes can't break already working functionalities. The Master branch can contain only fully tested and working code.