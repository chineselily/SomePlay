using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtil
{
    public static Vector3 WorldToTTUIPosition(Vector3 worldPos, string achor="center")
    {
        Vector3 sv;
        float halfWidth = Camera.main.pixelWidth / 2;
        float halfHeight = Camera.main.pixelHeight / 2;
        sv = Camera.main.WorldToScreenPoint(worldPos);
        if(achor == "center")
        {
            sv.x = sv.x - halfWidth;
            sv.y = sv.y - halfHeight;

            if (sv.x <= -halfWidth) sv.x = -halfWidth + 50;
            if (sv.x >= halfWidth) sv.x = halfWidth - 50;

            if (sv.y <= -halfHeight) sv.y = -halfHeight + 50;
            if (sv.y >= halfHeight) sv.y = halfHeight - 50;
        }
        return sv;
    }
}
