﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public sealed class Matrix
    {
        int row, column;            //矩阵的行列数
        public double[,] data;            //矩阵的数据

        #region 构造函数
        public Matrix(int rowNum, int columnNum)
        {
            row = rowNum;
            column = columnNum;
            data = new double[row, column];
        }
        public Matrix(double[,] members)
        {
            row = members.GetUpperBound(0) + 1;
            column = members.GetUpperBound(1) + 1;
            data = new double[row, column];
            Array.Copy(members, data, row * column);
        }
        public Matrix(double[] vector)
        {
            row = 1;
            column = vector.GetUpperBound(0) + 1;
            data = new double[1, column];
            for (int i = 0; i < vector.Length; i++)
            {
                data[0, i] = vector[i];
            }
        }
        #endregion


        #region 属性和索引器
        public int rowNum { get { return row; } }
        public int columnNum { get { return column; } }

        public double this[int r, int c]
        {
            get { return data[r, c]; }
            set { data[r, c] = value; }
        }
        #endregion


        public override string ToString()
        {
            string strRet = "";
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                {
                    strRet += data[i, j] + " , ";
                }
            return strRet;
        }
        #region 转置
        /// <summary>
        /// 将矩阵转置，得到一个新矩阵（此操作不影响原矩阵）
        /// </summary>
        /// <param name="input">要转置的矩阵</param>
        /// <returns>原矩阵经过转置得到的新矩阵</returns>
        public static Matrix transpose(Matrix input)
        {
            double[,] inverseMatrix = new double[input.column, input.row];
            for (int r = 0; r < input.row; r++)
                for (int c = 0; c < input.column; c++)
                    inverseMatrix[c, r] = input[r, c];
            return new Matrix(inverseMatrix);
        }
        #endregion

        #region 得到行向量或者列向量
        public Matrix getRow(int r)
        {
            if (r > row || r <= 0) throw new Exception("没有这一行。");
            double[] a = new double[column];
            Array.Copy(data, column * (row - 1), a, 0, column);
            Matrix m = new Matrix(a);
            return m;
        }
        public Matrix getColumn(int c)
        {
            if (c > column || c < 0) throw new Exception("没有这一列。");
            double[,] a = new double[row, 1];
            for (int i = 0; i < row; i++)
                a[i, 0] = data[i, c];
            return new Matrix(a);
        }
        #endregion

        #region 操作符重载  + - * / == !=
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.row != b.row || a.column != b.column)
                throw new Exception("矩阵维数不匹配。");
            Matrix result = new Matrix(a.row, a.column);
            for (int i = 0; i < a.row; i++)
                for (int j = 0; j < a.column; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }


        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a + b * (-1);
        }

        public static Matrix operator *(Matrix matrix, double factor)
        {
            Matrix result = new Matrix(matrix.row, matrix.column);
            for (int i = 0; i < matrix.row; i++)
                for (int j = 0; j < matrix.column; j++)
                    matrix[i, j] = matrix[i, j] * factor;
            return matrix;
        }
        public static Matrix operator *(double factor, Matrix matrix)
        {
            return matrix * factor;
        }

        //a 行元素 * b 列元素
        //a 列数 == b行数
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.column != b.row)
                throw new Exception("矩阵维数不匹配。");
            Matrix result = new Matrix(a.row, b.column);
            for (int i = 0; i < a.row; i++)
                for (int j = 0; j < b.column; j++)
                    for (int k = 0; k < a.column; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (object.Equals(a, b)) return true;
            if (object.Equals(null, b))
                return a.Equals(b);
            return b.Equals(a);
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Matrix)) return false;
            Matrix t = obj as Matrix;
            if (row != t.row || column != t.column) return false;
            return this.Equals(t, 10);
        }
        /// <summary>
        /// 按照给定的精度比较两个矩阵是否相等
        /// </summary>
        /// <param name="matrix">要比较的另外一个矩阵</param>
        /// <param name="precision">比较精度（小数位）</param>
        /// <returns>是否相等</returns>
        public bool Equals(Matrix matrix, int precision)
        {
            if (precision < 0) throw new Exception("小数位不能是负数");
            double test = Math.Pow(10.0, -precision);
            if (test < double.Epsilon)
                throw new Exception("所要求的精度太高，不被支持。");
            for (int r = 0; r < this.row; r++)
                for (int c = 0; c < this.column; c++)
                    if (Math.Abs(this[r, c] - matrix[r, c]) >= test)
                        return false;

            return true;
        }
        #endregion
    }
}
