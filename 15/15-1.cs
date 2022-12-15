using System.Numerics;

namespace Solutions {

  class FifteenOne {
    public static int Output() {
      return Solution();
    }

    public static List<Sensor> sensors = new List<Sensor>();

    public static int Solution() {
      string[] lines = System.IO.File.ReadAllText(@"./15/test-input.txt").Split("\n");
      ParseInput(lines);

      return -1;
    }

    public static void ParseInput(string[] lines) {
      foreach (var line in lines) {
        string[] split = line.Split(" ");
        var sensorX = int.Parse(split[2].Split("=")[1].Trim(','));
        var sensorY = int.Parse(split[3].Split("=")[1].Trim(':'));
        var beaconX = int.Parse(split[8].Split("=")[1].Trim(','));
        var beaconY = int.Parse(split[9].Split("=")[1]);
        var distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
        var Sensor = new Sensor(sensorX, sensorY, distance);
        sensors.Add(Sensor);
      }
    }
  }

  class Sensor {
    public int x;
    public int y;
    public int closestBeacon;

    public Sensor(int x, int y, int closestBeacon) {
      this.x = x;
      this.y = y;
      this.closestBeacon = closestBeacon;
    }
  }

}