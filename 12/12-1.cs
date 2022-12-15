using System.Numerics;

namespace Solutions {

  class TwelveOne {
    public static int Output() {
      return GetShortestPathLength();
    }

    public static int[][] dirs = {
      new int[]{ 0, 1 },
      new int[]{ 1, 0 },
      new int[]{ 0, -1 },
      new int[]{ -1, 0 },
    };

    public static int GetShortestPathLength() {
      string[] lines = System.IO.File.ReadAllText(@"./12/input.txt").Split("\n");
      char[][] graph = new char[lines.Length][];
      for (var i = 0; i < lines.Length; i++) graph[i] = lines[i].ToCharArray();

      var shortest = int.MaxValue;
      
      int depth = 0;
      Queue<int[]> queue = new Queue<int[]>();
      for (var i = 0; i < graph.Length; i++) {
        for (var j = 0; j < graph[0].Length; j++) {
          if (graph[i][j] == 'S') {
            queue.Enqueue(new int[]{ i, j });
            break;
          }
        }
        if (queue.Count > 0) break;
      }
      HashSet<string> visited = new HashSet<string>();
      visited.Add(0 + " " + 0);

      while (queue.Count > 0) {
        var size = queue.Count;

        for (var i = 0; i < size; i++) {
          var position = queue.Dequeue();
          var x = position[0];
          var y = position[1];
          var cell = graph[x][y];
          if (cell == 'S') cell = 'a';
          if (cell == 'E') return depth;

          foreach (var dir in dirs) {
            var newX = x + dir[0];
            var newY = y + dir[1];
            var newKey = newX + " " + newY;

            if (newX >= 0 && newX < graph.Length && newY >= 0 && newY < graph[0].Length && !visited.Contains(newKey)) {
              var newCell = graph[newX][newY];
              if (newCell == 'E') newCell = 'z';

              if (newCell - cell <= 1) {
                visited.Add(newKey);
                queue.Enqueue(new int[]{ newX, newY });
              }
            }
          }
        }
        
        depth++;
      }

      return -1;
    }
  }
}