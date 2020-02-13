using AssemblyCSharp.Assets.Models;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class Matrix3DTests
    {
        [Test]
        public void SimpleMatrixMultiplicationTest1()
        {
            var matrix1 = new float[4, 4]
            {
             { 5,  7,  9,  10},
             { 2,  3,  3,   8},
             { 8, 10,  2,   3},
             { 3,  3,  4,   8}
            };

            var matrix2 = new float[4, 4]
            {
             { 3,  10,  12,  18},
             { 12,  1,  4,    9},
             { 9,  10,  12,   2},
             { 3,  12,  4,   10}
            };

            var resultArrExpected = new float[4, 4]
            {
             { 210,  267,  236,  271},
             {  93,  149,  104,  149},
             { 171,  146,  172,  268},
             { 105,  169,  128,  169}
            };

            Matrix3D m1 = new Matrix3D(matrix1);
            Matrix3D m2 = new Matrix3D(matrix2);
            var multiplicationResultArr = m1 * m2;

            int size = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.AreEqual(resultArrExpected[j, i], multiplicationResultArr[j, i]);
                    Debug.Log($"[{j}{i}] { resultArrExpected[j, i] == multiplicationResultArr[j, i]}");
                }
            }
        }


        [Test]
        public void SimpleMatrixMultiplicationTest2()
        {
            var matrix1 = new float[4, 4]
            {
             { 5,  5,  5,  5},
             { 2,  2,  2,  2},
             { 8, 8,   8,  8},
             { 3,  3,  3,  3}
            };

            var matrix2 = new float[4, 4]
            {
             { 1,  0,  2,  3},
             { 0,  0,  0,  0},
             { 0,  0,  0,  0},
             { 1,  0,  2,  3}
            };

            var resultArrExpected = new float[4, 4]
            {
             { 10,  0,  20,  30},
             {  4,  0,   8,  12},
             { 16,  0,  32,  48},
             { 6,   0,  12,  18}
            };

            Matrix3D m1 = new Matrix3D(matrix1);
            Matrix3D m2 = new Matrix3D(matrix2);
            var multiplicationResultArr = m1 * m2;

            int size = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.AreEqual(resultArrExpected[j, i], multiplicationResultArr[j, i]);
                    Debug.Log($"[{j}{i}] { resultArrExpected[j, i] == multiplicationResultArr[j, i]}");
                }
            }
        }

        [Test]
        public void MatrixTranslation()
        {
            var matrix3d = new Matrix3D();
            var initialVector = new Vector4(x: 10, y: 10, z: 10, w: 1);
            var expectedVector = new Vector4(x: 20, y: 10, z: 10, w: 1);

            var offsetVector = new Vector4(10, 0, 0, 1);

            matrix3d = Matrix3D.GenerateTranslationMatrix(matrix3d, offsetVector);

            Assert.AreEqual(1, matrix3d[0, 0]);
            Assert.AreEqual(1, matrix3d[1, 1]);
            Assert.AreEqual(1, matrix3d[2, 2]);
            Assert.AreEqual(1, matrix3d[3, 3]);

            Assert.AreEqual(10, matrix3d[3, 0]);
            Assert.AreEqual(0, matrix3d[3, 1]);
            Assert.AreEqual(0, matrix3d[3, 2]);


            var translatedVector = Matrix3D.Multiply(matrix3d, initialVector);

            int size = 4;
            for (int i = 0; i < size; i++)
            {
                Debug.Log($"|| [{i}] || {translatedVector[i] == expectedVector[i]} || result:{translatedVector[i]} || expected:{expectedVector[i]}");
                Assert.AreEqual(expectedVector[i], translatedVector[i]);
            }
        }

        [Test]
        public void MatrixTranslationTest2()
        {
            var matrix3d = new Matrix3D();
            var initialVector = new Vector4(x: 0, y: 0, z: 0, w: 1);
            var expectedVector = new Vector4(x: 10, y: 0, z: 0, w: 1);

            var offsetVector = new Vector4(10, 0, 0, 1);

            matrix3d = Matrix3D.GenerateTranslationMatrix(matrix3d, offsetVector);

            Assert.AreEqual(1, matrix3d[0, 0]);
            Assert.AreEqual(1, matrix3d[1, 1]);
            Assert.AreEqual(1, matrix3d[2, 2]);
            Assert.AreEqual(1, matrix3d[3, 3]);

            Assert.AreEqual(10, matrix3d[3, 0]);
            Assert.AreEqual(0, matrix3d[3, 1]);
            Assert.AreEqual(0, matrix3d[3, 2]);

            var translatedVector = Matrix3D.Multiply(matrix3d, initialVector);

            int size = 4;
            for (int i = 0; i < size; i++)
            {
                Debug.Log($"|| [{i}] || {translatedVector[i] == expectedVector[i]} || result:{translatedVector[i]} || expected:{expectedVector[i]}");
                Assert.AreEqual(expectedVector[i], translatedVector[i]);
            }
        }

        [Test]
        public void MatrixScaling()
        {
            var scalingMatrix = new Matrix3D();
            var vectorToBeScaled = new Vector4(x: 16, y: 16, z: 16, w: 1);

            var expectedVector = new Vector4(x: 32, y: 32, z: 32, w: 1);
            var scaleVector = new Vector4(2, 2, 2, 1);

            scalingMatrix = Matrix3D.GenerateScalingMatrix(scalingMatrix, scaleVector);

             var scaledVector = Matrix3D.Multiply(scalingMatrix, vectorToBeScaled);

            int size = 4;
            for (int i = 0; i < size; i++)
            {
                Debug.Log($"|| [{i}] || {scaledVector[i] == expectedVector[i]} || result:{scaledVector[i]} || expected:{expectedVector[i]}");
                Assert.AreEqual(expectedVector[i], scaledVector[i]);
            }
        }
    }
}
