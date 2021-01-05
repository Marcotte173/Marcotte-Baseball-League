using System;
using System.Collections.Generic;
using System.Text;

public enum PitchLocation {Meatball,Inside, Outside,High, Low };
public enum PitchCall {Ball,Strike }
public enum HitType {Grounder,LineDrive,Looper,Pop }
public enum HitDirection {Left,Right,Center }

public class Engine
{
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
    public static PitchLocation pitchLocation;
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
            balls = 0;
            strikes = 0;
            AtBat(Return.Batter(), Return.Pitch());
            if (outs == 3) announcer.Add("And with three outs, the inning is over");
            Write.Display();
            announcer.Clear();
            Console.ReadKey();
            ChangeBatter();
        }        
        top = false;
    }  

    private static void AtBat(Player batter, Pitcher pitcher)
    {
        pitch++;
        bool hit = false;
        pitcher.pitchCount[week]++;
        int locationRoll = Return.RandomInt(0, 13);
        pitchLocation = (locationRoll == 1 || locationRoll == 2 || locationRoll == 3) ? PitchLocation.High : (locationRoll == 4 || locationRoll == 5 || locationRoll == 6) ? PitchLocation.Low : (locationRoll == 7 || locationRoll == 8 || locationRoll == 9) ? PitchLocation.Inside : (locationRoll == 10 || locationRoll == 11 || locationRoll == 12) ? PitchLocation.Outside : PitchLocation.Meatball;
        pitchSpeed = Return.RandomInt(pitcher.speed - 15, pitcher.speed);
        pitchControl = Return.RandomInt(0, pitcher.control);
        pitchDeception = Return.RandomInt(0, pitcher.tricky);
        bool wild = (pitchControl < 5) ? true : false;
        if (wild)
        {
            balls++;
            announcer.Add($"Pitch {pitch} - The pitch is wild!");
            Advance.AllRunners();
            pitch++;
        }
        else
        {
            if (pitchControl < 40 && pitchLocation == PitchLocation.Meatball) pitchSpeed /= 2;
            else if (pitchControl < 40 && pitchLocation!= PitchLocation.Meatball) pitchCall = PitchCall.Ball;
            else pitchCall = PitchCall.Strike;
        }
        int eye = (Return.RandomInt(0, batter.patience));
        int success = Return.RandomInt(0, batter.contact);
        if (pitchCall == PitchCall.Ball)
        {
            string where = (pitchLocation == PitchLocation.High) ? "high" : (pitchLocation == PitchLocation.Low) ? "low" : (pitchLocation == PitchLocation.Outside) ? "outside" : (pitchLocation == PitchLocation.Inside) ? "inside" : "";
            if (eye > pitchDeception)
            {                
                announcer.Add($"Pitch {pitch} - The pitch is {where}. {Color.PLAYER+ batter.name+Color.RESET} manages to hold off and takes a ball");
                balls++;
            }
            else
            {
                announcer.Add($"Pitch {pitch} - The pitch is {where}. {Color.PLAYER + batter.name + Color.RESET} chases it for a strike");
                strikes++;
            }
        }
        else
        {
            if (pitchLocation == PitchLocation.Meatball || strikes == 2)
            {                
                if (success < 20 )
                {
                    announcer.Add($"Pitch {pitch} - {Color.PLAYER + batter.name + Color.RESET} swings but can't connect. That's a strike");
                    strikes++;
                }
                else if (success < 40)
                {
                    announcer.Add($"Pitch {pitch} - {Color.PLAYER + batter.name + Color.RESET} makes contact but fouls away.");
                    if (strikes !=2) strikes++;
                }
                else hit = true;
            }
            else
            {
                if (eye > 70)
                {
                    announcer.Add($"Pitch {pitch} - {Color.PLAYER + batter.name + Color.RESET} doesn't like the pitch, and takes it for a strike");
                    strikes++;
                }
                else
                {
                    success = Return.RandomInt(0, batter.contact);
                    if (success < 20 || success < (pitchDeception/2 + pitchSpeed/2))
                    {
                        announcer.Add($"Pitch {pitch} - {Color.PLAYER + batter.name + Color.RESET} swings but can't connect. That's a strike");
                        strikes++;
                    }
                    else if (success < 40)
                    {
                        announcer.Add($"Pitch {pitch} - {Color.PLAYER + batter.name + Color.RESET} makes contact but fouls away.");
                        if (strikes != 2) strikes++;
                    }
                    else hit = true;
                }
            }
        }
        if (hit)
        {            
            int direction = Return.RandomInt(0, 3);
            if (direction == 0) hitDirection = HitDirection.Left;
            else if(direction == 2) hitDirection = HitDirection.Right;
            else if(direction == 1 && pitchLocation == PitchLocation.Inside) hitDirection = HitDirection.Left;
            else if (direction == 1 && pitchLocation == PitchLocation.Outside) hitDirection = HitDirection.Right;
            else hitDirection = HitDirection.Center;
            if(pitchLocation == PitchLocation.Low)
            {
                int type = (Return.RandomInt(0, 7));
                if (type < 4) hitType = HitType.Grounder;
                else if (type == 4) hitType = HitType.LineDrive;
                else if (type == 5) hitType = HitType.Looper;
                else if (type == 6) hitType = HitType.Pop;
            }
            else if (pitchLocation == PitchLocation.High)
            {
                int type = (Return.RandomInt(0, 7));
                if (type < 4) hitType = HitType.Pop;
                else if (type == 4) hitType = HitType.LineDrive;
                else if (type == 5) hitType = HitType.Grounder;
                else if (type == 6) hitType = HitType.Pop;
            }
            else
            {
                int type = (Return.RandomInt(0, 4));
                if (type == 0) hitType = HitType.Pop;
                else if (type == 1) hitType = HitType.LineDrive;
                else if (type == 2) hitType = HitType.Grounder;
                else if (type == 3) hitType = HitType.Pop;
            }
            int hitClean = (success / batter.contact * 8 / 10);
            hitPower = Return.RandomInt(batter.power/2, batter.power) * hitClean;
            if(hitType == HitType.Pop && hitPower > 85 || hitType == HitType.Looper && hitPower > 85)
            {
                Return.Batter().homeRunStat[Engine.week]++;
                announcer.Add($"Pitch {pitch} - {Color.PLAYER + batter.name + Color.RESET} makes great contact and it is OUT OF HERE! HOMERUN!.");
                if (runnerOn[3] != null) Advance.ScoreFromThird();
                if (runnerOn[2] != null) Advance.ScoreFromSecond();
                if (runnerOn[1] != null) Advance.ScoreFromFirst();
                Advance.ScoreFromHome();
            }
            else
            {
                string typeHit = (hitType == HitType.Grounder) ? "hard grounder" : (hitType == HitType.LineDrive) ? "line drive" : (hitType == HitType.Pop) ? "pop fly" : "looper";
                string whereHit = (hitDirection == HitDirection.Center) ? "up the middle" : (hitDirection == HitDirection.Right) ? "down the right side" : "down the left side";
                announcer.Add(Color.SPEAK + $"Pitch {pitch} - {Color.PLAYER + batter.name + Color.SPEAK} hits a {typeHit} {whereHit}"+ Color.RESET);
                //FIELDING THE BALL
                if (hitDirection == HitDirection.Left)
                {
                    if (hitType == HitType.LineDrive || hitType == HitType.Grounder)
                    {
                        if (Return.RandomInt(0, 2) == 0) Field(Return.InField().third);
                        else Field(Return.InField().shortStop);
                    }
                    else if (hitType == HitType.Looper)
                    {
                        if (hitPower > 50) Field(Return.InField().lf);
                        else
                        {
                            if (Return.RandomInt(0, 2) == 0) Field(Return.InField().third);
                            else Field(Return.InField().shortStop);
                        }
                    }
                    else if (hitType == HitType.Pop)
                    {
                        if (hitPower > 50) Field(Return.InField().lf);
                        else
                        {
                            if (Return.RandomInt(0, 2) == 0) Field(Return.InField().third);
                            else Field(Return.InField().shortStop);
                        }
                    }
                }
                else if (hitDirection == HitDirection.Center)
                {
                    if (hitType == HitType.LineDrive || hitType == HitType.Grounder)
                    {
                        if (Return.RandomInt(0, 2) == 0) Field(Return.InField().second);
                        else Field(Return.InField().shortStop);
                    }
                    else if (hitType == HitType.Looper)
                    {
                        if (hitPower > 50) Field(Return.InField().cf);
                        else
                        {
                            if (Return.RandomInt(0, 2) == 0) Field(Return.InField().second);
                            else Field(Return.InField().shortStop);
                        }
                    }
                    else if (hitType == HitType.Pop)
                    {
                        if (hitPower > 50) Field(Return.InField().cf);
                        else
                        {
                            if (Return.RandomInt(0, 2) == 0) Field(Return.InField().second);
                            else Field(Return.InField().shortStop);
                        }
                    }
                }
                else if (hitDirection == HitDirection.Right)
                {
                    if (hitType == HitType.LineDrive || hitType == HitType.Grounder)
                    {
                        if (Return.RandomInt(0, 2) == 0) Field(Return.InField().first);
                        else Field(Return.InField().second);
                    }
                    else if (hitType == HitType.Looper)
                    {
                        if (hitPower > 50) Field(Return.InField().rf);
                        else
                        {
                            if (Return.RandomInt(0, 2) == 0) Field(Return.InField().first);
                            else Field(Return.InField().second);
                        }
                    }
                    else if (hitType == HitType.Pop)
                    {
                        if (hitPower > 50) Field(Return.InField().rf);
                        else
                        {
                            if (Return.RandomInt(0, 2) == 0) Field(Return.InField().first);
                            else Field(Return.InField().second);
                        }
                    }
                }
            }
        }
        else if (balls == 4)
        {
            Return.Batter().walkStat[Engine.week]++;
            Advance.Walk();
            announcer.Add($"That's 4 balls. {Color.PLAYER + batter.name + Color.RESET} will take his base");
        }
        else if (strikes == 3)
        {
            Return.Batter().strikeOutStat[Engine.week]++;
            outs++;
            announcer.Add($"That's 3 strikes. {Color.PLAYER + batter.name + Color.RESET} is out");
        }
        else AtBat(batter,pitcher);
    }

    private static void Field(Player fielder)
    {
        int field = Return.RandomInt(0, fielder.fielding * fielder.positionSkill / 100);
        announcer.Add(Color.SPEAK + $"The ball is hit to {Color.POSITION + fielder.name+ Color.SPEAK} who attempts to field it"+ Color.RESET);
        if (field < 7)
        {
            announcer.Add(Color.POSITION + fielder.name + Color.RESET + " makes an ERROR!");
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
                        announcer.Add(Color.POSITION + fielder.name + Color.SPEAK + " makes a clean play and keeps it at a single"+ Color.RESET);
                        Advance.Single();
                    }
                    else if (baseNumber == 5)
                    {
                        announcer.Add(Color.SPEAK + "The ball bounces past " + Color.POSITION + fielder.name + Color.SPEAK + " and rolls to the wall. " + Color.PLAYER + Return.Batter().name + Color.SPEAK + " makes it to third for a triple"+ Color.RESET);
                        Advance.Triple();
                    }
                    else
                    {
                        announcer.Add(Color.POSITION + fielder.name + Color.SPEAK + " fields the ball and throws it to the cutoff, holding " + Color.PLAYER + Return.Batter().name + Color.SPEAK + " to a double"+Color.RESET);
                        Advance.Double();
                    }
                }
                else
                {
                    announcer.Add(Color.PLAYER + Return.Batter().name + Color.SPEAK + " beats the throw for a single"+ Color.RESET);
                    Advance.Single();
                }
            }
            else
            {
                if(hitType == HitType.Grounder && outs<2 && runnerOn[1] != null)
                {
                    announcer.Add(Color.POSITION + fielder.name + Color.SPEAK + " makes a DOUBLE PLAY!"+ Color.RESET);
                    runnerOn[1] = null;
                    outs +=2;
                }
                else
                {
                    announcer.Add(Color.POSITION + fielder.name + Color.SPEAK + " fields the ball cleanly for an out"+ Color.RESET);
                    outs++;
                }
            }
        }
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