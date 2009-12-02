namespace SEATLibrary
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class RosterFile
    {
        private ObservableCollection<string[]> parsedData;

        public RosterFile(string file)
        {
            this.parsedData = new ObservableCollection<string[]>();
            try
            {
                using (StreamReader read = new StreamReader(file))
                {
                    string line;
                    string[] row;

                    while ((line = read.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        this.parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                // Something bad happened
                e.ToString();
            }
        }

        public ObservableCollection<string[]> ParsedData
        {
            get { return this.parsedData; }
        }

        public int NumColumns
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.parsedData.Count; i++)
                {
                    int n = this.parsedData[i].Length;
                    if (n > num)
                    {
                        num = n;
                    }
                }

                return num;
            }
        }

        public int Count
        {
            get { return this.parsedData.Count; }
        }
    }
}
