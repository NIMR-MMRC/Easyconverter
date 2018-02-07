using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EasyConverter.Model
{
    public class StataConverter : ConverterBase
    {
        public const int DefaultCodePage = 1252;

        public StataConverter()
        {
        }

        private void CheckVarNames(string[] names)
        {
            string[] strArrays = names;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = strArrays[i];
                if (str.Length > 32)
                {
                    throw new Exception(string.Concat("Variable name too long: ", str));
                }
                if ("0123456789".IndexOf(str.Substring(0, 1)) >= 0)
                {
                    throw new Exception(string.Concat("Variable name may not start with a digit: ", str));
                }
                for (int j = 0; j < str.Length; j++)
                {
                    if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_".IndexOf(str[j]) < 0)
                    {
                        throw new Exception(string.Concat("Variable name contains one or more characters that are not allowed: ", str));
                    }
                }
            }
        }

        public void ConvertToStata(IDataSource dataSource  , string doFileName, string dtaFileName, StataConverter.AddMessageDelegate addMessage)
        {
            double num;
            Encoding encoding = Encoding.GetEncoding(1252);
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            addMessage("Inspecting inputs");
            addMessage("  Inspecting data, detecting variable types");
            ProtoData dataTypes = dataSource.GetDataTypes();
            string str = dataTypes.nvar.ToString(invariantCulture);
            int num1 = dataTypes.obs;
            string str1 = string.Format("  Found: {0} variables; {1} observations", str, num1.ToString(invariantCulture));
            addMessage(str1);
            string dataLabel = "";
            Dictionary<string, string> strs = new Dictionary<string, string>();
            Dictionary<string, vlabs> valueLabels = new Dictionary<string, vlabs>();
            Dictionary<string, string> varVal = new Dictionary<string, string>();
            if (File.Exists(doFileName))
            {
                addMessage("  Inspecting do-file, collecting variable labels");
                strs = dataSource.GetVarLabels();
                dataLabel = dataSource.GetDataLabel();
                addMessage("  Inspecting do-file, collecting value labels");
                valueLabels = dataSource.GetValueLabels();
                varVal = dataSource.GetVarVal();
            }
            addMessage("Converting data to Stata dataset");
            BinaryWriter binaryWriter = new BinaryWriter(new FileStream(dtaFileName, FileMode.OpenOrCreate));
            addMessage("  Writing meta-info");
            binaryWriter.Write((byte)113);
            binaryWriter.Write((byte)2);
            binaryWriter.Write((byte)1);
            binaryWriter.Write((byte)0);
            binaryWriter.Write(dataTypes.nvar);
            binaryWriter.Write(dataTypes.obs);
            binaryWriter.Write(encoding.GetBytes(dataLabel));
            if (dataLabel.Length > 80)
            {
                dataLabel = dataLabel.Substring(0, 80);
            }
            binaryWriter.Write(new byte[81 - dataLabel.Length]);
            string str2 = StataConverter.StataDateTime();
            binaryWriter.Write(encoding.GetBytes(str2));
            binaryWriter.Write(new byte[18 - str2.Length]);
            binaryWriter.Write(dataTypes.map);
            for (int i = 0; i < dataTypes.nvar; i++)
            {
                string str3 = dataTypes.vnames[i];
                binaryWriter.Write(encoding.GetBytes(str3));
                binaryWriter.Write(new byte[33 - str3.Length]);
            }
            binaryWriter.Write(new byte[2 * (dataTypes.nvar + 1)]);
            for (int j = 0; j < dataTypes.nvar; j++)
            {
                string str4 = (dataTypes.map[j] <= 244 ? "%9s" : "%6.2f");
                binaryWriter.Write(encoding.GetBytes(str4));
                binaryWriter.Write(new byte[12 - str4.Length]);
            }
            for (int k = 0; k < dataTypes.nvar; k++)
            {
                string str5 = dataTypes.vnames[k];
                if (!varVal.ContainsKey(str5))
                {
                    binaryWriter.Write(new byte[33]);
                }
                else
                {
                    string item = varVal[str5];
                    binaryWriter.Write(encoding.GetBytes(item));
                    binaryWriter.Write(new byte[33 - item.Length]);
                }
            }
            for (int l = 0; l < dataTypes.nvar; l++)
            {
                string str6 = dataTypes.vnames[l];
                string item1 = "";
                if (strs.ContainsKey(str6))
                {
                    item1 = strs[str6];
                }
                if (item1.Length > 80)
                {
                    item1 = item1.Substring(0, 80);
                }
                binaryWriter.Write(encoding.GetBytes(item1));
                binaryWriter.Write(new byte[81 - item1.Length]);
            }
            binaryWriter.Write(new byte[5]);
            addMessage("  Writing data");
            StataMissings stataMissing = new StataMissings();
            StreamReader streamReader = new StreamReader("tabFileName");
            streamReader.ReadLine();
            Label0:
            string str7 = streamReader.ReadLine();
            string str8 = str7;
            if (str7 == null)
            {
                streamReader.Close();
                addMessage("  Writing value labels");
                foreach (vlabs value in valueLabels.Values)
                {
                    value.WriteToFile(binaryWriter);
                }
                binaryWriter.Close();
                addMessage("Done");
                return;
            }
            string[] strArrays = str8.Split(new char[] { '\t' });
            for (int m = 0; m < (int)dataTypes.map.Length; m++)
            {
                string str9 = strArrays[m];
                if (str9.Length > 244)
                {
                    str9 = str9.Substring(1, 244);
                }
                if (dataTypes.map[m] > 244)
                {
                    if (str9 == "")
                    {
                        str9 = ".";
                    }
                    if (!StataMissings.IsMissingValue(str9))
                    {
                        double.TryParse(str9, out num);
                        binaryWriter.Write(num);
                    }
                    else
                    {
                        binaryWriter.Write(stataMissing.GetMissingValue(str9, dataTypes.map[m]));
                    }
                }
                else
                {
                    binaryWriter.Write(encoding.GetBytes(str9));
                    int length = dataTypes.map[m] - str9.Length;
                    if (length > 0)
                    {
                        binaryWriter.Write(new byte[length]);
                    }
                }
            }
            goto Label0;
        }

      

      

        private byte GetNumType(double x)
        {
            return (byte)255;
        }

        private static string GetSchemaName(string line)
        {
            char[] chrArray = new char[] { ' ' };
            return line.Split(chrArray)[2];
        }

      

        private static void GetValueLabels1(string line, vlabs q)
        {
            string empty = string.Empty;
            line = line.Substring(13 + q.SchemaName.Length).Trim();
            while (line.Length > 0 && line.IndexOf(Convert.ToChar(39)) >= 0)
            {
                int num = line.IndexOf(' ');
                string str = line.Substring(0, num);
                line = line.Substring(num + 1);
                num = 0;
                while (line[num] != '\"' || line[num + 1] != Convert.ToChar(39))
                {
                    num++;
                }
                string str1 = line.Substring(2, num - 2);
                line = line.Substring(num + 2).Trim();
                object[] objArray = new object[5];
                char[] chrArray = new char[] { '\t' };
                objArray[0] = empty.Trim(chrArray);
                objArray[1] = '\t';
                objArray[2] = str;
                objArray[3] = '\t';
                objArray[4] = str1;
                empty = string.Concat(objArray);
                Lbl lbl = new Lbl()
                {
                    Text = str1,
                    Value = Convert.ToInt32(str)
                };
                Lbl lbl1 = lbl;
                for (int i = q.lbls.Count - 1; i >= 0; i--)
                {
                    if (q.lbls[i].Value == lbl1.Value)
                    {
                        q.lbls.RemoveAt(i);
                    }
                }
                q.lbls.Add(lbl1);
            }
        }

       

        

       

        private static string StataDateTime()
        {
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            DateTime now = DateTime.Now;
            string[] strArrays = new string[] { "XYZ", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string[] strArrays1 = strArrays;
            string str = now.Day.ToString(invariantCulture);
            if (str.Length == 1)
            {
                str = string.Concat("0", str);
            }
            string str1 = strArrays1[now.Month];
            string str2 = now.Year.ToString(invariantCulture);
            string str3 = now.Hour.ToString(invariantCulture);
            if (str3.Length == 1)
            {
                str3 = string.Concat("0", str3);
            }
            string str4 = now.Minute.ToString(invariantCulture);
            if (str4.Length == 1)
            {
                str4 = string.Concat("0", str4);
            }
            object[] objArray = new object[] { str, str1, str2, str3, str4 };
            return string.Format("{0} {1} {2} {3}:{4}", objArray);
        }

        public delegate void AddMessageDelegate(string msg);
    }
}
