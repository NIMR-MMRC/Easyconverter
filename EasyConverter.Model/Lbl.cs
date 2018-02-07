using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConverter.Model
{
    public class Lbl : IComparable<Lbl>
    {
        public string Text
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public Lbl()
        {
        }

        public int CompareTo(Lbl other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}
