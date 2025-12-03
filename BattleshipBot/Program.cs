
using BattleshipBot;
using System.Xml;

string grid = "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************" +
    "************";
string cell = "X";
bool result = true;
bool avengerAvailable = true;
int mapId = 0;
int mapCount = 2;
int moveCount = 10;
bool finished = false;

int row = 0;
int col = 0;

Random rand = new Random();

//Sunken ships
bool patrolBoatSunk = false;
bool submarineSunk = false;
bool destroyerSunk = false;
bool battleshipSunk = false;
bool carrierSunk = false;
bool helicarrierSunk = false;

//Coordinations
List<Coordination> coordinationList = new List<Coordination>();
for (int i = 0; i < 12; i++)
{
    for (int j = 0; j < 12; j++)
    {
        coordinationList.Add(new Coordination(i, j));
    }
}

while (grid.Contains("X"))
{
    int x = grid.IndexOf("X");
    PatrolBoatSunk(x);
    SubmarineDestroyerSunk(x);
    SubmarineDestroyerSunk(x);
    BattleshipSunk(x);
    CarrierSunk(x);
    HelicarrierSunk(x);
    if (grid[x].Equals(char.Parse("X")))
    {
        grid = grid.Remove(x, 1);
        grid = grid.Insert(x, "S");
    }
}

//probability numbers
int[] probArray = new int[144];
for (int i = 0; i < probArray.Length; i++)
{
    probArray[i] = 0;
}

if (!patrolBoatSunk)
{
    PatrolBoat(probArray);
}
if (!submarineSunk)
{
    SubmarineDestroyer(probArray);
}
if (!destroyerSunk)
{
    SubmarineDestroyer(probArray);
}
if (!battleshipSunk)
{
    Battleship(probArray);
}
if (!carrierSunk)
{
    Carrier(probArray);
}
if (!helicarrierSunk)
{
    Helicarrier(probArray);
}

if (grid.Contains("S"))
{
    int s = grid.IndexOf("S");
    NextShot(s);
}
else
{
    //shooting by probability
    List<int> shootArray = new List<int>();
    for (int i = 0; i < probArray.Length; i++)
    {
        if (probArray[i] == probArray.Max())
        {
            shootArray.Add(i);
        }
    }
    Shot(shootArray[rand.Next(shootArray.Count())]);
}

//display of probability
int n = 0;
for (int i = 0; i < 12; i++)
{
	for (int j = 0; j < 12; j++)
	{
        if (probArray[n] < 10)
        {
            Console.Write(probArray[n] + "  ");
        }
        else
        {
            Console.Write(probArray[n] + " ");
        }
        n++;
    }
    Console.WriteLine();
}

//display of map
Console.WriteLine();

int n1 = 0;
for (int i = 0; i < 12; i++)
{
    for (int j = 0; j < 12; j++)
    {
        Console.Write(grid[n1] + " ");
        n1++;
    }
    Console.WriteLine();
}

//Rows
int Rows(int i)
{
    if (i < 12)
    {
        n = 1;
    }
    else if (i < 24)
    {
        n = 2;
    }
    else if (i < 36)
    {
        n = 3;
    }
    else if (i < 48)
    {
        n = 4;
    }
    else if (i < 60)
    {
        n = 5;
    }
    else if (i < 72)
    {
        n = 6;
    }
    else if (i < 84)
    {
        n = 7;
    }
    else if (i < 96)
    {
        n = 8;
    }
    else if (i < 108)
    {
        n = 9;
    }
    else if (i < 120)
    {
        n = 10;
    }
    else if (i < 132)
    {
        n = 11;
    }
    else if (i < 144)
    {
        n = 12;
    }
    else
    {
        n = 0;
    }
    return n;
}

//So anyway, I started blasting
void Shot(int x)
{
    col = coordinationList[x].Col;
    row = coordinationList[x].Row;
    Console.WriteLine(coordinationList[x].Col + ", " + coordinationList[x].Row);
}

void NextShot(int s)
{

    if (s == 0)
    {
        if (grid[s + 1].Equals(char.Parse("S")))
        {
            while (grid[s + 1].Equals(char.Parse("S")))
            {
                s++;
            }
            Shot(s + 1);
        }
        else if (grid[s + 12].Equals(char.Parse("S")))
        {
            while (grid[s + 12].Equals(char.Parse("S")))
            {
                s = s + 12;
            }
            Shot(s + 12);
        }
        else if (grid[s + 1].Equals(char.Parse(".")))
        {
            Shot(s + 12);
        }
        else
        {
            Shot(s + 1);
        }
    }
    else if (s == 11)
    {
        if (grid[s + 12].Equals(char.Parse("S")))
        {
            while (grid[s + 12].Equals(char.Parse("S")))
            {
                s = s + 12;
            }
            Shot(s + 12);
        }
        else if (grid[s - 1].Equals(char.Parse(".")))
        {
            Shot(s + 12);
        }
        else
        {
            Shot(s - 1);
        }
    }
    else if (s == 132)
    {
        if (grid[s + 1].Equals(char.Parse("S")))
        {
            while (grid[s + 1].Equals(char.Parse("S")))
            {
                s++;
            }
            Shot(s + 1);
        }
        else if (grid[s + 1].Equals(char.Parse(".")))
        {
            Shot(s - 12);
        }
        else
        {
            Shot(s + 1);
        }
    }
    else if (s == 143)
    {
        if (grid[s - 1].Equals(char.Parse(".")))
        {
            Shot(s - 12);
        }
        else
        {
            Shot(s - 1);
        }
    }
    else if (s > 0 && s < 11)
    {
        if (grid[s - 1].Equals(char.Parse("*")))
        {
            Shot(s - 1);
        }
        else if (grid[s + 1].Equals(char.Parse("S"))
            || grid[s + 12].Equals(char.Parse(".")))
        {
            while (grid[s + 1].Equals(char.Parse("S")))
            {
                s++;
            }
            Shot(s + 1);
        }
        else if (grid[s + 12].Equals(char.Parse("S")))
        {
            if (grid[s + 12].Equals(char.Parse("S"))
                && grid[s + 24].Equals(char.Parse("S"))
                && grid[s + 36].Equals(char.Parse("S"))
                && grid[s + 48].Equals(char.Parse("S")))
            {
                if (grid[s + 11].Equals(char.Parse("*")))
                {
                    Shot(s + 11);
                }
                else if (grid[s + 13].Equals(char.Parse("*")))
                {
                    Shot(s + 13);
                }
                else if (grid[s + 35].Equals(char.Parse("*")))
                {
                    Shot(s + 35);
                }
                else
                {
                    Shot(s + 37);
                }
            }
            else
            {
                while (grid[s + 12].Equals(char.Parse("S")))
                {
                    s = s + 12;
                }
                Shot(s + 12);
            }
        }
        else
        {
            Shot(s + 12);
        }
    }
    else if (s > 132 && s < 143)
    {
        if (grid[s - 1].Equals(char.Parse("*")))
        {
            Shot(s - 1);
        }
        else if (grid[s + 1].Equals(char.Parse("S"))
            || grid[s - 12].Equals(char.Parse(".")))
        {
            while (grid[s + 1].Equals(char.Parse("S")))
            {
                s++;
            }
            Shot(s + 1);
        }
        else
        {
            Shot(s - 12);
        }
    }
    else if (s == 12 + 12 * (Rows(s) - 2))
    {
        if (grid[s - 12].Equals(char.Parse("*")))
        {
            Shot(s - 12);
        }
        else if (grid[s + 12].Equals(char.Parse("S"))
            || grid[s + 1].Equals(char.Parse(".")))
        {
            while (grid[s + 12].Equals(char.Parse("S")))
            {
                s = s + 12;
            }
            Shot(s + 12);
        }
        else if (grid[s + 1].Equals(char.Parse("S")))
        {
            if (grid[s + 1].Equals(char.Parse("S"))
                && grid[s + 2].Equals(char.Parse("S"))
                && grid[s + 3].Equals(char.Parse("S"))
                && grid[s + 4].Equals(char.Parse("S")))
            {
                if (grid[s + 13].Equals(char.Parse("*")))
                {
                    Shot(s + 13);
                }
                else if (grid[s - 11].Equals(char.Parse("*")))
                {
                    Shot(s - 11);
                }
                else if (grid[s + 15].Equals(char.Parse("*")))
                {
                    Shot(s + 15);
                }
                else
                {
                    Shot(s - 9);
                }
            }
            else
            {
                while (grid[s + 1].Equals(char.Parse("S")))
                {
                    s++;
                }
                Shot(s + 1);
            }
        }
        else
        {
            Shot(s + 1);
        }
    }
    else if (s == 23 + 12 * (Rows(s) - 2))
    {
        if (grid[s - 12].Equals(char.Parse("*")))
        {
            Shot(s - 12);
        }
        else if (grid[s + 12].Equals(char.Parse("S"))
            || grid[s - 1].Equals(char.Parse(".")))
        {
            while (grid[s + 12].Equals(char.Parse("S")))
            {
                s = s + 12;
            }
            Shot(s + 12);
        }
        else
        {
            Shot(s - 1);
        }
    }
    else
    {
        if (grid[s - 12].Equals(char.Parse("*")))
        {
            Shot(s - 12);
        }
        else if (grid[s - 1].Equals(char.Parse("*")))
        {
            Shot(s - 1);
        }
        else if (grid[s + 1].Equals(char.Parse("S")))
        {
            if (grid[s + 1].Equals(char.Parse("S"))
                && grid[s + 2].Equals(char.Parse("S"))
                && grid[s + 3].Equals(char.Parse("S"))
                && grid[s + 4].Equals(char.Parse("S")))
            {
                if (grid[s + 13].Equals(char.Parse("*")))
                {
                    Shot(s + 13);
                }
                else if (grid[s - 11].Equals(char.Parse("*")))
                {
                    Shot(s - 11);
                }
                else if (grid[s + 15].Equals(char.Parse("*")))
                {
                    Shot(s + 15);
                }
                else
                {
                    Shot(s - 9);
                }
            }
            else
            {
                while (grid[s + 1].Equals(char.Parse("S")))
                {
                    s++;
                }
                Shot(s + 1);
            }
        }
        else if (grid[s + 12].Equals(char.Parse("S")))
        {
            if (grid[s + 12].Equals(char.Parse("S"))
                && grid[s + 24].Equals(char.Parse("S"))
                && grid[s + 36].Equals(char.Parse("S"))
                && grid[s + 48].Equals(char.Parse("S")))
            {
                if (grid[s + 11].Equals(char.Parse("*")))
                {
                    Shot(s + 11);
                }
                else if (grid[s + 13].Equals(char.Parse("*")))
                {
                    Shot(s + 13);
                }
                else if (grid[s + 35].Equals(char.Parse("*")))
                {
                    Shot(s + 35);
                }
                else
                {
                    Shot(s + 37);
                }
            }
            else
            {
                while (grid[s + 12].Equals(char.Parse("S")))
                {
                    s = s + 12;
                }
                Shot(s + 12);
            }
        }
        else 
        { 
            Shot(s + 1); 
        }
    }
}

//probability
int[] PatrolBoat(int[] probArray)
{
    for (int i = 0; i < grid.Length; i++)
	{
        int j = 11 + 12 * (Rows(i) - 1);
        //horizontal
        if (i != j)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 1].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 1]++;
            }
        }
        //vertical
        if (i < 132)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 12].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 12]++;
            }
        }
    }
	return probArray;
}

int[] SubmarineDestroyer(int[] probArray)
{
    for (int i = 0; i < grid.Length; i++)
    {
        int j = 10 + 12 * (Rows(i) - 1);
        //horizontal
        if (i < j)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 1].Equals(char.Parse("*"))
                && grid[i + 2].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 1]++;
                probArray[i + 2]++;
            }
        }
        //vertical
        if (i < 120)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 12].Equals(char.Parse("*"))
                && grid[i + 24].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 12]++;
                probArray[i + 24]++;
            }
        }
    }
    return probArray;
}

int[] Battleship(int[] probArray)
{
    for (int i = 0; i < grid.Length; i++)
    {
        int j = 9 + 12 * (Rows(i) - 1);
        //horizontal
        if (i < j)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 1].Equals(char.Parse("*"))
                && grid[i + 2].Equals(char.Parse("*"))
                && grid[i + 3].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 1]++;
                probArray[i + 2]++;
                probArray[i + 3]++;
            }
        }
        //vertical
        if (i < 108)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 12].Equals(char.Parse("*"))
                && grid[i + 24].Equals(char.Parse("*"))
                && grid[i + 36].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 12]++;
                probArray[i + 24]++;
                probArray[i + 36]++;
            }
        }
    }
    return probArray;
}

int[] Carrier(int[] probArray)
{
    for (int i = 0; i < grid.Length; i++)
    {
        int j = 8 + 12 * (Rows(i) - 1);
        //horizontal
        if (i < j)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 1].Equals(char.Parse("*"))
                && grid[i + 2].Equals(char.Parse("*"))
                && grid[i + 3].Equals(char.Parse("*"))
                && grid[i + 4].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 1]++;
                probArray[i + 2]++;
                probArray[i + 3]++;
                probArray[i + 4]++;
            }
        }
        //vertical
        if (i < 96)
        {
            if (grid[i].Equals(char.Parse("*"))
                && grid[i + 12].Equals(char.Parse("*"))
                && grid[i + 24].Equals(char.Parse("*"))
                && grid[i + 36].Equals(char.Parse("*"))
                && grid[i + 48].Equals(char.Parse("*")))
            {
                probArray[i]++;
                probArray[i + 12]++;
                probArray[i + 24]++;
                probArray[i + 36]++;
                probArray[i + 48]++;
            }
        }
    }
    return probArray;
}

int[] Helicarrier(int[] probArray)
{
    for (int i = 0; i < grid.Length; i++)
    {
        //horizontal
        int j = 8 + 12 * (Rows(i) - 1);
        if (i < j && i < 120)
        {
            if (grid[i + 1].Equals(char.Parse("*"))
                && grid[i + 3].Equals(char.Parse("*"))
                && grid[i + 12].Equals(char.Parse("*"))
                && grid[i + 13].Equals(char.Parse("*"))
                && grid[i + 14].Equals(char.Parse("*"))
                && grid[i + 15].Equals(char.Parse("*"))
                && grid[i + 16].Equals(char.Parse("*"))
                && grid[i + 25].Equals(char.Parse("*"))
                && grid[i + 27].Equals(char.Parse("*")))
            {
                probArray[i + 1]++;
                probArray[i + 3]++;
                probArray[i + 12]++;
                probArray[i + 13]++;
                probArray[i + 14]++;
                probArray[i + 15]++;
                probArray[i + 16]++;
                probArray[i + 25]++;
                probArray[i + 27]++;
            }
        }
        //vertical
        int jj = 10 + 12 * (Rows(i) - 1);
        if (i < jj && i < 96)
        {
            if (grid[i + 1].Equals(char.Parse("*"))
                && grid[i + 12].Equals(char.Parse("*"))
                && grid[i + 13].Equals(char.Parse("*"))
                && grid[i + 14].Equals(char.Parse("*"))
                && grid[i + 25].Equals(char.Parse("*"))
                && grid[i + 36].Equals(char.Parse("*"))
                && grid[i + 37].Equals(char.Parse("*"))
                && grid[i + 38].Equals(char.Parse("*"))
                && grid[i + 49].Equals(char.Parse("*")))
            {
                probArray[i + 1]++;
                probArray[i + 12]++;
                probArray[i + 13]++;
                probArray[i + 14]++;
                probArray[i + 25]++;
                probArray[i + 36]++;
                probArray[i + 37]++;
                probArray[i + 38]++;
                probArray[i + 49]++;
            }
        }

    }
    return probArray;
}

void ChangeIntoWater(int x)
{
    grid = grid.Remove(x, 1);
    grid = grid.Insert(x, ".");
}

void ChangeIntoSunk(int x)
{
    grid = grid.Remove(x, 1);
    grid = grid.Insert(x, "P");
}

void PatrolBoatSunk(int x)
{
    //Ship on the left side
    if (x == 0 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse(".")))
        {
            patrolBoatSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 1);
            if (x == 0)
            {
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
            }
            else if (x == 132)
            {
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
            }
            else
            {
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
            }
        }
    }
    //Ship on the right side
    else if (x == 10 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x - 1].Equals(char.Parse(".")))
        {
            patrolBoatSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 1);
            if (x == 10)
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
            }
            else if (x == 142)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
            }
            else
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
            }
        }
    }
    //Ship on the upper side
    if (x >= 0 && x <= 11)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
            && grid[x + 24].Equals(char.Parse(".")))
        {
            patrolBoatSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 12);
            if (x == 0)
            {
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
            }
            else if (x == 11)
            {
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
            }
            else
            {
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
            }
        }
    }
    //Ship on the bottom side
    else if (x >= 120 && x <= 131)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
            && grid[x - 12].Equals(char.Parse(".")))
        {
            patrolBoatSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 12);
            if (x == 120)
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
            }
            else if (x == 131)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
            }
            else
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
            }
        }
    }
    //Horizontal in a middle
    if (x != 0 + 12 * (Rows(x) - 1)
        && x != 10 + 12 * (Rows(x) - 1)
        && x != 11 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("."))
            && grid[x - 1].Equals(char.Parse(".")))
        {
            patrolBoatSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 1);
            if (x > 0 && x < 10)
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
            }
            else if (x > 132 && x < 142)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
            }
            else
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
            }
        }
    }
    //Vertical in a middle
    if (x > 11 && x < 120)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
        && grid[x + 24].Equals(char.Parse("."))
        && grid[x - 12].Equals(char.Parse(".")))
        {
            patrolBoatSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 12);
            if (x == 12 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
            }
            else if (x == 23 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
            }
            else
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
            }
        }
    }
}

void SubmarineDestroyerSunk(int x)
{
    //Ship on the left side
    if (x == 0 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 3].Equals(char.Parse(".")))
        {
            if ((x != 132 && grid[x + 13].Equals(char.Parse("."))
                || x != 0 && grid[x - 11].Equals(char.Parse(".")))
                || x == 132
                || x == 0)
            {
                if (submarineSunk == true)
                {
                    destroyerSunk = true;
                }
                else
                {
                    submarineSunk = true;
                }
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 1);
                ChangeIntoSunk(x + 2);
                if (x == 0)
                {
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                }
                else if (x == 132)
                {
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                }
                else
                {
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                } 
            }
        }
    }
    //Ship on the right side
    else if (x == 9 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x - 1].Equals(char.Parse(".")))
        {
            if ((x != 141 && grid[x + 13].Equals(char.Parse("."))
                || x != 9 && grid[x - 11].Equals(char.Parse(".")))
                || x == 141
                || x == 9)
            {
                if (submarineSunk == true)
                {
                    destroyerSunk = true;
                }
                else
                {
                    submarineSunk = true;
                }
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 1);
                ChangeIntoSunk(x + 2);
                if (x == 9)
                {
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                }
                else if (x == 141)
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                }
                else
                {
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                } 
            }
        }
    }
    //Ship on the upper side
    if (x >= 0 && x <= 11)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
            && grid[x + 24].Equals(char.Parse("X"))
            && grid[x + 36].Equals(char.Parse(".")))
        {
            if ((x != 11 && grid[x + 13].Equals(char.Parse("."))
                || x != 0 && grid[x + 11].Equals(char.Parse(".")))
                || x == 11
                || x == 0)
            {
                if (submarineSunk == true)
                {
                    destroyerSunk = true;
                }
                else
                {
                    submarineSunk = true;
                }
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 12);
                ChangeIntoSunk(x + 24);
                if (x == 0)
                {
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                }
                else if (x == 11)
                {
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                }
                else
                {
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                } 
            }
        }
    }
    //Ship on the bottom side
    else if (x >= 108 && x <= 119)
    {
        if (grid[x - 12].Equals(char.Parse("."))
            && grid[x + 12].Equals(char.Parse("X"))
            && grid[x + 24].Equals(char.Parse("X")))
        {
            if ((x != 119 && grid[x + 13].Equals(char.Parse("."))
                || x != 108 && grid[x + 11].Equals(char.Parse(".")))
                || x == 119
                || x == 108)
            {
                if (submarineSunk == true)
                {
                    destroyerSunk = true;
                }
                else
                {
                    submarineSunk = true;
                }
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 12);
                ChangeIntoSunk(x + 24);
                if (x == 108)
                {
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                }
                else if (x == 119)
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                }
                else
                {
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                } 
            }
        }
    }
    //Horizontal in a middle
    if (x != 0 + 12 * (Rows(x) - 1)
        && x != 9 + 12 * (Rows(x) - 1)
        && x != 10 + 12 * (Rows(x) - 1)
        && x != 11 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 3].Equals(char.Parse("."))
            && grid[x - 1].Equals(char.Parse(".")))
        {
            if ((x > 12 && x < 129 && grid[x + 13].Equals(char.Parse("."))
                || x > 12 && x < 129 && grid[x - 11].Equals(char.Parse(".")))
                || x > 0 && x < 9
                || x > 132 && x < 141)
            {
                if (submarineSunk == true)
                {
                    destroyerSunk = true;
                }
                else
                {
                    submarineSunk = true;
                }
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 1);
                ChangeIntoSunk(x + 2);
                if (x > 0 && x < 9)
                {
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                }
                else if (x > 132 && x < 141)
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                }
                else
                {
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                } 
            }
        }
    }
    //Vertical in a middle
    if (x > 11 && x < 108)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
        && grid[x + 24].Equals(char.Parse("X"))
        && grid[x + 36].Equals(char.Parse("."))
        && grid[x - 12].Equals(char.Parse(".")))
        {
            if ((x != 0 + 12 * (Rows(x) - 1) && grid[x + 11].Equals(char.Parse("."))
                || x != 11 + 12 * (Rows(x) - 1) && grid[x + 13].Equals(char.Parse(".")))
                || x == 0 + 12 * (Rows(x) - 1)
                || x == 11 + 12 * (Rows(x) - 1))
            {
                if (submarineSunk == true)
                {
                    destroyerSunk = true;
                }
                else
                {
                    submarineSunk = true;
                }
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 12);
                ChangeIntoSunk(x + 24);
                if (x == 12 + 12 * (Rows(x) - 2))
                {
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                }
                else if (x == 23 + 12 * (Rows(x) - 2))
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                }
                else
                {
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                } 
            }
        }
    }
}

void BattleshipSunk(int x)
{
    //Ship on the left side
    if (x == 0 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 3].Equals(char.Parse("X"))
            && grid[x + 4].Equals(char.Parse(".")))
        {
            battleshipSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 1);
            ChangeIntoSunk(x + 2);
            ChangeIntoSunk(x + 3);
            if (x == 0)
            {
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 15);
                ChangeIntoWater(x + 16);
            }
            else if (x == 132)
            {
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 8);
            }
            else
            {
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 15);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 8);
            }
        }
    }
    //Ship on the right side
    else if (x == 8 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 3].Equals(char.Parse("X"))
            && grid[x - 1].Equals(char.Parse(".")))
        {
            battleshipSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 1);
            ChangeIntoSunk(x + 2);
            ChangeIntoSunk(x + 3);
            if (x == 8)
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 15);
            }
            else if (x == 140)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
            }
            else
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 15);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
            }
        }
    }
    //Ship on the upper side
    if (x >= 0 && x <= 11)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
            && grid[x + 24].Equals(char.Parse("X"))
            && grid[x + 36].Equals(char.Parse("X"))
            && grid[x + 48].Equals(char.Parse(".")))
        {
            battleshipSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 12);
            ChangeIntoSunk(x + 24);
            ChangeIntoSunk(x + 36);
            if (x == 0)
            {
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 49);
            }
            else if (x == 11)
            {
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 47);
            }
            else
            {
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 47);
            }
        }
    }
    //Ship on the bottom side
    else if (x >= 96 && x <= 107)
    {
        if (grid[x - 12].Equals(char.Parse("."))
            && grid[x + 12].Equals(char.Parse("X"))
            && grid[x + 24].Equals(char.Parse("X"))
            && grid[x + 36].Equals(char.Parse("X")))
        {
            battleshipSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 12);
            ChangeIntoSunk(x + 24);
            ChangeIntoSunk(x + 36);
            if (x == 96)
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 37);
            }
            else if (x == 119)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
            }
            else
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
            }
        }
    }
    //Horizontal in a middle
    if (x != 0 + 12 * (Rows(x) - 1)
        && x != 8 + 12 * (Rows(x) - 1)
        && x != 9 + 12 * (Rows(x) - 1)
        && x != 10 + 12 * (Rows(x) - 1)
        && x != 11 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 3].Equals(char.Parse("X"))
            && grid[x + 4].Equals(char.Parse("."))
            && grid[x - 1].Equals(char.Parse(".")))
        {
            battleshipSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 1);
            ChangeIntoSunk(x + 2);
            ChangeIntoSunk(x + 3);
            if (x > 0 && x < 8)
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 15);
                ChangeIntoWater(x + 16);
            }
            else if (x > 132 && x < 140)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 8);
            }
            else
            {
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 12);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 15);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 8);
            }
        }
    }
    //Vertical in a middle
    if (x > 11 && x < 96)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
        && grid[x + 24].Equals(char.Parse("X"))
        && grid[x + 36].Equals(char.Parse("X"))
        && grid[x + 48].Equals(char.Parse("."))
        && grid[x - 12].Equals(char.Parse(".")))
        {
            battleshipSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 12);
            ChangeIntoSunk(x + 24);
            ChangeIntoSunk(x + 36);
            if (x == 12 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 49);
            }
            else if (x == 23 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 47);
            }
            else
            {
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 13);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 11);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 47);
            }
        }
    }
}

void CarrierSunk(int x)
{
    //Horizontal
    if (x != 8 + 12 * (Rows(x) - 1)
        && x != 9 + 12 * (Rows(x) - 1)
        && x != 10 + 12 * (Rows(x) - 1)
        && x != 11 + 12 * (Rows(x) - 1))
    {
        if (grid[x + 1].Equals(char.Parse("X"))
            && grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 3].Equals(char.Parse("X"))
            && grid[x + 4].Equals(char.Parse("X")))
        {
            if ((x > 8 && grid[x - 11].Equals(char.Parse("."))
                || x > 8 && grid[x - 9].Equals(char.Parse("."))
                || x < 131 && grid[x + 13].Equals(char.Parse("."))
                || x < 131 && grid[x + 15].Equals(char.Parse(".")))
                || x < 8
                || x > 131)
            {
                carrierSunk = true;
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 1);
                ChangeIntoSunk(x + 2);
                ChangeIntoSunk(x + 3);
                ChangeIntoSunk(x + 4);
                if (x == 0)
                {
                    ChangeIntoWater(x + 5);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x + 16);
                    ChangeIntoWater(x + 17);
                }
                else if (x == 7)
                {
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x + 16);
                }
                else if (x == 132)
                {
                    ChangeIntoWater(x + 5);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                    ChangeIntoWater(x - 8);
                    ChangeIntoWater(x - 7);
                }
                else if (x == 139)
                {
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                    ChangeIntoWater(x - 8);
                }
                else if (x > 0 && x < 7)
                {
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 5);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x + 16);
                    ChangeIntoWater(x + 17);
                }
                else if (x > 132 && x < 139)
                {
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 5);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                    ChangeIntoWater(x - 8);
                    ChangeIntoWater(x - 7);
                }
                else if (x == 12 + 12 * (Rows(x) - 2))
                {
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                    ChangeIntoWater(x - 8);
                    ChangeIntoWater(x - 7);
                    ChangeIntoWater(x + 5);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x + 16);
                    ChangeIntoWater(x + 17);
                }
                else if (x == 19 + 12 * (Rows(x) - 2))
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                    ChangeIntoWater(x - 8);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x + 16);
                }
                else
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 10);
                    ChangeIntoWater(x - 9);
                    ChangeIntoWater(x - 8);
                    ChangeIntoWater(x - 7);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 5);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 12);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 14);
                    ChangeIntoWater(x + 15);
                    ChangeIntoWater(x + 16);
                    ChangeIntoWater(x + 17);
                }
            }
        }
    }
    //Vertical
    if (x < 96)
    {
        if (grid[x + 12].Equals(char.Parse("X"))
        && grid[x + 24].Equals(char.Parse("X"))
        && grid[x + 36].Equals(char.Parse("X"))
        && grid[x + 48].Equals(char.Parse("X")))
        {
            if ((x != 0 + 12 * (Rows(x) - 1) && grid[x + 11].Equals(char.Parse("."))
                || x != 0 + 12 * (Rows(x) - 1) && grid[x + 35].Equals(char.Parse("."))
                || x != 11 + 12 * (Rows(x) - 1) && grid[x + 13].Equals(char.Parse("."))
                || x != 11 + 12 * (Rows(x) - 1) && grid[x + 37].Equals(char.Parse(".")))
                || x == 0 + 12 * (Rows(x) - 1)
                || x == 11 + 12 * (Rows(x) - 1))
            {
                carrierSunk = true;
                ChangeIntoSunk(x);
                ChangeIntoSunk(x + 12);
                ChangeIntoSunk(x + 24);
                ChangeIntoSunk(x + 36);
                ChangeIntoSunk(x + 48);
                if (x == 0)
                {
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x + 49);
                    ChangeIntoWater(x + 60);
                    ChangeIntoWater(x + 61);
                }
                else if (x == 11)
                {
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                    ChangeIntoWater(x + 47);
                    ChangeIntoWater(x + 59);
                    ChangeIntoWater(x + 60);
                }
                else if (x == 84)
                {
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x + 49);
                }
                else if (x == 95)
                {
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                    ChangeIntoWater(x + 47);
                }
                else if (x == 12 + 12 * (Rows(x) - 2))
                {
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x + 49);
                    ChangeIntoWater(x + 60);
                    ChangeIntoWater(x + 61);
                }
                else if (x == 23 + 12 * (Rows(x) - 2))
                {
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                    ChangeIntoWater(x + 47);
                    ChangeIntoWater(x + 59);
                    ChangeIntoWater(x + 60);
                }
                else if (x > 0 && x < 11)
                {
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x + 49);
                    ChangeIntoWater(x + 59);
                    ChangeIntoWater(x + 60);
                    ChangeIntoWater(x + 61);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                    ChangeIntoWater(x + 47);
                }
                else if (x > 84 && x < 95)
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x + 49);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                    ChangeIntoWater(x + 47);
                }
                else
                {
                    ChangeIntoWater(x - 13);
                    ChangeIntoWater(x - 12);
                    ChangeIntoWater(x - 11);
                    ChangeIntoWater(x - 1);
                    ChangeIntoWater(x + 1);
                    ChangeIntoWater(x + 13);
                    ChangeIntoWater(x + 25);
                    ChangeIntoWater(x + 37);
                    ChangeIntoWater(x + 49);
                    ChangeIntoWater(x + 11);
                    ChangeIntoWater(x + 23);
                    ChangeIntoWater(x + 35);
                    ChangeIntoWater(x + 47);
                    ChangeIntoWater(x + 59);
                    ChangeIntoWater(x + 60);
                    ChangeIntoWater(x + 61);
                }
            }
        }
    }
}

void HelicarrierSunk(int x)
{
    //Horizontal
    if (x != 0 + 12 * (Rows(x) - 1)
        && x != 9 + 12 * (Rows(x) - 1)
        && x != 10 + 12 * (Rows(x) - 1)
        && x != 11 + 12 * (Rows(x) - 1)
        && x < 120)
    {
        if (grid[x + 2].Equals(char.Parse("X"))
            && grid[x + 11].Equals(char.Parse("X"))
            && grid[x + 12].Equals(char.Parse("X"))
            && grid[x + 13].Equals(char.Parse("X"))
            && grid[x + 14].Equals(char.Parse("X"))
            && grid[x + 15].Equals(char.Parse("X"))
            && grid[x + 24].Equals(char.Parse("X"))
            && grid[x + 26].Equals(char.Parse("X")))
        {
            helicarrierSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 2);
            ChangeIntoSunk(x + 11);
            ChangeIntoSunk(x + 12);
            ChangeIntoSunk(x + 13);
            ChangeIntoSunk(x + 14);
            ChangeIntoSunk(x + 15);
            ChangeIntoSunk(x + 24);
            ChangeIntoSunk(x + 26);
            if (x == 1)
            {
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 4);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x + 28);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 36);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 39);
            }
            else if (x == 8)
            {
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 36);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 39);
            }
            else if (x == 109)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 4);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 28);
            }
            else if (x == 116)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
            }
            else if (x > 1 && x < 8)
            {
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 4);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 28);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 36);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 39);
            }
            else if (x > 109 && x < 116)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 4);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 28);
            }
            else if (x == 13 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 4);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 28);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 36);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 39);
            }
            else if (x == 20 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 36);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 39);
            }
            else
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 10);
                ChangeIntoWater(x - 9);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 3);
                ChangeIntoWater(x + 4);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 16);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 27);
                ChangeIntoWater(x + 28);
                ChangeIntoWater(x + 35);
                ChangeIntoWater(x + 36);
                ChangeIntoWater(x + 37);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 39);
            }

        }
    }
    //Vertical
    if (x != 0 + 12 * (Rows(x) - 1)
        && x != 11 + 12 * (Rows(x) - 1)
        && x < 96)
    {
        if (grid[x + 11].Equals(char.Parse("X"))
        && grid[x + 12].Equals(char.Parse("X"))
        && grid[x + 13].Equals(char.Parse("X"))
        && grid[x + 24].Equals(char.Parse("X"))
        && grid[x + 35].Equals(char.Parse("X"))
        && grid[x + 36].Equals(char.Parse("X"))
        && grid[x + 37].Equals(char.Parse("X"))
        && grid[x + 48].Equals(char.Parse("X")))
        {

            helicarrierSunk = true;
            ChangeIntoSunk(x);
            ChangeIntoSunk(x + 11);
            ChangeIntoSunk(x + 12);
            ChangeIntoSunk(x + 13);
            ChangeIntoSunk(x + 24);
            ChangeIntoSunk(x + 35);
            ChangeIntoSunk(x + 36);
            ChangeIntoSunk(x + 37);
            ChangeIntoSunk(x + 48);
            if (x == 1)
            {
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x + 2);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 26);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 50);
                ChangeIntoWater(x + 59);
                ChangeIntoWater(x + 60);
                ChangeIntoWater(x + 61);
            }
            else if (x == 10)
            {
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 34);
                ChangeIntoWater(x + 46);
                ChangeIntoWater(x + 59);
                ChangeIntoWater(x + 60);
                ChangeIntoWater(x + 61);
            }
            else if (x == 85)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x + 2);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 26);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 50);
            }
            else if (x == 94)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 34);
                ChangeIntoWater(x + 46);
            }
            else if (x == 13 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x + 2);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 26);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 50);
                ChangeIntoWater(x + 59);
                ChangeIntoWater(x + 60);
                ChangeIntoWater(x + 61);
            }
            else if (x == 22 + 12 * (Rows(x) - 2))
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 34);
                ChangeIntoWater(x + 46);
                ChangeIntoWater(x + 59);
                ChangeIntoWater(x + 60);
                ChangeIntoWater(x + 61);
            }
            else if (x > 1 && x < 10)
            {
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 34);
                ChangeIntoWater(x + 46);
                ChangeIntoWater(x + 2);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 26);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 50);
                ChangeIntoWater(x + 59);
                ChangeIntoWater(x + 60);
                ChangeIntoWater(x + 61);
            }
            else if (x > 85 && x < 94)
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 34);
                ChangeIntoWater(x + 46);
                ChangeIntoWater(x + 2);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 26);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 50);
            }
            else
            {
                ChangeIntoWater(x - 13);
                ChangeIntoWater(x - 12);
                ChangeIntoWater(x - 11);
                ChangeIntoWater(x - 1);
                ChangeIntoWater(x + 1);
                ChangeIntoWater(x + 23);
                ChangeIntoWater(x + 25);
                ChangeIntoWater(x + 47);
                ChangeIntoWater(x + 49);
                ChangeIntoWater(x - 2);
                ChangeIntoWater(x + 10);
                ChangeIntoWater(x + 22);
                ChangeIntoWater(x + 34);
                ChangeIntoWater(x + 46);
                ChangeIntoWater(x + 2);
                ChangeIntoWater(x + 14);
                ChangeIntoWater(x + 26);
                ChangeIntoWater(x + 38);
                ChangeIntoWater(x + 50);
                ChangeIntoWater(x + 59);
                ChangeIntoWater(x + 60);
                ChangeIntoWater(x + 61);
            }
        }
    }
}