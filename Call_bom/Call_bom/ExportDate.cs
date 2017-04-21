using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SQLite;
using NPOI.SS.Util;

namespace Call_bom
{
    public partial class ExportDate : Form
    {
        public ExportDate()
        {
            InitializeComponent();
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string startTime = dtpStartTime.Value.ToString("yyyy-MM-dd 00:00:00");
            string endTime = dtpEndTime.Value.ToString("yyyy-MM-dd 23:59:59");

            string connectionString = "Data Source=" + @"C:/Users/admin/Desktop/Call_Bom/Call_bom/Call_bom/bin/Debug/BomDB.sqlite" + ";Version=3;";

            string sql = string.Format("SELECT * FROM BomInformation where Call_Date >= '{0}' and Call_Date <= '{1}'", startTime, endTime);

            ExportFromDataBase("物料清单", sql, connectionString);

        }

        
/// <summary>
/// 导出excel
/// </summary>
/// <param name="fileName"></param>
/// <param name="sql"></param>
/// <param name="connectionString"></param>
        public void ExportFromDataBase(string fileName,string sql, string connectionString)
        {
            int sheetNum = 1;
            int rowCount = 0;
            int columnCount = 0;

            SaveFileDialog sfd = new SaveFileDialog();
     
            sfd.Filter = "Excel 2007格式|*.xls";
            sfd.FileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssms");
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            HSSFWorkbook wb = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)wb.CreateSheet(fileName + sheetNum);

            ICellStyle styleCommon = wb.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            styleCommon.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;



            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {

                    using (SQLiteDataReader adapter = command.ExecuteReader())
                    {
                        while (adapter.Read())
                        {
                            if(rowCount == 0)
                            {
                                HSSFRow headRow = (HSSFRow)sheet.CreateRow(0);

                                HSSFCell headCell = (HSSFCell)headRow.CreateCell(0, CellType.String);
                                headCell.SetCellValue("点料清单");

                                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));
                                ICellStyle style = wb.CreateCellStyle();
                                //设置单元格的样式：水平对齐居中
                                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                                //新建一个字体样式对象
                                IFont font = wb.CreateFont();
                                //设置字体加粗样式
                                font.Boldweight = short.MaxValue;
                                font.FontHeight = 20 * 20;
                                //使用SetFont方法将字体样式添加到单元格样式中 
                                style.SetFont(font);
                                //将新的样式赋给单元格
                                headCell.CellStyle = style;
                                //设置单元格的高度
                                headRow.Height = 30 * 20;
                                //设置单元格的宽度
                                sheet.SetColumnWidth(0, 20 * 256);



                                columnCount = 0;
                                rowCount = 1;
                                HSSFRow headRowSecond = (HSSFRow)sheet.CreateRow(rowCount);

                                HSSFCell headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("点料日期");
                                columnCount++;
                                sheet.SetColumnWidth(0, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;

                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("点料时间");
                                columnCount++;
                                sheet.SetColumnWidth(1, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;

                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                columnCount++;
                                headCellSecond.SetCellValue("配送时间");
                                sheet.SetColumnWidth(2, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;


                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("站点");
                                columnCount++;
                                sheet.SetColumnWidth(3, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;


                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("工位");
                                columnCount++;
                                sheet.SetColumnWidth(4, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;

                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("料号编码");
                                columnCount++;
                                sheet.SetColumnWidth(5, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;

                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("物料描述");
                                columnCount++;
                                sheet.SetColumnWidth(6, 50 * 256);
                                headCellSecond.CellStyle = styleCommon;

                                

                                headCellSecond = (HSSFCell)headRowSecond.CreateCell(columnCount);
                                headCellSecond.SetCellValue("数量");
                                columnCount++;
                                sheet.SetColumnWidth(5, 20 * 256);
                                headCellSecond.CellStyle = styleCommon;

                                rowCount = 2;
                            }
                            columnCount = 0;
                            

                            HSSFRow row = (HSSFRow)sheet.CreateRow(rowCount);

                            HSSFCell cell = (HSSFCell)row.CreateCell(columnCount);

                            string callDate = adapter["Call_Date"].ToString();
                            string[] date = callDate.Split(' ');
                            cell.SetCellValue(date[0]);
                            columnCount++;
                            cell.CellStyle = styleCommon;


                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.SetCellValue(adapter["Call_Time"].ToString());
                            columnCount++;
                            cell.CellStyle = styleCommon;

                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.CellStyle = styleCommon;
                            columnCount++;
                            if (adapter["Check_Time"] != null)
                            {
                                cell.SetCellValue(adapter["Check_Time"].ToString());
                            }
                            else
                            {
                                cell.SetCellType(CellType.Blank);
                            }

                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.SetCellValue(adapter["Station"].ToString());
                            columnCount++;
                            cell.CellStyle = styleCommon;


                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.SetCellValue(adapter["Work_Station"].ToString());
                            columnCount++;
                            cell.CellStyle = styleCommon;

                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.SetCellValue(adapter["Bom_Number"].ToString());
                            columnCount++;
                            cell.CellStyle = styleCommon;

                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.SetCellValue(adapter["Bom_Describe"].ToString());
                            columnCount++;
                            cell.CellStyle = styleCommon;

                            cell = (HSSFCell)row.CreateCell(columnCount);
                            cell.SetCellValue(adapter["Quantity"].ToString());
                            columnCount++;
                            cell.CellStyle = styleCommon;

                            rowCount++;
                            if (rowCount == 65535)
                            {
                                sheetNum++;
                                rowCount = 0;
                                sheet = (HSSFSheet)wb.CreateSheet(fileName + sheetNum);
                            }

                        }

                        using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            wb.Write(fs);
                        }
                        MessageBox.Show("导出成功！", "导出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //DataTable data = new DataTable();
                    //adapter.Fill(data);

                    //return data;
                }

                connection.Close();
            }

            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
