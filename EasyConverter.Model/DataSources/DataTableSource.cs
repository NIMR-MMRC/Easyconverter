using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConverter.Model.DataSources
{
    public class DataTableSource : IDataSource
    {
        public string GetDataLabel()
        {
            throw new NotImplementedException();
        }

        public ProtoData GetDataTypes()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetVarVal()
        {
            throw new NotImplementedException();
        }
    }
}
