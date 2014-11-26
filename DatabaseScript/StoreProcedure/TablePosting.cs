//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adibrata.Database.Script.StoreProcedure
{
    class TablePosting
    {
        DataTable _tablePosting = new DataTable() ;

        public DataTable  StructureTablePosting()
        {
            _tablePosting.Clear();
            _tablePosting.Columns.Add("SequenceNo");
            _tablePosting.Columns.Add("COANAME");
            _tablePosting.Columns.Add("Post");
            _tablePosting.Columns.Add("Amount");
            _tablePosting.Columns.Add("RefDesc");
            _tablePosting.Columns.Add("DepartmentID");
            _tablePosting.Columns.Add("VoucherDesc");
            return _tablePosting;
        }

    }
}

