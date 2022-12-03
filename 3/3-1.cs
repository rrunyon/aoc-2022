namespace Solutions {
  class ThreeOne {

    public static int Output() {
      return SumPrioritiesOfSharedItems();
    }

    public static int SumPrioritiesOfSharedItems() {
      string[] lines = System.IO.File.ReadAllText(@"./3/input.txt").Split("\n");
      int sum = 0;
      
      foreach(var line in lines) {
        int length = line.Length;
        char[] lineArray = line.ToCharArray();
        ArraySegment<char> sack = new ArraySegment<char>(line.ToCharArray());
        
        ArraySegment<char> left = new ArraySegment<char>(lineArray).Slice(0, length / 2);
        ArraySegment<char> right = new ArraySegment<char>(lineArray).Slice(length / 2, length / 2);
        HashSet<char> leftSet = new HashSet<char>(left);
        HashSet<char> rightSet = new HashSet<char>(right);
        leftSet.IntersectWith(rightSet);
        char sharedItem = leftSet.First();

        if (Char.IsLower(sharedItem)) {
          sum += sharedItem - 'a' + 1;
        } else {
          sum += sharedItem - 'A' + 27;
        }
      }

      return sum;
    }
  }
}
