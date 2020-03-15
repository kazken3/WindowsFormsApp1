using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WriteTraceLog("アプリケーション開始");
        }



        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                WriteTraceLog("処理開始");
                // ゼロ割り算Exceptionを発生させる
                //int a = 0;
                //a = 1 / a;
                WriteTraceLog("処理終了");
            }
            catch (Exception ex)
            {
                WriteTraceLog("エラーが発生しました", ex);
            }
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <remarks></remarks>
        private static void WriteTraceLog(String msg)
        {
            WriteTraceLog(msg, null);
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="ex">Exception(無指定の場合はメッセージのみ出力)</param>
        /// <remarks></remarks>
        private static void WriteTraceLog(String msg, Exception ex)
        {
            try
            {
                // ログフォルダ名作成
                DateTime dt = DateTime.Now;
                String logFolder = System.AppDomain.CurrentDomain.BaseDirectory + "Log";

                // ログフォルダ名作成
                System.IO.Directory.CreateDirectory(logFolder);

                // ログファイル名作成
                String logFile = logFolder + "\\screen_" + dt.ToString("yyyyMMdd") + ".log";

                // 翌日分のログファイル削除(１ヶ月分のログファイルしか保存しないようにするため)
                String logNext = logFolder + "\\TraceLog" + dt.AddDays(1).ToString("dd") + ".log";
                System.IO.File.Delete(logNext);

                // ログ出力文字列作成
                String logStr;
                logStr = dt.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + msg;
                if (ex != null)
                {
                    logStr = logStr + "\n" + ex.ToString();
                }

                // Shift-JISでログ出力
                System.IO.StreamWriter sw = null;
                try
                {
                    sw = new System.IO.StreamWriter(logFile, true);
                    sw.WriteLine(logStr);
                }
                catch { }
                finally
                {
                    if (sw != null) sw.Close();
                }
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteTraceLog("アプリケーション処理終了");

        }
    }
}
