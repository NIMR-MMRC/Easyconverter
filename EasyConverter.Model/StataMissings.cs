using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConverter.Model
{
    internal class StataMissings
    {
        private Dictionary<string, byte[]> _miss = new Dictionary<string, byte[]>();

        public StataMissings()
        {
            Dictionary<string, byte[]> strs = this._miss;
            byte[] numArray = new byte[] { 0, 0, 0, 0, 0, 0, 224, 127 };
            strs.Add(".", numArray);
            this._miss.Add(".a", new byte[] { 0, 0, 0, 0, 0, 1, 224, 127 });
            this._miss.Add(".b", new byte[] { 0, 0, 0, 0, 0, 2, 224, 127 });
            this._miss.Add(".c", new byte[] { 0, 0, 0, 0, 0, 3, 224, 127 });
            this._miss.Add(".d", new byte[] { 0, 0, 0, 0, 0, 4, 224, 127 });
            this._miss.Add(".e", new byte[] { 0, 0, 0, 0, 0, 5, 224, 127 });
            this._miss.Add(".f", new byte[] { 0, 0, 0, 0, 0, 6, 224, 127 });
            this._miss.Add(".g", new byte[] { 0, 0, 0, 0, 0, 7, 224, 127 });
            this._miss.Add(".h", new byte[] { 0, 0, 0, 0, 0, 8, 224, 127 });
            this._miss.Add(".i", new byte[] { 0, 0, 0, 0, 0, 9, 224, 127 });
            this._miss.Add(".j", new byte[] { 0, 0, 0, 0, 0, 10, 224, 127 });
            this._miss.Add(".k", new byte[] { 0, 0, 0, 0, 0, 11, 224, 127 });
            this._miss.Add(".l", new byte[] { 0, 0, 0, 0, 0, 12, 224, 127 });
            this._miss.Add(".m", new byte[] { 0, 0, 0, 0, 0, 13, 224, 127 });
            this._miss.Add(".n", new byte[] { 0, 0, 0, 0, 0, 14, 224, 127 });
            this._miss.Add(".o", new byte[] { 0, 0, 0, 0, 0, 15, 224, 127 });
            this._miss.Add(".p", new byte[] { 0, 0, 0, 0, 0, 16, 224, 127 });
            this._miss.Add(".q", new byte[] { 0, 0, 0, 0, 0, 17, 224, 127 });
            this._miss.Add(".r", new byte[] { 0, 0, 0, 0, 0, 18, 224, 127 });
            this._miss.Add(".s", new byte[] { 0, 0, 0, 0, 0, 19, 224, 127 });
            this._miss.Add(".t", new byte[] { 0, 0, 0, 0, 0, 20, 224, 127 });
            this._miss.Add(".u", new byte[] { 0, 0, 0, 0, 0, 21, 224, 127 });
            this._miss.Add(".v", new byte[] { 0, 0, 0, 0, 0, 22, 224, 127 });
            this._miss.Add(".w", new byte[] { 0, 0, 0, 0, 0, 23, 224, 127 });
            this._miss.Add(".x", new byte[] { 0, 0, 0, 0, 0, 24, 224, 127 });
            this._miss.Add(".y", new byte[] { 0, 0, 0, 0, 0, 25, 224, 127 });
            this._miss.Add(".z", new byte[] { 0, 0, 0, 0, 0, 26, 224, 127 });
        }

        public byte[] GetMissingValue(string dotValue, byte type)
        {
            if (type != 255 && type != 254 && type != 251)
            {
                throw new NotImplementedException("Missing values for non-double types are not defined");
            }
            return this._miss[dotValue];
        }

        public static bool IsMissingValue(object value)
        {
            if (value ==null||System.DBNull.Value.Equals(value) ||value.ToString() == "." || value.ToString() == ".a" || value.ToString() == ".b" || value.ToString() == ".c" || value == ".d" || value == ".e" || value == ".f" || value == ".g" || value == ".h" || value == ".i" || value == ".j" || value == ".k" || value == ".l" || value == ".m" || value == ".n" || value == ".o" || value == ".p" || value == ".q" || value == ".r" || value == ".s" || value == ".t" || value == ".u" || value == ".v" || value == ".w" || value == ".x" || value == ".y")
            {
                return true;
            }
            return value.ToString() == ".z";
        }
    }
}
