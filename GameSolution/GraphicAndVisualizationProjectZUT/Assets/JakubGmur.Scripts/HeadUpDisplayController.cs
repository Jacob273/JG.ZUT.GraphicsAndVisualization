using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class HeadUpDisplayController : MonoBehaviour
    {
        public List<GameObject> images;
        public GameObject defaultImage;
        private GameObject actualImage;

        public void UpdateActualImage(GameObject image)
        {
            try
            {
                actualImage = image;
            }
            catch (Exception ex)
            {
                Debug.Log($"Could not set actual image. {ex.Message}");
                actualImage = defaultImage;
            }
        }

        public void Start()
        {
            foreach (var image in images)
            {
                image.SetActive(false);
            }

            if (defaultImage != null)
            {
                actualImage = defaultImage;
            }
            else
            {
                actualImage = images.FirstOrDefault();
            }

            actualImage.SetActive(true);

            var children = actualImage.GetComponentInChildren<Image>();
            var rect = children.GetComponentInChildren<RectTransform>();

            rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 40, rect.rect.width);
            rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 40, rect.rect.height);
        }

        public void Update()
        {
            //TODO
        }

    }
}
