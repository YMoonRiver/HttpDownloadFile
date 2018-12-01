using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace HttpForm
{
    public partial class Form1 : Form
    {
        // 定义并启动线程数组
        System.Threading.Thread[] threads = null;
        HttpMultiThreadDownload[] httpDownloads = null;
        //下载文件URI
        public string SourceURI = null;
        //保存文件名
        public string TargetFileName = null;
        //是否暂停
        public volatile bool isPause = false;
        //是否首次下载
        public bool first = true;
        //线程数
        public int threadNum = 5;
        //每个线程接收文件的大小
        public int[] fileSize { get; set; }
        //每个线程已经接收文件的大小
        public int[] hasReceived { get; set; }
        //每个线程接收文件的文件名
        public string[] fileNames { get; set; }
        //每个线程接收文件的起始位置
        public int[] startPos { get; set; }
        //每个线程继续接收文件的位置
        public int[] restartPos { get; set; }
        //每个线程结束标志
        public bool[] threadIsEnd { get; set; }
        //文件合并标志
        public bool canMerge { get; set; }

        public Form1()
        {
            InitializeComponent();
            downloadButton.Visible = true;
            downloadButton.Enabled = true;
            pauseButton.Visible = false;
            pauseButton.Enabled = false;
        }

        //点击下载按钮
        private void DownloadFile(object sender, EventArgs e)
        {
            downloadButton.Visible = false;
            downloadButton.Enabled = false;
            pauseButton.Visible = true;
            pauseButton.Enabled = true;
            downloadFileURI.Enabled = false;
            saveFileName.Enabled = false;
            isPause = false;
            SourceURI = this.downloadFileURI.Text;
            TargetFileName = this.saveFileName.Text;
            HttpWebRequest request;
            long totalSize = 0;
            try
            {
                if (first)
                {
                    downloadLog.Items.Add(DateTime.Now.ToString() + "  开始下载 " + SourceURI);
                    request = (HttpWebRequest)WebRequest.Create(SourceURI);
                    totalSize = request.GetResponse().ContentLength;
                    downloadLog.Items.Add("文件字节数：" + totalSize);
                    progressBar.Maximum = (int)totalSize;
                    request.Abort();
                    Init(totalSize);
                    threads = new System.Threading.Thread[threadNum];
                    httpDownloads = new HttpMultiThreadDownload[threadNum];
                    for (int i = 0; i < threadNum; i++)
                    {
                        httpDownloads[i] = new HttpMultiThreadDownload(this, i);
                        threads[i] = new System.Threading.Thread(new System.Threading.ThreadStart(httpDownloads[i].receive));
                        threads[i].Start();
                    }
                    System.Threading.Thread merge = new System.Threading.Thread(new System.Threading.ThreadStart(MergeFile));
                    merge.Start();
                }
                else
                {
                    downloadLog.Items.Add(DateTime.Now.ToString() + "  继续下载");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //分配线程：每个线程平均分配文件大小，剩余部分由最后一个线程完成
        private void Init(long totalSize)
        {
            fileNames = new string[threadNum];
            startPos = new int[threadNum];
            restartPos = new int[threadNum];
            fileSize = new int[threadNum];
            hasReceived = new int[threadNum];
            threadIsEnd = new bool[threadNum];
            int size = (int)totalSize / threadNum;
            int endSize = size + (int)totalSize % threadNum;
            for (int i = 0; i < threadNum; i++)
            {
                fileNames[i] = i.ToString() + ".dat";
                startPos[i] = size * i;
                restartPos[i] = size * i;
                fileSize[i] = (i < threadNum - 1) ? (size - 1) : (endSize - 1);
                hasReceived[i] = 0;
                threadIsEnd[i] = false;
            }
        }

        //合并文件
        public void MergeFile()
        {
            while (true)
            {
                canMerge = true;
                for (int i = 0; i < threadNum; i++)
                {
                    //若有未结束线程，则等待
                    if (!threadIsEnd[i]) 
                    {
                        canMerge = false;
                        System.Threading.Thread.Sleep(100);
                        break;
                    }
                }
                if (canMerge == true)
                    break;
            }
            int bufferSize = 1024;
            int readSize;
            byte[] bytes = new byte[bufferSize];
            FileStream fileStream = new FileStream(TargetFileName, FileMode.OpenOrCreate);
            FileStream tempStream = null;
            for (int k = 0; k < threadNum; k++)
            {
                tempStream = new FileStream(fileNames[k], FileMode.Open);
                while (true)
                {
                    readSize = tempStream.Read(bytes, 0, bufferSize);
                    if (readSize > 0)
                        fileStream.Write(bytes, 0, readSize);
                    else
                        break;
                }
                tempStream.Close();
            }
            fileStream.Close();
            MessageBox.Show("下载完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WriteEndLog(DateTime.Now.ToString() + " 下载完成, 保存于 " + TargetFileName);
        }

        private void StopDownload(object sender, EventArgs e)
        {
            isPause = true;
            first = false;
            downloadButton.Text = "继续下载";
            downloadButton.Visible = true;
            downloadButton.Enabled = true;
            pauseButton.Visible = false;
            pauseButton.Enabled = false;
        }

        private void WriteEndLog(string str)
        {
            if (downloadLog.InvokeRequired)
            {
                ProcessingCallback d = new ProcessingCallback(WriteEndLog);
                Invoke(d, new object[] { str });
            }
            else
            {
                downloadLog.Items.Add(str);
                downloadLog.Items.Add("");
                downloadLog.Items.Add("");
                downloadButton.Text = "下载";
                downloadButton.Visible = true;
                downloadButton.Enabled = true;
                pauseButton.Visible = false;
                pauseButton.Enabled = false;
                progressBar.Value = 0;
                progress.Text = "0%";
                downloadFileURI.Enabled = true;
                saveFileName.Enabled = true;
                first = true;
            }
        }
    }
}
