using System.Numerics;

namespace Solutions {

  class ElevenOne {
    public static Int128 Output() {
      return Eleven.Solve(20, true);
    }
  }

  class Eleven {

    private static int divisorsProduct = 1;

    public static Int128 Solve(int rounds, Boolean divideWorries) {
      string[] lines = System.IO.File.ReadAllText(@"./11/input.txt").Split("\n");
      // Reset between test runs
      divisorsProduct = 1;
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

      Dictionary<int, Int128> inspectionCounts = new Dictionary<int, Int128>();

      for (var i = 0; i < rounds; i++) {
        for (var j = 0; j < monkeys.Count; j++) {
          var monkey = monkeys[j];

          foreach (var item in monkey.items) {
            if (!inspectionCounts.ContainsKey(j)) inspectionCounts.Add(j, 0);
            inspectionCounts[j]++;

            Int128 worryLevel = monkey.Operate(item);
            if (divideWorries) {
              worryLevel /= 3;
            } else {
              worryLevel %= divisorsProduct;
            }
            var receiver = monkey.Test(worryLevel) ? monkey.trueTestReceiver : monkey.falseTestReceiver;
            monkeys[receiver].items.Add(worryLevel);
          }

          monkey.items.Clear();
        }
      }

      List<Int128> counts = new List<Int128>();
      for (var i = 0; i < monkeys.Count; i++) {
        counts.Add(inspectionCounts[i]);
      }

      counts.Sort();

      return counts[counts.Count - 1] * counts[counts.Count - 2];
    }

    private static List<Int128> ParseItems(string line){
      List<Int128> list = new List<Int128>();
      string[] parsed = line.Split(":")[1].Split(", ");

      foreach (var number in parsed) {
        list.Add(Int128.Parse(number));
      }

      return list;
    }

    private static Func<Int128, Int128> ParseOperation(string line) {
      string operation = line.Split("=")[1].Trim();

      if (operation.Contains('+')) {
        var operand = int.Parse(operation.Split("+")[1]);
        return old => old + operand;
      } else if (operation.Contains('-')) {
        var operand = int.Parse(operation.Split("-")[1]);
        return old => old - operand;
      } else if (operation.Contains('*')) {
        string operand = operation.Split("*")[1];
        if (operand == " old") {
          return old => old * old;
        } else {
          return old => old * int.Parse(operand);
        }
      } else {
        var operand = int.Parse(operation.Split("/")[1]);
        return old => old - operand;
      }
    }

    private static Func<Int128, Boolean> ParseTest(string line) {
      int operand = int.Parse(line.Split(" ").Last());
      divisorsProduct *= operand;
      return input => input % operand == 0;
    }
  }

  class Monkey {
    public List<Int128> items = new List<Int128>();
    public int trueTestReceiver = -1;
    public int falseTestReceiver = -1;
    public int testOperand = -1;

    public Func<Int128, Int128> Operate = i => 1;
    public Func<Int128, Boolean> Test = i => true;
  }
}