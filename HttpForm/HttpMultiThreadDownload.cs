using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace HttpForm
{
    delegate void ProcessingCallback(string str);

    class HttpMultiThreadDownload
    {
        const int _bufferSize = 1024;
        public Form1 Form { get; set; }
        //线程ID   
        public int ThreadID { get; set; }
        //文件URI
        public string URI { get; set; }

        public HttpMultiThreadDownload(Form1 form, int threadID)
        {
            Form = form;
            ThreadID = threadID;
            URI = form.SourceURI;
        }

        //析构方法
        ~HttpMultiThreadDownload()
        {
            if (!Form.InvokeRequired)
            {
                Form.Dispose();
            }
        }

        public void receive()
        {
            //线程临时文件
            string fileName = Form.fileNames[ThreadID];     
            //接收缓冲区
            byte[] buffer = new byte[_bufferSize];         
            //接收字节数
            int readSize = 0;                              
            Stream stream = null;
            this.WriteLog("   线程[" + ThreadID.ToString() + "] 开始接收......");
            while (true)
            {
                if (!Form.isPause)
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
                            request.AddRange(Form.restartPos[ThreadID], Form.startPos[ThreadID] + Form.fileSize[ThreadID]);
                            stream = request.GetResponse().GetResponseStream();
                            readSize = stream.Read(buffer, 0, _bufferSize);
                            while (!Form.isPause && readSize > 0)
                            {
                                
                                fileStream.Write(buffer, 0, readSize);
                                Form.hasReceived[ThreadID] += readSize;
                                this.WriteLog("   线程[" + ThreadID.ToString() + "] 已接收" + Form.hasReceived[ThreadID] + "字节");
                                readSize = stream.Read(buffer, 0, _bufferSize);
                                Thread.Sleep(100);
                            }
                            Form.restartPos[ThreadID] = Form.startPos[ThreadID] + Form.hasReceived[ThreadID];
                            if (Form.hasReceived[ThreadID] < Form.fileSize[ThreadID])
                                this.WriteLog("   线程[" + ThreadID.ToString() + "] 暂停接收, 已接收" + Form.hasReceived[ThreadID] + "字节");
                            stream.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                if (Form.hasReceived[ThreadID] >= Form.fileSize[ThreadID] + 1)
                    break;
            }
            Form.threadIsEnd[ThreadID] = true;
            this.WriteLog("   线程[" + ThreadID.ToString() + "] 结束");
        }


        private void WriteLog(string str)
        {
            if (Form.downloadLog.InvokeRequired)
            {
                ProcessingCallback d = new ProcessingCallback(WriteLog);
                Form.Invoke(d, new object[] { str });
            }
            else
            {
                Form.downloadLog.Items.Add(str);
                if (!Form.threadIsEnd[ThreadID] && !Form.isPause)
                {
                    int tempValue = 0;
                    for(int i = 0; i < Form.threadNum; i++)
                    {
                        tempValue += Form.hasReceived[i];
                    }
                    Form.progressBar.Value = tempValue;
                    Form.progress.Text = (int)((float)Form.progressBar.Value / (float)Form.progressBar.Maximum * 100) + "%";
                }
            }
        }
    }
}
