using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private float _holeSizePercent = 5;

    [SerializeField] private SpriteRenderer _leftBar;
    [SerializeField] private SpriteRenderer _rightBar;

    private void Start()
    {
        Vector4 bounds = FindBounds();
        float holePositionPercent = Random.Range(_holeSizePercent, 100 - _holeSizePercent);

        float total = bounds.y * 2;
        float holeSize = (_holeSizePercent / 100);

        var leftStartX = bounds.x;
        var leftEndX = ((holePositionPercent / 100) * total) - total / 2;

        var rightStartX = bounds.y;
        var rightEndX = -(rightStartX - leftEndX - holeSize);


        _leftBar.transform.localPosition = new Vector3(leftStartX, 0, 0);
        _leftBar.size = new Vector2(leftEndX, _leftBar.size.y);

        _rightBar.transform.localPosition = new Vector3(rightStartX, 0, 0);
        _rightBar.size = new Vector2(rightEndX, _leftBar.size.y);

        print(ToString());

        string ToString()
        {
            return string.Format("Bounds: ({0}, {1}, {2}, {3}), \nHole Size Percent: {4}, \nHole Size: {5} \nLeft Start: {6}, \nLeft End: {7}, \nRight Start: {8}, \nRight End: {9}, \nTotal: {10}",
                bounds.x, bounds.y, bounds.z, bounds.w, holePositionPercent, holeSize, leftStartX, leftEndX, rightStartX, rightEndX, total);
        }
    }
    private Vector4 FindBounds()
    {
        Camera mainCam = Camera.main;
        Transform camPos = mainCam.transform;
        float vertExtent = mainCam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        
        var zMin = -vertExtent + camPos.position.z + 0.5f;
        var zMax = vertExtent + camPos.position.z - 0.5f;
        var xMin = -horzExtent + 0.5f;
        var xMax = horzExtent - 0.5f;

        return new Vector4(xMin, xMax, zMin, zMax);
    }
}


