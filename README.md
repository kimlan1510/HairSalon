# _Hair Salon_

#### _This program will show a list of stylists and clients who see individual stylists. _

#### By _**Kimlan Nguyen**_

## Description

_This program allows the users to add stylists to the database and for each stylist, the user will be able to see the clients who see that stylist._

## Specifications

 | Behavior   |Input Example   | Output Example      | Why?|
 |----------------       |:----------:    |:------------:        |---------:|
 |The database is empty at first | input nothing | database is empty | To check if the is nothing in the database at first |
 |One stylist cannot have 2 names that are the same| stylist: "Jacob", add stylist: "Jacob"| "Jacob" already exists| To avoid duplicates |
 |Be able to add stylists to the database | Add stylist: "Jacob" | "Jacob" added to database  | To check if the database can keep the information |
 |Be able to add clients for each stylist | Add client "Susan" to stylist "Jacob" | client "Susan" is added to stylist "Jacob" | To check if the connection between the client's table and the stylist's table is working properly|
 |Be able to see all the clients for each stylists| click on stylist "Jacob"| "Jacob"'s clients: "Susan", "Nick" | To check if each stylist can hold multiple clients|
 |Be able to update client's name | Change "Susan" to "Emilyn" | "Jacob"'s clients: "Emilyn", "Nick" | To check if a client's name can be changed without adding a new client"|
 |Be able to remove a client | remove "Emilyn" | "Jacob"'s clients: "Nick" | To check if a client can be remove from the database|  




## Setup/Installation Requirements
* _This program requires installing C#, Git and asp.net5. Follow the instruction here https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c to install c# and asp.net5 on your computer._
* _Download or clone this file using Git._
* _In Windows PowerShell navigate to the WordCounter folder._
* _Type "dnu restore" in the console to compile Nancy, exclude ""._
* _Type "dnx kestrel" in the console run the program, exclude ""._
* _Paste this link http://localhost:5004/ onto your web browser._

## Known Bugs

_There are no bugs that I am awared of._

## Support and contact details

_Kimlan1510@gmail.com_

## Technologies Used

* _HTML_
* _C#_
* _Nancy_
* _Razor_
* _Xunit_


### License

*This program is licensed under MIT License.*

Copyright (c) 2017 **_Kimlan Nguyen_**
