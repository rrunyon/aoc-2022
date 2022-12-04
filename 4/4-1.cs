namespace Solutions {
  class FourOne {

    public static int Output() {
      return CountRedundantAssignments();
    }

    public static int CountRedundantAssignments() {
      string[] lines = System.IO.File.ReadAllText(@"./4/input.txt").Split("\n");
      int count = 0;
      
      foreach(var line in lines) {
        string[] assignments = line.Split(",");
        string left = assignments[0];
        string right = assignments[1];
        string[] leftRange = left.Split("-");
        string[] rightRange = right.Split("-");
        int leftStart = Int32.Parse(leftRange[0]);
        int leftEnd = Int32.Parse(leftRange[1]);
        int rightStart = Int32.Parse(rightRange[0]);
        int rightEnd = Int32.Parse(rightRange[1]);
        
        if ((leftStart <= rightStart && leftEnd >= rightEnd) ||
            (rightStart <= leftStart && rightEnd >= leftEnd)) {
          count++;
        }
      }

      return count;
    }
  }
}
