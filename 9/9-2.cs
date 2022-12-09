namespace Solutions {
  class NineTwo {

    public static int Output() {
      return CountVisitedCells();
    }

    private static int[][] adjacentDirections = new int[][]{
      new int[]{0, 0},
      new int[]{0, 1},
      new int[]{1, 0},
      new int[]{0, -1},
      new int[]{-1, 0},
      new int[]{1, 1},
      new int[]{-1, 1},
      new int[]{1, -1},
      new int[]{-1, -1},
    };

    private static void Print(int[][] knots) {
      var graph = new string[1000][];
      for (var i = 0; i < graph.Length; i++) {
        string[] row = new string[1000];
        for (var j = 0; j < row.Length; j++) {
          row[j] = ".";
        }
        graph[i] = row;
      }

      for (var i = 0; i < knots.Length; i++) {
        if (graph[knots[i][0]][knots[i][1]] != ".") continue;

        graph[knots[i][0]][knots[i][1]] = i == 0 ? "H" : i.ToString();
      }

      for (var i = 0; i < graph.Length; i++) {
        System.Console.WriteLine(String.Join("", graph[i]));
      }
      System.Console.WriteLine("------------------------------------------------------------------");
    }

    private static int CountVisitedCells() {
      string[] lines = System.IO.File.ReadAllText(@"./9/input.txt").Split("\n");
      var knots = new int[10][];
      for (var i = 0; i < 10; i++) knots[i] = new int[]{500, 500};
      var visited = new HashSet<string>();
      visited.Add(knots[9][0] + " " + knots[9][1]);

      foreach (var line in lines) {
        var split = line.Split(" ");
        var direction = split[0];
        var units = int.Parse(split[1]);
        var head = knots[0];

        for (var i = 0; i < units; i++) {
          switch(direction) {
            case "U":
              head[0]--;
              break;
            case "D":
              head[0]++;
              break;
            case "L":
              head[1]--;
              break;
            case "R":
              head[1]++;
              break;
          }

          for (var j = 1; j < knots.Length; j++) {
            // this was previously reusing the 'head' var name and was causing strange memory overlap issues
            // rename to prev, avoid potential name collision
            var prev = knots[j-1];
            var tail = knots[j];
            var updateTail = true;
            foreach (var adjacent in adjacentDirections) {
              var newX = prev[0] + adjacent[0];
              var newY = prev[1] + adjacent[1];

              if (tail[0] == newX && tail[1] == newY) {
                updateTail = false;
                break;
              }
            }

            if (!updateTail) continue;

            // if we are in the same row or column, just increment/decrement the position by one to reattach
            if (prev[0] == tail[0]) {
              if (tail[1] < prev[1]) {
                tail[1]++;
              } else {
                tail[1]--;
              }
            } else if (prev[1] == tail[1]) {
              if (tail[0] < prev[0]) {
                tail[0]++;
              } else {
                tail[0]--;
              }
            // if we are not in the same row or column, move diagonally one position to catch up
            } else {
              if (prev[0] > tail[0]) {
                tail[0]++;
              } else {
                tail[0]--;
              }

              if (prev[1] > tail[1]) {
                tail[1]++;
              } else {
                tail[1]--;
              }
            }

            visited.Add(knots[9][0] + " " + knots[9][1]);
          }

          /*
            // For debugging
            System.Console.WriteLine(line);
            System.Console.WriteLine("Move: " + (i + 1));
            Print(knots);
          */
        }
      }

      return visited.Count;
    }
  }
}