using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacuumQC
{
    public class Report
    {
        public string serial;
        public string vacType;
        public vacProfile profile;
        public StringBuilder report;
        //public double[] values = { 0, 0, 0,
        //                           0, 0, 0,
        //                           0, 0 };
        public Test test;
        public bool pass;
        public Report(string name, Test t, vacProfile prof)
        {
            serial = name;
            test = test;
            profile = prof;
            vacType = prof.name;

            buildReport();
        }

        public void buildReport()
        {
            report.AppendLine("S/N: " + serial);
            report.AppendLine(vacType);
            report.AppendLine();
            if (test.pass)
            {
                report.AppendLine("PASS");
            }
            else { report.AppendLine("FAIL"); }
            report.AppendLine();
            report.AppendLine("Full Airflow:");
            report.AppendLine("Current (A):");
            report.AppendLine("MIN			TESTED			MAX");
            //report.AppendFormat("{0}")
        }
    }
}
