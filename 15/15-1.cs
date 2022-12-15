using System.Numerics;

namespace Solutions {

  class FifteenOne {
    public static int Output() {
      return Solution();
    }

    public static int XOffset = 1500000;
    public static int YOffset = 1500000;
    public static int MinX = int.MaxValue;
    public static int MaxX = -int.MaxValue;
    public static int MinY = int.MaxValue;
    public static int MaxY = -int.MaxValue;
    public static List<Sensor> Sensors = new List<Sensor>();
    public static List<Beacon> Beacons = new List<Beacon>();

    public static int Solution() {
      string[] lines = System.IO.File.ReadAllText(@"./15/input.txt").Split("\n");
      ParseInput(lines);

      char[][] grid = new char[MaxY + YOffset][];
      for (var i = 0; i <= MaxY + YOffset; i++) {
        char[] row = new char[MaxX + XOffset];
        for (var j = 0; j < MaxX + XOffset; j++) {
          row[j] = '.';
        }
        grid[i] = row;
      }

      foreach (var beacon in Beacons) {
        grid[beacon.Y][beacon.X] = 'B'; 
      }

      foreach (var sensor in Sensors) {
        grid[sensor.Y][sensor.X] = 'S';
        var upY = sensor.Y;
        var downY = sensor.Y;
        var left = sensor.X - sensor.ClosestBeaconDistance;
        var right = sensor.X + sensor.ClosestBeaconDistance;

        while (left <= right) {
          for (var i = left; i <= right; i++) {
            Print(grid);
            if (upY >= 0 && left >= 0 && i < grid[0].Length && grid[upY][i] == '.') {
              grid[upY][i] = '#';
            }
            if (downY < grid.Length && i >= 0 && i < grid[0].Length && grid[downY][i] == '.') {
              grid[downY][i] = '#';
            }
          }

          left++;
          right--;
          upY--;
          downY++;
        }
      }

      var count = 0;
      for (var i = 0; i < grid[20000].Length; i++) {
        if (grid[20000][i] == '#') count++;
      }

      return count;
    }

    public static void Print(char[][] grid) {
      for (var i = 0; i < grid.Length; i++) {
        string lineNumber = i.ToString();
        while (lineNumber.Length < 3) {
          lineNumber += " ";
        }
        lineNumber += ": ";
        System.Console.Write(lineNumber);

        var row = grid[i];
        System.Console.WriteLine(row);
      }
    }

    public static void ParseInput(string[] lines) {
      foreach (var line in lines) {
        string[] split = line.Split(" ");
        var sensorX = int.Parse(split[2].Split("=")[1].Trim(','));
        var sensorY = int.Parse(split[3].Split("=")[1].Trim(':'));
        var beaconX = int.Parse(split[8].Split("=")[1].Trim(','));
        var beaconY = int.Parse(split[9].Split("=")[1]);
        MinX = Math.Min(MinX, Math.Min(sensorX, beaconX));
        MaxX = Math.Max(MaxX, Math.Max(sensorX, beaconX));

        MinY = Math.Min(MinY, Math.Min(sensorY, beaconY));
        MaxY = Math.Max(MaxY, Math.Max(sensorY, beaconY));

        var distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
        var sensor = new Sensor(sensorX, sensorY, distance);
        Sensors.Add(sensor);

        var beacon = new Beacon(beaconX, beaconY);
        Beacons.Add(beacon);
      }
    }
  }

  class Sensor {
    public int X;
    public int Y;
    public int ClosestBeaconDistance;

    public Sensor(int x, int y, int closestBeaconDistance) {
      X = x;
      Y = y;
      ClosestBeaconDistance = closestBeaconDistance;
    }
  }
  class Beacon {
    public int X;
    public int Y;

    public Beacon(int x, int y) {
      X = x;
      Y = y;
    }
  }
}