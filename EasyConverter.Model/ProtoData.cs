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
                return (short)((int)this.VariableInfos.Length);
            }
        }

        public int obs
        {
            get;
            set;
        }

        public VariableInfo[] VariableInfos
        {
            get;
            set;
        }

        public ProtoData()
        {
        }
    }
}
