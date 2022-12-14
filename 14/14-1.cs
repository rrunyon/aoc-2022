using System.Numerics;

namespace Solutions {

  class FourteenOne {
    public static int Output() {
      return CountRestingSand();
    }

    public static int minDepth = int.MaxValue;
    public static int minWidth = int.MaxValue;
    public static int maxDepth = 0;
    public static int maxWidth = 0;

    public static int[][] dirs = {
      new int[]{ 1, 0 },
      new int[]{ 1, -1 },
      new int[]{ 1, 1 },
    };

    public static int CountRestingSand() {
      string[] lines = System.IO.File.ReadAllText(@"./14/input.txt").Split("\n");
      var rockPaths = getRockPaths(lines);
      var cave = buildCave(rockPaths);

      int grains = 0;
      var currentGrain = new int[]{ 0, 500 };

      while (true) {
        var keepFalling = false;
        foreach (var dir in dirs) {
          var newI = currentGrain[0] + dir[0];
          var newJ = currentGrain[1] + dir[1];

          if (newI == cave.Length) return grains;

          if (cave[newI][newJ] == '.') {
            cave[currentGrain[0]][currentGrain[1]] = '.';
            currentGrain = new int[]{ newI, newJ };
            keepFalling = true;
            break;
          }
        }
        if (!keepFalling) {
          grains++;
          cave[currentGrain[0]][currentGrain[1]] = 'o';
          currentGrain = new int[]{ 0, 500 };
          //Print(cave);
        }
      }
    }

    public static List<List<int[]>> getRockPaths(string[] lines) {
      List<List<int[]>> rockPaths = new List<List<int[]>>();
      List<int[]> path = new List<int[]>();

      foreach (var line in lines) {
        var split = line.Split(" -> ");

        foreach (var pair in split) {
          var plot = pair.Split(",");
          path.Add(new int[]{ int.Parse(plot[1]), int.Parse(plot[0]) });
        }

        rockPaths.Add(path);
        path = new List<int[]>();
      }

      return rockPaths;
    }

    public static char[][] buildCave(List<List<int[]>> rockPaths) {
      foreach (var path in rockPaths) {
        foreach (var plot in path) {
          minDepth = Math.Min(minDepth, plot[0]);
          maxDepth = Math.Max(maxDepth, plot[0] + 1);
          minWidth = Math.Min(minWidth, plot[1]);
          maxWidth = Math.Max(maxWidth, plot[1] + 1);
        }
      }

      char[][] cave = new char[maxDepth][];
      for (var i = 0; i < cave.Length; i++) {
        cave[i] = new char[maxWidth];
        for (var j = 0; j < cave[0].Length; j++) {
          cave[i][j] = '.';
        }
      }

      foreach (var path in rockPaths) {
        for (var i = 1; i < path.Count; i++ ) {
          var prevPlot = path[i - 1];
          var plot = path[i];

          // Moving right
          if (prevPlot[1] < plot[1]) {
            for (var j = prevPlot[1]; j <= plot[1]; j++) {
              cave[plot[0]][j] = '#';
            }
          // Moving left
          } else if (prevPlot[1] > plot[1]) {
            for (var j = prevPlot[1]; j >= plot[1]; j--) {
              cave[plot[0]][j] = '#';
            }
          // Moving down
          } else if (prevPlot[0] < plot[0]) {
          for (var j = prevPlot[0]; j <= plot[0]; j++) {
            cave[j][plot[1]] = '#';
          }
          // Moving up
          } else if (prevPlot[0] > plot[0]) {
            for (var j = prevPlot[0]; j >= plot[0]; j--) {
              cave[j][plot[1]] = '#';
            }
          }
        }
      }

      return cave;
    }

    private static void Print(char[][] cave) {
      System.Console.WriteLine();
      System.Console.WriteLine("----------------------------------------");
      for (var i = 0; i < maxDepth; i++) {
        var row = cave[i];
        for (var j = minWidth - 1; j < maxWidth; j++) {
          System.Console.Write(row[j]);
        }
        System.Console.WriteLine();
      }
    }
  }
}