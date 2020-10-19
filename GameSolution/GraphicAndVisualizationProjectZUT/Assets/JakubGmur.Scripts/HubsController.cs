using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class HubsController : MonoBehaviour
    {
        public List<GameObject> images;
        public GameObject defaultImage;
        
        private GameObject actualImage;


        private const float offsetX = 520.0f;
        private const float offsetY = -285.0f;
        private const float offsetZ = 140.0f;

        public void Start()
        {
            foreach(var image in images)
            {
                image.SetActive(false);
            }

            if(defaultImage != null)
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
            Debug.Log(rect.rect.width);
            Debug.Log(rect.rect.height);
        }

        public void Update()
        {
            //TODO
        }

    }
}
