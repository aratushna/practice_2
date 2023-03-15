using Kse.Algorithms.Samples;
var generator = new MapGenerator(new MapGeneratorOptions()

{
    Height = 35,
    Width = 90,
    
});

Point start = new Point (0, 0 );
Point goal = new Point (0, 0 );
List<Point> path = new List<Point>() { start, goal };


string[,] map = generator.Generate();
new MapPrinter().Print(map, path);


int err_wall;
int start_column, start_row, goal_column, goal_row;
do
{
    err_wall = 0; 
    Console.WriteLine("");
    Console.WriteLine("-----------------------------------------------------------------------------------");
    Console.WriteLine("enter coordinates of start andfinish");
    Console.Write("start: column (0 to 89)");
    start_column = Convert.ToInt32(Console.ReadLine());
    Console.Write("row (0 to 34)");
    start_row = Convert.ToInt32(Console.ReadLine());
    Console.Write("finish: column (0 to 89)");
    goal_column = Convert.ToInt32(Console.ReadLine());
    Console.Write("row (0 to 34)");
    goal_row = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("-----------------------------------------------------------------------------");
    Console.WriteLine("");
    if (start_column == goal_column & start_row == goal_row)
    {
        Console.WriteLine("Error");
        err_wall = 1;
    }

    if (map[start_column, start_row] == MapGenerator.Wall | map[goal_column, goal_row] == MapGenerator.Wall)
    {
        Console.WriteLine("Error");
        Console.WriteLine("coordinate is on a wall");
        err_wall = 1; 
    }
} while (err_wall == 1); 

start = new Point (start_column, start_row );
goal = new Point (goal_column, goal_row );
path = new List<Point>() { start, goal };
new MapPrinter().Print(map, path);
map[goal.Column, goal.Row] = " ";

var distances = new Dictionary<Point, int>();
var origins = new Dictionary<Point, Point>();
Point i_point = new Point();


for (var row = 0; row < map.GetLength(1); row++)
    for (var column = 0; column < map.GetLength(0); column++)
    {
        if (map[column, row] != MapGenerator.Wall)
        {
            i_point = new Point(column, row);
            distances[i_point] = 0;
            origins[i_point] = new Point(-1, -1);  
        }
    }


    void def_search (Point i_point)
    {

        var neighbours = generator.GetNeighbours(i_point.Column, i_point.Row, map, 1, true);
            foreach (var neighbour in neighbours)
            {

                if ((((distances[i_point] + 1) <= distances[neighbour]) | distances[neighbour] == 0) & i_point.Column != -1)
                {
                    origins[neighbour] = i_point;
                    distances[neighbour] = distances[i_point] + 1;

                    if (distances[goal] != 0 | origins[goal].Column != -1) return;
                    def_search(neighbour);
                }
            }
    }


def_search(start); // Ïðîöåäóðà îïèñàíà âûøå


List<Point> shortest_path = new List<Point>();

shortest_path.Add(goal);
int min_distance;
Point min_point = new Point();

    void def_path (Point i_point)
    {
            min_distance = 0;

            var neighbours = generator.GetNeighbours(i_point.Column, i_point.Row, map, 1, true);
            
            foreach (var neighbour in neighbours)
                if (distances[neighbour] == 1 | distances[neighbour] == 2) // íå ðàáîòàåò <=2 , çàìåíèë íà 2 îïåðàòîðà ==
                {
                    shortest_path.Add(neighbour); // Äîáàâëÿåì ïîñë.òî÷êó â ñïèñîê è âûõîäèì
                    return;
                }

            foreach (var neighbour in neighbours)
            {
                if ((min_distance == 0 | min_distance >= distances[neighbour]) & distances[neighbour] > 0) // íàõîäèì ìèíèìàëüíîå ðàññòîÿíèå
                {

                    min_distance = distances[neighbour];
 
                    min_point = origins[neighbour];
                }
            }

            shortest_path.Add(min_point);

            def_path(min_point);
    }


def_path(goal);
shortest_path.Add(start);

Point add_point = new Point();
   for (int id = 0; id < shortest_path.Count; id++)
   {
       if (id != 0)
       {
           int c_math = shortest_path[id].Column - shortest_path[id - 1].Column;
           int r_math = shortest_path[id].Row - shortest_path[id - 1].Row;
           if (Math.Abs(c_math) == 2 | Math.Abs(r_math) == 2)
           {
               add_point.Column = ((shortest_path[id].Column + shortest_path[id - 1].Column) / 2) / 1;
               add_point.Row = ((shortest_path[id].Row + shortest_path[id - 1].Row) / 2) / 1;
               if ((map[add_point.Column, add_point.Row] != MapGenerator.Wall)
                   & !((add_point.Column == shortest_path[id].Column & add_point.Row == shortest_path[id].Row)
                       | (add_point.Column == shortest_path[id - 1].Column & add_point.Row == shortest_path[id - 1].Row)))
               {
                   shortest_path.Insert(id, add_point);
                   id = id + 1;
               }
           }
       }
   }

Console.WriteLine("---------------------------------------------------------------------------------------");
Console.WriteLine("");
Console.WriteLine("map with an optinal route");
Console.WriteLine("");

new MapPrinter().Print(map, shortest_path);
Console.WriteLine("");
Console.WriteLine("the route length: " + distances[goal]);


