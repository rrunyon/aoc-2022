namespace Solutions {
  class SevenOne {

    public static int Output() {
      return SumFileSizes();
    }

    private static int SumFileSizes() {
      string[] lines = System.IO.File.ReadAllText(@"./7/input.txt").Split("\n");

      Directory root = new Directory();
      Directory currentNode = root;

      foreach(var line in lines) {
        string[] splitLine = line.Split(" ");
        if (splitLine[0] == "$") {
          if (splitLine[1] == "cd") {
            if (splitLine[2] == "..") {
              if (currentNode.parent != null) currentNode = currentNode.parent;
            } else if (splitLine[2] == "/") {
              currentNode = root;
            } else {
              Directory subDirectory = new Directory();
              subDirectory.parent = currentNode;
              currentNode.children.Add(splitLine[2], subDirectory);
              currentNode = subDirectory;
            }
          }
        } else {
          if (splitLine[0][0] != 'd') {
            currentNode.fileSizes.Add(int.Parse(splitLine[0]));
          }
        }
      }

      int total = 0;
      Dfs(root);
      return total;

      int Dfs(Directory node) {
        if (node.children.Count == 0) {
          int totalSize = node.TotalSize();
          if (totalSize <= 100000) total += totalSize;
          return totalSize;
        } else {
          int subdirectoriesSize = 0;
          foreach(var child in node.children) {
            subdirectoriesSize += Dfs(child.Value);
          }
          int totalSize = subdirectoriesSize + node.TotalSize();
          if (totalSize <= 100000) total += totalSize;
          return totalSize;
        }
      }
    }
  }
}

class Directory {
  public List<int> fileSizes = new List<int>();
  public Directory? parent;
  public Dictionary<string, Directory> children = new Dictionary<string, Directory>();

  public int TotalSize() {
    int total = 0;
    foreach(var fileSize in fileSizes) total += fileSize;
    return total;
  }
}