using System.Numerics;

namespace Solutions {

  class SeventeenTwo {
    public static int Output() {
      return Solution();
    }

    private static char[][] cave = new char[][]{};
    private static List<int[]> fallingRockPoints = new List<int[]>();
    private static List<int[]> intermediateRockPoints = new List<int[]>();
    private static int minY = int.MaxValue;

    public static int Solution() {
      char[] movements = System.IO.File.ReadAllText(@"./17/test-input.txt").ToCharArray();
      int movementIndex = 0;
      cave = CreateCave();

      minY = cave.Length;
      for (UInt64 i = 0; i < 2022; i++) {
        if (i % 100000 == 0) {
          var percent = Convert.ToDecimal(((double)i / 1000000000000) * 100);
          Console.WriteLine(percent + "%");
        }

        int startX = 2;
        int startY = minY - 4;
        Rock rock = GetCurrentRock(i % 5, startX, startY);

        Console.WriteLine("Inserting new rock");
        foreach (var space in rock.Spaces) {
          int currentX = startX + space[1];
          int currentY = startY + space[0];

          fallingRockPoints.Add(new int[]{ currentX, currentY });
          cave[currentY][currentX] = '@';
        }
        PrintCave(cave);

        while (true) {
          if (startY < minY) {
            var xShift = 0;
            for (var j = 0; j < minY - startY; j++) {
              var movement = movements[movementIndex++ % movements.Length];
              var shift = movement == '>' ? 1 : -1;
              xShift += shift;
            }
            Translate(xShift, minY - 1);
          } else {
            var movement = movements[movementIndex++ % movements.Length];

            if (movement == '>') {
              if (IsBlocked(1, 0)) {
                Console.WriteLine("Skipping");
              } else {
                Translate(1, 0);
                PrintCave(cave);
              }
            } else if (movement == '<') {
              if (IsBlocked(-1, 0)) {
                Console.WriteLine("Skipping");
              } else {
                Translate(-1, 0);
                PrintCave(cave);
              }
            }

            if (IsBlocked(0, 1)) {
              foreach (var point in fallingRockPoints) {
                var x = point[0];
                var y = point[1];

                minY = Math.Min(minY, y);
                cave[y][x] = '#';
              }
              fallingRockPoints.Clear();
              Console.WriteLine("Freezing rock");
              PrintCave(cave);
              break;
            } else {
              Console.WriteLine("Moving: Down");
              Translate(0, 1);
              PrintCave(cave);
            }
          }
        }
      }

      return cave.Length - minY;
    }

    private static char[][] CreateCave() {
      char[][] cave = new char[10000][];
      for (var i = 0; i < cave.Length; i++) {
        char[] row = new char[7];
        for (var j = 0; j < row.Length; j++) {
          row[j] = '.';
        }
        cave[i] = row;
      }

      return cave;
    }

    private static Rock GetCurrentRock(UInt64 i, int startX, int startY) {
      Rock rock = new Horizontal(startX, startY);
      switch(i) {
        case 0:
          rock = new Horizontal(startX, startY);
          break;
        case 1:
          rock = new Plus(startX, startY);
          break;
        case 2:
          rock = new Angled(startX, startY);
          break;
        case 3:
          rock = new Vertical(startX, startY);
          break;
        case 4:
          rock = new Square(startX, startY);
          break;
      }

      return rock;
    }

    private static Boolean IsBlocked(int xShift, int yShift) {
      var blocked = false;
      foreach (var point in fallingRockPoints) {
        var x = point[0];
        var y = point[1];
        var newX = x + xShift;
        var newY = y + yShift;

        if (newX >= 0 && newX < cave[0].Length && newY >= 0 && newY < cave.Length && cave[newY][newX] != '#') {
          continue;
        } else {
          blocked = true;
          break;
        }
      }

      return blocked;
    }

    private static void Translate(int xShift, int yShift) {
      foreach (var point in fallingRockPoints) {
        var x = point[0];
        var y = point[1];
        var newX = x + xShift;
        var newY = y + yShift;

        intermediateRockPoints.Add(new int[]{ newX, newY });
      }
      ClearAndSwap(fallingRockPoints, intermediateRockPoints);
    }

    private static void ClearAndSwap(List<int[]> primary, List<int[]> secondary) {
      foreach (var point in primary) {
        var x = point[0];
        var y = point[1];
        cave[y][x] = '.';
      }
      foreach (var point in intermediateRockPoints) {
        var x = point[0];
        var y = point[1];
        cave[y][x] = '@';
      }

      primary.Clear();
      foreach (var point in secondary) primary.Add(point);
      secondary.Clear();
    }

    private static void PrintCave(char[][] cave) {
      System.Console.WriteLine("---------------------------------");
      for (var i = cave.Length - 100; i < cave.Length; i++) {
        var row = cave[i];
        var line = i.ToString();
        while (line.Length < 5) {
          line += " ";
        }
        line += ": ";
        System.Console.Write(line);
        System.Console.Write('|');
        foreach (var cell in row) {
          System.Console.Write(cell);
        }
        System.Console.Write('|');
        System.Console.WriteLine();
      }

      for (var j = 0; j < 7; j++) System.Console.Write(' ');
      System.Console.Write('+');
      for (var i = 0; i < cave[0].Length; i++) {
        System.Console.Write('-');
      }
      System.Console.Write('+');
      System.Console.WriteLine();
    }
  }
}