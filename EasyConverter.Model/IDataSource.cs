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
        string[] GetVarnames();
        Dictionary<string, string> GetVarVal();

        ProtoData GetDataTypes();

        string GetDataLabel();


        Dictionary<string, vlabs> GetValueLabels();
    }
}
