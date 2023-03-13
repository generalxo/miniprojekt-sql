namespace MiniprojektSql
{
    public class MenuMethods
    {
        public static int menuIndex = 0;

        public static string DrawMenu(List<string> menuItem, string menuMsg)
        {

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(menuMsg);
            Console.WriteLine("");

            //looping through menuItems and displaying them. if menuIndex == i add '['  ']' to menuItem[i]
            for (int i = 0; i < menuItem.Count; i++)
            {
                if (i == menuIndex)
                {
                    Console.WriteLine($" [{menuItem[i]}]");
                }
                else
                {
                    Console.WriteLine($"  {menuItem[i]} ");
                }
            }

            //Check for key input
            ConsoleKeyInfo ckey = Console.ReadKey();

            //Down arrow key check
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (menuIndex == menuItem.Count - 1) { }
                else { menuIndex++; }
            }
            //Up arrow key check
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (menuIndex <= 0) { }
                else { menuIndex--; }
            }
            //Left arrow key check
            else if (ckey.Key == ConsoleKey.LeftArrow)
            {
                //Console.Clear();
            }
            //Right arrow key check
            else if (ckey.Key == ConsoleKey.RightArrow)
            {
                return menuItem[menuIndex];
            }
            //Enter key check
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return menuItem[menuIndex];
            }
            //else
            else
            {
                return "";
            }
            //else
            return "";
        }

        public static void StartMenu()
        {
            Console.CursorVisible = false;
            Console.Clear();

            //Declaration of variables
            bool runmenu = true;
            List<string> menuOptions = new() { "Add time", "View all timers", "View project timers", "Create project", "Create user", "Exit" };

            //Selecting MenuOption & corresponding function
            while (runmenu)
            {
                string selectedMenuOption = DrawMenu(menuOptions, "  Please select an option");

                if (selectedMenuOption == menuOptions[0])
                {
                    AddTime();
                }
                else if (selectedMenuOption == menuOptions[1])
                {
                    ViewAllTime();
                }
                else if (selectedMenuOption == menuOptions[2])
                {
                    ViewProjectTime();
                }
                else if (selectedMenuOption == menuOptions[3])
                {
                    CreateProject();
                }
                else if (selectedMenuOption == menuOptions[4])
                {
                    CreateUser();
                }

                else if (selectedMenuOption == menuOptions[5])
                {
                    runmenu = false;
                }
            }
        }

        public static void AddTime()
        {
            //Variable Declaration & Initation
            int hours;
            string? input;
            menuIndex = 0;
            bool runMenu = true;
            List<string> menuOptions = new();
            PersonModel selectedPerson;
            ProjectModel selectedProject;
            ProjectPersonModel projectPersonModel = new ProjectPersonModel();
            List<PersonModel> people = SqlConnection.LoadPersonModel();

            foreach (var person in people)
            {
                menuOptions.Add(person.person_name);
            }
            menuOptions.Add("Exit");

            while (runMenu)
            {
                string selectedMenuOption = DrawMenu(menuOptions, "  Please select a user");

                if (selectedMenuOption == "Exit")
                {
                    menuIndex = 0;
                    runMenu = false;
                }
                else if (selectedMenuOption != "")
                {
                    selectedPerson = people[menuIndex];

                    //Loading projectModel & seting up menu
                    bool runSubMenu = true;
                    List<ProjectModel> projects = SqlConnection.LoadProjectModel();
                    List<string> projectMenuOptions = new();
                    foreach (var project in projects)
                    {
                        projectMenuOptions.Add(project.project_name);
                    }
                    projectMenuOptions.Add("Exit");
                    //Set menuIndex to 0 so menu cursor starts att first position
                    menuIndex = 0;
                    while (runSubMenu)
                    {
                        selectedMenuOption = DrawMenu(projectMenuOptions, "  Please select project");

                        //Set menuIndex to 0 so menu cursor starts att first position
                        if (selectedMenuOption == "Exit")
                        {
                            menuIndex = 0;
                            runMenu = false;
                            runSubMenu = false;
                        }
                        //DrawMenu Returns "" if no option was selected
                        else if (selectedMenuOption != "")
                        {
                            selectedProject = projects[menuIndex];
                            Console.Clear();
                            Console.Write("\n  Enter whole hours worked on project\n\n  ");
                            input = Console.ReadLine();
                            int.TryParse(input, out hours);
                            projectPersonModel.project_id = selectedProject.id;
                            projectPersonModel.person_id = selectedPerson.id;
                            projectPersonModel.hours = hours;

                            SqlConnection.SaveProjectPersonModel(projectPersonModel);
                            Console.WriteLine("\n  Time Saved! press any key to continue");
                            Console.ReadKey();
                            runMenu = false;
                            runSubMenu = false;
                        }
                    }
                }

            }

        }

        public static void ViewProjectTime()
        {
            //This Method will display the total hours on a project
            //Declaration & Loading Models
            bool foundProject;
            List<ProjectHourModel> projectHoursModels = new();
            List<ProjectPersonModel> projectPersonModels = SqlConnection.LoadProjectPersonModel();
            List<ProjectModel> projectModels = SqlConnection.LoadProjectModel();
            Console.Clear();
            for (int i = 0; i < projectPersonModels.Count; i++)
            {
                for (int j = 0; j < projectModels.Count; j++)
                {
                    //Match Found
                    if (projectPersonModels[i].project_id == projectModels[j].id)
                    {
                        ProjectHourModel temp = new();
                        temp.project_name = projectModels[j].project_name;
                        temp.hours = projectPersonModels[i].hours;
                        if (projectHoursModels.Count == 0)
                        {
                            projectHoursModels.Add(temp);
                        }
                        //Adding total hours of each project to a list
                        else
                        {
                            foundProject = false;
                            for (int k = 0; k < projectHoursModels.Count; k++)
                            {
                                if (projectHoursModels[k].project_name == temp.project_name)
                                {
                                    projectHoursModels[k].hours += temp.hours;
                                    foundProject = true;
                                }
                            }
                            if (foundProject == false)
                            {
                                projectHoursModels.Add(temp);
                            }
                        }
                    }
                }
            }
            //Display
            Console.WriteLine($"\n  Total hours for each project");
            for (int l = 0; l < projectHoursModels.Count; l++)
            {
                Console.WriteLine($"\n  {projectHoursModels[l].project_name}: {projectHoursModels[l].hours}");
            }
            Console.ReadKey();
        }

        public static void ViewAllTime()
        {
            List<ProjectPersonModel> projectPersonModels = SqlConnection.LoadProjectPersonModel();
            List<PersonModel> personModels = SqlConnection.LoadPersonModel();
            List<ProjectModel> projectModels = SqlConnection.LoadProjectModel();

            Console.Clear();
            Console.WriteLine("");

            for (int i = 0; i < projectPersonModels.Count; i++)
            {
                for (int j = 0; j < personModels.Count; j++)
                {
                    if (projectPersonModels[i].person_id == personModels[j].id)
                    {
                        for (int k = 0; k < projectModels.Count; k++)
                        {
                            if (projectPersonModels[i].project_id == projectModels[k].id)
                            {
                                Console.WriteLine($"  {projectModels[k].project_name}: {personModels[j].person_name}: {projectPersonModels[i].hours}\n");
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"\n  Press any key to continue");
            Console.ReadKey();
        }

        public static void CreateUser()
        {
            string? input;
            PersonModel personModel = new();
            Console.Clear();
            Console.Write("\n  Please enter new user name\n  ");
            input = Console.ReadLine();
            //Check input length for DB restrictions
            if (input.Length == 0 || input.Length > 26)
            {
                Console.WriteLine("input invalid");
                Console.WriteLine(input);
            }
            //Inserting project into DB
            else
            {
                Console.WriteLine("\n  User Saved!\n  Press any key to continue");
                personModel.person_name = input;
                SqlConnection.SavePerson(personModel);
            }
            Console.ReadKey();
        }
        public static void CreateProject()
        {
            string? input;
            ProjectModel projectModel = new();
            Console.Clear();
            Console.Write("\n  Please enter new project name\n  ");
            input = Console.ReadLine();
            //Check input length for DB restrictions
            if (input.Length == 0 || input.Length > 51)
            {
                Console.WriteLine("input invalid");
                Console.WriteLine(input);
            }
            //Inserting project into DB
            else
            {
                Console.WriteLine("\n  User Saved!\n  Press any key to continue");
                projectModel.project_name = input;
                SqlConnection.SaveProject(projectModel);
            }
            Console.ReadKey();
        }
    }
}
