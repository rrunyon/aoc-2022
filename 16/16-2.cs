using System.Numerics;

namespace Solutions {

  class SixteenTwo {
    public static int Output() {
      return Solution();
    }

    public static int Solution() {
      string[] lines = System.IO.File.ReadAllText(@"./16/input.txt").Split("\n");
      Dictionary<string, Node> graph = ParseInput(lines);

      var maxPressure = int.MinValue;
      HashSet<string> memo = new HashSet<string>();
      HashSet<string> openValves = new HashSet<string>();
      Dfs(1, 0, graph["AA"], graph["AA"]);
      return maxPressure;

      void Dfs(int minute, int currentPressure, Node currentValve, Node elephantValve) {
        if (minute > 26) return;

        var pressureIncrease = 0;
        foreach (var key in openValves) {
          pressureIncrease += graph[key].FlowRate;
        }
        currentPressure += pressureIncrease;

        var maxAchievable = currentPressure;
        for (var i = minute + 1; i <= 26; i++) {
          foreach (var valve in graph.Values) {
            maxAchievable += valve.FlowRate;
          }
        }

        if (maxAchievable < maxPressure) return;
        
        var memoKey = minute + " " + currentPressure + " " + currentValve.Key + " " + elephantValve.Key;
        foreach (var valve in openValves) {
          memoKey += " V:" + valve;
        }
        if (!memo.Contains(memoKey)) {
          memo.Add(memoKey);
        } else {
          return;
        }

        maxPressure = Math.Max(maxPressure, currentPressure);

        if (minute == 26) return;

        if (openValves.Count == graph.Values.Where(v => v.FlowRate > 0).Count()) {
          Dfs(minute + 1, currentPressure, currentValve, elephantValve);
          return;
        }

        if (!openValves.Contains(currentValve.Key) && !openValves.Contains(elephantValve.Key)) {
          openValves.Add(currentValve.Key);
          openValves.Add(elephantValve.Key);
          Dfs(minute + 1, currentPressure, currentValve, elephantValve);
          openValves.Remove(currentValve.Key);
          openValves.Remove(elephantValve.Key);
        } if (!openValves.Contains(currentValve.Key)) {
          openValves.Add(currentValve.Key);
          foreach (var elephantChild in elephantValve.Children) {
            Dfs(minute + 1, currentPressure, currentValve, elephantChild);
          }
          openValves.Remove(currentValve.Key);
        } if (!openValves.Contains(elephantValve.Key)) {
          openValves.Add(elephantValve.Key);
          foreach (var child in currentValve.Children) {
            Dfs(minute + 1, currentPressure, child, elephantValve);
          }
          openValves.Remove(elephantValve.Key);
        }

        foreach (var child in currentValve.Children) {
          foreach (var elephantChild in elephantValve.Children) {
            if (child == elephantChild) continue;
            Dfs(minute + 1, currentPressure, child, elephantChild);
          }
        }
      }
    }

    public static Dictionary<string, Node> ParseInput(string[] lines) {
      Dictionary<string, Node> graph = new Dictionary<string, Node>();

      foreach (var line in lines) {
        string[] split = line.Split(" ");
        var key = split[1];
        var flowRate = int.Parse(split[4].Split("=")[1].TrimEnd(';'));

        if (!graph.ContainsKey(key)) {
          var newNode = new Node(key);
          graph[key] = newNode;
        }

        var node = graph[key];
        node.FlowRate = flowRate;

        string[] childrenKeys = line.Contains("valves") ? line.Split(" valves ")[1].Split(", ") : line.Split(" valve ")[1].Split(", ");
        foreach (var childKey in childrenKeys) {
          var childNode = graph.ContainsKey(childKey) ? graph[childKey] : new Node(childKey);
          node.Children.Add(childNode);
          graph[childKey] = childNode;
        }
      }

      return graph;
    }
  }
}