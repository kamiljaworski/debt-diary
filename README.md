<img align="left" src="https://raw.githubusercontent.com/kamiljaworski/DebtDiary/master/images/Icon.png" alt=""/>

# Debt diary
*Debt diary* is a simple desktop application to store your debts history.

## Goal
This application was made to resolve a certain problem.
There are many people that don't want to keep a notebook with
their debts or they don't want to use excel and they are looking
for application like this. *Debt diary* is an application that
does exactly that. You can add debtors and add for each of them
some operations in easy way. And you have nice line charts.

<img align="left" src="https://raw.githubusercontent.com/kamiljaworski/DebtDiary/master/images/LoginPage.png" alt=""/>

## Features

- creating an account
- logging in to your account in any place
- managing account data
- changing account avatar
- changing password to the account
- adding a debtor
- editing a debtor
- deleting a debtor
- each debtor has an color-customizable avatar for fast recognition
- browsing debtor operations list
- viewing operations line chart
- adding four types of operations to debtors:
	- debtors loan
	- users loan
	- debtors repayment
	- users repayment
- sorting debtors ordered by their debt
- browsing summary subpage with all debtors:
	- last operations list
	- operations summed up in a line chart
- texts in the application dependent on users and debtors gender
- multi language support:
	- english
	- polish
	
<img align="left" src="https://raw.githubusercontent.com/kamiljaworski/DebtDiary/master/images/SummarySubpage.png" alt=""/>

## Technology

*Debt diary* is a WPF application implemented with MVVM design pattern.
This code requires Visual Studio 2017 with .NET Framework 4.6.1.

To build this application you need to add App.config file to DebtDiary project
with Entity Framework configuration and connection string.

<img align="left" src="https://raw.githubusercontent.com/kamiljaworski/DebtDiary/master/images/DebtorInfoSubpage.png" alt=""/>

## Libraries/Extensions

- Entity Framework 6
- Fody
- Fody PropertyChanged
- Live-Charts
- Microsoft SDK Expression Blend
- Microsoft Windows Shell
- Ninject
- NUnit
- Moq

