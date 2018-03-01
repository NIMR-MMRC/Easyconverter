using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace EasyConverter.Model.DataSources
{
    public class TabSeparatorDataSource : IDataSource
    {
        private string filename = string.Empty;

        public string GetDataLabel()
        {
            
                string str = "";
                StreamReader streamReader = new StreamReader(filename);
                while (true)
                {
                    string str1 = streamReader.ReadLine();
                    string str2 = str1;
                    if (str1 == null)
                    {
                        break;
                    }
                    str2 = str2.Trim();
                    if (str2.StartsWith("label data "))
                    {
                        str2 = str2.Substring(10).Trim();
                        str = str2.Substring(2, str2.Length - 4);
                    }
                }
                streamReader.Close();
                return str;
            
        }

        public ProtoData GetDataTypes()
        {
            
                double num;
                ProtoData protoDatum = new ProtoData()
                {
                    obs = 0
                };
                StreamReader streamReader = new StreamReader(filename);
               // protoDatum.vnames = GetVarnames();
               // this.CheckVarNames(protoDatum.vnames);
                protoDatum.map = new byte[protoDatum.nvar];
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

            vlabs vlab;
            Dictionary<string, vlabs> strs = new Dictionary<string, vlabs>();
            StreamReader streamReader = new StreamReader(filename);
            while (true)
            {
                string str = streamReader.ReadLine();
                string str1 = str;
                if (str == null)
                {
                    break;
                }
                str1 = str1.Trim();
                //if (str1.StartsWith("label define "))
                //{
                //    string schemaName = StataConverter.GetSchemaName(str1);
                //    if (!strs.ContainsKey(schemaName))
                //    {
                //        vlab = new vlabs();
                //        strs.Add(schemaName, vlab);
                //    }
                //    vlab = strs[schemaName];
                //    vlab.SchemaName = StataConverter.GetSchemaName(str1);
                //    StataConverter.GetValueLabels1(str1, vlab);
                //}
            }
            streamReader.Close();
            foreach (string key in strs.Keys)
            {
                strs[key].Construct();
            }
            return strs;
        }

        public Dictionary<string, string> GetVarLabels()
        {
            Dictionary<string, string> strs = new Dictionary<string, string>();
            StreamReader streamReader = new StreamReader(filename);
            while (true)
            {
                string str = streamReader.ReadLine();
                string str1 = str;
                if (str == null)
                {
                    break;
                }
                str1 = str1.Trim();
                if (str1.StartsWith("label variable "))
                {
                    str1 = str1.Substring(14).Trim();
                    int num = str1.IndexOf(' ');
                    string str2 = str1.Substring(0, num).Trim();
                    string str3 = str1.Substring(num + 3, str1.Length - num - 5);
                    strs.Add(str2, str3);
                }
            }
            streamReader.Close();
            return strs;
        }
        TextReader reader;

        public DataTable Data => throw new NotImplementedException();

        public string[] GetVarnames()
        {
            return reader.ReadLine().Split(new char[] { '\t' });
        }

        public Dictionary<string, string> GetVarVal()
        {
            Dictionary<string, string> strs = new Dictionary<string, string>();
            StreamReader streamReader = new StreamReader(filename);
            while (true)
            {
                string str = streamReader.ReadLine();
                string str1 = str;
                if (str == null)
                {
                    break;
                }
                str1 = str1.Trim();
                if (str1.StartsWith("label values "))
                {
                    str1 = str1.Substring(13).Trim();
                    string[] strArrays = str1.Split(new char[0]);
                    strs.Add(strArrays[0], strArrays[1]);
                }
            }
            streamReader.Close();
            return strs;
        }

        public VariableInfo[] GetVariableInformations()
        {
            throw new NotImplementedException();
        }
    }
}
