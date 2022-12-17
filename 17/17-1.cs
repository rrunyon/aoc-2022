using System.Numerics;

namespace Solutions {

  class SeventeenOne {
    public static int Output() {
      return Solution();
    }

    private static char[][] cave = new char[][]{};
    private static HashSet<string> fallingRockPoints = new HashSet<string>();
    private static HashSet<string> intermediateRockPoints = new HashSet<string>();
    private static int minY = int.MaxValue;

    public static int Solution() {
      char[] movements = System.IO.File.ReadAllText(@"./17/input.txt").ToCharArray();
      int movementIndex = 0;
      cave = CreateCave();

      minY = cave.Length;
      for (var i = 0; i < 2022; i++) {
        int startX = 2;
        int startY = minY - 4;
        Rock rock = GetCurrentRock(i, startX, startY);

        Console.WriteLine("Inserting new rock");
        foreach (var space in rock.Spaces) {
          int currentX = startX + space[1];
          int currentY = startY + space[0];

          fallingRockPoints.Add(currentX + " " + currentY);
          cave[currentY][currentX] = '@';
        }
        PrintCave(cave);

        while (true) {
          var movement = movements[movementIndex++ % movements.Length];
          Console.WriteLine("Moving: " + movement);

          if (movement == '>') {
            if (IsBlocked(1, 0)) {
              Console.WriteLine("Skipping");
            } else {
              Translate(1, 0);
              //PrintCave(cave);
            }
          } else if (movement == '<') {
            if (IsBlocked(-1, 0)) {
              Console.WriteLine("Skipping");
            } else {
              Translate(-1, 0);
              //PrintCave(cave);
            }
          }

          if (IsBlocked(0, 1)) {
            foreach (var point in fallingRockPoints) {
              var x = int.Parse(point.Split(" ")[0]);
              var y = int.Parse(point.Split(" ")[1]);

              minY = Math.Min(minY, y);
              cave[y][x] = '#';
            }
            fallingRockPoints.Clear();
            Console.WriteLine("Freezing rock");
            //PrintCave(cave);
            break;
          } else {
            Console.WriteLine("Moving: Down");
            Translate(0, 1);
            //PrintCave(cave);
          }
        }
      }

      return cave.Length - minY;
    }

    private static char[][] CreateCave() {
      char[][] cave = new char[4000][];
      for (var i = 0; i < cave.Length; i++) {
        char[] row = new char[7];
        for (var j = 0; j < row.Length; j++) {
          row[j] = '.';
        }
        cave[i] = row;
      }

      return cave;
    }

    private static Rock GetCurrentRock(int i, int startX, int startY) {
      Rock rock = new Horizontal(startX, startY);
      switch(i % 5) {
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
        var x = int.Parse(point.Split(" ")[0]);
        var y = int.Parse(point.Split(" ")[1]);
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
        var x = int.Parse(point.Split(" ")[0]);
        var y = int.Parse(point.Split(" ")[1]);
        var newX = x + xShift;
        var newY = y + yShift;

        intermediateRockPoints.Add(newX + " " + newY);
      }
      ClearAndSwap(fallingRockPoints, intermediateRockPoints);
    }

    private static void ClearAndSwap(HashSet<string> primary, HashSet<string> secondary) {
      foreach (var point in primary) {
        var x = int.Parse(point.Split(" ")[0]);
        var y = int.Parse(point.Split(" ")[1]);
        cave[y][x] = '.';
      }
      foreach (var point in intermediateRockPoints) {
        var x = int.Parse(point.Split(" ")[0]);
        var y = int.Parse(point.Split(" ")[1]);
        cave[y][x] = '@';
      }

      primary.Clear();
      foreach (var point in secondary) primary.Add(point);
      secondary.Clear();
    }

    private static void PrintCave(char[][] cave) {
      System.Console.WriteLine("---------------------------------");
      for (var i = 3975; i < cave.Length; i++) {
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

  public class Rock {

    public int[][] Spaces = new int[][]{};
    public int StartX;
    public int StartY;

    public Rock(int startX, int startY) {
      StartX = startX;
      StartY = startY;
    }
  }

  public class Horizontal : Rock {

    public Horizontal(int startX, int startY) : base (startX, startY) {
      Spaces = new int[][] {
        new int[]{ 0, 0 },
        new int[]{ 0, 1 },
        new int[]{ 0, 2 },
        new int[]{ 0, 3 },
      };
    }
  }

  public class Plus : Rock {

    public Plus(int startX, int startY) : base (startX, startY) {
      Spaces = new int[][] {
        new int[]{ -2, 1 },
        new int[]{ -1, 0 },
        new int[]{ -1, 1 },
        new int[]{ -1, 2 },
        new int[]{ 0, 1 },
      };
    }
  }

  public class Angled : Rock {

    public Angled(int startX, int startY) : base (startX, startY) {
      Spaces = new int[][] {
        new int[]{ -2, 2 },
        new int[]{ -1, 2 },
        new int[]{ 0, 0 },
        new int[]{ 0, 1 },
        new int[]{ 0, 2 },
      };
    }
  }

  public class Vertical : Rock {

    public Vertical(int startX, int startY) : base (startX, startY) {
      Spaces = new int[][] {
        new int[]{ -3, 0 },
        new int[]{ -2, 0 },
        new int[]{ -1, 0 },
        new int[]{ 0, 0 },
      };
    }
  }

  public class Square : Rock {

    public Square(int startX, int startY) : base (startX, startY) {
      Spaces = new int[][] {
        new int[]{ -1, 0 },
        new int[]{ -1, 1 },
        new int[]{ 0, 1 },
        new int[]{ 0, 0 },
      };
    }
  }
}