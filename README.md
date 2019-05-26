# <b>ModelStore Example</b>

This C# project uses ModelStore from [PowerBuilder.Data](https://www.nuget.org/packages/PowerBuilder.Data/) for creating Web APIs.  It makes use of the latest released Appeon PowerBuilder 2019, including SnapDevelop (PB Edition), and shows how CRUD operations and transaction management works in ModelStore.

##### Sample Project Structure

The sample contains two projects: 

1. A C# project. This project uses ModelStore from [PowerBuilder.Data](https://www.nuget.org/packages/PowerBuilder.Data/). 

   Four different sets of project files are included, respectively for working with different databases (SQL Server, Oracle, SQL Anywhere, and PostgreSQL).

   The project is structured as follows.

   ```
   |—— Appeon.ModelStoreDemo		Implemented with ModelStore from PowerBuilder.Data
       |—— Appeon.ModelStoreDemo(Sql Server)      For working with SQL Server
       |—— Appeon.ModelStoreDemo(Oracle)          For working with Oracle
       |—— Appeon.ModelStoreDemo(SA)              For working with SQL Anywhere
       |—— Appeon.ModelStoreDemo(PostgreSQL)      For working with PostgreSQL
   ```

2. A PowerBuilder project. This project is a sales demo application. The project is structured as follows.

   ```
   |—— Appeon.SalesDemo.PB			PB source code
   	|—— salesdemo.pbt			PB target
   	|—— Release					PB executable that can directly run
   		|—— salesdemo.exe			PB c/s application
   ```

##### Setting Up the Project

1. Open the PowerBuilder project in PowerBuilder 2019.

2. Open the C# project in SnapDevelop (PB Edition). 

3. In NuGet Package Manager window in SnapDevelop (PB Edition), make sure that Internet connection is available and the option "Include prerelease" is selected, so that the NuGet package can be restored.

4. Download the appropriate database backup file from [https://github.com/Appeon/.NET-Project-Sample-Database](https://github.com/Appeon/.NET-Project-Sample-Database) according to the database you are using, and restore the database using the downloaded database backup file.

5. Inside the configuration file *Appeon.ModelStoreDemo\appsettings.json*, modify the following ConnectionStrings with the correct database connection information.

   ```
   //Sql Server  //Remember to change the Data Source, User ID, Password and Initial Catalog according to the actual settings  "ConnectionStrings": {    "AdventureWorks": "Data Source=127.0.0.1; Initial Catalog=AdventureWorks; Integrated Security=False; User ID=sa; Password=123456; Connect Timeout=15; Encrypt=False; TrustServerCertificate=True; ApplicationIntent=ReadWrite;MultiSubnetFailover= False; Pooling=true; Max Pool Size=2;"  }    
   
   //Oracle  
   //Remember to change the HOST, User ID, Password to the actual settings  "ConnectionStrings": {    "AdventureWorks": "User Id=sa;Password=123456; Data Source=(DESCRIPTIOn=(ADDRESS=(PROTOCOL=Tcp)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ADVENTUREWORKS)));"  }   
   
   //SA  
   //Remember to change the uid, pwd to the actual settings  "ConnectionStrings": {    "AdventureWorks": "DSN=ASA_AdventureWorks;uid=sa;pwd=123456"  }    
   
   //PostgreSQL  
   //Remember to change the HOST, User ID, Password to the actual settings  "ConnectionStrings": {    "AdventureWorks":  "PORT=5432;DATABASE=AdventureWorks;HOST=127.0.0.1;PASSWORD=sa;USER ID=123456"  }  
   ```


