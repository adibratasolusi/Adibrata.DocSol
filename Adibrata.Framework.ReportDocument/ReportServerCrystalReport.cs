using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adibrata.Framework.Logging;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.ReportSource;
using System.IO;

namespace Adibrata.Framework.ReportDocument
{
    //[CLSCompliant(false)]
    public class ReportServerCrystalReport
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument _docreport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        ParameterDiscreteValue _paramdiscrete;
        ParameterFields _paramfields;
        //ParameterFieldDefinition _paramdefinition;
        ParameterValues _paramvalues;

        public enum DocumentType { Word, PDF, EXCEL, DataOnly, Text};
        public ReportServerCrystalReport (ReportingEntities _ent)
        {
            try
            {

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerCrystalReport",
                    FunctionName = "ReportServerCrystalReport / Constructor",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public void AddParameter (string _name, string _value)
        {
            try
            {
                _paramdiscrete = new ParameterDiscreteValue();

                _paramfields = new ParameterFields();
                //_paramfields._paramdefinition = _docreport.DataDefinition.ParameterFields(_name);
                _paramfields.Add(_name);
                _paramdiscrete.Value = _value;
                _paramvalues = new ParameterValues();
                //_paramvalues = _paramdefinition.DefaultValues;
                _paramvalues.Add(_paramdiscrete);
                //_paramdefinition.ApplyCurrentValues(_paramvalues);

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerCrystalReport",
                    FunctionName = "AddParameter",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }
        
        public void GetReportDocument() 
        { 
            try
            {

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerCrystalReport",
                    FunctionName = "GetReportDocument",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
        }

        public void ExportDocument (DocumentType _doctype)
        {
            try
            {

            }
            catch (Exception _exp)
            {
                ErrorLogEntities _errent = new ErrorLogEntities
                {
                    UserName = "REPORT",
                    NameSpace = "Adibrata.Framework.ReportDocument",
                    ClassName = "ReportServerCrystalReport",
                    FunctionName = "ExportDocument",
                    ExceptionNumber = 1,
                    EventSource = "Report",
                    ExceptionObject = _exp,
                    EventID = 1, // 1 Untuk Framework 
                    ExceptionDescription = _exp.Message
                };
                ErrorLog.WriteEventLog(_errent);
            }
          
        }

    }
}

#region "Remark"

//'Produced By Henry Sudarma
//#End Region
//Imports CrystalDecisions.ReportAppServer.ClientDoc
//Imports CrystalDecisions.ReportAppServer.Controllers
//Imports CrystalDecisions.ReportAppServer.DataDefModel
//Imports CrystalDecisions.ReportAppServer.DataSetConversion

//Public Class CrystalReportHelper
//    Private _servername As String
//    Private _reportclientdocument As New ReportClientDocument
//    Private _reportdatset As System.Data.DataSet
//    Private _xsdpath As String
//    Private _reportpath As String

//#Region "Property"
//    Public Property ServerName() As String
//        Get
//            Return _servername
//        End Get
//        Set(ByVal Value As String)
//            _servername = Value
//        End Set
//    End Property

//    Public Property ReportDataSet() As System.Data.DataSet
//        Get
//            Return _reportdatset
//        End Get
//        Set(ByVal Value As System.Data.DataSet)
//            _reportdatset = Value
//        End Set
//    End Property

//    Public Property XSDPath() As String
//        Get
//            Return _xsdpath
//        End Get
//        Set(ByVal Value As String)
//            _xsdpath = Value
//        End Set
//    End Property

//    Public Property ReportPath() As String
//        Get
//            Return _reportpath
//        End Get
//        Set(ByVal Value As String)
//            _reportpath = Value
//        End Set
//    End Property
//#End Region

//    Public Function CreateReport() As ReportClientDocument
//        Dim Path As Object

//        ' create report client document


//        _reportclientdocument.ReportAppServer = Me.ServerName


//        ' new report document
//        '_reportclientdocument.[New]()

//        ' add a datasource using a schema file
//        ' note that if you have distributed environment, you should use AddDataSourceUsingDataSet method instead.
//        ' for more information, refer to comments on these methods.
//        ' grab customer name and country field

//        'Path = ReportPath
//        Dim oReportPath As Object
//        oReportPath = ReportPath
//        _reportclientdocument.Open(oReportPath)
//        If Not IsNothing(Me.ReportDataSet) Then
//            AddDataSourceUsingSchemaFile(_reportclientdocument, Me.XSDPath, "Test", Me.ReportDataSet)
//            'AddDataSourceUsingDataSet(Me.ReportDataSet)
//        End If

//        'oOriginalReportPath = "rassdk://" + sBaseDir + sOriginalReport
//        'rasOriginal.Open(oOriginalReportPath, 1)

//        ' view report
//        Return _reportclientdocument

//    End Function

//    Public Sub AddDataSourceUsingDataSet(ByVal data As System.Data.DataSet)

//        ' add a datasource
//        DataSetConverter.AddDataSource(_reportclientdocument, data)

//    End Sub
//    Public Sub AddDataSourceUsingSchemaFile()
//        AddDataSourceUsingSchemaFile(_reportclientdocument, Me.XSDPath, "", Me.ReportDataSet)
//    End Sub

//    Private Sub AddDataSourceUsingSchemaFile( _
//                ByRef rcDoc As ReportClientDocument, _
//                ByVal schema_file_name As String, _
//                ByVal table_name As String, _
//                ByVal data As System.Data.DataSet)

//        Dim crLogonInfo As PropertyBag          ' logon info
//        Dim crAttributes As PropertyBag         ' logon attributes
//        Dim crConnectionInfo As ConnectionInfo  ' connection info
//        Dim crTable As CrystalDecisions.ReportAppServer.DataDefModel.Table
//        ' database table

//        ' create logon property
//        crLogonInfo = New PropertyBag
//        crLogonInfo("XML File Path") = schema_file_name

//        ' create logon attributes
//        crAttributes = New PropertyBag
//        crAttributes("Database DLL") = "crdb_adoplus.dll"
//        crAttributes("QE_DatabaseType") = "ADO.NET (XML)"
//        crAttributes("QE_ServerDescription") = "NewDataSet"
//        crAttributes("QE_SQLDB") = True
//        crAttributes("QE_LogonProperties") = crLogonInfo

//        ' create connection info
//        crConnectionInfo = New ConnectionInfo
//        crConnectionInfo.Kind = CrConnectionInfoKindEnum.crConnectionInfoKindCRQE
//        crConnectionInfo.Attributes = crAttributes

//        ' create a table
//        'crTable = New CrystalDecisions.ReportAppServer.DataDefModel.Table
//        'crTable.ConnectionInfo = crConnectionInfo
//        'crTable.Name = table_name
//        'crTable.Alias = table_name

//        ' add a table
       
//        '_reportclientdocument.DatabaseController.AddTable(crTable, Nothing)

//        ' pass dataset
//        _reportclientdocument.DatabaseController.SetDataSource(DataSetConverter.Convert(data), data.Tables(0).TableName, data.Tables(0).TableName)
//    End Sub

//#Region "PassParameter"
//    Public Sub PassParameter( _
//           ByRef doc As ReportClientDocument, _
//           ByVal report_name As String, _
//           ByVal param_name As String, _
//           ByVal param_value As Object)

//        ' create parameter discrete value
//        Dim param_val As New ParameterFieldDiscreteValue

//        ' set parameter value
//        param_val.Value = param_value

//        ' create parameter value collection
//        Dim vals As New Values

//        ' add parameter value to this collection
//        vals.Add(param_val)

//        ' set current value
//        doc.DataDefController.ParameterFieldController.SetCurrentValues(report_name, param_name, vals)
//    End Sub

//    ' identical to method above except this method takes index of the parameter instead of name
//    Public Sub PassParameter(ByVal param_index As Integer, _
//            ByVal param_value As Object)

//        PassParameter(_reportclientdocument, "", _reportclientdocument.DataDefinition.ParameterFields(param_index).Name, param_value)
//    End Sub
//#End Region

//#Region "Pass Field"
//    Dim iField As Integer
//    Dim field_table As Field


//    Private Sub PassField(ByRef doc As ReportClientDocument, ByVal fieldname As String)
//        iField = doc.Database.Tables(0).DataFields.Find(fieldname, CrFieldDisplayNameTypeEnum.crFieldDisplayNameName, CeLocale.ceLocaleUserDefault)
//        field_table = CType(doc.Database.Tables(0).DataFields(iField), CrystalDecisions.ReportAppServer.DataDefModel.Field)
//        ' add customer name and country field
//        doc.DataDefController.ResultFieldController.Add(-1, field_table)

//    End Sub

//    ' identical to method above except this method takes index of the parameter instead of name
//    Public Sub PassField(ByVal fieldname As String)
//        PassField(fieldname)
//    End Sub
//#End Region

//End Class
#endregion 

#region "cr Standard Helper"
//#Region "Imports"
//Imports CrystalDecisions.Shared
//Imports CrystalDecisions.CrystalReports.Engine
//Imports CrystalDecisions.Web
//Imports CrystalDecisions.ReportSource
//Imports System.IO
//#End Region

//<CLSCompliant(False)> Public Class CrystalReportStandardHelper

//    Private oDocumentReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
//    Dim discrete As ParameterDiscreteValue
//    Dim ParamFields As ParameterFields
//    Dim ParamField As ParameterFieldDefinition
//    Dim CurrentValue As ParameterValues

//    Public Enum FileType As Integer
//        Excel = 1
//        Word = 2
//        PDF = 3
//        OnlyData = 4
//        Text = 5
//    End Enum

//    'Amelya230708 begin
//    Public Enum FileTypeExport As Integer
//        Text
//        Doc
//        HTML
//        RTF
//        PDF
//    End Enum
//    'Amelya 230708 end
//    Public Sub AddParameter(ByVal oParametersName As String, ByVal oParametersValue As String)
//        '----------Parameter---------
//        ParamFields = New ParameterFields
//        ParamField = oDocumentReport.DataDefinition.ParameterFields(oParametersName)
//        discrete = New ParameterDiscreteValue
//        discrete.Value = oParametersValue
//        CurrentValue = New ParameterValues
//        CurrentValue = ParamField.DefaultValues
//        CurrentValue.Add(discrete)
//        ParamField.ApplyCurrentValues(CurrentValue)
//    End Sub

//    Public Sub ReportPath(ByVal oPathReport As String, ByVal oReportName As String)
//        Dim _Path As String
//        'appPath = MapPath(".")
//        _Path = oPathReport + "\\" + oReportName
//        oDocumentReport.Load(_Path)
//    End Sub

//    Public Overloads Sub DataSource(ByVal oDataSet As DataSet)
//        Dim oDatatable As DataTable
//        oDatatable = oDataSet.Tables(0)
//        oDocumentReport.SetDataSource(oDatatable)
//    End Sub
//    Public Overloads Sub DataSource(ByVal oDataTable As DataTable)
//        oDocumentReport.SetDataSource(oDataTable)
//    End Sub
//    Public Function ReportBind() As CrystalDecisions.CrystalReports.Engine.ReportDocument
//        Return oDocumentReport
//    End Function

//    Public Overloads Sub OpenSubReport(ByVal oSubReportName As String, ByVal oDataSetSubReport As DataSet)
//        oDocumentReport.OpenSubreport(oSubReportName).SetDataSource(oDataSetSubReport)
//    End Sub
//    Public Overloads Sub OpenSubReport(ByVal oSubReportName As String, ByVal oDataTableSubReport As DataTable)
//        oDocumentReport.OpenSubreport(oSubReportName).SetDataSource(oDataTableSubReport)
//    End Sub
//    Public Overloads Sub ExportReport(ByVal oFileLocation As String)
//        Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//        oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//        oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
//        DiskOpts.DiskFileName = oFileLocation
//        oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//        oDocumentReport.Export()
//    End Sub

//    Public Overloads Sub ExportReport(ByVal oFileLocation As String, ByVal typeFile As FileType)
//        Select Case typeFile
//            Case FileType.Excel
//                Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//                oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.Excel
//                DiskOpts.DiskFileName = oFileLocation
//                oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//                oDocumentReport.Export()
//            Case FileType.PDF
//                Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//                oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
//                DiskOpts.DiskFileName = oFileLocation
//                oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//                oDocumentReport.Export()
//            Case FileType.Word
//                Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//                oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.WordForWindows
//                DiskOpts.DiskFileName = oFileLocation
//                oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//                oDocumentReport.Export()
//            Case FileType.OnlyData
//                Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//                oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.ExcelRecord
//                DiskOpts.DiskFileName = oFileLocation
//                oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//                oDocumentReport.Export()
//                'Case FileType.Text
//                '    Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//                '    oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//                '    oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.Text
//                '    DiskOpts.DiskFileName = oFileLocation
//                '    oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//                '    oDocumentReport.Export()
//        End Select

//    End Sub

//    'Amelya 230708 begin
//    Public Overloads Sub ExportReport2(ByVal oFileLocation As String, ByVal FileType As FileTypeExport)
//        Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//        oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//        Select Case FileType
//            'Case FileTypeExport.Text
//            '    oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.Text
//            Case FileTypeExport.Doc
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.WordForWindows
//            Case FileTypeExport.HTML
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.HTML40
//            Case FileTypeExport.RTF
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.RichText
//            Case FileTypeExport.PDF
//                oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat

//        End Select

//        DiskOpts.DiskFileName = oFileLocation
//        oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//        oDocumentReport.Export()
//    End Sub
//    'Amelya 230708 end

//    Protected Overrides Sub Finalize()
//        oDocumentReport.Dispose()
//        MyBase.Finalize()
//    End Sub

//    'jibril 11 feb 2014
//    Public Sub ExportReportToExcel(ByVal oFileLocation As String)
//        Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
//        oDocumentReport.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
//        oDocumentReport.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.Excel 'jibril 6 maret 2014
//        DiskOpts.DiskFileName = oFileLocation
//        oDocumentReport.ExportOptions.DestinationOptions = DiskOpts
//        oDocumentReport.Export()
//    End Sub
//    'end jibril
//End Class

#endregion 