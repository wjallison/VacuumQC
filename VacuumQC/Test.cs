using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacuumQC
{
    public class Test
    {
        public string serial;
        public string model;
        public vacProfile profileSettings;
        public List<List<int>> overUnder = new List<List<int>>();
        public List<List<double>> avgValues = new List<List<double>>();
        public bool pass = true;

        public Test(string SN, vacProfile profile)
        {
            serial = SN;
            profileSettings = profile;
        }

        public void evalLists(List<double> pressList, List<double> currList, List<double> flowList)
        {
            avgValues.Add(new List<double>());
            overUnder.Add(new List<int>());
            avgValues.Last().Add(avg(currList));
            avgValues.Last().Add(avg(pressList));
            avgValues.Last().Add(avg(flowList));

            int ind = avgValues.Count - 1;
            for(int i = 0; i <3; i++)
            {
                if (avgValues[ind][i] < profileSettings.minMax[ind][2 * i])
                {
                    overUnder[ind].Add(-1);
                    pass = false;
                }
                else if (avgValues[ind][i] > profileSettings.minMax[ind][2 * i + 1])
                {
                    overUnder[ind].Add(-1);
                    pass = false;
                }
                else
                {
                    overUnder[ind].Add(0);
                }
            }
        }

        public double avg(List<double> ls)
        {
            double sum = 0;
            for(int i = 0; i < ls.Count; i++)
            {
                sum += ls[i];
            }
            return sum / ls.Count;
        }
    }
}
