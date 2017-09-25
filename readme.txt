This is a repository for Team TacoCatBagelWaffle's project in Software tools.

We are making a "badge" system for the Graduate School of Theology.

Collaborators:
              Kyle Wood: https://github.com/DemonWav
              Tim Shimp: https://github.com/valcus
              Emily Pielemeier: https://github.com/epielemeier
              Keenan Gates: https://github.com/dinosaurSenses
              Colby Dial: https://github.com/CrudenDarkeyes
              Weston Buck: https://github.com/WestonBuck

SETUP
-----

You need to have SQLServer 2014 installed, with Visual Studio 2015.

 1. Create a new empty database called GSTdata.

 2. a) Open a new query for the GSTdata database, and paste in the contents of the
       CreateTables.sql file in the root directory of the project. Run the query

    b) Since this is an empty database, the "DROP TABLE" statements may cause issues.
       If they do, simply comment them out before you run the query.

 3. Open the "MVC Badge System" solution in Visual Studio 2015.

 4. Set the "FillPersonalDatabase" project as the Startup Project.

 5. Run "FillPersonalDatabase"

    a) Enter the full path to the root directory of the project, or the directory
       which holds the "BadgeBank.csv", "BadgeGiftLog.csv", and "BadgeSystemPeople.csv"
       files. This will import each of the csv files into the database.

 6. In Visual Studio 2015, set the "MVC Badge System" project as the startup project.

 7. Run the "MVC Badge System" project, which should automatically open in your web browser.
