using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025;

public class Puzzle8 : Puzzle
{
    public string[] Input { get; init; }

    public int MaxConnectionCount { get; init; }

    public int MaxCircuitCount { get; init; }

    public List<int> CircuitSizes { get; set; }

    private List<JunctionBox> _junctionBoxes { get; set; }

    private List<Connection> _connections { get; set; }

    private List<Circuit> _circuits { get; set; }

    public Puzzle8(string inputFileName, int connectionCount, int maxCircuitCount) : base(inputFileName)
    {
        Input = File.ReadAllLines(inputFileName);
        MaxConnectionCount = connectionCount;
        MaxCircuitCount = maxCircuitCount;
        CircuitSizes = new List<int>();
        _junctionBoxes = new List<JunctionBox>();
        _connections = new List<Connection>();
        _circuits = new List<Circuit>();
    }

    public Puzzle8(string inputFileName) : base(inputFileName)
    {
        Input = File.ReadAllLines(inputFileName);
        CircuitSizes = new List<int>();
        _junctionBoxes = new List<JunctionBox>();
        _connections = new List<Connection>();
        _circuits = new List<Circuit>();
    }

    protected override void SolveFirstPartInternal()
    {
        int idCounter = 1;

        foreach (string line in Input)
        {
            string[] coordinates = line.Split(',');
            JunctionBox newJunctionBox = new JunctionBox(
                int.Parse(coordinates[0]),
                int.Parse(coordinates[1]),
                int.Parse(coordinates[2]),
                idCounter);

            BuildConnectionsForJunctionBoxPartOne(newJunctionBox);
            _junctionBoxes.Add(newJunctionBox);
            idCounter++;
        }

        //PrintJunctionBoxes();
        //PrintConnections();

        _connections = _connections.Take(MaxConnectionCount).ToList();
        PrintConnections();

        BuildCircuits();
        PrintCircuits();

        foreach (var circuit in _circuits)
        {
            CircuitSizes.Add(circuit.JunctionBoxes.Count);
        }

        CircuitSizes.Sort();
        CircuitSizes = CircuitSizes.TakeLast(MaxCircuitCount).ToList();

        long result = 1;
        foreach (var circuitSize in CircuitSizes)
        {
            result *= circuitSize;
        }

        Console.WriteLine($"result={result}");
    }

    protected override void SolveSecondPartInternal()
    {
        int idCounter = 1;

        foreach (string line in Input)
        {
            string[] coordinates = line.Split(',');
            JunctionBox newJunctionBox = new JunctionBox(
                int.Parse(coordinates[0]),
                int.Parse(coordinates[1]),
                int.Parse(coordinates[2]),
                idCounter);

            BuildConnectionsForJunctionBoxPartTwo(newJunctionBox);
            _junctionBoxes.Add(newJunctionBox);
            idCounter++;

            if(idCounter % 10 == 0)
            {
                Console.WriteLine($"idCounter={idCounter}");
            }
        }

        Console.WriteLine($"junctionBoxes={_junctionBoxes.Count},connectionsCount={_connections.Count}");
        Console.ReadLine();
        
        BuildOneCircuit();

    }

    private void BuildConnectionsForJunctionBoxPartOne(JunctionBox newJunctionBox)
    {
        foreach (var junctionBox in _junctionBoxes)
        {
            Connection connection = new Connection(newJunctionBox, junctionBox);
            bool connectionAdded = false;
            for(int i = 0; i < _connections.Count; i++)
            {
                if (i > MaxConnectionCount)
                {
                    connectionAdded = true;
                    break;
                }

                if(connection.Distance <= _connections[i].Distance)
                {
                    _connections.Insert(i, connection);
                    connectionAdded = true;
                    break;
                }
            }

            if (!connectionAdded)
                _connections.Add(connection);
        }
    }

    private void BuildConnectionsForJunctionBoxPartTwo(JunctionBox newJunctionBox)
    {
        foreach (var junctionBox in _junctionBoxes)
        {
            Connection connection = new Connection(newJunctionBox, junctionBox);
            bool connectionAdded = false;
            for (int i = 0; i < _connections.Count; i++)
            {
                if (connection.Distance <= _connections[i].Distance)
                {
                    _connections.Insert(i, connection);
                    connectionAdded = true;
                    break;
                }
            }

            if (!connectionAdded)
                _connections.Add(connection);
        }
    }

    private void BuildCircuits()
    {
        foreach(var connection in _connections)
        {
            bool junctionBoxAFound = false;
            int circuitIndexA = 0;

            bool junctionBoxBFound = false;
            int circuitIndexB = 0;

            // Find circuits for connection
            for(int circuitIndex = 0; circuitIndex < _circuits.Count; circuitIndex++)
            {
                for(int junctionBoxIndex = 0; junctionBoxIndex < _circuits[circuitIndex].JunctionBoxes.Count;
                    junctionBoxIndex++)
                {
                    if(connection.JunctionBoxA.Id == _circuits[circuitIndex].JunctionBoxes[junctionBoxIndex].Id)
                    {
                        junctionBoxAFound = true;
                        circuitIndexA = circuitIndex;
                    }

                    if(connection.JunctionBoxB.Id == _circuits[circuitIndex].JunctionBoxes[junctionBoxIndex].Id)
                    {
                        junctionBoxBFound = true;
                        circuitIndexB = circuitIndex;
                    }

                    if(junctionBoxAFound && junctionBoxBFound)
                    {
                        break;
                    }
                }

                if (junctionBoxAFound && junctionBoxBFound)
                {
                    break;
                }
            }

            if (!junctionBoxAFound && !junctionBoxBFound)
            {
                Circuit newCircuit = new Circuit(connection);
                _circuits.Add(newCircuit);
                continue;
            }

            if (junctionBoxAFound && !junctionBoxBFound)
            {
                _circuits[circuitIndexA].JunctionBoxes.Add(connection.JunctionBoxB);
                continue;
            }

            if (!junctionBoxAFound && junctionBoxBFound)
            {
                _circuits[circuitIndexB].JunctionBoxes.Add(connection.JunctionBoxA);
                continue;
            }

            if (junctionBoxAFound && junctionBoxBFound)
            {
                if(circuitIndexA != circuitIndexB)
                {
                    _circuits[circuitIndexA].JunctionBoxes.AddRange(_circuits[circuitIndexB].JunctionBoxes);
                    _circuits.RemoveAt(circuitIndexB);
                }
                continue;
            }
        }
    }

    private void BuildOneCircuit()
    {
        foreach (var connection in _connections)
        {
            bool junctionBoxAFound = false;
            int circuitIndexA = 0;

            bool junctionBoxBFound = false;
            int circuitIndexB = 0;

            // Find circuits for connection
            for (int circuitIndex = 0; circuitIndex < _circuits.Count; circuitIndex++)
            {
                for (int junctionBoxIndex = 0; junctionBoxIndex < _circuits[circuitIndex].JunctionBoxes.Count;
                    junctionBoxIndex++)
                {
                    if (connection.JunctionBoxA.Id == _circuits[circuitIndex].JunctionBoxes[junctionBoxIndex].Id)
                    {
                        junctionBoxAFound = true;
                        circuitIndexA = circuitIndex;
                    }

                    if (connection.JunctionBoxB.Id == _circuits[circuitIndex].JunctionBoxes[junctionBoxIndex].Id)
                    {
                        junctionBoxBFound = true;
                        circuitIndexB = circuitIndex;
                    }

                    if (junctionBoxAFound && junctionBoxBFound)
                    {
                        break;
                    }
                }

                if (junctionBoxAFound && junctionBoxBFound)
                {
                    break;
                }
            }

            if (!junctionBoxAFound && !junctionBoxBFound)
            {
                Circuit newCircuit = new Circuit(connection);
                _circuits.Add(newCircuit);
                continue;
            }

            if (junctionBoxAFound && !junctionBoxBFound)
            {
                _circuits[circuitIndexA].JunctionBoxes.Add(connection.JunctionBoxB);

                if (_circuits[circuitIndexA].JunctionBoxes.Count == _junctionBoxes.Count)
                {
                    Console.WriteLine($"junctionBoxA={connection.JunctionBoxA}, junctionBoxB={connection.JunctionBoxB}");
                    Console.WriteLine($"result={connection.JunctionBoxA.X * connection.JunctionBoxB.X}");
                    break;
                }
                continue;
            }

            if (!junctionBoxAFound && junctionBoxBFound)
            {
                _circuits[circuitIndexB].JunctionBoxes.Add(connection.JunctionBoxA);

                if (_circuits[circuitIndexB].JunctionBoxes.Count == _junctionBoxes.Count)
                {
                    Console.WriteLine($"junctionBoxA={connection.JunctionBoxA}, junctionBoxB={connection.JunctionBoxB}");
                    Console.WriteLine($"result={connection.JunctionBoxA.X * connection.JunctionBoxB.X}");
                    break;
                }
                continue;
            }

            if (junctionBoxAFound && junctionBoxBFound)
            {
                if (circuitIndexA != circuitIndexB)
                {
                    _circuits[circuitIndexA].JunctionBoxes.AddRange(_circuits[circuitIndexB].JunctionBoxes);
                    if (_circuits[circuitIndexA].JunctionBoxes.Count == _junctionBoxes.Count && _circuits.Count == 2)
                    {
                        Console.WriteLine($"junctionBoxA={connection.JunctionBoxA}, junctionBoxB={connection.JunctionBoxB}");
                        Console.WriteLine($"result={connection.JunctionBoxA.X * connection.JunctionBoxB.X}");
                        break;
                    }

                    Console.WriteLine($"Merging circuits... New circuit size={_circuits[circuitIndexA].JunctionBoxes.Count}");

                    _circuits.RemoveAt(circuitIndexB);
                }
                continue;
            }
        }
    }

    private void FindCircuitsForConnection(Connection connection)
    {

    }

    private void PrintJunctionBoxes()
    {
        Console.WriteLine($"JunctionBoxes: {string.Join(',', _junctionBoxes)}");
    }



    private void PrintConnections()
    {
        Console.WriteLine("Connections:");
        foreach(var connection in _connections)
        {
            Console.WriteLine($"{connection}");
        }
    }

    private void PrintCircuits()
    {
        Console.WriteLine("Circuits:");
        foreach (var circuit in _circuits)
        {
            Console.WriteLine($"{circuit}");
        }
    }
}

public class JunctionBox
{
    public int Id { get; init; }

    public int X { get; init; }

    public int Y { get; init; }

    public int Z { get; init; }

    public JunctionBox(int x, int y, int z, int id)
    {
        X = x;
        Y = y;
        Z = z;
        Id = id;
    }

    public override string ToString()
    {
        return $"Id={Id}[{X},{Y},{Z}]";
    }
}

public class Connection
{
    public JunctionBox JunctionBoxA { get; init; }

    public JunctionBox JunctionBoxB { get; init; }

    public double Distance { get; init; }

    public Connection(JunctionBox a, JunctionBox b)
    {
        JunctionBoxA = a;
        JunctionBoxB = b;
        Distance = CountDistance();
    }

    private double CountDistance()
    {
        return Math.Sqrt(Math.Pow(JunctionBoxA.X - JunctionBoxB.X, 2) 
            + Math.Pow(JunctionBoxA.Y - JunctionBoxB.Y, 2)
            + Math.Pow(JunctionBoxA.Z - JunctionBoxB.Z, 2));
    }

    public override string ToString()
    {
        return $"{JunctionBoxA}---{JunctionBoxB}: {Distance}";
    }
}

public class Circuit
{
    public List<JunctionBox> JunctionBoxes { get; set; }

    public Circuit(Connection connection)
    {
        JunctionBoxes = new List<JunctionBox>();
        JunctionBoxes.Add(connection.JunctionBoxA);
        JunctionBoxes.Add(connection.JunctionBoxB);
    }

    public override string ToString()
    {
        string result = "Circuit: ";
        foreach (var junctionBox in JunctionBoxes)
        {
            result += $"{junctionBox},";
        }
        return result;
    }
}


