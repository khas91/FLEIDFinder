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
            List<String> studentIDs = File.ReadAllLines(args[0]).ToList();
            Dictionary<String, String> FLEIDDict = new Dictionary<string, string>();
            

            foreach (String fileName in Directory.EnumerateFiles(".", "*.*", SearchOption.AllDirectories))
            {
                String[] pathComps = fileName.Split(new char[] { '\\' });

                String name = pathComps[pathComps.Length - 1];

                if (!(name == args[0]) && !(System.AppDomain.CurrentDomain.FriendlyName == name)
                    && name.StartsWith("DPS"))
                {
                    foreach (String line in File.ReadAllLines(fileName))
                    {
                        String studentID = line.Substring(48, 10).Trim();

                        if (line.Length < 575)
                        {
                            continue;
                        }

                        String FLEID = line.Substring(561, 14);

                        if (studentIDs.Contains(studentID) && !FLEIDDict.ContainsKey(studentID) && FLEID.Trim() != "")
                        {
                            FLEIDDict.Add(studentID, FLEID);
                        }
                    }
                }               
            }

            using (StreamWriter output = new StreamWriter("output.csv"))
            {
                foreach (String studentID in studentIDs)
                {
                    output.WriteLine("=\"" + studentID + "\"," + (FLEIDDict.ContainsKey(studentID) ? FLEIDDict[studentID] : "              "));
                }
            }
        }
    }
}
