using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class HeadUpDisplayController : MonoBehaviour
    {
        public static HeadUpDisplayController Instance = null;


        public List<GameObject> hudImages;
        public GameObject defaultImage;

        private string activePlayerName;
        private Dictionary<string, GameObject> playersImages = new Dictionary<string, GameObject>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateActiveImageList(PickedItemEventArgs pickedItemArgs)
        {
            if (pickedItemArgs != null)
            {
                playersImages[pickedItemArgs.playerName] = pickedItemArgs.pickableData.HeadUpDisplayObj;
            }
        }

        public void Start()
        {
            foreach (var image in hudImages)
            {
                image.SetActive(false);
            }

            foreach (var playerObj in GameObjectSwitcher.Instance.playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                playersImages.Add(player.name, defaultImage);
            }
            RegisterOnEveryPlayerPicking();
            RegisterOnPlayerSwitched();
        }

        private void RegisterOnPlayerSwitched()
        {
            GameObjectSwitcher.Instance.OnMainPlayerChanged += OnMainPlayerChanged;
        }

        private void OnMainPlayerChanged(object sender, PlayerObject e)
        {
            activePlayerName = e.gameObject.name;
            UpdateDisplay(activePlayerName);
        }

        private void RegisterOnEveryPlayerPicking()
        {
            foreach (var playerObj in GameObjectSwitcher.Instance.playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                player.pickingItemsController.LastPickedItemChanged += OnLastPickedChanged;
            }
        }

        private void OnLastPickedChanged(object sender, PickedItemEventArgs e)
        {
            UpdateActiveImageList(e);
            UpdateDisplay(e.playerName);
        }

        private void UpdateDisplay(string activePlayerName)
        {
            foreach (var img in hudImages)
            {
                if(img.active)
                {
                    img.SetActive(false);
                }
            }

            //getting the actual image for player
            var actualImageForPlayer = playersImages[activePlayerName];
            
            //need to get to a rect and position the image
            var children = actualImageForPlayer.GetComponentInChildren<Image>();
            var rect = children.GetComponentInChildren<RectTransform>();
            rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 40, rect.rect.width);
            rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 40, rect.rect.height);

            //make it active!
            actualImageForPlayer.SetActive(true);
        }
    }
}
