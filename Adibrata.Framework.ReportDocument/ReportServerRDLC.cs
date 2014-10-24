﻿using Microsoft.Reporting.WebForms;
using System.IO;
using System;
using Adibrata.Framework.Logging; 

namespace Adibrata.Framework.ReportDocument
{
    public class ReportServerRDLC
    {
        #region "How To Use this Class"
          //ReportingEntities _ent = new ReportingEntities();
          //   _defaultreportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
          //   _defaultreportpath += "CoaSchemeDetailRpt.rdlc";
             
          //   _ent.ReportPath = _defaultreportpath;
          //   _ent.ReportData = GetData();
          //   _ent.DataSetName = "CoaSchemeDetail_DS";
          //   ReportServerRDLC _report = new ReportServerRDLC(_ent);
          //   _ent = _report.ReportOutput(_ent, ReportServerRDLC.DocumentType.PDF);
          //   _ent.FileNameDocument = "test";
          //   _ent.Extention = "pdf";
          //   Response.Buffer = true;
          //   Response.Clear();
          //   Response.ContentType = _ent.MimeDocument;
          //   Response.AddHeader("content-disposition", "attachment; filename=" + _ent.FileNameDocument + "." + _ent.Extention);
          //   Response.BinaryWrite(_ent.ReportResult); // create the file
        //   Response.Flush(); // send it to the client to download
        #endregion 

        public enum DocumentType { Word, PDF, EXCEL};

        ReportViewer _viewer = new ReportViewer();
       
        public ReportServerRDLC(ReportingEntities _ent)
        {
            try
            {
                if (File.Exists(@_ent.ReportPath))
                {
                    
                    _viewer.LocalReport.ReportPath = @_ent.ReportPath;
                }
                else
                {
                    throw new Exception("File Report Template Not Exists");
                }
                if (_ent.ReportData != null)
                {
                    // Create Report DataSource
                    ReportDataSource rds = new ReportDataSource(_ent.DataSetName,_ent.ReportData);
                    _viewer.ProcessingMode = ProcessingMode.Local;
                    _viewer.LocalReport.DataSources.Clear();
                    _viewer.LocalReport.DataSources.Add(rds);
                    //_viewer.DataBind();
                }
                if (_ent.SubReportData != null)
                {
                    //_viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler()
                    _viewer.LocalReport.DataSources.Add(new ReportDataSource(_ent.SubReportDataSetName, _ent.SubReportData));
                }
            }

            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerRDLC",
                    FunctionName = "ReportServerRDLC / Construtor",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public ReportingEntities ReportOutput(ReportingEntities _ent, DocumentType documenttype)
        {
            Warning[] warnings;
            string[] streamIds;
            
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            try
            {
                _ent.ReportResult = _viewer.LocalReport.Render(documenttype.ToString(), null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                _ent.MimeDocument = mimeType;
                _ent.Encoding = encoding;
                _ent.Extention = extension;
            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerRDLC",
                    FunctionName = "ReportOutput",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
            return _ent;
        }
    }
}
