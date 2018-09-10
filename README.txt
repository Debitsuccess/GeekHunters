Geek Registration System (GRS): 
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
A consol application using .Net Frameworj 4.6.1
Using GRS a recruitment agent should be able to:
1. Register a new candidate: 
	a. first name / last name
	b. Select technologies candidate has experience in from the predefined list
2. View all candidates
3. filter candidates by technology
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
Configuration:
1. Database Connection is configured:
	<add name="GeekRegistrationConnectionString" connectionString="Data Source=Database\\GeekHunter.sqlite" />
2. Maximum and minimum length of string fields: 
   	<add key="MaximumStringLength" value="25"/>
   	<add key="MinimumStringLength" value="6"/>
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
How to USE:
1. Run consol application called 'GeekRegistrationSystem'
2. Follow the instruction by the consol:
    a. A main menu appears to select any of the available functions
3. A subsequent interactive console messages to read inputs from users and proceed with the selected functionality
4. A proper message is displayed based on the user selection.
5. A user can enter thier choices by typing the number (Id) beside the menu item

XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
Dependencies:
System.Data.SQLite
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

