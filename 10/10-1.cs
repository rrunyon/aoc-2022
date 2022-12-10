namespace Solutions {
  class TenOne {

    public static int Output() {
      return SumSignalStrengths();
    }

    private static int cycles = 0;

    private static int register = 1;

    private static List<int> signalStrengths = new List<int>();

    private static void Tick() {
      cycles++;
      signalStrengths.Add(register * cycles);
    }

    private static int SumSignalStrengths() {
      string[] lines = System.IO.File.ReadAllText(@"./10/input.txt").Split("\n");

      foreach (var command in lines) {
        Tick();

        if (command != "noop") {
          Tick();
          int increment = int.Parse(command.Split(" ")[1]);
          register += increment;
        }
      }

      return signalStrengths[19] + signalStrengths[59] + signalStrengths[99] + signalStrengths[139] + signalStrengths[179] + signalStrengths[219];
    }
  }
}