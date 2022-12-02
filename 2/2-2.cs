namespace Solutions {
  class TwoTwo {

    public static int Output() {
      return EvaluateStrategies();
    }

    public static int EvaluateStrategies() {
      string text = System.IO.File.ReadAllText(@"./2/input.txt");
      int score = 0;

      string[] strategies = text.Split("\n");
      foreach (var strategy in strategies) {
        if (String.IsNullOrEmpty(strategy)) continue;

        Dictionary<string, int> scoreFromStrategy = new Dictionary<string, int>();
        scoreFromStrategy.Add("A X", 3);
        scoreFromStrategy.Add("A Y", 4);
        scoreFromStrategy.Add("A Z", 8);

        scoreFromStrategy.Add("B X", 1);
        scoreFromStrategy.Add("B Y", 5);
        scoreFromStrategy.Add("B Z", 9);

        scoreFromStrategy.Add("C X", 2);
        scoreFromStrategy.Add("C Y", 6);
        scoreFromStrategy.Add("C Z", 7);

        score += scoreFromStrategy[strategy];
      }

      return score;
    }
  }
}
