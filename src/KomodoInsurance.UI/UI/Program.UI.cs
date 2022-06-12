using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class ProgramUI
    {
        private readonly DeveloperREPO _dRepo = new DeveloperREPO();
        private readonly DevTeamREPO _dtRepo = new DevTeamREPO();

        public void Run()
        {
            SeedData();

            RunApplication();
        }

        private void SeedData()
        {
            Developer stan = new Developer(1, "Stan", "Smith", Pluralsight.Has_Access);
            Developer ben = new Developer(2, "Ben", "Wyatt", Pluralsight.No_Access);
            Developer peter = new Developer(3, "Peter", "Griffin", Pluralsight.No_Access);
            Developer michael = new Developer(4, "Michael", "Scott", Pluralsight.Has_Access);
            Developer natassia = new Developer(5, "Natassia", "Caldwell", Pluralsight.No_Access);
            Developer jessie = new Developer(6, "Jessie", "Spano", Pluralsight.Has_Access);
            Developer leslie = new Developer(7,"Leslie", "Knope", Pluralsight.Has_Access);
            Developer annalise = new Developer(8, "Annalise", "Keating", Pluralsight.No_Access);
            Developer lisa = new Developer(9, "Lisa", "Simpson", Pluralsight.Has_Access);
            Developer rachel = new Developer(10, "Rachel", "Green", Pluralsight.Has_Access);
            Developer sterling = new Developer(11, "Sterling", "Archer", Pluralsight.No_Access);
            Developer sam = new Developer(12, "Sam", "Malone", Pluralsight.Has_Access);
            Developer dexter = new Developer(13, "Dexter", "Morgan", Pluralsight.No_Access);
            Developer dana = new Developer(14, "Dana", "Scully", Pluralsight.No_Access);
            Developer arya = new Developer(15, "Arya", "Stark", Pluralsight.No_Access);


            _dRepo.AddDeveloperToDatabase(stan);
            _dRepo.AddDeveloperToDatabase(ben);
            _dRepo.AddDeveloperToDatabase(peter);
            _dRepo.AddDeveloperToDatabase(michael);
            _dRepo.AddDeveloperToDatabase(natassia);
            _dRepo.AddDeveloperToDatabase(leslie);
            _dRepo.AddDeveloperToDatabase(jessie);
            _dRepo.AddDeveloperToDatabase(annalise);
            _dRepo.AddDeveloperToDatabase(lisa);
            _dRepo.AddDeveloperToDatabase(rachel);
            _dRepo.AddDeveloperToDatabase(sterling);
            _dRepo.AddDeveloperToDatabase(sam);
            _dRepo.AddDeveloperToDatabase(dexter);
            _dRepo.AddDeveloperToDatabase(dana);
            _dRepo.AddDeveloperToDatabase(arya);

            DevTeam group_1 = new DevTeam("Rockets", new List<Developer>{stan, jessie, leslie});
            DevTeam group_2 = new DevTeam("Pacers", new List<Developer>{lisa, dana, sterling});
            DevTeam group_3 = new DevTeam("Lakers", new List<Developer>{annalise, peter, rachel});

            _dtRepo.AddDevTeamToDatabase(group_1);
            _dtRepo.AddDevTeamToDatabase(group_2);
            _dtRepo.AddDevTeamToDatabase(group_3);
        }

        public void RunApplication()
        {
            bool isRunning = true;

            while(isRunning)
            {
                Console.Clear();
                System.Console.WriteLine("\t ===Komodo Insurance===");

                System.Console.WriteLine(
                    "\tPlease make a selection: \n"
                    +"  === Komodo Insurance Developer Team === \n"
                    +" 1. Add Developer Team \n"
                    +" 2. View all Developer Teams \n"
                    +" 3. View a Developer Team \n"
                    +" 4. Update Developer Team \n"
                    +" 5. Delete Developer Teams \n"
                    +"-------------------------------------------- \n"
                    +"    === Komodo Insurance Developers === \n"
                    +" 6. Add Developer \n"
                    +" 7. View all Developers \n"
                    +" 8. View a Developer \n"
                    // +"-------------------------------------------- \n"
                    // +" \t=== Other === \n"
                    // // +" 9. View Dev's Pluralsight Access Status \n"
                    +"-------------------------------------------- \n"
                    +" X. Close Application \n"
                );

                string userInput = Console.ReadLine();

                switch(userInput.ToLower())
                {
                    case "1":
                        AddDeveloperTeamToDatabase();
                        break;
                    case "2":
                        ViewAllDeveloperTeams();
                        break;
                    case "3":
                        ViewDeveloperTeamByID();
                        break;
                    case "4":
                        UpdateDeveloperTeam();
                        break;
                    case "5":
                        DeleteDeveloperTeam();
                        break;
                    case "6":
                        AddDeveloperToDatabase();
                        break;
                    case "7":
                        ViewAllDevelopers();
                        break;
                    case "8":
                        ViewDeveloperByID();
                        break;
                    // case "9":
                    //     DisplayPSAccess();
                    //     break;
                    case "x":
                        isRunning = CloseApplication();
                        break;

                    default:
                    System.Console.WriteLine("Invalid Selection.");
                    break;

                }
            }
        }


        private bool CloseApplication()
        {
            Console.Clear();
            System.Console.WriteLine("Thank you and have a great day!");
            PressAnyKey();
            return false;
        }

        private void PressAnyKey()
        {
            System.Console.WriteLine("Press ANY KEY to continue...");
            Console.ReadLine();
        }

        private void AddDeveloperTeamToDatabase()
        {
            Console.Clear();

            DevTeam developerTeam = new DevTeam();
            var currentDevelopers = _dRepo.GetAllDevelopers();

            System.Console.WriteLine("Please enter a name for the Developer Team: ");
            developerTeam.Name = System.Console.ReadLine();

            bool hasAssignedDevelopers = false;
            while(!hasAssignedDevelopers)
            {
                System.Console.WriteLine("Do you have have any developers? y/n");
                string hasDevelopers = Console.ReadLine().ToLower();

                if(hasDevelopers == "y")
                {
                    foreach(var d in currentDevelopers)
                    {
                        System.Console.WriteLine($"ID {d.ID}: {d.FirstName} {d.LastName}");
                    }
                    System.Console.WriteLine("Please select a developer by their ID: \n");
                    int developerSelection = int.Parse (Console.ReadLine());
                    Developer selectedDeveloper = _dRepo.GetDeveloperByID(developerSelection);

                    if(selectedDeveloper != null)
                    {
                        developerTeam.Developer.Add(selectedDeveloper);

                        currentDevelopers.Remove(selectedDeveloper);
                    }
                    else
                    {
                        System.Console.WriteLine($"Sorry, the Developer Team with ID: {selectedDeveloper} cannot be used.");
                    }
                }
                else
                {
                    hasAssignedDevelopers = true;
                }

            }


            bool isSuccessful = _dtRepo.AddDevTeamToDatabase(developerTeam);
            if(isSuccessful)
            {
                System.Console.WriteLine($"Developer Team:{developerTeam.Name} was added to the database!");
            }
            else
            {
                System.Console.WriteLine("Developer Team failed to be added to database.");
            }

            PressAnyKey();
        }        

        private void ViewAllDeveloperTeams()
        {
            Console.Clear();

            System.Console.WriteLine("=== Developer Teams ===");

            var teamsInDB = _dtRepo.GetAllTeams();
            foreach(DevTeam t in teamsInDB)
            {
                DisplayDevTeams(t);
            }

            PressAnyKey();
        }

        private void ViewDeveloperTeamByID()
        {
            Console.Clear();

            System.Console.WriteLine("=== Developer Team Info ===");
            var teamsInDB = _dtRepo.GetAllTeams();

            foreach(DevTeam t in teamsInDB)
            {
                DisplayDevTeams(t);
            }
            try
            {
                System.Console.WriteLine("Please select a Developer Team by their ID: \n");
                int userInput = int.Parse(Console.ReadLine());
                var selectedDevTeam = _dtRepo.GetDevTeamByID(userInput);

                if(selectedDevTeam != null)
                {
                    DisplayDevTeamInfo(selectedDevTeam);
                }
                else
                {
                    System.Console.WriteLine($"Sorry, the Developer Team with ID: {userInput} does not exist. Try again.");
                }
            }
            catch
            {
                System.Console.WriteLine("Sorry, invalid selection.");
            }
            PressAnyKey();
        }


        private void UpdateDeveloperTeam()
        {
            Console.Clear();

            var availableTeams = _dtRepo.GetAllTeams();
            foreach (var t in availableTeams)
            {
                DisplayDevTeamInfo(t);
            }

            System.Console.WriteLine("Please enter a Developer Team ID: \n");
            int userInput = int.Parse(Console.ReadLine());
            var selectedDevTeam = _dtRepo.GetDevTeamByID(userInput);

            if(selectedDevTeam != null)
            {
                Console.Clear();
                DevTeam newDevTeam = new DevTeam();
                var currentDeveloper = _dRepo.GetAllDevelopers();

                System.Console.WriteLine("Please enter a Team Name: \n");
                newDevTeam.Name = Console.ReadLine();

                bool hasAssignedDevelopers = false;
                while(!hasAssignedDevelopers)
                {
                    System.Console.WriteLine("Do you have any developers to add to this team? y/n \n");
                    string developerInput = Console.ReadLine().ToLower();

                    if(developerInput == "y")
                    {
                        foreach(var d in currentDeveloper)
                        {
                            System.Console.WriteLine($"{d.ID} {d.FirstName} {d.LastName}");
                        }
                        System.Console.WriteLine("Please choose a developer by ID: \n");
                        int developerSelected = int.Parse(Console.ReadLine());
                        var selectedDeveloper = _dRepo.GetDeveloperByID(developerSelected);
                        
                        if(selectedDeveloper != null)
                        {
                            newDevTeam.Developer.Add(selectedDeveloper);
                            currentDeveloper.Remove(selectedDeveloper);
                        }
                        else
                        {
                            System.Console.WriteLine("Sorry, that developer does not exist.");
                        }
                    }
                    else
                    {
                        hasAssignedDevelopers = true;
                    }
                }

                bool isSuccessful = _dtRepo.UpdateDevTeamData(userInput, newDevTeam);
                if(isSuccessful)
                {
                    System.Console.WriteLine("Dev Team is updated!");
                }
                else
                {
                    System.Console.WriteLine("Dev Team failed to update.");
                }
            }
            else
            {
                System.Console.WriteLine($"The store with ID: {userInput} isn't valid.");
            }

            PressAnyKey();
        }

        private void DeleteDeveloperTeam()
        {
            Console.Clear();

            System.Console.WriteLine("=== Deletion ===");
            var devTeams = _dtRepo.GetAllTeams();
            foreach(DevTeam t in devTeams)
            {
                DisplayDevTeamInfo(t);
            }
            try
            {
                System.Console.WriteLine("Please enter a Dev Team by their ID: \n");
                int userSelection = int.Parse(Console.ReadLine());
                bool isSuccessful = _dtRepo.RemoveDevTeamFromDatabase(userSelection);

                if(isSuccessful)
                {
                    System.Console.WriteLine("Dev Team has been deleted.");
                }
                else
                {
                    System.Console.WriteLine("Team was not deleted. ");
                }
            }
            catch
            {
                System.Console.WriteLine("Incorrect selection. ");
            }

            PressAnyKey();
        }

        private void AddDeveloperToDatabase()
        {
            Console.Clear();

            var newDeveloper = new Developer();
            System.Console.WriteLine("=== Add A New Developer ===");

            System.Console.WriteLine("First Name: ");
            newDeveloper.FirstName = Console.ReadLine();

            System.Console.WriteLine("Last Name: ");
            newDeveloper.LastName = Console.ReadLine();

            newDeveloper.Pluralsight = GivePluralsightInfo(newDeveloper);

            bool isSuccessful = _dRepo.AddDeveloperToDatabase(newDeveloper);

            if(isSuccessful)
            {
                System.Console.WriteLine($"{newDeveloper.FirstName} {newDeveloper.LastName} was added to the database. ");
            }
            else
            {
                System.Console.WriteLine("Unable to add developer.");
            }
            PressAnyKey();
        }

        private void ViewAllDevelopers()
        {
            Console.Clear();

            List<Developer> developersinDB = _dRepo.GetAllDevelopers();
            if(developersinDB.Count > 0)
            {
                foreach (Developer d in developersinDB)
                {
                    DisplayAllDeveloperInfo(d);
                }
            }
            else
            {
                System.Console.WriteLine("There are no developers to display.");
            }

            PressAnyKey();
        }


        private void ViewDeveloperByID()
        {
            Console.Clear();

            System.Console.WriteLine("=== Developer Information ===");
            System.Console.WriteLine("Enter employee ID: ");
            int inputDevID = int.Parse(Console.ReadLine());

            Developer developer = _dRepo.GetDeveloperByID(inputDevID);
            if(developer != null)
            {
                DisplayAllDeveloperInfo(developer);
            }
            else
            {
                System.Console.WriteLine($"{inputDevID} is not a valid developer ID.");
            }

            PressAnyKey();
        }

        // private void DisplayPSAccess(Developer developer)
        // {
        //     Console.Clear();

        //     System.Console.WriteLine("=== Pluralsight Access 2Status Page === \n");
        //     System.Console.WriteLine("Please choose from the following options: \n" +
        //     "1. Developers WITH Pluralsight Access \n" +
        //     "2. Developers WITHOUT Pluralsight Access \n"
        //     );
            
        //     string hrInput = Console.ReadLine();
        //     List<Developer> developersinDB = _dRepo.GetAllDevelopers();
        //     switch(hrInput)
        //     {
        //         case "1":
        //             ViewDevWithPSAccess();
        //             break;
        //         case "2":
        //             ViewDevWithNoPSAccess();
        //             break;
        //         default:
        //             DisplayAllDeveloperInfo();

        //     }

        //     PressAnyKey();
        // }

        //Helper Methods
        private void DisplayDevTeams(DevTeam devTeam) 
        {
            System.Console.WriteLine($"\tDeveloper Team: {devTeam.TeamID} \n \tTeam Name: {devTeam.Name} \n");
            
        }
        private void DisplayDevTeamInfo(DevTeam thisDevTeam)
        {
            Console.Clear();

            System.Console.WriteLine("*** Developer Teams *** \n");
            System.Console.WriteLine($"Team ID: {thisDevTeam.TeamID} \n" + $"Team Name: {thisDevTeam.Name} \n");

            if(thisDevTeam.Developer.Count > 0)
            {
                foreach(var d in thisDevTeam.Developer)
                {
                    DisplayAllDeveloperInfo(d);
                }
            }
            else
            {
                System.Console.WriteLine("There is no developers in this team.");
            }
            PressAnyKey();
        }

        private void DisplayAllDeveloperInfo(Developer developer)
        {
            System.Console.WriteLine(
                $"ID: {developer.ID} \n" +
                $"First Name: {developer.FirstName} \n" +
                $"Last Name: {developer.LastName} \n" +
                $"Pluralsight Access: {developer.Pluralsight} \n" 
            );
        }

        private Pluralsight GivePluralsightInfo(Developer developer)
        {
            System.Console.WriteLine("Do they have access to Pluralsight? y/n? \n" +
            
            "1. Yes \n" +
            "2. No \n"
            );

            string pluralsight = Console.ReadLine();
            switch(pluralsight)
            {
                case "1":
                    return Pluralsight.Has_Access;
                case "2":
                    return Pluralsight.No_Access;
                default:
                    System.Console.WriteLine("Invalid selection");
                    return Pluralsight.No_Access;
                    PressAnyKey();
            }

        }

        
        //! Tried to make a list of names with a particular enum value attached to it. Didn't work. Could you explain why? Or rather the correct to do the 1st challenge of project.
        // private Pluralsight ViewDevWithPSAccess(Developer developer)
        // {
        //     Console.Clear();
        //     System.Console.WriteLine("=== Developers WITH Pluralsight Access === \n");
        //     foreach(Developer d in developer)
        //     {
        //         if(Pluralsight.Has_Access = d)
        //         {
        //             DisplayAllDeveloperInfo(d);
        //         }
        //         else
        //         {
        //             return null;
        //         }
        //     }
        //     return null;
            
        //     PressAnyKey();
        // }

        // private Pluralsight ViewDevWithNoPSAccess(Developer developer)
        // {
        //     Console.Clear();
        //     System.Console.WriteLine("=== Developers WITH NO Pluralsight Access === \n");
            
        //     foreach(Developer d in developer)
        //     {
        //         if(Pluralsight.No_Access = d)
        //         {
        //             DisplayAllDeveloperInfo(d);
        //         }
        //         else
        //         {
        //             return null;
        //         }
        //     }
            
        //     return null;
            
        //     PressAnyKey();
        // }
        


    }
        
        
        