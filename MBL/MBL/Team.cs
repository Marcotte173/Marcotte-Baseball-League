using System;
using System.Collections.Generic;
using System.Text;

public class Team
{
    public string name;
    public int value;
    public int money;
    public int owner;
    public int[] runs = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] runsAgainst = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public Player catcher;
    public Player first;
    public Player second;
    public Player third;
    public Player shortStop;
    public Player cf;
    public Player lf;
    public Player rf;
    public Pitcher pitcher;
    public List<Player> roster = new List<Player> { };
    public Player[] battingOrder = new Player[9];
    public Player atBat;
    public Team(string name)
    {
        this.name = name;
    }
}