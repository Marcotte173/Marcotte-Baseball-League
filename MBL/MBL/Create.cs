using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Create
{
    public static List<string> nameList = new List<string> { };

    public static List<Player> catcherList = new List<Player> { };
    public static List<Player> firstList = new List<Player> { };
    public static List<Player> secondList = new List<Player> { };
    public static List<Player> thirdList = new List<Player> { };
    public static List<Player> shortList = new List<Player> { };
    public static List<Player> ofList = new List<Player> { };
    public static List<Player> pitcherList = new List<Player> { };
    static int franchise;
    static int elite;

    public static Random rand = new Random();

    public static void Players()
    {
        Catchers();
        FirstBasemen();
        SecondBasemen();
        ThirdBasemen();
        ShortStop();
        OutFielders();
        Pitchers();
    }

    private static void Pitchers()
    {
        int franchiseStarter = Return.RandomInt(1, 3);
        int eliteStarter = Return.RandomInt(2, 5);
        int franchiseRelief = Return.RandomInt(3, 6);
        int eliteRelief = Return.RandomInt(4, 7);
        for (int i = 0; i < franchiseStarter; i++) { pitcherList.Add(new Pitcher(80, 99, 94, 3,true)); }
        for (int i = 0; i < franchiseRelief; i++) { pitcherList.Add(new Pitcher(80, 99, 94, 3,false)); }
        for (int i = 0; i < eliteStarter; i++) { pitcherList.Add(new Pitcher(75, 93, 89, 3, true)); }
        for (int i = 0; i < eliteRelief; i++) {  pitcherList.Add(new Pitcher(75, 93, 89, 3, false)); }
        for (int i = 0; i < 15; i++) { pitcherList.Add(new Pitcher(60, 90, 75, 10, true)); }
        for (int i = 0; i < 15; i++) { pitcherList.Add(new Pitcher(60, 90, 75, 10, false)); }
    }

    private static void Catchers()
    {
        franchise = Return.RandomInt(1, 3);
        elite = Return.RandomInt(2, 4);
        for (int i = 0; i < franchise; i++) { catcherList.Add(new Player(80, 99, 94, 3, Position.Catcher)); }
        for (int i = 0; i < elite; i++) { catcherList.Add(new Player(75, 93, 89, 3, Position.Catcher)); }
        for (int i = 0; i < 6; i++) { catcherList.Add(new Player(75, 89, 85, 5, Position.Catcher)); }
        for (int i = 0; i < 10; i++) { catcherList.Add(new Player(70, 83, 75, 6, Position.Catcher)); }
    }

    private static void FirstBasemen()
    {
        franchise = Return.RandomInt(1, 3);
        elite = Return.RandomInt(2, 4);
        for (int i = 0; i < franchise; i++) { firstList.Add(new Player(80, 99, 94, 3, Position.First)); }
        for (int i = 0; i < elite; i++) { firstList.Add(new Player(75, 93, 89, 3, Position.First)); }
        for (int i = 0; i < 6; i++) { firstList.Add(new Player(75, 89, 85, 5, Position.First)); }
        for (int i = 0; i < 10; i++) { firstList.Add(new Player(70, 83, 75, 6, Position.First)); }
    }

    private static void SecondBasemen()
    {
        franchise = Return.RandomInt(1, 3);
        elite = Return.RandomInt(2, 4);
        for (int i = 0; i < franchise; i++) { secondList.Add(new Player(80, 99, 94, 3, Position.Second)); }
        for (int i = 0; i < elite; i++) { secondList.Add(new Player(75, 93, 89, 3, Position.Second)); }
        for (int i = 0; i < 6; i++) { secondList.Add(new Player(75, 89, 85, 5, Position.Second)); }
        for (int i = 0; i < 10; i++) { secondList.Add(new Player(70, 83, 75, 6, Position.Second)); }
    }

    private static void ThirdBasemen()
    {
        franchise = Return.RandomInt(1, 3);
        elite = Return.RandomInt(2, 4);
        for (int i = 0; i < franchise; i++) { thirdList.Add(new Player(80, 99, 94, 3, Position.Third)); }
        for (int i = 0; i < elite; i++) { thirdList.Add(new Player(75, 93, 89, 3, Position.Third)); }
        for (int i = 0; i < 6; i++) { thirdList.Add(new Player(75, 89, 85, 5, Position.Third)); }
        for (int i = 0; i < 10; i++) { thirdList.Add(new Player(70, 83, 75, 6, Position.Third)); }
    }

    private static void ShortStop()
    {
        franchise = Return.RandomInt(1, 3);
        elite = Return.RandomInt(2, 4);
        for (int i = 0; i < franchise; i++) { shortList.Add(new Player(80, 99, 94, 3, Position.Short)); }
        for (int i = 0; i < elite; i++) { shortList.Add(new Player(75, 93, 89, 3, Position.Short)); }
        for (int i = 0; i < 6; i++) { shortList.Add(new Player(75, 89, 85, 5, Position.Short)); }
        for (int i = 0; i < 10; i++) { shortList.Add(new Player(70, 83, 75, 6, Position.Short)); }
    }

    private static void OutFielders()
    {
        franchise = Return.RandomInt(2, 5);
        elite = Return.RandomInt(4, 7);
        for (int i = 0; i < franchise; i++) { ofList.Add(new Player(80, 99, 94, 3, Position.Outfield)); }
        for (int i = 0; i < elite; i++) { ofList.Add(new Player(75, 93, 89, 3, Position.Outfield)); }
        for (int i = 0; i < 15; i++) { ofList.Add(new Player(75, 89, 85, 5, Position.Outfield)); }
        for (int i = 0; i < 30; i++) { ofList.Add(new Player(70, 83, 75, 6, Position.Outfield)); }
    }

    public static void Teams()
    {

    }
}
