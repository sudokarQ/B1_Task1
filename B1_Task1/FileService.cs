namespace B1_Task1
{
    public class FileService
    {
        public string FolderPath { get; set; }

        public FileService(string path)
        {
            FolderPath = path;
        }

        public void GenerateFiles(int fileCount, int linesPerFile)
        {
            TextService textService = new TextService();

            Directory.CreateDirectory(FolderPath);

            for (int i = 0; i < fileCount; i++)
            {
                var filePath = Path.Combine(FolderPath, $"file_{i + 1}.txt");

                using StreamWriter writer = new StreamWriter(filePath);

                for (int j = 0; j < linesPerFile; j++)
                {
                    writer.WriteLine(textService.ToString());
                }
            }
        }

        public int MergeFiles(string substringToRemove)
        {
            int removedCount = 0;

            using (StreamWriter resultWriter = new StreamWriter(Path.Combine(FolderPath, "result")))
            {
                foreach (string filePath in Directory.GetFiles(FolderPath, "*.txt"))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!line.Contains(substringToRemove) || string.IsNullOrWhiteSpace(substringToRemove))
                            {
                                resultWriter.WriteLine(line);
                            }
                            else
                            {
                                removedCount++;
                            }
                        }
                    }
                }
            }

            return removedCount;
        }
    }
}
