using OfficeOpenXml;
using System;
using System.Data;
using System.Linq;


namespace ChemAnalyst.ViewModel
{

    public static class ExcelPackageExtensions
    {
        public static DataTable ToYearlyDataTable(this ExcelPackage package)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 6])
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 6];
                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }

            return table;
        }

        public static DataTable ToMonthlyDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 7])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 7];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 4])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 4];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToDataTable_DemandByEndUse(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToDataTechnologyLicensorTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            try
            {
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 8])// workSheet.Dimension.End.Column
                {
                    table.Columns.Add(firstRowCell.Text.Trim());
                }
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, 8];//workSheet.Dimension.End.Column

                    var newRow = table.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text.Trim();
                    }
                    table.Rows.Add(newRow);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }


            return table;
        }

        public static DataTable ToDataTable_IndustryMarketSize(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDataTable_Demandbygrade(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToDataTable_DemandByRegion(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToDataTable_DemandByCompanyShare(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToDataTableDemandBySalesChanels(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
                table.Columns.Add(firstRowCell.Text.Trim());

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();

                foreach (var cell in row)
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();

                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDataPortWiseImportTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 17])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 16];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDataPortWiseExportTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 17])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 17];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDatacustomerTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 13])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 13];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDataSupplierAnalysisTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 12])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 12];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToWeeklyNewDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 9])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 9];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }



        public static DataTable ToQuarterlyDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToLocDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 6])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 6];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDailyDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 100])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 100];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDailyNewDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 10])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 10];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


        public static DataTable ToWeekDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 41])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 41];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToCDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 5])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 5];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToMaintenanceAndShutdownDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 19])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 19];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToExpectedNewCapacityDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 8])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 8];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToRegionalCapacityChangeDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 4])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 4];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable ToPermenantShutdownDataTable(this ExcelPackage package)
        {

            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 7])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 7];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


        public static DataTable ToPlantMergerDataTable(this ExcelPackage package)
        {

            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 7])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 7];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
        public static DataTable TologisticDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 12])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 12];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToDataTable(this ExcelPackage package, int noOfCol)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, noOfCol])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, noOfCol];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


        public static DataTable FreightIndexDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 8])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 8];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable PreSalesDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 15])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 15];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


        public static DataTable ProcurementCompany(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, 6])// workSheet.Dimension.End.Column
            {
                table.Columns.Add(firstRowCell.Text.Trim());
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 6];//workSheet.Dimension.End.Column

                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text.Trim();
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


    }

}