using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkChecker
{
    public class NetworkValidator
    {
        private List<Connection> _connections;
        public bool CheckCohesion(List<Connection> connections)
        {
            _connections = connections;
            for (int i = 0; i < 20; i++)
            {
                for (int j = i; j < 20; j++)
                {
                    var path = TryToConnect(i, j, new List<int>());
                    if (path == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckWorkload(List<Connection> connections)
        {
            _connections = connections;
            List<Connection> connectionsWorkload = new List<Connection>(_connections);

            for (int i = 0; i < 20; i++)
            {
                for (int j = i; j < 20; j++)
                {
                    var path = TryToConnect(i, j, new List<int>());
                    for (int c = 1; c < path.Count; c++)
                    {
                        connectionsWorkload
                            .First(con => con.Edges.Contains(path[c - 1]) && con.Edges.Contains(path[c])).ActualWorkload += Input.NetworkLoad[i][j] + Input.NetworkLoad[j][i];
                    }
                }
            }

            return connectionsWorkload.All(c => c.TransitionLoad > c.ActualWorkload);
        }

        public bool CheckMaxLatency(List<Connection> connections)
        {
            _connections = connections;
            float connectionsSum = 0f;
            //Dictionary<Connection, int> connectionWorkLoads = _connections.ToDictionary(c => c, c => 0);
            //for (int i = 0; i < 20; i++)
            //{
            //    for (int j = i; j < 20; j++)
            //    {
            //        var path = TryToConnect(i, j, new List<int>());
            //        for (int c = 1; c < path.Count; c++)
            //        {
            //            /*connectionWorkLoads[*/_connections
            //                .First(con => con.Edges.Contains(path[c - 1]) && con.Edges.Contains(path[c])).TransitionLoad -=
            //                Input.NetworkLoad[i][j] + Input.NetworkLoad[j][i];
            //        }
            //    }
            //}

            foreach (var connection in _connections)
            {
                connectionsSum += connection.ActualWorkload / (float)(connection.TransitionLoad - connection.ActualWorkload);
            }

            float sumOfTransmissions = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = i; j < 20; j++)
                {
                    sumOfTransmissions += Input.NetworkLoad[i][j] + Input.NetworkLoad[j][i];
                }
            }

            return connectionsSum / sumOfTransmissions < Input.MaxAverageLatency;
        }

        private List<int> TryToConnect(int start, int end, List<int> path)
        {
            if (start == end)
                return path;

            var paths = new List<List<int>>();
            foreach (var connection in _connections)
            {
                if (connection.Edges[0] == start && !path.Contains(connection.Edges[1]))
                {
                    paths.Add(TryToConnect(connection.Edges[1], end, CopyAndExtendList(path, connection.Edges[1])));
                }
                else if (connection.Edges[1] == start && !path.Contains(connection.Edges[0]))
                {
                    paths.Add(TryToConnect(connection.Edges[0], end, CopyAndExtendList(path, connection.Edges[0])));
                }
            }

            if (paths.Count == 0)
                return null;

            var minPathLength = paths.Min(p => p?.Count ?? int.MaxValue);

            return paths.FirstOrDefault(p => p != null && p.Count == minPathLength);
        }

        private List<int> CopyAndExtendList(List<int> source, int newElement)
        {
            var newList = new List<int>(source);
            newList.Add(newElement);
            return newList;
        }
    }
}
