using System;
using System.Collections.Generic;
using System.Text;

public class Pitcher:Player
{
    public bool starter;
    public int stamina;
    public int speed;
    public int tricky;
    public int fatigue;
    public int control;
    public int[] pitchCount = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] earnedRuns = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] inningsPitched = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static Random rand = new Random();
    public Pitcher(int minimum, int maximum, int target, int range, bool starter)
    :base()
    {
        this.starter = starter;
        int nameRoll = rand.Next(0, Create.nameList.Count);
        name = $"{Create.nameList[nameRoll]}";
        Create.nameList.Remove(Create.nameList[nameRoll]);
        positionString = "Pitcher";
        do
        {
            control = rand.Next(minimum, maximum);
            tricky = rand.Next(minimum, maximum);
            speed = rand.Next(minimum, maximum);
            if (starter) stamina = rand.Next(minimum, maximum);
            else stamina = rand.Next(10, maximum / 2);
            contact = rand.Next(minimum, maximum);
            power = rand.Next(minimum, maximum);
            fielding = rand.Next(minimum, maximum);
            throwing = rand.Next(minimum, maximum);
            running = rand.Next(minimum, maximum);
            positionSkill = rand.Next(minimum, maximum);
        } while (target > Overall + range || target < Overall - range);
        coefficient = (Overall > 90) ? 5 : (Overall > 85) ? 3 : (Overall > 80) ? 2 : 1;
        price = Overall * (400 + rand.Next(-20, 20)) * coefficient;
    }

    public override double Overall
    {
        get
        {
            if (starter)
            {
                return (
                                    control * 6 +
                                    speed * 6 +
                                    stamina * 5 +
                                    tricky * 6 +
                                    contact * 2 +
                                    power * 2 +
                                    fielding * 3 +
                                    throwing * 3 +
                                    running * 2
                                    )
                                    / 35;
            }
            else
            {
                return (
                                    control * 6 +
                                    speed * 6 +
                                    tricky * 6 +
                                    contact * 2 +
                                    power * 2 +
                                    fielding * 3 +
                                    throwing * 3 +
                                    running * 2
                                    )
                                    / 30;
            }
        }
    }
}