namespace Solutions {
  class SevenTwo {

    public static int Output() {
      return SmallestDeletableDirectorySize();
    }

    private static int SmallestDeletableDirectorySize() {
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

      List<int> directorySizes = new List<int>();
      int rootSize = Dfs(root);
      int unusedSpace = 70000000 - rootSize;
      int requiredSpace = 30000000 - unusedSpace;
      directorySizes.Sort();

      foreach(var size in directorySizes) {
        if (size >= requiredSpace) return size;
      }

      return -1;

      int Dfs(Directory node) {
        if (node.children.Count == 0) {
          int totalSize = node.TotalSize();
          directorySizes.Add(totalSize);
          return totalSize;
        } else {
          int subdirectoriesSize = 0;
          foreach(var child in node.children) {
            subdirectoriesSize += Dfs(child.Value);
          }
          int totalSize = subdirectoriesSize + node.TotalSize();
          directorySizes.Add(totalSize);
          return totalSize;
        }
      }
    }
  }
}