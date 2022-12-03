namespace Solutions {
  class ThreeTwo {

    public static int Output() {
      return SumPrioritiesOfSharedItems();
    }

    public static int SumPrioritiesOfSharedItems() {
      string[] lines = System.IO.File.ReadAllText(@"./3/input.txt").Split("\n");
      int sum = 0;
      
      for (var i = 0; i < lines.Length; i += 3) {
        HashSet<char> intersection = new HashSet<char>(lines[i]);
        for (var j = i + 1; j < i + 3; j++) {
          intersection.IntersectWith(new HashSet<char>(lines[j]));
        }

        char sharedItem = intersection.First();

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
