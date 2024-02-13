// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Threading.Channels;
using System.Xml.Linq;

//game set up

List<Herb> herbs = new List<Herb>();
Player me = null;
int goes = 0;

//# title screen  

Console.WriteLine(
    @"
  _    _ ______ _____  ____                                                                                
 | |  | |  ____|  __ \|  _ \                                                                               
 | |__| | |__  | |__) | |_) |                                                                              
 |  __  |  __| |  _  /|  _ <                                                                               
 | |  | | |____| | \ \| |_) |                                                                              
 |_|__|_|______|_|__\_\____/__  ______ _   _ ______ ______ _____    ___                                    
  / ____|   /\   |  __ \|  __ \|  ____| \ | |  ____|  ____|  __ \  |__ \                                   
 | |  __   /  \  | |__) | |  | | |__  |  \| | |__  | |__  | |__) |    ) |                                  
 | | |_ | / /\ \ |  _  /| |  | |  __| | . ` |  __| |  __| |  _  /    / /                                   
 | |__| |/ ____ \| | \ \| |__| | |____| |\  | |____| |____| | \ \   / /_                                   
  \_____/_/    \_\_|__\_\_____/|______|_|_\_|______|______|_|_ \_\ |____|____          _      ____   ____  
 |  ____| |    |  ____/ ____|__   __|  __ \|_   _/ ____| |  _ \| |  | |/ ____|   /\   | |    / __ \ / __ \ 
 | |__  | |    | |__ | |       | |  | |__) | | || |      | |_) | |  | | |  __   /  \  | |   | |  | | |  | |
 |  __| | |    |  __|| |       | |  |  _  /  | || |      |  _ <| |  | | | |_ | / /\ \ | |   | |  | | |  | |
 | |____| |____| |___| |____   | |  | | \ \ _| || |____  | |_) | |__| | |__| |/ ____ \| |___| |__| | |__| |
 |______|______|______\_____|  |_|  |_|  \_\_____\_____| |____/ \____/ \_____/_/    \_\______\____/ \____/ 
                                                                                                           
                                                                                                           
                                                                         
"
);

Console.WriteLine("what is your name?");
string namez = Console.ReadLine();

Console.WriteLine("what kind of herb would you like?");

string herbName1 = Console.ReadLine();
herbs.Add(Herb.Create(herbName1));

Console.WriteLine("what 2nd herb herb would you like?");

string herbName2 = Console.ReadLine();
herbs.Add(Herb.Create(herbName2));

me = Player.Create(herbs, namez);

Console.WriteLine(namez);

Console.WriteLine("in this game you can water you plants, pick the leaves, and swap which one your currently looking at!");
Console.WriteLine("just type 'water', 'pick' or 'swap'");
Console.WriteLine("have fun");


//#made the game 100 turn long for now

while (goes <= 100)
{
    me.Choice();
    goes += 1;
}

public class Herb
{
    public virtual string Name { set; get; }
    public virtual bool IsWet { set; get; }
    public virtual bool IsAlive { set; get; }
    public virtual int Leaves { set; get; }
    public virtual int WaterdTimer { set; get; }

    public Herb(string name, bool isWet, bool isAlive, int leaves)
    {
        Name = name;
        IsWet = isWet;
        IsAlive = isAlive;
        Leaves = leaves;
        WaterdTimer = 0;
    }

    //#plant has a type, knows if is wet or not, can die and has leaves.
    public static Herb Create(string name = "basil", bool isWet = false, bool isAlive = true, int leaves = 1)
    {
        return new Herb(name, isWet, isAlive, leaves);
    }

    public void SetIsWet(bool state)
    {
        IsWet = state;
    }

    public string Get()
    {
        string description = $"This herb is a {Name}, it has {Leaves} leaves";
        
        if (IsWet is false)
        {
            description += ", is dry";
        }
        else
        {
            description += ", is wet";
        }

        if (IsAlive is false)
        {
            description += " and is dead";
        }
        else
        {
            description += " and is alive";
        }

        return description;
    }

    //# methods will be to grow leaves when watered, to have leaves picked, and to die. maybe it can flower which gives you a new plant. 
    public int Grow()
    {
        if (IsAlive is false)
        {
            Console.WriteLine("this plant is dead");

        }
        else if (IsWet)
        {
            Leaves += 1;
            WaterdTimer += 1;
            IsWet = false;
        }

        return Leaves;
    }

    public void Die()
    {
        if (IsAlive)
        {
            IsAlive = false;
        }
    }

    public void OverWater()
    {
        if (WaterdTimer >= 3 && WaterdTimer <= 4 )
        {
            Console.WriteLine("dont just water this plant or it will die, try doing something else!");

        }
        else if (WaterdTimer == 5)
        {
            Die();
            Console.WriteLine("you over waterd this plant and it had died");
        }
    }
}

//# methods will be to water the plant, and to pick the leaves. maybe to get new plants too.

public class Player
{
    public virtual IEnumerable<Herb> Plants { set; get; }
    public virtual string Name { set; get; }
    public virtual int YouLeaves { set; get; }
    public virtual int CurrentPlant{ set; get; }


    //player will have a name, a list of plants, and an amount of leaves.
    public Player(IEnumerable<Herb> plants, string name = "Tamzin", int youLeaves = 0)
    {
        Plants = plants;
        Name = name;
        YouLeaves = youLeaves;
        CurrentPlant = 0;
    }

    public static Player Create(IEnumerable<Herb> plants, string name = "Tamzin", int leaves = 0)
    {
        Player player =  new Player(plants, name, leaves);
        return player;
    }


    public void Choice()
    {
        Console.WriteLine("what you you like to do?");
        string go = Console.ReadLine();

        if (go == "water")
        {
            Water();
        }
        else if (go == "pick")
        {
            Pick();
        }
        else if (go == "swap")
        {
            Swap();
        }
        else
        {
            Console.WriteLine("problem");
        }
    }

    public string Print()
    {
        string result = $"Hi {Name}, you have a:";

        foreach (Herb herb in Plants)
        {
            result += herb.Name + " ";
        }

        result += $"and a total of {Plants} leaves";
        return result;
    }

    public void Water()
    {
        Herb herb = Plants.ElementAt(CurrentPlant);

        if (herb.IsAlive is false)
        {
            Console.WriteLine("this plant is dead.");
        }
        else if (herb.IsWet)
        {
            Console.WriteLine("this plant is already wet");
        }
        else
        {
            herb.IsWet = true;
            herb.OverWater();
            herb.Grow();

            if (herb.IsAlive)
            {
                Console.WriteLine($"you waterd this plant, it now has {herb.Leaves} leaves");
            }
        }
    }

    public void Swap()
    {
        CurrentPlant += 1;


        if (CurrentPlant > Plants.Count())
        {
            CurrentPlant = 0;
        }

        Herb herb = Plants.ElementAt(CurrentPlant);

        Console.WriteLine("you are now looking at your " + herb.Get());
    }


    public void Pick()
    {
        Herb herb = Plants.ElementAt(CurrentPlant);

        if (herb.Leaves == 0)
        {
            herb.Die();
            Console.WriteLine("oh bugger you picked all the leaves and killed it");
        }
        else if (herb.Leaves >=1 && herb.IsAlive)
        {
            herb.Leaves -=1;
            herb.WaterdTimer += 2;
            YouLeaves += 1;
            Console.WriteLine($"you picked a leaf, you now have {YouLeaves}");
            
            if (herb.Leaves == 1)
            {
                Console.WriteLine("careful only 1 leaf left");
            }
        }
        else
        {
            Console.WriteLine("this plant is dead");
        }
    }
}
