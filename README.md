# Introduction 
The project is a simple mailbox.
 
User can:
- send emails to any user from a global contact list,
- display inbox,
- open incoming emails,
- manage emails (change status[read/unread], move them between folders, filter and search),
- manage folders (create and delete folders),
- create groups of users.

As a group owner user can:
- create and delete groups,
- add and remove users from group,
- change group name.

Admin (user with special permissions):
- display users list,
- approve new users requests,
- change the period of time for a notification,
- managed users (set their status to enable/disable),
- display groups list.

Used technologies:
- project management - Azure DevOps,
- web framework - ASP.NET Core 3.1.,
- authentication - Azure Active Directory B2C,
- UI - Bootstrap, CSS,
- dynamic user-server interaction - Razor, AJAX,
- validations - FluentValidation,
- unit tests - xUnit.

Idea of external API use:
When an email is sent by one user to another, the relevant data are sent to API. The user has a desktop application that periodically sends to API requests for notifications. In case of receiving non-empty data, the application saves them in its memory and sends a command to the API to delete the received notifications. Then the user receives a notification via the desktop application that he/she has new emails in his/her Mailbox.

# Getting Started
The project builds on ASP.NET Core 3.1. Can be opened in Visual Studio 2019.

# Build and Test
The project is built and tested after committing. Can be opened and compiled in Visual Studio 2019.

# Contribute
All added functionalities have to be tested. Changes can't break already working functionalities. The Master branch can contain only fully tested and working code.