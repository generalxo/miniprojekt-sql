﻿namespace MiniprojektSql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartMenu();
        }
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

            bool runmenu = true;
            List<string> menuOptions = new List<string> { "Add time", "Create user", "Create project" };

            //List<PersonModel> people = SqlConnection.LoadPersonModel();

            string menuMsg = "  Please select an option";

            while (runmenu)
            {
                string selectedMenuOption = DrawMenu(menuOptions, menuMsg);

                if (selectedMenuOption == menuOptions[0])
                {
                    Console.WriteLine("Selecting user would start here");
                    Console.ReadKey();
                }
                else if (selectedMenuOption == menuOptions[1])
                {
                    Console.WriteLine("Create user would start here");
                    Console.ReadKey();
                }
                else if (selectedMenuOption == menuOptions[2])
                {
                    Console.WriteLine("Create project would start here");
                    Console.ReadKey();
                }

            }
        }

    }
}