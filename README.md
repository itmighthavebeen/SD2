# What's For Dinner
Dinner is a C# CRUD API using Sqlite and Swagger with .Net Framework 7.0.  It will maintain a list of Dinner types, with a restaurant name for the dinner type and the menu items that are needed for the dinner.  Think of this as a database of where you go to eat out and what is usually ordered or menu favorites. This database consists of 2 tables, DinnerOrders and MenuItems.  It is a One to Many relationship using Sqlite.  A Dinner Order can have many MenuItems. The primary key is an integer for the DinnerOrder and the MenuItem.  MenuItem contains a foreign key back to the DinnerOrder table that is maintained in Sqlite.  
## Features
The API contains these features:

•	CRUD API with functional Post, Get all, Get by Id, Put by Id and Delete by Id

•	Contains 2 database tables

•	Is Asynchronous

•	Perform a raw SQL query

The API was created with these goals: easy to use and hard to misuse.  Swagger documentation was added 

## CRUD Operations
To POST, the primary integer Id key can be left alone for the DinnerOrder and the MenuItem records; system will generate.  If the user chooses to input the primary integer key, there is some validation of input.  Multiple MenuItems can be put in the request body, using  copy/paste and separate the envelope with a comma. Examples are in the Swagger Response Body. The newly added record is returned as the response.

To GET all records select Execute. All database records are returned as response.

To GET by Id, enter the Id. The response is the DinnerOrder record and all MenuItem records for that DinnerOrder.

To PUT, enter the Id of the record to change.  Put will update Dinner Name, update Restaurant Name, update a menu item (must put in the menu item id) or will ADD a menu item.  The updated record items are returned in the response.

To DELETE, enter the Id of the record to remove.  The removed record is returned as the response.
