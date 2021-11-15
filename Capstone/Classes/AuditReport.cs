using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class AuditReport
    {
        private List<AuditReportTracking> acitonsTracked = new List<AuditReportTracking>();

        private const string filePath = @"C:\Users\Student\source\repos\week-4-c-sharp-pairs-team-0\module-1_Mini-Capstone\Log.txt";
      
        public void UploadAuditedItems(List<AuditReportTracking> auditLog)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (AuditReportTracking trackedAction in acitonsTracked)
                    {
                        writer.WriteLine($"{trackedAction.ActionTakenTime}  {trackedAction.ActionTaken} {trackedAction.ActionCurrentAccountBalance.ToString("C")}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("There was a problem saving the audit file. Not all changes may appear.");
                Console.WriteLine(ex.Message);
            }
        }

        public List<AuditReportTracking> AllAudits
        {
            get
            {
                return acitonsTracked;
            }
        }

        public void AddAction(AuditReportTracking newTracking)
        {
            acitonsTracked.Add(newTracking);
        }
    }
}
