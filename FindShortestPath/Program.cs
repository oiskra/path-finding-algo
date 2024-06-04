using FindShortestPath;

string[] lines = new string[]
{
    "..###.....#####.....#####",
    "..###.....#####.....#####",
    "..###.....#####.....#####",
    "...........###......#####",
    "......###...........#####",
    "...#..###..###......#####",
    "###############.#######..",
    "###############.#######.#",
    "###############.#######..",
    "................########.",
    "................#######..",
    "...#######..##.......##.#",
    "...#######..##.......##..",
    "...##...........########.",
    "#####...........#######..",
    "#####...##..##..#...#...#",
    "#####...##..##....#...#.."
};

string[] newLines = new string[]
{
    ".########........########",
    "........#........#......#",
    "#######.#..####..#.####.#",
    "#.#...#.#..#..#..#......#",
    "#.#.#.#.####..####..#####",
    "#.#.#.#........#.........",
    "#.#.#.########.#####.####",
    "#.#................#.....",
    "#.######.##############.#",
    "#.#....#................#",
    "#.#.##.###########.######",
    "#...##.....#...#.......##",
    "######.....#.#.#..##.####",
    ".....#..###...#..##.#....",
    ".....#.#######.#..##.####",
    ".....#...........##......",
    "########.#######....####."
};


GraphMatrix<(int, int)> CreateGraphFromInput(string[] input) 
{
    List<(int, int)> vertices = new();
    int rows = input.Length; 
    int cols = input[0].Length; 

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            vertices.Add((i, j));
        }
    }

    var graph = new GraphMatrix<(int, int)>(vertices);

    foreach (var v in vertices)
    {
        int x = v.Item1;
        int y = v.Item2;

        if (input[x][y] == '#') 
            continue;

        if (y + 1 < cols && input[x][y + 1] != '#')
            graph.AddEdge((x, y), (x, y + 1));

        if (x + 1 < rows && input[x + 1][y] != '#')
            graph.AddEdge((x, y), (x + 1, y));
    }

    return graph;
}


try
{
    int rows = lines.Length;
    int cols = lines[0].Length;
    var g = CreateGraphFromInput(lines);
    var path = Algo.FindShortestPath<(int, int)>(g, (0, 0), (rows - 1, cols - 1)) ?? throw new Exception("Path not found");

    int counter = 0;
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (path.Contains((i, j)))
                Console.Write('o');
            else
                Console.Write(lines[i][j]);

            counter++;
        }
        Console.WriteLine();
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}









