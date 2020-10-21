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

        public GameObject itemPicker;

        private GameObject actualImage;

        public void UpdateActualImage(GameObject image)
        {
            try
            {
                if (image != null)
                {
                    actualImage = image;

                    foreach (var img in images)
                    {
                        img.SetActive(false);
                    }

                    var children = actualImage.GetComponentInChildren<Image>();
                    var rect = children.GetComponentInChildren<RectTransform>();

                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 40, rect.rect.width);
                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 40, rect.rect.height);
                    actualImage.SetActive(true);
                }
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
                UpdateActualImage(defaultImage);
            }
            else
            {
                UpdateActualImage(images.FirstOrDefault());
            }
            RegisterOnPickerChanged();
        }

        private void RegisterOnPickerChanged()
        {
            itemPicker.GetComponent<PickingItemsController>().LastPickedItemChanged += OnLastPickedChanged;
        }

        private void OnLastPickedChanged(object sender, GameObject hudObj)
        {
            Debug.Log($"HeadUpDisplayController::Updating actual iamge {hudObj.name}");
            UpdateActualImage(hudObj);
        }
    }
}
