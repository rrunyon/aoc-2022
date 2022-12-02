namespace Solutions {
  class TwoOne {

    public static int Output() {
      return evaluateStrategies();
    }

    public static int evaluateStrategies() {
      string text = System.IO.File.ReadAllText(@"./2/input.txt");
      int score = 0;

      string[] strategies = text.Split("\n");
      foreach (var strategy in strategies) {
        if (String.IsNullOrEmpty(strategy)) continue;
        System.Console.WriteLine(strategy);

        string[] moves = strategy.Split(" ");
        string opponentMove = moves[0];
        string playerMove = moves[1];

        HashSet<string> winningCombos = new HashSet<string>();
        winningCombos.Add("A Y");
        winningCombos.Add("B Z");
        winningCombos.Add("C X");

        Dictionary<string, int> moveScores = new Dictionary<string, int>();
        moveScores.Add("X", 1);
        moveScores.Add("Y", 2);
        moveScores.Add("Z", 3);

        Dictionary<string, string> moveMap = new Dictionary<string, string>();
        moveMap.Add("A", "X");
        moveMap.Add("B", "Y");
        moveMap.Add("C", "Z");

        score += moveScores[playerMove];

        if (winningCombos.Contains(strategy)) {
          score += 6;
        } else if (moveMap[opponentMove] == playerMove) {
          score += 3;
        }

        System.Console.WriteLine(score);
      }

      return score;
    }
  }
}
