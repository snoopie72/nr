using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace NRWeb.Commons
{
    public class ExcelReader
    {
        public void SaveToExcel(ICollection<string> fieldNames, ICollection<Dictionary<string, object>> values,
           string filepath, string sheetname)
        {
            if (!ValidateInput(fieldNames, values))
            {
                throw new ArgumentException("Invalid fields");
            }
            var spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            var workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            var sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = sheetname
            };

            var i = 0;
            foreach (var fieldName in fieldNames)
            {
                i++;
                var cell = InsertCellInWorksheet(i, 1, worksheetPart);
                cell.CellValue = new CellValue(fieldName);
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                uint j = 1;
                foreach (var dictionary in values)
                {
                    j++;

                    if (dictionary[fieldName] != null)
                    {
                        cell = InsertCellInWorksheet(i, j, worksheetPart);
                        cell.CellValue = new CellValue(Convert.ToString(dictionary[fieldName]));
                        cell.DataType = IsNumeric(dictionary[fieldName].GetType())
                            ? new EnumValue<CellValues>(CellValues.Number)
                            : new EnumValue<CellValues>(CellValues.String);    
                    }
                    
                }
            }


            sheets.Append(sheet);

            workbookpart.Workbook.Save();
            spreadsheetDocument.Close();
        }

        private static bool ValidateInput(ICollection<string> fieldNames, IEnumerable<Dictionary<string, object>> values)
        {
            if (fieldNames == null || values == null)
            {
                return false;
            }
            foreach (var dictionary in values)
            {
                if (dictionary.Count != fieldNames.Count)
                {
                    return false;
                }
                if (fieldNames.Any(fieldName => !dictionary.ContainsKey(fieldName)))
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Dictionary<string, object>> LoadFromExcel(string filepath)
        {
            var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var obj =  ReadExcelFileDom(fileStream);
            fileStream.Close();
            return obj;
        }

        public IEnumerable<Dictionary<string, object>> LoadFromExcel(Stream stream)
        {
            return ReadExcelFileDom(stream);
        }

        private static Cell InsertCellInWorksheet(int columnIndex, uint rowIndex, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            var cellReference = columnIndex + "," + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Count(r => r.RowIndex == rowIndex) != 0)
            {
                row = sheetData.Elements<Row>().First(r => r.RowIndex == rowIndex);
            }
            else
            {
                row = new Row { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a theCell with the specified column name, insert one.  
            if (row.Elements<Cell>().Any(c => c.CellReference.Value == cellReference))
            {
                return row.Elements<Cell>().First(c => c.CellReference.Value == cellReference);
            }
            // Cells must be in sequential order according to CellReference. Determine where to insert the new theCell.
            var refCell =
                row.Elements<Cell>()
                    .FirstOrDefault(
                        cell =>
                            String.Compare(cell.CellReference.Value, cellReference,
                                StringComparison.OrdinalIgnoreCase) > 0);

            var newCell = new Cell { CellReference = cellReference };
            row.InsertBefore(newCell, refCell);

            worksheet.Save();
            return newCell;
        }

        public static bool IsNumeric(Type dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException("dataType");

            return (dataType == typeof(int)
                    || dataType == typeof(double)
                    || dataType == typeof(long)
                    || dataType == typeof(short)
                    || dataType == typeof(float)
                    || dataType == typeof(Int16)
                    || dataType == typeof(Int32)
                    || dataType == typeof(Int64)
                    || dataType == typeof(uint)
                    || dataType == typeof(UInt16)
                    || dataType == typeof(UInt32)
                    || dataType == typeof(UInt64)
                    || dataType == typeof(sbyte)
                    || dataType == typeof(Single)
                );
        }

        private IEnumerable<Dictionary<string, object>> ReadExcelFileDom(Stream excelStream)
        {
            var list = new List<Dictionary<string, object>>();
            var keys = new List<String>();
            using (var spreadsheetDocument = SpreadsheetDocument.Open(excelStream, false))
            {
                var workbookPart = spreadsheetDocument.WorkbookPart;
                var worksheetPart = workbookPart.WorksheetParts.First();
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                var i = 0;
                foreach (var r in sheetData.Elements<Row>())
                {
                    var j = 0;
                    var dict = new Dictionary<string, object>();
                    foreach (var c in r.Elements<Cell>())
                    {
                        var value = GetCellValue(workbookPart, c);
                        
                        if (i == 0)
                        {

                            keys.Add(Convert.ToString(value));
                            
                        }
                        else
                        {
                            var key = keys[j];
                            dict.Add(key, value);
                        }
                        j++;
                    }
                    if (i > 0)
                    {
                        list.Add(dict);
                    }
                    i++;

                }
            }
            return list;
        }

       

        public static object GetCellValue(WorkbookPart workbookPart, Cell theCell)
        {
            int a = 1;
            if (theCell.DataType == null)
            {
                return theCell.CellValue.Text;
            }
            var s = theCell.DataType;
            
            if (theCell.DataType.InnerText.Equals("str"))
            {
                return Convert.ToString(theCell.CellValue.Text);
            }
            if (theCell.DataType.InnerText.Equals("n"))
            {
                return Convert.ToDecimal(theCell.CellValue.Text);
            }
            return theCell.DataType == null ? theCell.CellValue.Text : workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(theCell.CellValue.Text)).InnerText;
        }

        public bool ValidateColumns(ICollection<Dictionary<string, object>> elements, ICollection<string> keys)
        {
            if (elements.Any(element => element.Keys.Any(key => !keys.Contains(key))))
            {
                return false;
            }
            if (keys.Any(key => elements.Any(element => !element.ContainsKey(key))))
            {
                return false;
            }
            return true;
        }
    }
}
