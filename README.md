Elite Project
Project Overview

Elite Project is a small web application designed to manage and display account statements. Users can select an account from a dropdown list, choose a date range, and view account transactions with detailed calculations. The application includes features for searching, printing, and exporting account statements to Excel.

Features

Display a list of accounts in a searchable dropdown (autocomplete).

Filter transactions by account and date range.

Display account statements in a table with the following columns:

Account ID

Account Name

Previous Balance

Debit Amount

Credit Amount

Final Balance

Transaction Details (icon to open popup)

Popup modal to show detailed transaction information.

Table footer with totals for each column.

Summary div showing:

Account total debits

Account total credits

Account first previous balance

Account final balance

Client-side and server-side validations.

Loading indicators for data fetching, printing, and Excel export.

Optimized for large data sets.

Buttons for:

Search – filter transactions

Print – print the table

Export to Excel – export the table data

Technologies

ASP.NET Core MVC

SQL Server

jQuery & JavaScript

CSS & Bootstrap

ClosedXML (for Excel export)

Toastr (for notifications)

Database Structure
Balance Table

BalanceId (int)

BalanceName (nvarchar)

BalanceType (char) – 'D' for debit, 'C' for credit

Balance_History Table

BalanceHisId (int)

BalanceId (int)

PrevBalnce (decimal)

Debtor (decimal)

Creditor (decimal)

Date (datetime)

Remarks (nvarchar)

Calculations

Final Balance depends on the account type:

Debit account: (Previous Balance + Debit) - Credit

Credit account: (Previous Balance + Credit) - Debit

Setup Instructions

Clone the repository.

Configure the SQL Server connection string in appsettings.json.

Apply migrations (if using EF Core) or ensure database exists with tables.

Restore NuGet packages:

ClosedXML

Toastr

jQuery

Bootstrap

Run the application in Visual Studio.

Open the app in a browser to test the functionality.

Usage

Select an account from the dropdown.

Choose From and To dates.

Click Search to display transactions.

Click the Details icon to view more information about a transaction.

Use Print to print the table.

Use Export to Excel to download the account statement.

Notes

For testing large data sets, use account name Clients class A with BalanceId = 11038.

The application is optimized to handle large amounts of transaction data efficiently.

Proper client-side and server-side validations are implemented.

Toastr notifications are used for errors and messages.
