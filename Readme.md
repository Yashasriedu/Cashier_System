First: 
#To create the database: 
Run the "CreateData.sql" file in sql server and execute it. 

(OR)

After opening CashierSystem.sln in visual studios -> go to App_Data open "Db.sql" File in Visual studios and click execute. A small window will be prompted which will ask for:
Server Name: If you have one: enter that server name, else just enter "."
Authentication: SQL Authentication
Username and Password: It will be the database credentials,
Database name: master.

Second: 
#Connecting to the database: 
Open the solution in Visual Studio. Go to the "Web.config" file which is not  in the Shared folder. In the line CSConnectionString edit User ID and Password according to MSSQL server authentication credentials. 
