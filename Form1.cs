using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[,] A = new int[4, 4];
        int[,] B = new int[4, 4];
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_GetResult_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            Random rNum = new Random();
            int n;//定义变量
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    n = rNum.Next(100);
                    A[i, j] = n;//随机生成16个数值
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    textBox1.Text = textBox1.Text + A[i, j].ToString().PadRight(6);
                    //将矩阵A的数据写入textBox1
                }
                textBox1.Text += Environment.NewLine;
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    n = rNum.Next(16);
                    B[i, j] = n;//随机生成16个数值
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    textBox2.Text = textBox2.Text + B[i, j].ToString().PadRight(6);
                    //将矩阵A的数据写入textBox2
                }
                textBox2.Text += Environment.NewLine;
            }


        }

        private void Btn_Mult_Click(object sender, EventArgs e)
        {

            int[,] C = new int[4, 4];

            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    for (int k = 0; k < 4; k++) {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    textBox3.Text = textBox3.Text + C[i, j].ToString().PadRight(6);
                    //将矩阵C中的数据写入textBox3
                }
                textBox3.Text += Environment.NewLine;
            }

        }


        //矩阵加法
        private void Btn_Add_Click(object sender, EventArgs e)
        {
       
                int[,] C = new int[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            C[m, j] = A[m, j] + B[m, j];
                        }
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        textBox3.Text = textBox3.Text + C[i, j].ToString().PadRight(6);
                        //将矩阵C中的数据写入textBox3
                    }
                    textBox3.Text += Environment.NewLine;
                }
   
        }
       
        //矩阵减法
        private void Btn_Sub_Click(object sender, EventArgs e)
        {

                int[,] C = new int[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            C[m, j] = A[m, j] - B[m, j];
                        }
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        textBox3.Text = textBox3.Text + C[i, j].ToString().PadRight(6);
                        //将矩阵C中的数据写入textBox3
                    }
                    textBox3.Text += Environment.NewLine;
                }
   
        }

        private void Btn_Transf_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            Random rNum = new Random();
            int n;//定义变量
            try
            {
                if (text_Row.Text.Length!=0|| text_Colu.Text.Length!=0)
                {
                    int _row = Convert.ToInt32(text_Row.Text);
                    int _colu = Convert.ToInt32(text_Colu.Text);
                    Matrix NMat = new Matrix(_row, _colu);//初始化矩阵行列
                    
                    for (int i = 0; i < _row; i++)
                    {
                        for (int j = 0; j < _colu; j++)
                        {
                            n= rNum.Next(100);
                            NMat.data[i, j] = n;
                        }
                    }

                    //将矩阵A的数据写入textBox1
                    for (int i = 0; i < _row; i++)
                    {
                        for (int j = 0; j < _colu; j++)
                        {
                            textBox1.Text = textBox1.Text + NMat.data[i, j].ToString().PadRight(6);
                        }
                        textBox1.Text += Environment.NewLine;
                    }

                    //转置计算
                    Matrix TransRes = Matrix.transpose(NMat);

                    //将矩阵A转置后显示在textBox3结果中
                    for (int i = 0; i < _colu; i++)
                    {
                        for (int j = 0; j < _row; j++)
                        {
                            textBox3.Text = textBox3.Text + TransRes.data[i, j].ToString().PadRight(6);
                        }
                        textBox3.Text += Environment.NewLine;
                    }

                }
                else
                {
                    MessageBox.Show("请输入行列！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
