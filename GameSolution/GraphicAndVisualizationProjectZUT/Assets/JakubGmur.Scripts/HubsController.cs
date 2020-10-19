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
        }

        public void Update()
        {
            //TODO
        }

    }
}
