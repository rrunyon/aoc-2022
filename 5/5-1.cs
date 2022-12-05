namespace Solutions {
  class FiveOne {

    public static string Output() {
      return PeekStacks();
    }

    private static string PeekStacks() {
      string[] lines = System.IO.File.ReadAllText(@"./5/input.txt").Split("\n");
      List<Stack<char>> stacks = ParseStacks(lines);
      List<int[]> commands = ParseCommands(lines);

      foreach(var command in commands) {
        int count = command[0];
        int from = command[1];
        int to = command[2];

        for (var i = 0; i < count; i++) {
          stacks[to].Push(stacks[from].Pop());
        }
      }

      string result = "";
      foreach(var stack in stacks) {
        if (stack.Count > 0) result += stack.Peek();
      }
      return result;
    }

    private static List<Stack<char>> ParseStacks(string[] lines) {
      List<Stack<char>> stacks = new List<Stack<char>>();
      for (var i = 0; i < 9; i++) stacks.Add(new Stack<char>());

      for (var i = 0; i < 9; i++) {
        char[] line = lines[i].ToCharArray();
        int stackNumber = 0;
        for (var j = 0; j < line.Length; j += 4) {
          if (line[j] != ' ') {
            stacks[stackNumber].Push(line[j+1]);
          }
          stackNumber++;
        }
      }

      for (var i = 0; i < stacks.Count; i++) stacks[i] = new Stack<char>(stacks[i]);
      return stacks;
    }

    private static List<int[]> ParseCommands(string[] lines) {
      List<int[]> commands = new List<int[]>();

      for (var i = 10; i < lines.Length; i++) {
        string[] parsedCommand = lines[i].Split(" ");
        int[] command = new int[3];
        command[0] = int.Parse(parsedCommand[1]);
        command[1] = int.Parse(parsedCommand[3]) - 1;
        command[2] = int.Parse(parsedCommand[5]) - 1;
        commands.Add(command);
      }

      return commands;
    }
  }
}
