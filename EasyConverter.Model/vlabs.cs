using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EasyConverter.Model
{
    public class vlabs
    {
        public int[] data
        {
            get;
            set;
        }

        public List<Lbl> lbls
        {
            get;
            set;
        }

        private int N
        {
            get;
            set;
        }

        public string SchemaName
        {
            get;
            set;
        }

        public byte[] txt
        {
            get;
            set;
        }

        public byte[] ValueLabelTable
        {
            get
            {
                byte[] numArray = new byte[(int)this.data.Length * 4 + (int)this.txt.Length];
                int num = 0;
                for (int i = 0; i < (int)this.data.Length; i++)
                {
                    byte[] bytes = BitConverter.GetBytes(this.data[i]);
                    numArray[num] = bytes[0];
                    numArray[num + 1] = bytes[1];
                    numArray[num + 2] = bytes[2];
                    numArray[num + 3] = bytes[3];
                    num += 4;
                }
                for (int j = 0; j < (int)this.txt.Length; j++)
                {
                    numArray[num] = this.txt[j];
                    num++;
                }
                return numArray;
            }
        }

        public vlabs()
        {
            this.lbls = new List<Lbl>();
        }

        public void Construct()
        {
            this.lbls.Sort();
            this.N = this.lbls.Count;
            int length = 0;
            for (int i = 0; i < this.N; i++)
            {
                length = length + this.lbls[i].Text.Length + 1;
            }
            this.data = new int[2 + 2 * this.N];
            this.data[0] = this.N;
            this.txt = new byte[length];
            int num = 0;
            for (int j = 0; j < this.N; j++)
            {
                this.data[j + 2] = num;
                this.data[j + 2 + this.N] = this.lbls[j].Value;
                string text = this.lbls[j].Text;
                for (int k = 0; k < text.Length; k++)
                {
                    string str = text[k].ToString(CultureInfo.InvariantCulture);
                    byte[] bytes = Encoding.GetEncoding(StataConverter.DefaultCodePage).GetBytes(str);
                    this.txt[num] = bytes[0];
                    num++;
                }
                this.txt[num] = 0;
                num++;
            }
            this.data[1] = num;
        }

        public void WriteToFile(BinaryWriter w)
        {
            byte[] valueLabelTable = this.ValueLabelTable;
            w.Write((int)valueLabelTable.Length);
            Encoding encoding = Encoding.GetEncoding(StataConverter.DefaultCodePage);
            w.Write(encoding.GetBytes(this.SchemaName));
            if (this.SchemaName.Length < 32)
            {
                w.Write(new byte[32 - this.SchemaName.Length]);
            }
            w.Write(0);
            w.Write(valueLabelTable);
        }
    }
}
