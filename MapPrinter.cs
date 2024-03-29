namespace Kse.Algorithms.Samples
{
    using System;
    using System.Collections.Generic;

    public class MapPrinter
    {

        public void Print(string[,] maze, List<Point> path)
        {

            PrintTopLine();

            string old_point_maze = " ";
            

            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t");
                
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    foreach (var i_point in path.Select((value, index)=>(value,index)))
                    {
                        // 
                        if (i_point.value.Column == column & i_point.value.Row == row)
                        {
                            if (maze[column, row] != "A" & maze[column, row] != "B") old_point_maze = maze[column, row];
                            
                            maze[column, row] = "*";
                       
                            if (i_point.index == 0) maze[column, row] = "A";
                           
                            if (i_point.index == (path.Count - 1))
                              {
                                  if (i_point.value.Column != 0 & i_point.value.Row != 0)
                                      maze[column, row] = "B";
                                  else
                                      maze[column, row] = old_point_maze;
                              }
                           
                            Console.ForegroundColor = ConsoleColor.Red; 
                        }
                    }
                    Console.Write(maze[column, row]);
                    Console.ResetColor();
                    
                }
                Console.WriteLine();
            }

           
            void PrintTopLine()
            {
                Console.Write($" \t");
                
                for (int i = 0; i < maze.GetLength(0); i++)
                {
        
                    Console.Write(i % 10 == 0 ? i / 10 : " ");
                }

                Console.Write($"\n \t");

                for (int i = 0; i < maze.GetLength(0); i++)
                {
                   
                    Console.Write(i % 10);
                }

                Console.WriteLine("\n");
            }
            
        }
    }
}
