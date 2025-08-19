# Elite Project

A web application to manage and display **account statements** with search, print, and export features.

---

## Features

- Searchable **Accounts Dropdown** (autocomplete: `Account Code - Account Name`).  
- Filter transactions by **Account** and **Date Range**.  
- Display account statements in a **table** with columns:  
  - Account ID  
  - Account Name  
  - Previous Balance  
  - Debit Amount  
  - Credit Amount  
  - Final Balance  
  - Transaction Details (clickable icon to open a **modal**)  
- Modal popup to show detailed transaction information.  
- Table footer with **column totals**.  
- Summary panel showing:  
  - Total Debits  
  - Total Credits  
  - First Previous Balance  
  - Final Balance  
- Client-side and server-side **validations**.  
- Loading indicators for **data fetching**, **printing**, and **Excel export**.  
- Optimized for **large datasets**.  
- Buttons: **Search**, **Print**, **Export to Excel**.

---

## Technologies Used

- ASP.NET Core MVC  
- SQL Server  
- jQuery & JavaScript  
- CSS & Bootstrap  
- **ClosedXML** for Excel export  
- **Toastr** for notifications  

---

## Database Structure

### Balance Table
| Column       | Type      | Description                  |
|--------------|----------|------------------------------|
| BalanceId    | int      | Account ID                   |
| BalanceName  | nvarchar | Account Name                 |
| BalanceType  | char     | 'D' = Debit, 'C' = Credit   |

### Balance_History Table
| Column        | Type      | Description                      |
|---------------|----------|----------------------------------|
| BalanceHisId  | int      | Transaction ID                   |
| BalanceId     | int      | Account ID                       |
| PrevBalnce    | decimal  | Previous balance                 |
| Debtor        | decimal  | Debit amount                     |
| Creditor      | decimal  | Credit amount                    |
| Date          | datetime | Transaction date                 |
| Remarks       | nvarchar | Notes                             |

---

## Calculations

- **Debit Account**: `(Previous Balance + Debit) - Credit`  
- **Credit Account**: `(Previous Balance + Credit) - Debit`

---

## Setup Instructions

1. Clone the repository.  
2. Configure SQL Server connection in `appsettings.json`.  
3. Ensure database exists with tables (or run EF Core migrations).  
4. Restore NuGet packages:  
   - ClosedXML  
   - Toastr  
   - jQuery  
   - Bootstrap  
5. Run the application in Visual Studio.  
6. Open in a browser to test functionality.  

---

## Usage

1. Select an account from the dropdown.  
2. Choose **From** and **To** dates.  
3. Click **Search** to display transactions.  
4. Click **Details** icon to view transaction information.  
5. Click **Print** to print the table.  
6. Click **Export to Excel** to download the account statement.  

---

## Notes

- For testing large datasets, use account: **Clients class A**, `BalanceId = 11038`.  
- Fully optimized for **large-scale transactions**.  
- Proper client-side and server-side validations are implemented.  
- **Toastr** notifications show errors and messages.
