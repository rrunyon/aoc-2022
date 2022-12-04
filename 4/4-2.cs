namespace Solutions {
  class FourTwo {

    public static int Output() {
      return CountOverlappingAssignments();
    }

    public static int CountOverlappingAssignments() {
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
        
        if (leftStart <= rightStart) {
          if (leftEnd >= rightStart) count++;
        } else {
          if (rightEnd >= leftStart) count++;
        }
      }

      return count;
    }
  }
}
