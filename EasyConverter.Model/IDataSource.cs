using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConverter.Model
{
public    interface IDataSource
    {

        System.Data.DataTable Data
        { get; }
        Dictionary<string, string> GetVarLabels();
        VariableInfo[] GetVariableInformations();
        Dictionary<string, string> GetVarVal();

        ProtoData GetDataTypes();

        string GetDataLabel();


        bool IsColumnText(string columnName);


        Dictionary<string, vlabs> GetValueLabels();
    }
}
