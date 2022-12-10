namespace Solutions {
  class TenTwo {

    public static void Output() {
      RenderPixels();
    }

    private static int cycles = 0;
    private static int register = 1;
    private static List<string> pixels = new List<string>();

    private static void Tick() {
      cycles++;

      var position = cycles % 40;
      if (position == register || position == register + 1 || position == register + 2) {
        pixels.Add("#");
      } else {
        pixels.Add(".");
      }
    }

    private static void RenderPixels() {
      string[] lines = System.IO.File.ReadAllText(@"./10/input.txt").Split("\n");

      foreach (var command in lines) {
        Tick();

        if (command != "noop") {
          Tick();
          int increment = int.Parse(command.Split(" ")[1]);
          register += increment;
        }
      }

      for (var i = 0; i < pixels.Count; i += 40) {
        System.Console.WriteLine(String.Join("", new ArraySegment<string>(pixels.ToArray()).Slice(i, 40)));
      }
    }
  }
}