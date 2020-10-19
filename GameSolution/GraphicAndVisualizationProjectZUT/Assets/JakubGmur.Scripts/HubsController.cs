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
            actualImage.GetComponentInChildren<RectTransform>().position.Set(offsetX, offsetY, offsetZ);
        }

        public void Update()
        {
            //TODO
        }

    }
}
