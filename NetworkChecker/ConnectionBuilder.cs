using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkChecker
{
    public static class ConnectionBuilder
    {
        public static void BuildConnection(string connectionProject)
        {
            Input.Connections = new List<Connection>();
            switch (connectionProject)
            {
                case "line":
                    SimpleLineConnection();
                    break;
                case "adv-line":
                    AdditionalLineConnection();
                    break;
                case "adv-circle":
                    AdditionalCircularConnection();
                    break;
                case "grid":
                    GridConnection();
                    break;
                default:
                    throw new ArgumentException();
            }
            int trasitionDivider = 0;
            foreach (var connection in Input.Connections)
            {
                trasitionDivider += connection.TransitionLoad;
            }
            int transitionMultiplier = Input.SumOfTranstions / trasitionDivider;
            foreach (var connection in Input.Connections)
            {
                connection.TransitionLoad *= transitionMultiplier;
            }
        }

        private static void SimpleLineConnection()
        {
            for (int i = 1; i < 20; i++)
            {
                Input.Connections.Add(new Connection
                {
                    Latency = 2,
                    TransitionLoad = 1,
                    Edges = new int[] { i - 1, i }
                });
            }
        }


        private static void AdditionalLineConnection()
        {
            for (int i = 1; i < 20; i++)
            {
                Input.Connections.Add(new Connection
                {
                    Latency = 2,
                    TransitionLoad = 1,
                    Edges = new int[] { i - 1, i }
                });
            }

            for (int i = 0; i < 9; i+=1)
            {
                Input.Connections.Add(new Connection
                {
                    Latency = 5,
                    TransitionLoad = 3,
                    Edges = new int[] { i , i+10 }
                });
            }
        }

        private static void AdditionalCircularConnection()
        {
            for (int i = 1; i < 20; i++)
            {
                Input.Connections.Add(new Connection
                {
                    Latency = 2,
                    TransitionLoad = 1,
                    Edges = new int[] { i - 1, i }
                });
            }

            Input.Connections.Add(new Connection
            {
                Latency = 2,
                TransitionLoad = 1,
                Edges = new int[] { 0, 19 }
            });

            for (int i = 1; i < 8; i += 1)
            {
                Input.Connections.Add(new Connection
                {
                    Latency = 5,
                    TransitionLoad = 3,
                    Edges = new int[] { i, i + 10 }
                });
            }
        }

        private static void GridConnection()
        {
            for (int i = 0; i <= 15; i+=5)
            {
                for (int j = 0; j < 4; j++)
                {
                    Input.Connections.Add(new Connection
                    {
                        Latency = 2,
                        TransitionLoad = 1,
                        Edges = new int[] { i+j, i +j+1}
                    });
                }
            }

            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Input.Connections.Add(new Connection
                    {
                        Latency = 2,
                        TransitionLoad = 1,
                        Edges = new int[] { i + j*5, i + j*5 + 5 }
                    });
                }
            }

            Input.Connections.RemoveAt(30);

            IncreaseTransition(6,7);
            IncreaseTransition(7,8);
            IncreaseTransition(11,12);
            IncreaseTransition(12,13);
            IncreaseTransition(6,11);
            IncreaseTransition(7,12);
            IncreaseTransition(8,13);
        }

        private static void IncreaseTransition(int x, int y)
        {
            Input.Connections.First(c => c.Edges.Contains(x) && c.Edges.Contains(y)).TransitionLoad++;
        }
    }
}
