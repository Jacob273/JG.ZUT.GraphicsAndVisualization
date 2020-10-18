using Assets.Enums;
using System;
using UnityEngine;

namespace AssemblyCSharp.Assets.Models
{
    public class Matrix3D
    {
        #region Private members

        public const int RowsCount = 4;

        public const int ColumnsCount = 4;

        #endregion

        #region Construction

        public Matrix3D()
        {
            InitializeMatrixWithZeros();
        }

        public Matrix3D(Matrix3D matrix)
        {
            InitializeMatrixWithZeros();
        }

        public Matrix3D(float[,] matrix)
        {
            if (matrix.GetLength(0) != RowsCount || matrix.GetLength(1) != ColumnsCount)
            {
                throw new Exception($"Matrix3D can be initialized only with [{RowsCount}, {ColumnsCount}] dimension array.");
            }
            Matrix = matrix;
        }

        #endregion

        #region Public properties
        protected float[,] Matrix { get; set; }

        public float this[int row, int col]
        {
            get
            {
                return Matrix[row, col];
            }

            set
            {
                if (Matrix != null)
                {
                    Matrix[row, col] = value;
                }
                else
                {
                    throw new Exception("Cannot se value to unitialized matrix.");
                }
            }
        }

        #endregion

        #region Operators

        public static Matrix3D operator *(Matrix3D m1, Matrix3D m2)
        {
            var matrix1 = m1;
            var matrix2 = m2;

            int size = 0;
            if (RowsCount == ColumnsCount)
            {
                size = RowsCount == ColumnsCount ? RowsCount : throw new Exception("Rows count and columns count has to represent the same value.");
            }

            var resultArr = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float num = 0;
                    for (int k = 0; k < size; k++)
                    {
                        num += matrix1[i, k] * matrix2[k, j];
                    }
                    resultArr[i, j] = num;
                }
            }

            return new Matrix3D(resultArr);
        }

        #endregion

        #region Public Static methods

        public static Matrix3D GenerateTranslationMatrix(Matrix3D translationMatrix, Vector4 offset)
        {
            translationMatrix[0, 0] = 1;
            translationMatrix[1, 1] = 1;
            translationMatrix[2, 2] = 1;
            translationMatrix[3, 3] = 1;

            translationMatrix[3, 0] = offset.x;
            translationMatrix[3, 1] = offset.y;
            translationMatrix[3, 2] = offset.z;
            return translationMatrix;
        }

        public static Matrix3D GenerateScalingMatrix(Matrix3D scalingMatrix, Vector4 scaleVector)
        {
            scalingMatrix[0, 0] = scaleVector.x;//x
            scalingMatrix[1, 1] = scaleVector.y;//y
            scalingMatrix[2, 2] = scaleVector.z;//z
            scalingMatrix[3, 3] = 1;
            return scalingMatrix;
        }

        public static Matrix3D GenerateRotationMatrix_X(Matrix3D rotationMatrix, double angle)
        {
            rotationMatrix[0, 0] = 1;
            rotationMatrix[0, 1] = 0;
            rotationMatrix[0, 2] = 0;
            rotationMatrix[0, 3] = 0;


            rotationMatrix[1, 0] = 0;
            rotationMatrix[1, 1] = (float)Math.Cos(angle);
            rotationMatrix[1, 2] = (float)Math.Sin(angle);
            rotationMatrix[1, 2] = 0;


            rotationMatrix[2, 0] = 0;
            rotationMatrix[2, 1] = (float)Math.Sin(angle);
            rotationMatrix[2, 2] = (float)Math.Cos(angle);
            rotationMatrix[2, 3] = 0;

            rotationMatrix[3, 0] = 0;
            rotationMatrix[3, 1] = 0;
            rotationMatrix[3, 2] = 0;
            rotationMatrix[3, 3] = 1;

            return rotationMatrix;
        }

        public static Matrix3D GenerateRotationMatrix_Y(Matrix3D rotationMatrix, double angle)
        {
            rotationMatrix[0, 0] = (float)Math.Cos(angle);
            rotationMatrix[0, 1] = 0;
            rotationMatrix[0, 2] = (float)Math.Sin(angle);

            rotationMatrix[1, 0] = 0;
            rotationMatrix[1, 1] = 1;
            rotationMatrix[1, 2] = 0;

            rotationMatrix[2, 0] = (float)Math.Sin(angle) * -1;
            rotationMatrix[2, 1] = 0;
            rotationMatrix[2, 2] = (float)Math.Cos(angle);

            return rotationMatrix;
        }

        public static Matrix3D GenerateRotationMatrix(Matrix3D rotationMatrix, double angle)
        {
            rotationMatrix[0, 0] = (float)Math.Cos(angle);
            rotationMatrix[0, 1] = (float)Math.Sin(angle) * -1;
            rotationMatrix[0, 2] = 0;

            rotationMatrix[1, 0] = (float)Math.Sin(angle);
            rotationMatrix[1, 1] = (float)Math.Cos(angle);
            rotationMatrix[1, 2] = 0;

            rotationMatrix[2, 0] = 0;
            rotationMatrix[2, 1] = 0;
            rotationMatrix[2, 2] = 1;

            return rotationMatrix;
        }

        #endregion

        #region Private methods

        public static Vector4 Multiply(Matrix3D matrix, Vector4 vector)
        {
            int size = RowsCount == ColumnsCount ? RowsCount : throw new Exception("Rows count and columns count has to represent the same value.");
            float[,] resultArr = new float[1, ColumnsCount];
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    resultArr[0, i] += (matrix[j,i] * vector[j]);
                }
            }

            return new Vector4(resultArr[0, 0], resultArr[0, 1], resultArr[0, 2], resultArr[0, 3]);
        }

        private void InitializeMatrixWithZeros()
        {
            Matrix = new float[RowsCount, ColumnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    Matrix[i, j] = 0;
                }
            }
        }

        private static void InitializeMatrixWithZeros(Matrix3D matrix)
        {
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        #endregion
    }

    public static class Extensions
    {
        public static double ToRadian(this double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static void Print(this Matrix3D matrix)
        {
            int size = 4;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Debug.Log($"[{i}, {j}] {matrix[i, j]}");
                }
            }
        }

        public static double[] ToEulerAnglesArray(this Matrix3D matrix)
        {
            var sy = Math.Sqrt(matrix[0, 0] * matrix[0, 0] + matrix[1, 0] * matrix[1, 0]);

            bool singular = sy < 1e-6;
            double x, y, z;

            if (!singular)
            {
                x = Math.Atan2(matrix[2, 1], matrix[2, 2]);
                y = Math.Atan2(-matrix[2, 0], sy);
                z = Math.Atan2(matrix[1, 0], matrix[0, 0]);
            }
            else
            {
                x = Math.Atan2(-matrix[1, 2], matrix[1, 1]);
                y = Math.Atan2(-matrix[2, 0], sy);
                z = 0;
            }

            return new double[] { x, y, z };
        }

        public static Vector3 ToEulerAnglesVector(this Matrix3D matrix)
        {
            var eulerAngles = ToEulerAnglesArray(matrix);
            return new Vector3((float)eulerAngles[0], (float)eulerAngles[1], (float)eulerAngles[2]);
        }
    }
}
