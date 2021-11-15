using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    /// <summary>
    /// This class should contain any and all details of access to files
    /// </summary>
    public class FileAccess
    {
        // All external data files for this application should live in this directory.
        // You will likely need to create this directory and copy / paste any needed files.
        private const string filePath = @"C:\Users\Student\source\repos\week-4-c-sharp-pairs-team-0\module-1_Mini-Capstone\cateringsystem.csv";

        public void LoadCateringItems(Catering items)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        string[] parts = line.Split("|");

                        CateringItem item = new CateringItem();

                        item.Type = parts[0];
                        item.Code = parts[1];
                        item.Name = parts[2];
                        item.Price = Convert.ToDecimal(parts[3]);

                        items.AddCateringItem(item);
                    }
                }
            }
            catch(IOException ex )
            {
                Console.WriteLine("There are no catering items in this file. ");
            }
        }
    }
}
