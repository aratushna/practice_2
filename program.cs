

using System.Runtime.InteropServices.JavaScript;
using Kse.Algorithms.Samples;



var generator = new MapGenerator(new MapGeneratorOptions()

{
    Height = 35,
    Width = 90,
   
    Seed = 2,
    
});



Point start = new Point (0, 0 );
Point goal = new Point (0, 0 );

List<Point> path = new List<Point>() { start, goal };


string[,] map = generator.Generate();
new MapPrinter().Print(map, path);

int err_wall = 0;
int start_column, start_row, goal_column, goal_row;


Console.WriteLine("");
Console.WriteLine("-----------------------------------------------------------------------------");
Console.WriteLine("Введите координаты точек: Старта и Финиша");
Console.Write("Старт: Вертикаль,Столбец (от 0 до 89) >> ");
start_column = Convert.ToInt32(Console.ReadLine());
Console.Write("       Горизональ,Строка (от 0 до 34) >> ");
start_row = Convert.ToInt32(Console.ReadLine());
Console.Write("Финиш: Вертикаль,Столбец (от 0 до 89) >> ");
goal_column = Convert.ToInt32(Console.ReadLine());
Console.Write("       Горизональ,Строка (от 0 до 34) >> ");
goal_row = Convert.ToInt32(Console.ReadLine());
if (map[start_column, start_row] == MapGenerator.Wall | map[goal_column, goal_row] == MapGenerator.Wall) err_wall = 1;
do
{
    
} while (expression);
    




start = new Point (0, 0 );
//goal = new Point (65, 24 );
goal = new Point (10, 1 );

path = new List<Point>() { start, goal };


new MapPrinter().Print(map, path);

map[goal.Column, goal.Row] = " ";


var distances = new Dictionary<Point, int>();
var origins = new Dictionary<Point, Point>();
for (var row = 0; row < map.GetLength(1); row++)
    for (var column = 0; column < map.GetLength(0); column++)
    {
        
        //if (map[column, row] != MapGenerator.Wall)
        //{
            Point i_point = new Point(column, row);
            distances[i_point] = 0;
            origins[i_point] = new Point(0, 0);  
        //}
    }


void def_search (Point i_point)
    {
        var neighbours = generator.GetNeighbours(i_point.Column, i_point.Row, map, 1, true);
            foreach (var neighbour in neighbours)
            {
                if (((distances[i_point] +1 ) <= distances[neighbour]) | distances[neighbour] == 0 )
                {
                    origins[neighbour] = i_point;
                    distances[neighbour] = distances[i_point] + 1;
                    
                    if (distances[goal] != 0 | origins[goal].Column != 0 | origins[goal].Row != 0) return;
                    
                    Console.WriteLine("дистанция" + distances[neighbour]);
                    Console.WriteLine("колонка/строка: " + origins[neighbour].Column + " " + origins[neighbour].Row);
                    def_search(neighbour); 
                }
            }
    }

    def_search(start);
