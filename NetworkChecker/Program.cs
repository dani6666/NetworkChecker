using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkChecker
{
    class Program
    {
        private static readonly NetworkValidator networkValidator = new NetworkValidator();
        static void Main(string[] args)
        {
            var allArgs = new string[]{ "line", "adv-line","adv-circle", "grid"};
            foreach (var arg in allArgs)
            {
                Console.WriteLine();
                Console.WriteLine(arg);
                ConnectionBuilder.BuildConnection(arg);
                if (!(networkValidator.CheckCohesion(Input.Connections) && networkValidator.CheckWorkload(Input.Connections)))
                {
                    Console.WriteLine("Bad network");
                }
                else
                {
                    Console.WriteLine("Network reliability: " + MeasureNetworkReliability());

                    int[][] savedNetworkLoad = new int[20][];
                    for (int i = 0; i < 20; i++)
                    {
                        savedNetworkLoad[i] = new int[20];
                        for (int j = 0; j < 20; j++)
                        {
                            savedNetworkLoad[i][j] = Input.NetworkLoad[i][j];
                        }
                    }

                    Console.WriteLine("Increasing network load");
                    for (int m = 0; m < 10; m++)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < 20; j++)
                            {
                                Input.NetworkLoad[i][j] = (int)(Input.NetworkLoad[i][j] * 1.2);
                            }
                        }

                        Console.WriteLine(MeasureNetworkReliability());
                    }

                    Input.NetworkLoad = savedNetworkLoad;

                    var savedConnections = Input.Connections.Select(c => new Connection(c)).ToList();

                    Console.WriteLine("Decreasing connection capacity");
                    for (int m = 0; m < 10; m++)
                    {
                        foreach (var connection in Input.Connections)
                        {
                            connection.TransitionLoad = (int)(connection.TransitionLoad / 1.2);
                        }

                        Console.WriteLine(MeasureNetworkReliability());
                    }

                    Input.Connections = savedConnections.Select(c => c).ToList();


                    Console.WriteLine("Adding connections");
                    for (int m = 0; m < 10; m++)
                    {
                        Input.Connections.Add(GetRandomConnection());

                        Console.WriteLine(MeasureNetworkReliability());
                    }

                    Input.Connections = savedConnections.Select(c => c).ToList();
                }
            }
           

            Console.ReadKey();
        }

        private static float MeasureNetworkReliability()
        {
            Random random = new Random();
            int functioning = 0, failing = 0;
            for (int j = 0; j < 200; j++)
            {
                if(j==50 || j == 150)
                {


                }
                var connections = new List<Connection>(Input.Connections);

                foreach (var connection in connections)
                {
                    connection.ActualWorkload = 0;
                }

                for (int i = 0; i < connections.Count; i++)
                {
                    if (random.Next(100) < Input.FailProbability)
                    {
                        connections.RemoveAt(i);
                        i--;
                    }
                }

                if (networkValidator.CheckCohesion(connections) &&
                    networkValidator.CheckWorkload(connections) &&
                    networkValidator.CheckMaxLatency(connections))
                {
                    functioning++;
                }
                else
                {
                    failing++;
                }

            }

            return functioning / (float)(functioning + failing);
        }

        private static Connection GetRandomConnection()
        {
            Random random = new Random();

            var connection = new Connection();

            connection.Edges = new int[] { random.Next(20), random.Next(20) };
            while(connection.Edges[0] == connection.Edges[1])
            {
                connection.Edges[1] = random.Next(20);
            }

            connection.Latency = (int)Input.Connections.Average(c => c.Latency);
            connection.TransitionLoad = (int)Input.Connections.Average(c => c.TransitionLoad);

            return connection;
        }
    }
}
