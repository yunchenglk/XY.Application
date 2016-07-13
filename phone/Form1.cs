using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace phone
{
    public partial class Form1 : Form
    {
        DataTable dTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //创建一个选择文件对话窗
            OpenFileDialog ofd = new OpenFileDialog();
            //为对话框设置标题
            ofd.Title = "请选择上传文件";
            //设置筛选的图片格式
            ofd.Filter = " Excel文件|*.xls|Excel文件|*.xlsx";
            //设置是否允许多选
            ofd.Multiselect = false;
            //如果你点了“确定”按钮
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //获得文件的完整路径（包括名字后后缀）
                string filePath = ofd.FileName;
                //找到文件名比如“1.xlsx”前面的那个“\”的位置
                int position = filePath.LastIndexOf("\\");
                //从完整路径中截取出来文件名“1.xlsx”
                string fileName = filePath.Substring(position + 1);
                var temp = ExceltoDataSet(filePath);

            }
        }
        //打开方法 
        public DataTable ExceltoDataSet(string path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            string tableName = schemaTable.Rows[0][2].ToString().Trim();

            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "Select   *   From   [" + tableName + "]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);

            ds = new DataSet();

            myCommand.Fill(ds, tableName);
            System.Data.DataTable dt = ds.Tables[0];

            dTable = dt;
            return dt;
        }
    }
}
