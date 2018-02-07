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
           return (from System.Data.DataColumn f in dataTable.Columns select f.ColumnName ).ToArray() ;
        }

        public Dictionary<string, string> GetVarVal()
        {
            throw new NotImplementedException();
        }
    }
}
