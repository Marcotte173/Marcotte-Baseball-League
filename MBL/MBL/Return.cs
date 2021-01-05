using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class Return
{
    internal static Random rand = new Random();
    internal static int RandomInt(int min, int max) => rand.Next(min, max);
    internal static int Integer()
    {
        int sellChoice;
        do
        {

        } while (!int.TryParse(Console.ReadLine(), out sellChoice));
        return sellChoice;
    }
    internal static int Int()
    {
        int sellChoice;
        do
        {

        } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out sellChoice));
        return sellChoice;
    }

    internal static bool Confirm()
    {
        Write.Line(0, Console.WindowHeight - 3, "[1]" + Color.RUN + " Yes" + Color.RESET);
        Write.Line(0, Console.WindowHeight - 2, "[2]" + Color.STRENGTH + " No" + Color.RESET);
        string choice = Return.Option();
        if (choice == "1") return true;
        else if (choice == "2") return false;
        else Confirm();
        return false;
    }

    internal static bool Confirm(int x, int y)
    {
        Write.Line(x, y, "[1]" + Color.RUN + " Yes" + Color.RESET);
        Write.Line(x, y, "[2]" + Color.STRENGTH + " No" + Color.RESET);
        string choice = Return.Option();
        if (choice == "1") return true;
        else if (choice == "2") return false;
        else Confirm();
        return false;
    }   
    internal static Player Batter()
    {
        foreach (Player p in AtBat().roster) if (p.batting == Batting.AtBat) return p;
        return null;
    }
    internal static Pitcher Pitch() => InField().pitcher;
    internal static Team AtBat() => (Engine.top)? Engine.visitor: Engine.home;
    internal static Team InField() => (Engine.top) ? Engine.home : Engine.visitor;
    internal static string Option() => Console.ReadKey(true).KeyChar.ToString().ToLower();
    internal static string String() => Console.ReadLine();    
}