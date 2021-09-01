using System;
using System.Threading;

/*
 * 1. Select Cell
 * 2. Mark visited
 * 3.Randomly remove 1 wall
 * 4.Go to neighbor. 
 * 5.repeat

*/
namespace ConsoleApp4
{
    public class Maze
    {
        public Walls[,] array;
        public int width;
        public int height;

        private int[,] back;
        private int count = 0;

        public int current_i;
        public int current_j;
        public int exited = -1;
        public Maze(int width2, int height2)
        {

            this.array = new Walls[width2, height2];
            this.back = new int[1000000,2];
            this.width = width2;
            this.height = height2;
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.array[i, j] = new Walls();

                }
            }
        }
        public void Generate()
        {
            Console.WriteLine("Entering Generation");
            var random_num = new Random();
            int num = random_num.Next(0, this.width);





            this.array[0, num].wall_top = false;

            

            Neighborize(0, num);
            




            var random_exit = new Random();
            while (this.exited<0) {
                int num2 = random_exit.Next(0, this.width);
                if (this.array[this.height-1, num2].visited)
                {
                    this.exited = num2;

                }

            }
            Console.Clear();
            





        }

        private void Neighborize(int i, int j)
        {
            
            var random_num = new Random();
            bool picked = false;
            int decision_i = i;
            int decision_j = j;
            int looper = 0;
           
            while (picked == false)
            {
                int num = random_num.Next(0, 4);
               
                switch (num)
                {
                    case 0:
                      //  Console.WriteLine($"[{i},{j-1}],{num}");
                        if (i < this.height && j-1 >= 0)
                        {

                           // Console.WriteLine((i < this.height && j - 1 >= 0));
                            if (this.array[i, j - 1].visited == false)
                            {
                               // Console.WriteLine("Picked");
                                picked = true;
                                decision_j = j - 1;
                                this.array[i, j - 1].wall_right = false;
                                this.array[i, j].visited = true;

                                break;
                            }
                        }

                    break;
                    
                    case 1:
                      //  Console.WriteLine($"[{i-1},{j}],{num}");
                        if (i-1 >= 0 && j < this.width)
                        {
                            //Console.WriteLine((this.array[i - 1, j].visited == false));
                            if (this.array[i - 1, j].visited == false)
                            {
                              //  Console.WriteLine("Picked");
                                picked = true;
                                decision_i = i - 1;
                                this.array[i, j].wall_top = false;
                                this.array[i, j].visited = true;
                                break;
                            }
                        }

                        break;

                    case 2:
                      //  Console.WriteLine($"[{i},{j+1}],{num}");
                        if (i < this.height && j+1 < this.width)
                        {
                        //    Console.WriteLine((this.array[i, j + 1].visited == false));
                            if (this.array[i, j + 1].visited == false)
                            {
                             //   Console.WriteLine("Picked");
                                picked = true;
                                decision_j = j + 1;
                                this.array[i, j].wall_right = false;
                                this.array[i, j].visited = true;
                                break;
                            }
                        }

                        break;

                   case 3:
                      //  Console.WriteLine($"[{i+1},{j}],{num}");
                        if (i+1< this.height && j < this.width)
                        {
                          //  Console.WriteLine((this.array[i + 1, j].visited == false));
                            if (this.array[i+1, j].visited == false)
                            {
                                //Console.WriteLine("Picked");
                                picked = true;
                                decision_i = i + 1;
                                this.array[i + 1, j].wall_top = false;
                                this.array[i, j].visited = true;
                                break;
                            }
                        }

                        break;

                     

                }
                if ((i < this.height && j - 1 > 0) && (i - 1 > 0 && j < this.width) && (i < this.height && j + 1 < this.width) && (i + 1 < this.height && j < this.width))
                {
                    if ((this.array[i, j - 1].visited == true) && (this.array[i - 1, j].visited == true) && (this.array[i, j + 1].visited == true) && (this.array[i + 1, j].visited == true))
                    {
                      //  Console.WriteLine("Backtrack Line 149");
                       // Console.WriteLine($"Backtracking to: ${this.back[this.count, 0]},{this.back[this.count, 1]}");
                        if (this.back[this.count, 0] >= 0 && this.back[this.count, 1] >= 0 && this.back[this.count, 1] < this.width)
                        {
                            if ((this.array[i, j].wall_top != true || this.array[this.back[this.count, 0], this.back[this.count, 1]].wall_top != true) && (this.array[i, j].wall_right != true || this.array[this.back[this.count, 0], this.back[this.count, 1]].wall_right != true))
                            {
                                decision_i = this.back[this.count, 0];
                                decision_j = this.back[this.count, 1];
                            }
                        }
                        this.count -= 1;
                        break;
                        

                    }
                    

                }

                looper++;
                if(looper > 10)
                {
                   // Console.WriteLine("Backtrack Line 165");
                    //Console.WriteLine($"Backtracking to: ${this.back[this.count, 0]},{this.back[this.count, 1]}");
                    if(this.count > 0) { 
                    if (this.back[this.count, 0] >= 0 && this.back[this.count, 1] >= 0 && this.back[this.count, 1] < this.width)
                    {
                        if ((this.array[i, j].wall_top != true || this.array[this.back[this.count, 0], this.back[this.count, 1]].wall_top != true) && (this.array[i, j].wall_right != true || this.array[this.back[this.count, 0], this.back[this.count, 1]].wall_right != true))
                        {
                            decision_i = this.back[this.count, 0];
                            decision_j = this.back[this.count, 1];
                        }
                    }
                    this.count -= 1;
                    looper = 1;

                    }
                    else
                    {
                        this.count += 3;


                    }
                    break;
                }

                
            }
            current_i = i;
            current_j = j;
            this.Display();
            Console.SetCursorPosition(0, 0);
            
            Console.CursorVisible = false;


            if ((i < this.height && j - 1 > 0) && (i - 1 > 0 && j < this.width) && (i < this.height && j + 1 < this.width) && (i + 1 < this.height && j < this.width))
            {
                if((this.array[i, j - 1].visited == true) && (this.array[i - 1, j].visited == true) && (this.array[i, j + 1].visited == true) && (this.array[i + 1, j].visited == true))
                {
                   // Console.WriteLine("Backtrack Line 182");
                    try
                    {
                        if (this.back[this.count, 0] >= 0 && this.back[this.count, 1] >= 0 && this.back[this.count, 1] < this.width && this.count > 1)
                        {
                            if ((this.array[i, j].wall_top != true || this.array[this.back[this.count, 0], this.back[this.count, 1]].wall_top != true)&& (this.array[i, j].wall_right != true || this.array[this.back[this.count, 0], this.back[this.count, 1]].wall_right != true))
                            {
                                decision_i = this.back[this.count, 0];
                                decision_j = this.back[this.count, 1];
                            }
                        }
                      //  Console.WriteLine($"Backtracking to: ${this.back[this.count, 0]},{this.back[this.count, 1]}");
                        this.count -= 1;
                        Neighborize(decision_i, decision_j);
                    }
                    catch
                    {
                       
                    }
                }
                else
                {
                    this.count++;
                    this.back[this.count, 0] = decision_i;
                    this.back[this.count, 1] = decision_j;
                    Neighborize(decision_i, decision_j);
                }

            }
            else
            {
                Neighborize(decision_i, decision_j);
            }






            return;
        } 
        public void Display()
        {
            {
                for (int i = 0; i < this.height; i++)
                {
                    for (int j = 0; j < this.width; j++)
                    {
                        if (this.array[i, j].wall_top == true){
                            Console.Write("XX");
                        }
                        else
                        {
                            
                                Console.Write("  ");
                            if(i == 0)
                            {
                                Console.Write("  ");
                            }
                            
                            
                        }
                        
                        Console.Write("XX");



                        //Console.WriteLine($"{i},{j},{this.array[i, j].wall_top},{this.array[i, j].wall_right},{this.array[i, j].wall_bottom},{this.array[i, j].wall_left}");

                    }
                    Console.Write("\nXX");



                    for (int j2 = 0; j2 < this.width; j2++)
                    {
                        Console.Write("  ");
                        if (this.array[i, j2].wall_right == true)
                        {
                            Console.Write("XX");
                            
                        }
                        else
                        {
                            if (i == current_i && j2 == current_j)
                            {
                                Console.Write("  ");
                               
                            }
                            else
                            {
                                Console.Write("  ");
                            }
                           
                            
                        }
                        



                        //Console.WriteLine($"{i},{j},{this.array[i, j].wall_top},{this.array[i, j].wall_right},{this.array[i, j].wall_bottom},{this.array[i, j].wall_left}");

                    }
                    Console.Write("\nXX");
                    for (int j3 = 0; j3 < this.width; j3++)
                    {
                        if (this.array[i, j3].wall_bottom == true)
                        {
                           // Console.Write("X");
                        }
                        else
                        {
                            //Console.Write(" ");
                        }
                       // Console.Write("X");



                        //Console.WriteLine($"{i},{j},{this.array[i, j].wall_top},{this.array[i, j].wall_right},{this.array[i, j].wall_bottom},{this.array[i, j].wall_left}");

                    }
                    //Console.Write("\n");


                }
            }
            for(int j = 0;  j<this.width; j++)
            {
                if (j != this.exited)
                {
                    Console.Write("XXXX");
                }
                else
                {
                    Console.Write("    ");
                }
            }
            Console.WriteLine($"\n{this.count}");
        }

    }
    public class Walls
    {
        public bool wall_left;
        public bool wall_top;
        public bool wall_right;
        public bool wall_bottom;
        public bool visited;
        public int visited_count;

        public Walls()
        {
            this.wall_left = true;
            this.wall_top = true;
            this.wall_right =  true;
            this.wall_bottom = true;
            this.visited = false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var stackSize = 10000000;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            
            Console.WriteLine("Hello World!");
            Thread th = new Thread(() =>
            {
                //Call your recursive thing here
                
                var Mazes = new Maze(15, 15);
                Mazes.Generate();
                Mazes.Display();
                Console.WriteLine("\nProgram Complete");
            },
            stackSize);
            th.Start();
            th.Join();


        }
    }
}
//☐