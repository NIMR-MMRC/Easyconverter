using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConverter.Model
{
  public class VariableInfo
    {

        public string Name { get; set; }


        public int Length { get; set; }


        public int Decimals { get; set; }

        public bool IsTime { get; set; }
        public bool isDate { get; set; } = false;
    }
}
