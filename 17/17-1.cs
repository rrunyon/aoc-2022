using System.Numerics;

namespace Solutions {

  class SeventeenOne {
    public static int Output() {
      return Solution();
    }

    public static void PrintCave(char[][] cave) {
      System.Console.WriteLine("---------------------------------");
      for (var i = 3950; i < cave.Length; i++) {
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

    public static int Solution() {
      char[] movements = System.IO.File.ReadAllText(@"./17/test-input.txt").ToCharArray();

      char[][] cave = new char[4000][];
      for (var i = 0; i < cave.Length; i++) {
        char[] row = new char[7];
        for (var j = 0; j < row.Length; j++) {
          row[j] = '.';
        }
        cave[i] = row;
      }

      int minY = cave.Length - 1;
      HashSet<string> fallingRockPoints = new HashSet<string>();
      HashSet<string> intermediateRockPoints = new HashSet<string>();
      for (var i = 0; i < 2022; i++) {
        int startX = 2;
        int startY = minY - 3;
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

        foreach (var space in rock.Spaces) {
          int currentX = startX + space[1];
          int currentY = startY + space[0];

          fallingRockPoints.Add(currentX + " " + currentY);
          cave[currentY][currentX] = '@';
        }

        foreach (var movement in movements) {
          Console.WriteLine("Moving: " + movement);
          PrintCave(cave);

          if (movement == '>') {
            var blocked = false;
            foreach (var point in fallingRockPoints) {
              var x = int.Parse(point.Split(" ")[0]);
              var y = int.Parse(point.Split(" ")[1]);
              var newX = x + 1;

              if (newX >= cave[0].Length || cave[y][newX] == '#') {
                blocked = true;
                break;
              }
            }
            if (blocked) continue;

            foreach (var point in fallingRockPoints) {
              var x = int.Parse(point.Split(" ")[0]);
              var y = int.Parse(point.Split(" ")[1]);
              var newX = x + 1;

              cave[y][x] = '.';
              cave[y][newX] = '@';
              intermediateRockPoints.Add(newX + " " + y);
            }
            ClearAndSwap(fallingRockPoints, intermediateRockPoints);
          } else if (movement == '<') {
            var blocked = false;
            foreach (var point in fallingRockPoints) {
              var x = int.Parse(point.Split(" ")[0]);
              var y = int.Parse(point.Split(" ")[1]);
              var newX = x - 1;

              if (newX < 0 || cave[y][newX] == '#') {
                blocked = true;
                break;
              }
            }
            if (blocked) continue;

            foreach (var point in fallingRockPoints) {
              var x = int.Parse(point.Split(" ")[0]);
              var y = int.Parse(point.Split(" ")[1]);
              var newX = x - 1;

              cave[y][x] = '.';
              cave[y][newX] = '@';
              intermediateRockPoints.Add(newX + " " + y);
            }
            ClearAndSwap(fallingRockPoints, intermediateRockPoints);
          }

          var blockedDown = false;
          foreach (var point in fallingRockPoints) {
            var x = int.Parse(point.Split(" ")[0]);
            var y = int.Parse(point.Split(" ")[1]);
            var newY = y + 1;

            if (newY >= cave.Length || cave[newY][x] == '#') {
              blockedDown = true;
              break;
            }
          }
          if (blockedDown) {
            foreach (var point in fallingRockPoints) {
              var x = int.Parse(point.Split(" ")[0]);
              var y = int.Parse(point.Split(" ")[1]);

              cave[y][x] = '#';
            }
            fallingRockPoints.Clear();
            break;
          }

          Console.WriteLine("Moving: Down");
          foreach (var point in fallingRockPoints) {
            var x = int.Parse(point.Split(" ")[0]);
            var y = int.Parse(point.Split(" ")[1]);
            minY = Math.Min(minY, y);
            var newY = y + 1;

            cave[y][x] = '.';
            cave[newY][x] = '@';
            intermediateRockPoints.Add(x + " " + newY);
          }
          foreach (var point in fallingRockPoints) {
            var y = int.Parse(point.Split(" ")[1]);

          }
          ClearAndSwap(fallingRockPoints, intermediateRockPoints);
        }
      }

      return minY;
    }

    public static void ClearAndSwap(HashSet<string> primary, HashSet<string> secondary) {
      primary.Clear();
      foreach (var point in secondary) primary.Add(point);
      secondary.Clear();
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