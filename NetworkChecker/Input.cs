using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkChecker
{
    public static class Input
    {
        public static int[][] NetworkLoad { get; set; } = new int[][]
        {
            new int[20]{4,2,6,8,9,6,3,2,4,7,8,9,4,5,7,8,9,6,3,1},
            new int[20]{1,6,9,2,3,2,4,6,1,1,3,8,6,1,4,9,3,7,1,2},
            new int[20]{4,1,1,1,1,3,3,2,3,7,8,1,4,2,5,1,2,1,3,8},
            new int[20]{9,4,3,4,9,4,9,7,2,1,4,2,9,8,7,5,3,2,4,7},
            new int[20]{6,5,9,2,8,1,8,8,9,6,5,5,7,9,4,8,6,8,2,5},
            new int[20]{1,3,7,9,5,2,5,3,7,2,6,6,6,6,3,2,5,9,6,2},
            new int[20]{3,9,8,8,6,7,6,1,6,4,2,2,5,4,9,4,7,5,8,3},
            new int[20]{7,7,5,7,7,8,7,3,3,5,3,3,4,7,1,9,1,2,9,7},
            new int[20]{3,8,6,4,1,9,3,9,4,9,5,8,3,5,7,7,2,3,6,9},
            new int[20]{5,5,4,6,3,5,1,8,1,7,6,1,1,2,2,4,8,4,2,0},
            new int[20]{9,6,2,5,4,6,2,6,6,8,9,1,2,1,7,2,4,1,1,3},
            new int[20]{1,1,1,1,5,7,6,7,7,1,5,3,8,3,6,5,3,7,5,2},
            new int[20]{3,2,3,3,6,8,4,4,3,4,1,2,9,0,2,7,4,9,4,4},
            new int[20]{4,3,7,8,1,1,3,2,2,2,2,5,3,3,1,8,6,9,8,3},
            new int[20]{1,6,9,7,2,2,8,1,5,3,4,3,2,1,8,4,1,8,7,6},
            new int[20]{7,9,5,2,7,9,6,3,1,8,5,8,6,7,3,5,3,4,6,5},
            new int[20]{8,7,6,4,8,3,7,5,2,6,6,9,5,9,9,1,9,2,9,9},
            new int[20]{9,2,2,2,3,4,9,2,9,5,8,6,8,8,2,2,8,0,5,7},
            new int[20]{4,2,4,1,4,1,3,9,8,7,2,5,7,5,4,8,5,1,4,8},
            new int[20]{1,2,6,8,9,6,1,7,6,4,1,7,1,2,1,9,7,4,3,1}
        };
        public static float MaxAverageLatency { get; set; } = 80;
        public static int SumOfTranstions { get; set; } = 2500000;
        public static int FailProbability { get; set; } = 13;
        public static List<Connection> Connections { get; set; } = new List<Connection>();

        static Input()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    NetworkLoad[i][j] = NetworkLoad[i][j] * 100;
                }
            }
        }
    }
}
