namespace Solutions {
  class EightTwo {

    public static int Output() {
      return GetHighestScenicScore();
    }

    private static int GetHighestScenicScore() {
      string[] lines = System.IO.File.ReadAllText(@"./8/input.txt").Split("\n");
      int[][] treeHeights = new int[lines.Length][];

      for (var i = 0; i < lines.Length; i++) {
        char[] line = lines[i].ToArray();
        treeHeights[i] = new int[line.Length];
        for (var j = 0; j < lines[i].Length; j++) {
          treeHeights[i][j] = int.Parse(line[j].ToString());
        }
      }

      int maxScenicScore = -1;

      for (var i = 0; i < treeHeights.Length; i++) {
        for (var j = 0; j < treeHeights[i].Length; j++) {
          int currentHeight = treeHeights[i][j];

          // Right
          int rightScore = j < treeHeights[i].Length - 1 ? 1 : 0;
          int newJ = j + 1;
          while (newJ < treeHeights[i].Length - 1 && treeHeights[i][newJ] < currentHeight) {
            rightScore++;
            newJ++;
          }

          // Left
          int leftScore = j > 0 ? 1 : 0;
          newJ = j - 1;
          while (newJ > 0 && treeHeights[i][newJ] < currentHeight) {
            leftScore++;
            newJ--;
          }

          // Down
          int downScore = i < treeHeights.Length - 1 ? 1 : 0;
          int newI = i + 1;
          while (newI < treeHeights.Length - 1 && treeHeights[newI][j] < currentHeight) {
            downScore++;
            newI++;
          }

          // Up
          int upScore = i > 0 ? 1 : 1;
          newI = i - 1;
          while (newI > 0 && treeHeights[newI][j] < currentHeight) {
            upScore++;
            newI--;
          }

          maxScenicScore = Math.Max(maxScenicScore, rightScore * leftScore * downScore * upScore);
        }
      }

      return maxScenicScore;
    }
  }
}