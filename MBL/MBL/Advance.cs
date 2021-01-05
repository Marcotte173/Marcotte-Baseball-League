using System;
using System.Collections.Generic;
using System.Text;

public class Advance
{
    public static void ToFirst()
    {
        Engine.runnerOn[1] = Return.Batter();
    }
    public static void ToSecond()
    {
        Engine.runnerOn[2] = Return.Batter();
    }
    public static void ToThird()
    {
        Engine.runnerOn[3] = Return.Batter();
    }
    public static void FirstToSecond()
    {
        Engine.announcer.Add(Engine.runnerOn[1].name + " advances to second");
        Engine.runnerOn[2] = Engine.runnerOn[1];
        Engine.runnerOn[1] = null;
    }
    public static void FirstToThird()
    {
        Engine.announcer.Add(Engine.runnerOn[1].name + " advances to third");
        Engine.runnerOn[3] = Engine.runnerOn[1];
        Engine.runnerOn[1] = null;
    }
    public static void SecondToThird()
    {
        Engine.announcer.Add(Engine.runnerOn[2].name + " advances to third");
        Engine.runnerOn[3] = Engine.runnerOn[2];
        Engine.runnerOn[2] = null;
    }
    public static void ScoreFromHome()
    {
        Engine.announcer.Add(Return.Batter().name + " scores!");
        Return.AtBat().runs[Engine.week]++;
        Return.InField().runsAgainst[Engine.week]++;
    }
    public static void ScoreFromFirst()
    {
        Engine.announcer.Add(Engine.runnerOn[1].name + " scores!");
        Return.AtBat().runs[Engine.week]++;
        Return.InField().runsAgainst[Engine.week]++;
        Engine.runnerOn[1] = null;
    }
    public static void ScoreFromSecond()
    {
        Engine.announcer.Add(Engine.runnerOn[2].name + " scores!");
        Return.AtBat().runs[Engine.week]++;
        Return.InField().runsAgainst[Engine.week]++;
        Engine.runnerOn[2] = null;
    }
    public static void ScoreFromThird()
    {
        Engine.announcer.Add(Engine.runnerOn[3].name + " scores!");
        Return.AtBat().runs[Engine.week]++;
        Return.InField().runsAgainst[Engine.week]++;
        Engine.runnerOn[3] = null;
    }

    internal static void Force()
    {
        if (Engine.runnerOn[1] != null && Engine.runnerOn[2] == null && Engine.runnerOn[3] == null) FirstToSecond();
        if (Engine.runnerOn[1] != null && Engine.runnerOn[2] != null && Engine.runnerOn[3] == null)
        {
            SecondToThird();
            FirstToSecond();
        }
        if (Engine.runnerOn[1] != null && Engine.runnerOn[2] == null && Engine.runnerOn[3] != null) AllRunners();
    }

    internal static void AllRunners()
    {
        if (Engine.runnerOn[3] != null) ScoreFromThird();
        if (Engine.runnerOn[2] != null) SecondToThird();
        if (Engine.runnerOn[1] != null) FirstToSecond();
    }
    internal static void AllRunners2()
    {
        if (Engine.runnerOn[3] != null) ScoreFromThird();
        if (Engine.runnerOn[2] != null) ScoreFromSecond();
        if (Engine.runnerOn[1] != null) FirstToThird();
    }
    internal static void AllRunners3()
    {
        if (Engine.runnerOn[3] != null) ScoreFromThird();
        if (Engine.runnerOn[2] != null) ScoreFromSecond();
        if (Engine.runnerOn[1] != null) ScoreFromFirst();
    }
    public static void Walk()
    {
        Force();
        ToFirst();
    }
    public static void Single()
    {
        Return.Batter().singleStat[Engine.week]++;
        if (Return.RandomInt(0, 3) == 0) AllRunners2();
        else AllRunners();
        ToFirst();
    }
    public static void Double()
    {
        Return.Batter().doubleStat[Engine.week]++;
        if (Return.RandomInt(0, 3) == 0) AllRunners3();
        else AllRunners2();
        ToSecond();
    }
    public static void Triple()
    {
        Return.Batter().tripleStat[Engine.week]++;
        AllRunners3();
        ToThird();
    }
    public static void HomeRun()
    {

    }
}