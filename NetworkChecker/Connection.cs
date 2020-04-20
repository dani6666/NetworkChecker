using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkChecker
{
    public class Connection
    {
        public int[] Edges { get; set; }
        public int Latency { get; set; }
        public int TransitionLoad { get; set; }
        public int ActualWorkload { get; set; }

        public Connection()
        {

        }

        public Connection(Connection connection)
        {
            Edges = connection.Edges;
            Latency = connection.Latency;
            TransitionLoad = connection.TransitionLoad;
            ActualWorkload = connection.ActualWorkload;
        }
    }
}
