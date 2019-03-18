using System;
using System.Collections.Generic;
namespace Matrix
{
    public class MatrixException : Exception
    {
        public MatrixException(string Message) : base(Message)
        {
        }
    }

    public static class ListExtensions
    {
        private static bool AnyDiagonalMatrix(this List<Matrix> list)
        {
            foreach(var m in list)
            {
                if (m.IsDiag())
                    return true;
            }
            return false;
        }
    }

    public struct Matrix : IEquatable<Matrix>
    {
        private readonly int rows;
        private readonly int cols;
        private readonly double[,] matrix;

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            matrix = new double[this.rows, this.cols];
        }

        public static Matrix Random(int rows, int cols, int minValue, int maxValue)
        {
            Random random = new Random();
            Matrix matrix = new Matrix(rows, cols);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                    matrix[i, j] = (double)random.Next(-minValue, maxValue);
            }
            return matrix;
        }

        public static Matrix Random(int rows, int cols)
        {
            Random random = new Random();
            Matrix matrix = new Matrix(rows, cols);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                    matrix[i, j] = random.NextDouble();
            }
            return matrix;
        }

        public Matrix Clone()
        {
            Matrix newMatrix = new Matrix(rows, cols);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                    newMatrix[i, j] = matrix[i, j];
            }
            return newMatrix;
        }

        public override string ToString()
        {
            string s = string.Empty;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                    s += this[i, j] + " ";
                s += "\r\n";
            }
            return s;
        }

        public bool Equals(Matrix other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (rows != other.rows || cols != other.cols)
                return false;

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    if (this[i, j] != other[i, j])
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((Matrix)obj);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                    hash ^= matrix[i, j].GetHashCode();
            }
            hash ^= Tuple.Create(rows, cols).GetHashCode();
            return hash;
        }

        public bool IsDiag()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    if (i != j && this[i, j] != 0)
                        return false;
                }
            }
            return true;
        }

        // Operators
        public double this[int row, int col]
        {
            get { return matrix[row, col]; }
            set { matrix[row, col] = value; }
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (m1 == null)
                return m2 == null;

            return m1.Equals(m2);
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.rows != m2.rows || m1.cols != m2.cols)
                throw new MatrixException("Matrices must have the same size");

            Matrix newMatrix = new Matrix(m1.rows, m1.cols);
            for (int i = 0; i < newMatrix.rows; ++i)
            {
                for (int j = 0; j < newMatrix.cols; ++j)
                    newMatrix[i, j] = m1[i, j] + m2[i, j];
            }
            return newMatrix;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.rows != m2.rows || m1.cols != m2.cols)
                throw new MatrixException("Matrices must have the same size");

            Matrix newMatrix = new Matrix(m1.rows, m1.cols);
            for (int i = 0; i < newMatrix.rows; ++i)
            {
                for (int j = 0; j < newMatrix.cols; ++j)
                    newMatrix[i, j] = m1[i, j] - m2[i, j];
            }
            return newMatrix;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.cols != m2.rows)
                throw new MatrixException("Matrices must have the same size");

            Matrix newMatrix = new Matrix(m1.rows, m2.cols);
            for (int i = 0; i < newMatrix.rows; ++i)
            {
                for (int j = 0; j < newMatrix.cols; ++j)
                {
                    for (int k = 0; k < m1.cols; ++k)
                        newMatrix[i, j] += m1[i, k] * m2[k, j];
                }
            }
            return newMatrix;
        }
    }
}
