using System;
using System.Collections.Generic;
using System.Text;

public enum Position { Catcher, First, Second, Third, Short, Outfield}
public enum Batting {None, AtBat,OnDeck,InTheHole };

public class Player
{
    static Random rand = new Random();
    public string name;
    public int patience;
    public int age;
    public int contact;
    public int power;
    public int fielding;
    public int throwing;
    public int running;
    public int overall;
    public int positionSkill;
    public double coefficient;
    public double price;
    public Batting batting;
    public int gamesPlayed;
    public int[] singleStat  =    new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] doubleStat =     new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] tripleStat =     new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] homeRunStat =    new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] strikeOutStat =  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] walkStat =       new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] stealStat =      new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] errorStat =      new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public Position position;
    public string positionString;

    public Player() { }

    public Player(int minimum, int maximum, int target, int range, Position position)
    : base()
    {
        this.position = position;
        int nameRoll = rand.Next(0, Create.nameList.Count);
        name = $"{Create.nameList[nameRoll]}";
        Create.nameList.Remove(Create.nameList[nameRoll]);
        positionString = (position == Position.Catcher) ? "Catcher" : (position == Position.First) ? "First Base" : (position == Position.Second) ? "Second Base" : (position == Position.Third) ? "Third Base" : (position == Position.Short) ? "Short Stop" : "Outfield";
        do
        {
            patience = rand.Next(minimum, maximum);
            contact = rand.Next(minimum, maximum);
            power = rand.Next(minimum, maximum);
            fielding = rand.Next(minimum, maximum);
            throwing = rand.Next(minimum, maximum);
            running = rand.Next(minimum, maximum);
            positionSkill = Return.RandomInt(maximum, 101);
        } while (target > Overall + range || target < Overall - range);
        coefficient = (Overall > 90) ? 5 : (Overall > 85) ? 3 : (Overall > 80) ? 2 : 1;
        price = Overall * (400 + rand.Next(-20, 20)) * coefficient;
    }

    public virtual double Overall
    {
        get
        {
            return (
                    patience * 3+
                    contact * 6 +
                    power * 6 +
                    fielding * 3 +
                    throwing * 3 +
                    running * 3 +
                    positionSkill *3
                    )
                    / 27 ;
        }
    }
}
