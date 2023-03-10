namespace MiniprojektSql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PersonModel> people = SqlConnection.LoadPersonModel();

            Console.WriteLine();
            for (int i = 0; i < people.Count; i++)
            {
                Console.WriteLine($"id: {people[i].id}");
                Console.WriteLine($"person: {people[i].person_name}");
            }
            bool runmenu = true;

            //while (runmenu)
            //{
            //    string selectedItem = DrawMenu(menuOptions);
            //}


        }
        public static int menuIndex = 0;
        public static string DrawMenu(List<string> menuItem)
        {

            Console.Clear();
            Console.WriteLine("");

            //looping through menuItems and displaying them. if menuIndex == i add "["  "]" to menuItem[i]
            for (int i = 0; i < menuItem.Count; i++)
            {
                if (i == menuIndex)
                {
                    Console.WriteLine($"[{menuItem[i]}]");
                }
                else
                {
                    Console.WriteLine($" {menuItem[i]} ");
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

    }
}