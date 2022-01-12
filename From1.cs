using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 串口通讯
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                
                try
                {
                    serialPort1.Open();
                    button1.Text = "关闭串口";

                    
                   

                }
                catch (Exception)
                {
                    MessageBox.Show("打开串口失败", "提示");
                    
                }

            }
            else
            {
                
                try
                {
                    serialPort1.Close();
                    
                    button1.Text = "打开串口";
                }
                catch (Exception)
                {
                    MessageBox.Show("关闭串口失败", "提示");
                    
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            { MessageBox.Show("请先关闭串口","提示"); }
            else
            { 
                comboBox1.Items.Clear();
                try
                {
                    string[] portList = System.IO.Ports.SerialPort.GetPortNames(); //自动获取串行口名称

                    for (int i = 0; i < portList.Length; i++)
                    {
                        string name = portList[i];
                        comboBox1.Items.Add(name);
                    }
                    comboBox1.Text = portList[0];
                }
                catch
                {
                    MessageBox.Show("未检测到串口！", "提示");
                }            
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                }
                catch
                {
                    MessageBox.Show("串口设置错误","提示");
                }
            
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);         //波特率
                serialPort1.Parity = 0;
                serialPort1.StopBits = (System.IO.Ports.StopBits)1;
                serialPort1.DataBits = 8;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            if (serialPort1.IsOpen)
            {
                    string str1;
                    try
                    {
                    if (button3.Text == "点灯")
                    {
                         str1 = "0";
                        button3.Text = "关灯";
                    }
                    else
                    {
                         str1 = "1";
                        button3.Text = "点灯";
                    }

                    byte[] Data = Encoding.ASCII.GetBytes(str1.Substring(0, 1));

                     serialPort1.Write(Data, 0, 1);


                        while (serialPort1.BytesToRead > 0)
                        {
                            byte serialReadByte = (byte)serialPort1.ReadByte();


                            textBox2.Text += serialReadByte;
                            if (Convert.ToInt32(serialReadByte) == 65)
                            {
                                my_LED1.LedStatus = true;
                                
                        }
                            else if (Convert.ToInt32(serialReadByte) == 97)
                            {
                                my_LED1.LedStatus = false;
                                 
                        }
                        }
                }
                    catch (Exception)
                    {
                        MessageBox.Show("发送错误", "提示");
                    }
                
            }
        }

        private void button6_Click(object sender, EventArgs e)//检测灯泡和继电器的状态
        {
           if(serialPort1.IsOpen)
            {
               
               string str2 = "9";
                byte serialReadByte;
                byte[] Data = Encoding.ASCII.GetBytes(str2.Substring(0, 1));
                 serialPort1.Write(Data, 0, 1);
               
                    while (serialPort1.BytesToRead > 0)
                {
                    serialReadByte = (byte)serialPort1.ReadByte();
                    textBox2.Text += serialReadByte;
                    if (Convert.ToInt32(serialReadByte) == 97)
                        {
                            my_LED1.LedStatus = true;
                            button3.Text = "关灯";
                        }
                        else if (Convert.ToInt32(serialReadByte) == 65)
                        {
                            my_LED1.LedStatus = false;
                            button3.Text = "点灯";
                        }
                    }
            }
           else
            { MessageBox.Show("请先将串口打开", "提示"); }
        }
    }
}
