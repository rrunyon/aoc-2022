using System.Numerics;

namespace Solutions {

  class FifteenTwo {
    public static int Output() {
      return Solution();
    }

    public static int MinX = int.MaxValue;
    public static int MaxX = -int.MaxValue;
    public static int MinY = int.MaxValue;
    public static int MaxY = -int.MaxValue;
    public static List<Sensor> Sensors = new List<Sensor>();
    //public static List<Beacon> Beacons = new List<Beacon>();
    public static Dictionary<string, int[]> Beacons = new Dictionary<string, int[]>();

    public static int Solution() {
      string[] lines = System.IO.File.ReadAllText(@"./15/input.txt").Split("\n");
      ParseInput(lines);

      var MAX = 4000000;

      for (var i = 0; i <= MAX; i++) {
        if (i % 1000 == 0) Console.WriteLine(i);
        for (var j = 0; j <= MAX; j++) {
          var anyMatching = false;

          foreach (var sensor in Sensors) {
            int pointDistance = Math.Abs(sensor.X - j) + Math.Abs(sensor.Y - i);
            if (sensor.ClosestBeaconDistance >= pointDistance) {
              j = Math.Max(j, Math.Abs(sensor.Y - i) + sensor.ClosestBeaconDistance);
              anyMatching = true;
              break;
            };
          }

          if (!anyMatching) {
            System.Console.WriteLine(j + " " + i);
            return j * 4000000 + i;
            //System.Console.Write(".");
          }
        }
      }

      return -1;
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

        var beaconKey = beaconX + " " + beaconY;
        Beacons[beaconKey] = new int[]{ beaconX, beaconY };
      }
    }
  }
}