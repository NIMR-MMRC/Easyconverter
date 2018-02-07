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
            return string.Empty;
        }
        private byte GetNumType(double x)
        {
            return (byte)255;
        }
        public ProtoData GetDataTypes()
        {
            double num=0;
            ProtoData protoDatum = new ProtoData()
            {
                obs = 0
            };
            
            protoDatum.vnames = GetVarnames();


            // this.CheckVarNames(protoDatum.vnames);
            protoDatum.map = new byte[protoDatum.nvar];


            foreach(System.Data.DataRow dr in dataTable.Rows)
            {
                foreach(System.Data.DataColumn dc in dataTable.Columns)
                {
                    var i = dataTable.Columns.IndexOf(dc);
                    var str2 =  dr[dc];
                    if (StataMissings.IsMissingValue(str2) || double.TryParse(str2.ToString(), out num)  )
                    {
                        byte numType = this.GetNumType(num);
                        if (numType > protoDatum.map[i])
                        {
                           protoDatum.map[i] = numType;
                        }
                    }
                    else
                    {
                        var str3 = str2.ToString();
                        if (str3.Length > 244)
                        {
                            str3 = str3.Substring(1, 244);
                        }
                        if (str3.Length > protoDatum.map[i])
                        {
                            protoDatum.map[i] = Convert.ToByte(str3.Length);
                        }
                    }
                }
                ProtoData protoDatum1 = protoDatum;
                protoDatum1.obs = protoDatum1.obs + 1;

            }
            
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
            return new Dictionary<string, vlabs>();
        }

        public Dictionary<string, string> GetVarLabels()
        {
            var dict = new Dictionary<string, string>();
           foreach(System.Data.DataColumn t in dataTable.Columns)

            {
                dict.Add(t.ColumnName, t.ColumnName);
            }
            return dict;
        }

        public string[] GetVarnames()
        {
           return (from System.Data.DataColumn f in dataTable.Columns select f.ColumnName ).ToArray() ;
        }

        public Dictionary<string, string> GetVarVal()
        {
            return new Dictionary<string, string>();
        }
    }
}
