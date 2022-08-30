using System;
using System.Collections.Generic;
using System.Text;

public enum PitchLocationX {Inside, Outside,Center };
public enum PitchLocationY { High,Low,Center};
public enum PitchCall {Ball,Strike }
public enum HitType {Grounder,LineDrive,Looper,Pop }
public enum HitDirection {Left,Right,Center }

public class Engine
{
    public static List<string> lowPitchPhrases = new List<string> {"towards the bottom of the strikezone","at the bottom of the strikezone" };
    public static List<string> tooLowPitchPhrases = new List<string> {"low","below the strike zone" };
    public static List<string> tooLowPitchPhrasesX = new List<string> {"in the dirt" };
    public static List<string> highPitchPhrases = new List<string> {"towards the top of the strikezone" };
    public static List<string> tooHighPitchPhrases = new List<string> {"high" };
    public static List<string> outsidePitchPhrases = new List<string> { };
    public static List<string> tooOutsidePitchPhrases = new List<string> { };
    public static List<string> insidePitchPhrases = new List<string> { };
    public static List<string> tooInsidePitchPhrases = new List<string> { };
    public static List<string> meatballPitchPhrases = new List<string> { };
    public static bool meatball;
    public static bool completedAtBat;
    static int success;
    public static int pitch;
    public static int week;                                                                                                                                                                                                                                                                                             
    public static Team a = new Team("Brooklyn Brokers");
    public static Team b = new Team("Edmonton Sandwiches");
    public static Team home = new Team("");
    public static Team visitor = new Team("");
    public static int outs;
    public static int innings;
    public static bool top;
    public static int balls;
    public static int strikes;
    public static bool display;
    public static PitchLocationX pitchLocationX;
    public static PitchLocationY pitchLocationY;
    public static PitchCall pitchCall;
    public static int pitchSpeed;
    public static int pitchControl;
    public static int pitchDeception;
    public static bool swing;
    public static List<string> announcer = new List<string> { };
    public static HitType hitType;
    public static HitDirection hitDirection;
    public static int hitPower;
    public static Player[] runnerOn = new Player[] {null,null,null,null };

    public static void Setup()
    {
        a.roster.Add(new Player(80, 90, 85, 2, Position.Catcher));
        a.roster.Add(new Player(80, 90, 85, 2, Position.First));
        a.roster.Add(new Player(80, 90, 85, 2, Position.Second));
        a.roster.Add(new Player(80, 90, 85, 2, Position.Third));
        a.roster.Add(new Player(80, 90, 85, 2, Position.Short));
        a.roster.Add(new Player(80, 90, 85, 2, Position.Outfield));
        a.roster.Add(new Player(80, 90, 85, 2, Position.Outfield));
        a.roster.Add(new Player(80, 90, 85, 2, Position.Outfield));
        a.roster.Add(new Pitcher(80, 90, 85, 2, true));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Catcher));
        b.roster.Add(new Player(80, 90, 85, 2, Position.First));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Second));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Third));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Short));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Outfield));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Outfield));
        b.roster.Add(new Player(80, 90, 85, 2, Position.Outfield)); 
        b.roster.Add(new Pitcher(80, 90, 85, 2, true));
        for (int i = 0; i < a.battingOrder.Length; i++) a.battingOrder[i] = a.roster[i];
        for (int i = 0; i < b.battingOrder.Length; i++) b.battingOrder[i] = b.roster[i];
        a.catcher = a.roster[0];
        a.first = a.roster[1];
        a.second = a.roster[2];
        a.third = a.roster[3];
        a.shortStop = a.roster[4];
        a.cf = a.roster[5];
        a.lf = a.roster[6];
        a.rf = a.roster[7];
        a.pitcher = (Pitcher)a.roster[8];
        b.catcher = b.roster[0];
        b.first = b.roster[1];
        b.second = b.roster[2];
        b.third = b.roster[3];
        b.shortStop = b.roster[4];
        b.cf = b.roster[5];
        b.lf = b.roster[6];
        b.rf = b.roster[7];
        b.pitcher = (Pitcher)b.roster[8];
        a.battingOrder[0].batting = Batting.AtBat;
        a.battingOrder[1].batting = Batting.OnDeck;
        a.battingOrder[2].batting = Batting.InTheHole;
        b.battingOrder[0].batting = Batting.AtBat;
        b.battingOrder[1].batting = Batting.OnDeck;
        b.battingOrder[2].batting = Batting.InTheHole;
        week = 1;
        Game(a,b,3);
    }

    private static void Game(Team homeTeam, Team visitorTeam,int howManyInnings)
    {
        home = homeTeam;
        visitor = visitorTeam;
        innings = 1;
        if (innings < howManyInnings+1) Inning( howManyInnings);           
    }

    private static void Inning(int howManyInnings)
    {
        top = true;
        HalfInning();
        HalfInning();
        innings++;
        if (innings < howManyInnings+1) Inning(howManyInnings);
    }
    private static void HalfInning()
    {
        runnerOn[1] = null;
        runnerOn[2] = null;
        runnerOn[3] = null;
        outs = 0;
        while (outs < 3)
        {
            int onNumber = 0;
            bool[] on = new bool[4];
            if (runnerOn[1] != null)
            {
                on[1] = true;
                onNumber++;
            }
            if (runnerOn[2] != null)
            {
                on[2] = true;
                onNumber++;
            }
            if (runnerOn[3] != null)
            {
                on[3] = true;
                onNumber++;
            }
            completedAtBat = false;
            balls = 0;
            strikes = 0;
            announcer.Clear();
            Announce($"{Return.Batter().name} steps up to the plate");
            if (!on[1] && !on[2] && !on[3])
            {
                Announce($"There is no one on and {outs} outs");
            }
            else
            {
                string a = (on[1]) ? "first" : (on[2] ? "second" : "third");
                string b = (on[2] ? "second" : "third");
                string c = "third";
                string d = (onNumber == 1) ? "a runner on "+a : (onNumber == 2) ? "runners on "+a + " and " + b : "runners on "+a + ", " + b + " and " +c;
                Announce($"There are {outs} outs and {d}");
            }
            Write.Display();
            Console.ReadKey();
            announcer.Clear();
            pitch = 0;
            AtBat(Return.Batter(), Return.Pitch());
            if (outs == 3) Announce("And with three outs, the inning is over");
            Write.Display();
            announcer.Clear();
            Console.ReadKey();
            ChangeBatter();           
        }
        top = false;
    }  

    private static void AtBat(Player batter, Pitcher pitcher)
    {
        if (!completedAtBat)
        {
            pitch++;
            pitcher.pitchCount[week]++;
            ThePitch(pitcher);
            TheDecision(batter);
            if (balls == 4)
            {
                Return.Batter().walkStat[Engine.week]++;
                Advance.Walk();
                Announce($"That's 4 balls. {Color.PLAYER + batter.name + Color.RESET} will take his base");
                completedAtBat = true;
            }
            else if (strikes == 3)
            {
                Return.Batter().strikeOutStat[Engine.week]++;
                outs++;
                Announce($"That's 3 strikes. {Color.PLAYER + batter.name + Color.RESET} is out");
                completedAtBat = true;

            }
            else AtBat(batter, pitcher);
        }
    }

    private static void ThePitch(Pitcher pitcher)
    {
        int locationRollX = Return.RandomInt(1, 6);
        int locationRollY = Return.RandomInt(1, 6);
        pitchLocationX = (locationRollX == 1 || locationRollX == 2) ? PitchLocationX.Inside : (locationRollX == 3 || locationRollX == 4) ? PitchLocationX.Outside : PitchLocationX.Center;
        pitchLocationY = (locationRollY== 1 || locationRollY == 2) ? PitchLocationY.High : (locationRollY == 3 || locationRollY == 4) ? PitchLocationY.Low : PitchLocationY.Center;
        meatball = (pitchLocationX == PitchLocationX.Center && pitchLocationY == PitchLocationY.Center) ? true : false;
        pitchSpeed = Return.RandomInt((pitcher.speed-20<=0)?0:pitcher.speed -20, pitcher.speed);
        pitchControl = Return.RandomInt(0, pitcher.control);
        pitchDeception = Return.RandomInt(0, pitcher.tricky);
        bool wild = (pitchControl < 5) ? true : false;
        if (wild)
        {
            balls++;
            Announce($"Pitch {pitch} - The pitch is wild!");
            Advance.AllRunners();
            pitch++;
        }
        else
        {
            if (pitchControl < 40 && meatball) pitchSpeed /= 2;
            else if (pitchControl < 40 && !meatball) pitchCall = PitchCall.Ball;
            else pitchCall = PitchCall.Strike;
            
        }
    }


    private static void TheDecision(Player batter)
    {
        int eye = (Return.RandomInt(0, batter.patience));
        //Success based on contact and random number
        int success = Return.RandomInt((batter.contact - 40 <= 0) ? 0 : batter.contact - 40, batter.contact);
        //Success afected by deception vs eye
        success -= (pitchDeception - eye)>0?pitchDeception-eye:0;
        //Success affected by speed of the ball
        if (pitchSpeed < 40) success += 40 - pitchSpeed;
        if (pitchSpeed > 80) success -= pitchSpeed - 70;
        string x = (pitchLocationX == PitchLocationX.Inside) ? "inside" : (pitchLocationX == PitchLocationX.Outside) ? "outside" : "";
        string high = (pitchCall == PitchCall.Ball) ? tooHighPitchPhrases[Return.RandomInt(0, tooHighPitchPhrases.Count)] : highPitchPhrases[Return.RandomInt(0, highPitchPhrases.Count)];
        string lowBall = (x != "") ? tooLowPitchPhrases[Return.RandomInt(0, tooLowPitchPhrases.Count)] : tooLowPitchPhrasesX[Return.RandomInt(0, tooLowPitchPhrasesX.Count)];
        string low = (pitchCall == PitchCall.Ball) ? lowBall : lowPitchPhrases[Return.RandomInt(0,lowPitchPhrases.Count)];
        string y = (pitchLocationY == PitchLocationY.High) ? high : (pitchLocationY == PitchLocationY.Low) ? low : "";
        
       
        
      
        string where = (y =="" && x =="")?"right down the middle":(y == "")?x:(x=="")?y: $"{y} and {x}";
        Announce($"Pitch {pitch} - The pitch is {where} and ");
        if (pitchCall == PitchCall.Ball)
        {
            success /= 2;            
            if(eye > pitchDeception)
            {
                Append($"{Color.PLAYER + batter.name + Color.RESET} manages to hold off and takes a ball");
                balls++;
            }
            else
            {                
                Append($"{Color.PLAYER + batter.name + Color.RESET} chases it ");
                TheSwing(batter,success,true);
            }
            
        }
        else
        {
            TheSwing(batter,success,false);            
        }
    }

    private static void TheSwing(Player batter,int success,bool chase)
    {
        if (success < 15)
        {
            SwingStrike(batter, chase);
        }
        else if (success < 30)
        {
            if (chase) SwingStrike(batter,chase);
            else LayStrike(batter);
        }
        else if (success < 45)
        {
            int roll = Return.RandomInt(0, 3);
            if (roll != 1)
            {
                Append($"{Color.PLAYER + batter.name + Color.RESET} makes contact but fouls away.");
                if (strikes != 2) strikes++;
            }
            else LayStrike(batter);
        }
        else
        {
            int hitQuality = 25;
            TheHit(batter, hitQuality);
        }
    
        
    }

    private static void LayStrike(Player batter)
    {
        Append($"{Color.PLAYER + batter.name + Color.RESET} decides not to swing. That's a strike");
        strikes++;
    }

    private static void SwingStrike(Player batter,bool chase)
    {
        if (!chase) Append($"{Color.PLAYER + batter.name + Color.RESET} swings but can't connect. That's a strike");
        else Append("but can't connect. That's a strike");
        strikes++;
    }

    private static void TheHit(Player batter, int hitQuality)
    {
        int direction = Return.RandomInt(0, 3);
        if (direction == 0) hitDirection = HitDirection.Left;
        else if (direction == 2) hitDirection = HitDirection.Right;
       //else if (direction == 1 && pitchLocationX == PitchLocation.Inside) hitDirection = HitDirection.Left;
       //else if (direction == 1 && pitchLocationX == PitchLocation.Outside) hitDirection = HitDirection.Right;
       //else hitDirection = HitDirection.Center;
       //if (pitchLocationX == PitchLocation.Low)
       //{
       //    int type = (Return.RandomInt(0, 7));
       //    if (type < 4) hitType = HitType.Grounder;
       //    else if (type == 4) hitType = HitType.LineDrive;
       //    else if (type == 5) hitType = HitType.Looper;
       //    else if (type == 6) hitType = HitType.Pop;
       //}
       //else if (pitchLocationX == PitchLocation.High)
       //{
       //    int type = (Return.RandomInt(0, 7));
       //    if (type < 4) hitType = HitType.Pop;
       //    else if (type == 4) hitType = HitType.LineDrive;
       //    else if (type == 5) hitType = HitType.Grounder;
       //    else if (type == 6) hitType = HitType.Pop;
       //}
        else
        {
            int type = (Return.RandomInt(0, 4));
            if (type == 0) hitType = HitType.Pop;
            else if (type == 1) hitType = HitType.LineDrive;
            else if (type == 2) hitType = HitType.Grounder;
            else if (type == 3) hitType = HitType.Pop;
        }
        int hitClean = (success / batter.contact * 8 / 10);
        hitPower = Return.RandomInt(batter.power / 2, batter.power) * hitClean;
        if (hitType == HitType.Pop && hitPower > 85 || hitType == HitType.Looper && hitPower > 85)
        {
            Return.Batter().homeRunStat[Engine.week]++;
            Append($"{Color.PLAYER + batter.name + Color.RESET} makes great contact and it is OUT OF HERE! HOMERUN!.");
            if (runnerOn[3] != null) Advance.ScoreFromThird();
            if (runnerOn[2] != null) Advance.ScoreFromSecond();
            if (runnerOn[1] != null) Advance.ScoreFromFirst();
            Advance.ScoreFromHome();
        }
        else
        {
            string typeHit = (hitType == HitType.Grounder) ? "hard grounder" : (hitType == HitType.LineDrive) ? "line drive" : (hitType == HitType.Pop) ? "pop fly" : "looper";
            string whereHit = (hitDirection == HitDirection.Center) ? "up the middle" : (hitDirection == HitDirection.Right) ? "down the right side" : "down the left side";
            Append($"{Color.PLAYER + batter.name + Color.SPEAK} hits a {typeHit} {whereHit}" + Color.RESET);
            WhereDoesTheBallGo();
        }
        completedAtBat = true;
    }

    private static void WhereDoesTheBallGo()
    {        
        //Where Does It go
        if (hitDirection == HitDirection.Left)
        {
            if (hitType == HitType.LineDrive || hitType == HitType.Grounder)
            {
                if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().third);
                else ThePlay(Return.InField().shortStop);
            }
            else if (hitType == HitType.Looper)
            {
                if (hitPower > 50) ThePlay(Return.InField().lf);
                else
                {
                    if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().third);
                    else ThePlay(Return.InField().shortStop);
                }
            }
            else if (hitType == HitType.Pop)
            {
                if (hitPower > 50) ThePlay(Return.InField().lf);
                else
                {
                    if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().third);
                    else ThePlay(Return.InField().shortStop);
                }
            }
        }
        else if (hitDirection == HitDirection.Center)
        {
            if (hitType == HitType.LineDrive || hitType == HitType.Grounder)
            {
                if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().second);
                else ThePlay(Return.InField().shortStop);
            }
            else if (hitType == HitType.Looper)
            {
                if (hitPower > 50) ThePlay(Return.InField().cf);
                else
                {
                    if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().second);
                    else ThePlay(Return.InField().shortStop);
                }
            }
            else if (hitType == HitType.Pop)
            {
                if (hitPower > 50) ThePlay(Return.InField().cf);
                else
                {
                    if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().second);
                    else ThePlay(Return.InField().shortStop);
                }
            }
        }
        else if (hitDirection == HitDirection.Right)
        {
            if (hitType == HitType.LineDrive || hitType == HitType.Grounder)
            {
                if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().first);
                else ThePlay(Return.InField().second);
            }
            else if (hitType == HitType.Looper)
            {
                if (hitPower > 50) ThePlay(Return.InField().rf);
                else
                {
                    if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().first);
                    else ThePlay(Return.InField().second);
                }
            }
            else if (hitType == HitType.Pop)
            {
                if (hitPower > 50) ThePlay(Return.InField().rf);
                else
                {
                    if (Return.RandomInt(0, 2) == 0) ThePlay(Return.InField().first);
                    else ThePlay(Return.InField().second);
                }
            }
        }
    }    

    private static void ThePlay(Player fielder)
    {
        int field = Return.RandomInt(0, fielder.fielding * fielder.positionSkill / 100);
        Announce(Color.SPEAK + $"The ball is hit to {Color.POSITION + fielder.name+ Color.SPEAK} who attempts to field it"+ Color.RESET);
        if (field < 7)
        {
            Announce(Color.POSITION + fielder.name + Color.RESET + " makes an ERROR!");
            Advance.AllRunners2();
            Advance.ToSecond();
        }
        else
        {
            int hitTypeNumber = (hitType == HitType.Pop) ? 20 + Return.Batter().running / 8 : (hitType == HitType.LineDrive) ? 30 + Return.Batter().running / 8 : (hitType == HitType.Looper) ? 40 : 25;
            if (field - Return.Batter().running / 8 < hitTypeNumber)
            {
                if (fielder == Return.InField().cf || fielder == Return.InField().rf || fielder == Return.InField().lf)
                {
                    int baseNumber = Return.RandomInt(0, 5);
                    if (baseNumber == 0)
                    {
                        Announce(Color.POSITION + fielder.name + Color.SPEAK + " makes a clean play and keeps it at a single"+ Color.RESET);
                        Advance.Single();
                    }
                    else if (baseNumber == 5)
                    {
                        Announce(Color.SPEAK + "The ball bounces past " + Color.POSITION + fielder.name + Color.SPEAK + " and rolls to the wall. " + Color.PLAYER + Return.Batter().name + Color.SPEAK + " makes it to third for a triple"+ Color.RESET);
                        Advance.Triple();
                    }
                    else
                    {
                        Announce(Color.POSITION + fielder.name + Color.SPEAK + " fields the ball and throws it to the cutoff, holding " + Color.PLAYER + Return.Batter().name + Color.SPEAK + " to a double"+Color.RESET);
                        Advance.Double();
                    }
                }
                else
                {
                    Announce(Color.PLAYER + Return.Batter().name + Color.SPEAK + " beats the throw for a single"+ Color.RESET);
                    Advance.Single();
                }
            }
            else
            {
                if(hitType == HitType.Grounder && outs<2 && runnerOn[1] != null)
                {
                    Announce(Color.POSITION + fielder.name + Color.SPEAK + " makes a DOUBLE PLAY!"+ Color.RESET);
                    runnerOn[1] = null;
                    outs +=2;
                }
                else
                {
                    Announce(Color.POSITION + fielder.name + Color.SPEAK + " fields the ball cleanly for an out"+ Color.RESET);
                    outs++;
                }
            }
        }
    }

    private static void Append(string x)
    {
        announcer[announcer.Count - 1] += x;
    }

    private static void Announce(string x)
    {
        announcer.Add(x);
    }

    private static void ChangeBatter()
    {
        for (int i = 0; i < 9; i++)
        {
            if(Return.AtBat().battingOrder[i].batting == Batting.AtBat)
            {
                Return.AtBat().battingOrder[i].batting = Batting.None;
                if (i == 6) 
                {
                    Return.AtBat().battingOrder[i + 1].batting = Batting.AtBat;
                    Return.AtBat().battingOrder[i + 2].batting = Batting.OnDeck;
                    Return.AtBat().battingOrder[0].batting = Batting.InTheHole;
                }
                else if (i == 7)
                {
                    Return.AtBat().battingOrder[i + 1].batting = Batting.AtBat;
                    Return.AtBat().battingOrder[0].batting = Batting.OnDeck;
                    Return.AtBat().battingOrder[1].batting = Batting.InTheHole;
                }
                else if (i == 8)
                {
                    Return.AtBat().battingOrder[0].batting = Batting.AtBat;
                    Return.AtBat().battingOrder[1].batting = Batting.OnDeck;
                    Return.AtBat().battingOrder[2].batting = Batting.InTheHole;
                }
                else
                {
                    Return.AtBat().battingOrder[i + 1].batting = Batting.AtBat;
                    Return.AtBat().battingOrder[i + 2].batting = Batting.OnDeck;
                    Return.AtBat().battingOrder[i + 3].batting = Batting.InTheHole;
                }
                break;
            }            
        }
    }
}