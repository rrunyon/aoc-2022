namespace Solutions {
  class NineOne {

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

    private static int CountVisitedCells() {
      string[] lines = System.IO.File.ReadAllText(@"./9/input.txt").Split("\n");
      var head = new int[]{1000, 0};
      var tail = new int[]{1000, 0};
      var visited = new HashSet<string>();
      visited.Add(tail[0] + " " + tail[1]);

      foreach (var line in lines) {
        var split = line.Split(" ");
        var direction = split[0];
        var units = int.Parse(split[1]);

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

          var updateTail = true;
          foreach (var adjacent in adjacentDirections) {
            var newX = head[0] + adjacent[0];
            var newY = head[1] + adjacent[1];

            if (tail[0] == newX && tail[1] == newY) {
              updateTail = false;
              break;
            }
          }

          if (!updateTail) continue;

          switch(direction) {
            case "U":
              tail[0] = head[0] + 1;
              tail[1] = head[1];
              break;
            case "D":
              tail[0] = head[0] - 1;
              tail[1] = head[1];
              break;
            case "L":
              tail[1] = head[1] + 1;
              tail[0] = head[0];
              break;
            case "R":
              tail[1] = head[1] - 1;
              tail[0] = head[0];
              break;
          }

          visited.Add(tail[0] + " " + tail[1]);
        }
      }

      return visited.Count;
    }
  }
}