// <copyright file="RosterFile.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
namespace SEATLibrary
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class RosterFile
    {
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<string[]> parsedData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string[]> ParsedData
        {
            get { return this.parsedData; }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return this.parsedData.Count; }
        }
    }
}
