using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EasyConverter.Model.DataSources
{
    public class DataTableSource : IDataSource
    {


        private System.Data.DataTable dataTable;
        public string GetDataLabel()
        {
            throw new NotImplementedException();
        }

        public ProtoData GetDataTypes()
        {
            double num;
            ProtoData protoDatum = new ProtoData()
            {
                obs = 0
            };
            
            protoDatum.vnames = GetVarnames();


            // this.CheckVarNames(protoDatum.vnames);
            protoDatum.map = new byte[protoDatum.nvar];


            foreach(System.Data.DataColumn dc in dataTable.Columns)
            {
                if(dc.DataType ==typeof(int))
                {
                    byte numType = this.GetNumType(num);
                    if (numType > protoDatum.map[i])
                    {
                        protoDatum.map[i] = numType;
                    }

                }

            }
            while (true)
            {
                string str = streamReader.ReadLine();
                string str1 = str;
                if (str == null)
                {
                    break;
                }
                string[] strArrays = str1.Split(new char[] { '\t' });
                for (int i = 0; i < (int)protoDatum.map.Length; i++)
                {
                    string str2 = strArrays[i];
                    if (double.TryParse(str2, out num) || StataMissings.IsMissingValue(str2))
                    {
                        //byte numType = this.GetNumType(num);
                        //if (numType > protoDatum.map[i])
                        //{
                        //    protoDatum.map[i] = numType;
                        //}
                    }
                    else
                    {
                        if (str2.Length > 244)
                        {
                            str2 = str2.Substring(1, 244);
                        }
                        if (str2.Length > protoDatum.map[i])
                        {
                            protoDatum.map[i] = Convert.ToByte(str2.Length);
                        }
                    }
                }
                ProtoData protoDatum1 = protoDatum;
                protoDatum1.obs = protoDatum1.obs + 1;
            }
            streamReader.Close();
            for (int j = 0; j < protoDatum.nvar; j++)
            {
                if (protoDatum.map[j] == 0)
                {
                    protoDatum.map[j] = 255;
                }
            }
            return protoDatum;
        }

        public Dictionary<string, vlabs> GetValueLabels()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetVarLabels()
        {
            throw new NotImplementedException();
        }

        public string[] GetVarnames()
        {
           return (from System.Data.DataColumn f in dataTable.Columns select f.ColumnName ).ToArray() ;
        }

        public Dictionary<string, string> GetVarVal()
        {
            throw new NotImplementedException();
        }
    }
}
