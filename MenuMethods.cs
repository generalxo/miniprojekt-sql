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

            //looping through menuItems and displaying them. if menuIndex == i add "["  "]" to menuItem[i]
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
            List<string> menuOptions = new List<string> { "Add time", "View timers", "Create project", "Create user", "Exit" };

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
                    Console.WriteLine("View timers would start here");
                    Console.ReadKey();
                }
                else if (selectedMenuOption == menuOptions[2])
                {
                    Console.WriteLine("Create project would start here");
                    Console.ReadKey();
                }
                else if (selectedMenuOption == menuOptions[3])
                {
                    Console.WriteLine("Create user would start here");
                    Console.ReadKey();
                }

                else if (selectedMenuOption == menuOptions[4])
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
                            Console.WriteLine($"\n  hours: {input}");
                            int.TryParse(input, out hours);
                            projectPersonModel.project_id = selectedProject.id;
                            projectPersonModel.person_id = selectedPerson.id;
                            projectPersonModel.hours = hours;

                            SqlConnection.SaveProjectPersonModel(projectPersonModel);
                            Console.ReadKey();
                            runMenu = false;
                            runSubMenu = false;
                        }
                    }
                }

            }

        }

        public static void ViewTime()
        {

        }
    }
}
