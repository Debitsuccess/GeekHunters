# Geek Hunters

Geek Registration System (GRS): 
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX<br />
A consol application using .Net Frameworj 4.6.1
<br/>
Using GRS a recruitment agent should be able to:
<br/>
1. Register a new candidate: 
	<br/>
	a. first name / last name
	<br/>
	b. Select technologies candidate has experience in from the predefined list
<br/>
2. View all candidates
<br/>
3. filter candidates by technology
<br/>
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX<br />
Configuration:
<br />
* Database Connection is configured for Main app and Unit test as : 
<br />
	add name="GeekRegistrationConnectionString" connectionString="Data Source=Database\\GeekHunter.sqlite" 
<br />
	add name="GeekRegistrationConnectionStringTest" connectionString="Data Source=Database\\GeekHunterTest.sqlite"
* Maximum and minimum length of string fields: 
<br />   	
	add key="MaximumStringLength" value="25"
<br />
   	add key="MinimumStringLength" value="6"
<br/>
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX<br />
How to USE:<br />
1. Run consol application called 'GeekRegistrationSystem'
<br/>
2. Follow the instruction by the consol:
<br/>
    a. A main menu appears to select any of the available functions
<br/>
3. A subsequent interactive console messages to read inputs from users and proceed with the selected functionality
<br/>
4. A proper message is displayed based on the user selection.
<br/>
5. A user can enter thier choices by typing the number (Id) beside the menu item
<br/>
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX<br />
Dependencies:<br />
System.Data.SQLite
<br />
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX<br />

Please note that the current DB file configuration for the main application is under Debug/Database/GeekHunter.sqlite. <br />

This will allow the user to run the application with the current configuration or simply change the database file location to a desired folder and update the configuration accordingly.

A copy of the database file is under the root folder named as 'GeekHunterV2.sqlite'.
