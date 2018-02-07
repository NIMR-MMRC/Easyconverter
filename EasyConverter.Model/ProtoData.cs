using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConverter.Model
{
    public class ProtoData
    {
        public const int strlimit = 244;

        public byte[] map
        {
            get;
            set;
        }

        public short nvar
        {
            get
            {
                return (short)((int)this.vnames.Length);
            }
        }

        public int obs
        {
            get;
            set;
        }

        public string[] vnames
        {
            get;
            set;
        }

        public ProtoData()
        {
        }
    }
}
