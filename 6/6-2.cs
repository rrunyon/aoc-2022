namespace Solutions {
  class SixTwo {

    public static int Output() {
      return FindStartOfMessageMarker();
    }

    private static int FindStartOfMessageMarker() {
      char[] line = System.IO.File.ReadAllText(@"./6/input.txt").ToCharArray();
      HashSet<char> set = new HashSet<char>();

      for (var i = 0; i < line.Length - 14; i++) {
        for (var j = i; j < i + 14; j++) {
          set.Add(line[j]);
        }

        if (set.Count == 14) return i + 14;
        set.Clear();
      }

      return -1;
    }
  }
}
