namespace Solutions {
  class EightOne {

    public static int Output() {
      return CountVisibleTrees();
    }

    private static int CountVisibleTrees() {
      string[] lines = System.IO.File.ReadAllText(@"./8/input.txt").Split("\n");
      int lineLength = lines[0].Length;
      HashSet<string> visibleTrees = new HashSet<string>();
     
      int maxHeight = -1;

      // Left
      for (var i = 0; i < lines.Length; i++) {
        char[] line = lines[i].ToArray();
        for (var j = 0; j < lineLength; j++) {
          int currentHeight = Convert.ToInt16(line[j]);
          if (currentHeight > maxHeight) {
            visibleTrees.Add(string.Join(" ", i, j));
          }
          maxHeight = Math.Max(maxHeight, currentHeight);
        }
        maxHeight = -1;
      }


      // Right
      for (var i = 0; i < lines.Length; i++) {
        char[] line = lines[i].ToArray();
        for (var j = lineLength - 1; j >= 0; j--) {
          int currentHeight = Convert.ToInt16(line[j]);
          if (currentHeight > maxHeight) {
            visibleTrees.Add(string.Join(" ", i, j));
          }
          maxHeight = Math.Max(maxHeight, currentHeight);
        }
        maxHeight = -1;
      }

      // Down
      for (var i = 0; i < lineLength; i++) {
        for (var j = 0; j < lines.Length; j++) {
          char[] line = lines[j].ToArray();
          int currentHeight = Convert.ToInt16(line[i]);
          if (currentHeight > maxHeight) {
            visibleTrees.Add(string.Join(" ", j, i));
          }
          maxHeight = Math.Max(maxHeight, currentHeight);
        }
        maxHeight = -1;
      }

      // Up
      for (var i = 0; i < lineLength; i++) {
        for (var j = lines.Length - 1; j >= 0; j--) {
          char[] line = lines[j].ToArray();
          int currentHeight = Convert.ToInt16(line[i]);
          if (currentHeight > maxHeight) {
            visibleTrees.Add(string.Join(" ", j, i));
          }
          maxHeight = Math.Max(maxHeight, currentHeight);
        }
        maxHeight = -1;
      }

      return visibleTrees.Count;
    }
  }
}