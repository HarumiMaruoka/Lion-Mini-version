using System;
using UnityEngine;

namespace Lion
{
    public static class MovementUtilities
    {
        public static bool IsArrived(Vector3 startPoint, Vector3 currentPoint, Vector3 endPoint)
        {
            if (startPoint == endPoint || currentPoint == endPoint) return true;

            if (startPoint.x <= endPoint.x && startPoint.y <= endPoint.y) // �E��
            {
                if (currentPoint.x >= endPoint.x && currentPoint.y >= endPoint.y)
                {
                    return true;
                }
            }
            else if (startPoint.x >= endPoint.x && startPoint.y <= endPoint.y) // ����
            {
                if (currentPoint.x <= endPoint.x && currentPoint.y >= endPoint.y)
                {
                    return true;
                }
            }
            else if (startPoint.x <= endPoint.x && startPoint.y >= endPoint.y) // �E��
            {
                if (currentPoint.x >= endPoint.x && currentPoint.y <= endPoint.y)
                {
                    return true;
                }
            }
            else if (startPoint.x >= endPoint.x && startPoint.y >= endPoint.y) // ����
            {
                if (currentPoint.x <= endPoint.x && currentPoint.y <= endPoint.y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}