using System.ComponentModel;

partial class Program
{

    class File
    {
        public File(string name, int size)
        {
            this.Size = size;
            this.Name = name;
        }

        public int Size;
        public string Name;
    }

    class Directory 
    {
        public Directory? Parent;

        public string Name;

        public int Size 
        {
            get
            {
                int totalSize = 0;
                foreach(File file in Files)
                {
                    totalSize += file.Size;
                }

                foreach(Directory directory in Directories)
                {
                    totalSize += directory.Size;
                }


                /*
                if (totalSize <= 100000)
                {
                    Console.WriteLine(totalSize);
                }
                */

                if (totalSize >= 1111105)
                {
                    Console.WriteLine(totalSize);
                }


                return totalSize;
            }
        }


        public Directory(string name, Directory? parent = null)
        {
            this.Parent = parent;
            this.Name = name;
        }

        public List<File> Files = new List<File>();
        public List<Directory> Directories = new List<Directory>();

        public void AddFile(File f)
        {
            //no duplicate directory
            if(!Files.Any(existingFile => existingFile.Name == f.Name))
            {
                Files.Add(f);
            }
        }

        public void AddDirectory(Directory d)
        {
            d.Parent = this;
            //no duplicate directories
            if(!Directories.Any(existingDirectory => existingDirectory.Name == d.Name))
            {
                Directories.Add(d);
            }
        }
    }

    public static void Main()
    {
        Directory currentDir = null;

        foreach(string line in System.IO.File.ReadLines("input.txt"))
        {
            if(line.Contains("$ cd .."))
            {
                //move up one directory
                currentDir = currentDir.Parent;
            }
            else if(line.Contains("$ cd "))
            {
                //move to dir
                string targetDirName = line.Split(" ")[2];

                if(currentDir == null)
                {
                    currentDir = new Directory(targetDirName);
                }
                else
                {
                    currentDir = currentDir.Directories.First(d => d.Name == targetDirName);
                }
            }
            else if(line.Contains("dir "))
            {
                string newDirName = line.Split(" ")[1];
                //add dir to current dir
                currentDir.AddDirectory(new Directory(newDirName));
            }
            else if(line.Contains("$ ls"))
            {
                //dont do anything here
            }
            else
            {
                //must be a file 
                int fileSize = Int32.Parse(line.Split(' ')[0]);
                string fileName = line.Split(' ')[1];
                File file = new File(fileName, fileSize);
                currentDir.AddFile(file);
            }
        }

        while (currentDir?.Parent != null)
        {
            currentDir = currentDir.Parent;
        }

        int rootSize = currentDir.Size;
        int freespace = 70000000 - rootSize;
        Console.WriteLine($"           root size: {rootSize}");
        Console.WriteLine($"          free space: {freespace}");
        Console.WriteLine($"extra space required: {30000000 - freespace}");


        Console.ReadKey();
    }

}



