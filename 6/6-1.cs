namespace Solutions {
  class SixOne {

    public static int Output() {
      return FindStartOfPacketMarker();
    }

    private static int FindStartOfPacketMarker() {
      char[] line = System.IO.File.ReadAllText(@"./6/input.txt").ToCharArray();
      HashSet<char> set = new HashSet<char>();

      for (var i = 0; i < line.Length - 4; i++) {
        for (var j = i; j < i + 4; j++) {
          set.Add(line[j]);
        }

        if (set.Count == 4) return i + 4;
        set.Clear();
      }

      return -1;
    }
  }
}
