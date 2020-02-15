using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject gameObject; //assign player gameobject to variable in the inspector

    public bool lockY = false;
    private Vector3 offset;
    private Vector3 tempVect;
    private const string PlayerTagName = "Player";

    void Start()
    {
        if(gameObject == null)
        {
            Debug.Log("FollowPlayer script will not work properly because game object is not set.");
            return;
        }
        offset = transform.position - gameObject.transform.position; //store initial camera offset.
    }

    void LateUpdate()
    {
        tempVect = gameObject.transform.position + offset;

        if (lockY) // toggle this to allow or disallow y-axis tracking
        {
            tempVect.y -= gameObject.transform.position.y; // remove y component of player position
        }
        transform.position = tempVect;
    }
}
