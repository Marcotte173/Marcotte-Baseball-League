using System;
using System.Collections.Generic;
using System.Text;

public class Write
{
    internal static void Line(int x, int y, string words) { Console.SetCursorPosition(x, y); Console.Write(words); Console.WriteLine(Color.RESET); }
    internal static void Line(string words) { Console.WriteLine(words); Console.WriteLine(Color.RESET); }
    internal static void Line(int x, int y, string word1, string word2)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(word1);
        Console.SetCursorPosition(x + 25, y);
        Console.Write(word2);
    }

    internal static void Display()
    {
        Console.Clear();
        
        Line(Color.TEAM,Return.AtBat().name);
        Line(98 - Return.InField().name.Length/2, 0, Color.TEAM+ Return.InField().name);
        Line(Return.AtBat().name.Length/2, 1,Color.RUN +  Return.AtBat().runs[Engine.week].ToString());
        Line(97, 1, Color.RUN + Return.InField().runs[Engine.week].ToString());
        for (int i = 0; i < 9; i++)
        {
            if(Return.AtBat().battingOrder[i].batting == Batting.AtBat) Line(0, 3 + i, $"[{i + 1}] " + Color.PLAYER + Return.AtBat().battingOrder[i].name + Color.CLASS + " AT BAT");
            else Line(0, 3 + i, $"[{i + 1}] " + Color.PLAYER + Return.AtBat().battingOrder[i].name);
        }
        Line(80, 3,  "[A] "+Color.POSITION+"Catcher     "+Color.RESET+" -   " + Color.PLAYER + Return.InField().catcher.name);
        Line(80, 4,  "[B] "+Color.POSITION+"First Base  "+Color.RESET+" -   " + Color.PLAYER + Return.InField().first.name);
        Line(80, 5,  "[C] "+Color.POSITION+"Second Base "+Color.RESET+" -   " + Color.PLAYER + Return.InField().second.name);
        Line(80, 6,  "[D] "+Color.POSITION+"Third Base  "+Color.RESET+" -   " + Color.PLAYER + Return.InField().third.name);
        Line(80, 7,  "[E] "+Color.POSITION+"Short Stop  "+Color.RESET+" -   " + Color.PLAYER + Return.InField().shortStop.name);
        Line(80, 8,  "[F] "+Color.POSITION+"Left Field  "+Color.RESET+" -   " + Color.PLAYER + Return.InField().lf.name);
        Line(80, 9,  "[G] "+Color.POSITION+"Center Field"+Color.RESET+" -   " + Color.PLAYER + Return.InField().cf.name);
        Line(80, 10, "[H] "+Color.POSITION+"Right Field "+Color.RESET+" -   " + Color.PLAYER + Return.InField().rf.name);
        Line(80, 12, "[P] "+Color.POSITION+"Pitcher     "+Color.RESET+" -   " + Color.PLAYER + Return.InField().pitcher.name);
        Line(80, 14, "Pitch Count      -   " + Return.InField().pitcher.pitchCount[Engine.week]);
        string topOrBottom = (Engine.top) ? "Top of the " : "Botom of the ";
        Line(45, 6, "     /\\		");
        Line(45, 7, "    /  \\	");
        Line(45, 8, "   /    \\	");
        Line(45, 9, "  /      \\	");
        Line(45, 10, "  \\      /	");
        Line(45, 11, "   \\    /	");
        Line(45, 12, "	 \\  /	");
        Line(45, 13, "     \\/		");
        if (Engine.runnerOn[1] != null) Line(56, 9, Color.PLAYER + Engine.runnerOn[1].name);
        if (Engine.runnerOn[2] != null) Line(51 - Engine.runnerOn[2].name.Length / 2, 5, Color.PLAYER + Engine.runnerOn[2].name);
        if (Engine.runnerOn[3] != null) Line(46- Engine.runnerOn[3].name.Length, 9, Color.PLAYER + Engine.runnerOn[3].name);
        Line(45,0,topOrBottom + Engine.innings);
        Line(45, 1, Engine.outs + " Outs");
        Line(45, 2, Engine.balls + " Balls");
        Line(45, 3, Engine.strikes + " Strikes");
        for (int i = 0; i < Engine.announcer.Count; i++)
        {
            Line(0, 29- Engine.announcer.Count + i, Engine.announcer[i]);
        }
    }

    internal static void Line(int x, int y, string word1, string word2, string word3)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(word1);
        Console.SetCursorPosition(x + 12, y);
        Console.Write(word2);
        Console.SetCursorPosition(x + 25, y);
        Console.Write(word3);
    }
    internal static void Line(int x, int y, string word1, string word2, string word3, string word4)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(word1);
        Console.SetCursorPosition(x + 15, y);
        Console.Write(word2);
        Console.SetCursorPosition(x + 30, y);
        Console.Write(word3);
        Console.SetCursorPosition(x + 45, y);
        Console.Write(word4);
    }
    internal static void Line(int x, int y, string word1, string word2, string word3, string word4, string word5, string word6, string word7)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(word1);
        Console.SetCursorPosition(x, y);
        Console.Write(word2);
        Console.SetCursorPosition(x + 15, y);
        Console.Write(word3);
        Console.SetCursorPosition(x + 30, y);
        Console.Write(word4);
        Console.SetCursorPosition(x + 45, y);
        Console.Write(word5);
        Console.SetCursorPosition(x + 60, y);
        Console.Write(word6);
        Console.SetCursorPosition(x + 75, y);
        Console.Write(word7);
    }
    internal static void KeyPress()
    {
        Write.Line("Press any key to continue");
        Console.ReadKey(true);
    }
    internal static void KeyPress(int a)
    {
        for (int i = 0; i < a; i++)
        {
            Console.WriteLine("");
        }
        Write.Line("Press any key to continue");
        Console.ReadKey(true);
    }
    internal static void KeyPress(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Write.Line("Press any key to continue");
        Console.ReadKey(true);
    }

    public static void Line(string colour, string text)
    {
        Console.Write($"{colour}" + $"{text}" + Color.RESET);
    }

    public static void Line(string colour, string text1, string text2, string text3)
    {
        Console.Write(
            $"{text1}"
            + colour + $"{text2}"
            + Color.RESET + $"{text3}\n");
    }

    public static void Line(string colour, string colour2, string text1, string text2, string text3, string text4, string text5)
    {
        Console.Write(
            $"{text1}"
            + colour
            + $"{text2}"
            + Color.RESET + $"{text3}"
            + colour2 + $"{text4}"
            + Color.RESET + $"{text5}\n");
    }

    public static void Line(string colour, string colour2, string colour3, string text1, string text2, string text3, string text4, string text5, string text6, string text7)
    {
        Console.Write(
            $"{text1}"
            + colour + $"{text2}"
            + Color.RESET + $"{text3}"
            + colour2 + $"{text4}"
            + Color.RESET + $"{text5}"
            + colour3 + $"{text6}"
            + Color.RESET
            + $"{text7}\n");
    }

    public static void Line(string colour1, string colour2, string colour3, string colour4, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9)
    {
        Console.Write(
            $"{text1}"
            + colour1
            + $"{text2}"
            + Color.RESET
            + $"{text3}"
            + colour2
            + $"{text4}"
            + Color.RESET
            + $"{text5}"
            + colour3
            + $"{text6}"
            + Color.RESET
            + $"{text7}"
            + colour4
            + $"{text8}"
            + Color.RESET
            + $"{text9}\n");
    }

    public static void Line(string colour1, string colour2, string colour3, string colour4, string colour5, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9, string text10, string text11)
    {
        Console.Write(
             $"{text1}"
             + colour1
             + $"{text2}"
             + Color.RESET
             + $"{text3}"
             + colour2
             + $"{text4}"
             + Color.RESET
             + $"{text5}"
             + colour3
             + $"{text6}"
             + Color.RESET
             + $"{text7}"
             + colour4
             + $"{text8}"
             + Color.RESET
             + $"{text9}"
             + colour5
             + $"{text10}"
             + Color.RESET
             + $"{text11}\n");
    }

    public static void Line(int x, int y, string colour, string colour2, string colour3, string text1, string text2, string text3, string text4, string text5, string text6, string text7)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(
            $"{text1}"
            + colour + $"{text2}"
            + Color.RESET + $"{text3}"
            + colour2 + $"{text4}"
            + Color.RESET + $"{text5}"
            + colour3 + $"{text6}"
            + Color.RESET
            + $"{text7}\n");
    }

    public static void Line(int x, int y, string colour1, string colour2, string colour3, string colour4, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(
            $"{text1}"
            + colour1
            + $"{text2}"
            + Color.RESET
            + $"{text3}"
            + colour2
            + $"{text4}"
            + Color.RESET
            + $"{text5}"
            + colour3
            + $"{text6}"
            + Color.RESET
            + $"{text7}"
            + colour4
            + $"{text8}"
            + Color.RESET
            + $"{text9}\n");
    }

    public static void Line(int x, int y, string colour1, string colour2, string colour3, string colour4, string colour5, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9, string text10, string text11)
    {
        Console.Write(
             $"{text1}"
             + colour1
             + $"{text2}"
             + Color.RESET
             + $"{text3}"
             + colour2
             + $"{text4}"
             + Color.RESET
             + $"{text5}"
             + colour3
             + $"{text6}"
             + Color.RESET
             + $"{text7}"
             + colour4
             + $"{text8}"
             + Color.RESET
             + $"{text9}"
             + colour5
             + $"{text10}"
             + Color.RESET
             + $"{text11}\n");
    }
}