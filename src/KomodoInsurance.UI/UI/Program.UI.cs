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
            Developer stan = new Developer("Stan", "Smith");
            Developer ben = new Developer("Ben", "Wyatt");
            Developer peter = new Developer("Peter", "Griffin");
            Developer michael = new Developer("Michael", "Scott");
            Developer natassia = new Developer("Natassia", "Caldwell");
            Developer leslie = new Developer("Leslie", "Knope");
            Developer jessie = new Developer("Jessie", "Spano");
            Developer annalise = new Developer("Annalise", "Keating");
            Developer lisa = new Developer("Lisa", "Simpson");
            Developer rachel = new Developer("Rachel", "Green");
            Developer sterling = new Developer("Sterling", "Archer");
            Developer sam = new Developer("Sam", "Malone");
            Developer dexter = new Developer("Dexter", "Morgan");
            Developer dana = new Developer("Dana", "Scully");
            Developer arya = new Developer("Arya", "Stark");


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

            DevTeam group_1 = new DevTeam("Rockets");
            DevTeam group_2 = new DevTeam("Pacers");
            DevTeam group_3 = new DevTeam("Lakers");

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
                    +" 9. Update a Developer \n"
                    +" 10. Delete a Developer \n"
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
                    // case "6":
                    //     AddDeveloperToDatabase();
                    //     break;
                    // case "7":
                    //     ViewAllDevelopers();
                    //     break;
                    // case "8":
                    //     ViewDeveloperByID();
                    //     break;
                    // case "9":
                    //     UpdateDeveloperByID();
                    //     break;
                    // case "9":
                    //     DeleteDeveloperByID();
                    //     break;
                    case "x":
                        isRunning = CloseApplication();
                        break;

                    default:
                    System.Console.WriteLine("Invalid Selection. Please Try Again.");
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

            DevTeam newDeveloperTeam = new DevTeam();
            var currentDevelopers = _dRepo.GetAllDevelopers();

            System.Console.WriteLine("Please enter a name for the Developer Team: ");
            newDeveloperTeam.Name = System.Console.ReadLine();

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
                        newDeveloperTeam.Developer.Add(selectedDeveloper);

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


            bool isSuccessful = _dtRepo.AddDevTeamToDatabase(newDeveloperTeam);
            if(isSuccessful)
            {
                System.Console.WriteLine($"Developer Team:{newDeveloperTeam.Name} was added to the database!");
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
            var devTeams = _dtRepo.GetAllTeams();

            foreach(DevTeam t in devTeams)
            {
                DisplayDevTeamInfo(t);
            }
            try
            {
                System.Console.WriteLine("Please select a Developer Team by their ID: \n");
                int userInput = int.Parse(Console.ReadLine());
                var selectedDevTeam = _dtRepo.GetDevTeamByID(userInput);

                if(selectedDevTeam != null)
                {
                    DisplayDevTeams(selectedDevTeam);
                }
                else
                {
                    System.Console.WriteLine($"Sorry, the Developer Team with ID: {userInput} does not exist. Try again.");
                }
            }
            catch
            {
                System.Console.WriteLine("Sorry, invalid selection. Please try again.");
            }
            PressAnyKey();
        }

        private void DisplayDevTeamInfo(DevTeam thisDevTeam)
        {
            Console.Clear();

            System.Console.WriteLine($"Team ID: {thisDevTeam.TeamID} \n" + $"Team Name: {thisDevTeam.Name} \n");

            System.Console.WriteLine("*** Team Developers *** \n");
            if(thisDevTeam.Developer.Count > 0)
            {
                foreach(var d in thisDevTeam.Developer)
                {
                    DisplayDeveloperInfo(d);
                }
            }
            else
            {
                System.Console.WriteLine("There is no developers in this team.");
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
                            System.Console.WriteLine("Sorry, that developer does not exist. Please try again.");
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
                    System.Console.WriteLine("Dev Team failed to update. Please try again.");
                }
            }
            else
            {
                System.Console.WriteLine($"The store with ID: {userInput} isn't valid. Please try again.");
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
                    System.Console.WriteLine("Team was not deleted. Please try again.");
                }
            }
            catch
            {
                System.Console.WriteLine("Incorrect selection. Please try again.");
            }

            PressAnyKey();
        }

        //Helper Methods
        private void DisplayDevTeams(DevTeam devTeam) 
        {
            System.Console.WriteLine($"Developer Team: {devTeam.TeamID} \n Team Name: {devTeam.Name}\n");
        }


        private void DisplayDeveloperInfo(Developer developer)
        {
            System.Console.WriteLine(
                $" \tDeveloper ID: {developer.ID} \n \tDeveloper Name: {developer.FirstName} {developer.LastName} \n"
            );
        }
    } 