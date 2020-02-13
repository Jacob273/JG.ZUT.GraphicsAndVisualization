using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    using Assets.Enums;
    using global::AssemblyCSharp.Assets.Models;
    using UnityEngine;

    public class PlanetarySystemRotation : MonoBehaviour
    {
        public string name;

        /// <summary>
        /// Obiekt ktorym obracamy wzgledem 0,0,0.
        /// </summary>
        public PlanetarySystemRotation center;

        /// <summary>
        /// Obiekt ktorym obracamy.
        /// </summary>
        public GameObject gameObjectToRotate;

        /// <summary>
        /// Narastajacy kat.
        /// </summary>
        public float angle = 30f;

        /// <summary>
        /// Odleglosc od punktu 0,0,0.
        /// </summary>
        public float R = 1f;

        /// <summary>
        /// Tempo obracania.
        /// </summary>
        public float speed = 1f;

        /// <summary>
        /// Macierz rotacji.
        /// </summary>
        Matrix3D rotationMatrix;
        
        /// <summary>
        /// Macierz translacji.
        /// </summary>
        Matrix3D translationMatrix;

        /// <summary>
        /// Wektor przesuniceia
        /// </summary>
        private Vector3 offset;

        /// <summary>
        /// Wektor wynikowy po przesunieciu.
        /// </summary>
        private Vector3 vectorPrim;

        /// <summary>
        /// Zlozone dwie macierze lokalne translacji i rotacji z uwzgledniona rotacja parenta.
        /// </summary>
        private Matrix3D combinedParentAndChildrenMatrix;

        public Matrix3D GetCurrentTranslationMatrix()
        {
            return translationMatrix;
        }

        public Matrix3D GetMatrix()
        {
            if(center != null)
            {
                return translationMatrix * center.rotationMatrix;
            }
            return translationMatrix;
        }

        // Start is called before the first frame update
        void Start()
        {
            rotationMatrix = new Matrix3D();
            translationMatrix = new Matrix3D();

            if (gameObjectToRotate != null)
            {
                    //Stworzenie wektora przesuniecia wzdluz osi Z       
                    offset = new Vector4(0, 0, R, 1);

                    //Domyslna macierz translacji uwzgledniajaca przesuniecie o wektor offset
                    translationMatrix = Matrix3D.GenerateTranslationMatrix(translationMatrix, offset);

                    //Przemnozenie wektora 0,0,0 przez macierz przesuniecia
                    vectorPrim = Matrix3D.Multiply(translationMatrix, new Vector4(0, 0, 0, 1));
                    gameObjectToRotate.transform.position = vectorPrim;
                }
        }

        void Update()
        {
            if (gameObjectToRotate != null && center == null)
            {
                //  Zmiana macierzy rotacji o kat, wzgledem osi XZ
                rotationMatrix = Matrix3D.GenerateRotationMatrix_Y(rotationMatrix, angle += 0.5f * speed * Time.deltaTime);
                //Zlozenie macierzy
                var localMatrixMultiplicated = rotationMatrix * translationMatrix;
                gameObjectToRotate.transform.position = Matrix3D.Multiply(localMatrixMultiplicated, vectorPrim);
                //Przypisanie nowej pozycji
            }

            if(gameObjectToRotate != null && center != null)
            {
                rotationMatrix = Matrix3D.GenerateRotationMatrix_Y(rotationMatrix, angle += 0.5f * speed * Time.deltaTime);
                var localCombined = rotationMatrix * translationMatrix;
                //  Zmiana macierzy rotacji o kat, wzgledem osi XZ
                var parentMatrix = center.GetMatrix();
                //Zlozenie macierzy
                var allCombined = localCombined * parentMatrix;
                gameObjectToRotate.transform.localPosition = Matrix3D.Multiply(allCombined, vectorPrim);
                //Przypisanie nowej pozycji
            }
        }
    }
}
