using Assets.JakubGmur.Scripts;
using UnityEngine;

public class PointsDrawer : MonoBehaviour
{

    public IPointsLoader _pointsLoader;

    const int verticalOffset = 5;

    // Start is called before the first frame update
    void Start()
    {
        _pointsLoader = GetComponent<IPointsLoader>();

        if (_pointsLoader == null)
        {
            Debug.Log("Points loader could not be found...");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var point in _pointsLoader.GetPoints())
        {
            var offsetPoint = new Vector3(point.x, point.y + verticalOffset, point.z);
            Debug.DrawLine(point, offsetPoint, Color.red, 2, false);
        }
    }
}
