using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FLEIDFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> outputRows = new List<string>();
            List<String> studentIDs = new List<string>();
            
            foreach (String fileName in Directory.EnumerateFiles(".", "*.*", SearchOption.AllDirectories))
            {
                String[] pathComps = fileName.Split(new char[] { '\\' });

                String name = pathComps[pathComps.Length - 1];

                if (!(System.AppDomain.CurrentDomain.FriendlyName == name) && name.StartsWith("DPS"))
                {
                    foreach (String line in File.ReadAllLines(fileName))
                    {
                        String studentID = line.Substring(48, 10).Trim();

                        if (line.Length < 575)
                        {
                            continue;
                        }

                        String FLEID = line.Substring(561, 14);

                        if (FLEID.Trim() != "" && !studentIDs.Contains(studentID))
                        {
                            outputRows.Add(line);
                            studentIDs.Add(studentID);
                        }
                    }
                }
            }

            using (StreamWriter output = new StreamWriter("FLEID_Master.txt"))
            {
                foreach (String line in outputRows)
                {
                    output.WriteLine(line);
                }
            }
        }
    }
}
