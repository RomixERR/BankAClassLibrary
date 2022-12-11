using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HW12_6_BankA
{
    public interface IDataChangeAtributes
    {
        DateTime dateTime { get; set; }
        string WhatDataIsChange { get; set; }
        string TypeDataChange { get; set; }
        string WhoIsChange { get; set; }
    }
}
