namespace Solutions {
  class ElevenOne {

    public static int Output() {
      string[] lines = System.IO.File.ReadAllText(@"./11/input.txt").Split("\n");
      List<Monkey> monkeys = new List<Monkey>();
      Monkey currentMonkey = new Monkey();

      foreach (var line in lines) {
        var firstWord = line.Trim().Split(" ")[0];

        switch(firstWord) {
          case "Monkey":
            currentMonkey = new Monkey();
            monkeys.Add(currentMonkey);
            break;
          case "Starting":
            currentMonkey.items = ParseItems(line);
            break;
          case "Operation:":
            currentMonkey.Operate = ParseOperation(line);
            break;
          case "Test:":
            currentMonkey.Test = ParseTest(line);
            break;
          case "If":
            if (line.Contains("true")) {
              currentMonkey.trueTestReceiver = int.Parse(line.Split(" ").Last());
            } else {
              currentMonkey.falseTestReceiver = int.Parse(line.Split(" ").Last());
            }
            break;
        }
      }

      Dictionary<int, int> inspectionCounts = new Dictionary<int, int>();

      for (var i = 0; i < 20; i++) {
        for (var j = 0; j < monkeys.Count; j++) {
          var monkey = monkeys[j];

          foreach (var item in monkey.items) {
            if (!inspectionCounts.ContainsKey(j)) inspectionCounts.Add(j, 0);
            inspectionCounts[j]++;

            int worryLevel = monkey.Operate(item);
            worryLevel /= 3;
            var receiver = monkey.Test(worryLevel) ? monkey.trueTestReceiver : monkey.falseTestReceiver;
            monkeys[receiver].items.Add(worryLevel);
          }

          monkey.items.Clear();
        }
      }

      List<int> counts = new List<int>();
      for (var i = 0; i < monkeys.Count; i++) {
        counts.Add(inspectionCounts[i]);
      }

      counts.Sort();

      return counts[counts.Count - 1] * counts[counts.Count - 2];
    }

    private static List<int> ParseItems(string line){
      List<int> list = new List<int>();
      string[] parsed = line.Split(":")[1].Split(", ");

      foreach (var number in parsed) {
        list.Add(int.Parse(number));
      }

      return list;
    }

    private static Func<int, int> ParseOperation(string line) {
      string operation = line.Split("=")[1].Trim();

      if (operation.Contains('+')) {
        int operand = int.Parse(operation.Split("+")[1]);
        return old => old + operand;
      } else if (operation.Contains('-')) {
        int operand = int.Parse(operation.Split("-")[1]);
        return old => old - operand;
      } else if (operation.Contains('*')) {
        string operand = operation.Split("*")[1];
        if (operand == " old") {
          return old => old * old;
        } else {
          return old => old * int.Parse(operand);
        }
      } else {
        int operand = int.Parse(operation.Split("/")[1]);
        return old => old - operand;
      }
    }

    private static Func<int, Boolean> ParseTest(string line) {
      int operand = int.Parse(line.Split(" ").Last());
      return input => input % operand == 0;
    }
  }

  class Monkey {
    public List<int> items = new List<int>();
    public int trueTestReceiver = -1;
    public int falseTestReceiver = -1;

    public Func<int, int> Operate = i => -1;
    public Func<int, Boolean> Test = i => true;
  }
}