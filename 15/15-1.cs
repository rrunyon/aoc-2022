using System.Numerics;

namespace Solutions {

  class FifteenOne {
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

      int count = 0;
      for (var i = MinX - 10000000; i <= MaxX + 10000000; i++) {
        var x = i;
        var y = 2000000;
        var anyMatching = false;

        foreach (var beacon in Beacons) {
          var beaconX = beacon.Value[0];
          var beaconY = beacon.Value[1];
          if (beaconX == x && beaconY == y) {
            //System.Console.Write("B");
            count--;
          }
        }

        foreach (var sensor in Sensors) {
          int pointDistance = Math.Abs(sensor.X - x) + Math.Abs(sensor.Y - y);
          if (sensor.ClosestBeaconDistance >= pointDistance) {
            anyMatching = true;
            break;
          };
          if (anyMatching) break;
        }

        if (anyMatching) {
          //System.Console.Write("#");
          count++;
        } else {
          //System.Console.Write(".");
        }
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

        var beaconKey = beaconX + " " + beaconY;
        Beacons[beaconKey] = new int[]{ beaconX, beaconY };
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
}