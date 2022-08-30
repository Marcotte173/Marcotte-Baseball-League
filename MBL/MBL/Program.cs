using System;

namespace MBL
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(, 40);
            Console.CursorVisible = false;
            Color.SetupConsole();
            string[] firstNames = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/FirstNames.txt");
            string[] lastNames = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/LastNames.txt");
            for (int i = 0; i < firstNames.Length; i++)
            {
                for (int j = 0; j < lastNames.Length; j++) { Create.nameList.Add($"{firstNames[i]} {lastNames[j]}"); }
            }
            Create.Players();
            Create.Teams();
            Engine.Setup();            
        }
    }
}
