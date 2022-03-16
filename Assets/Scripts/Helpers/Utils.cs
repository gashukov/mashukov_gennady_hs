using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class Utils
    {
        private const int SamplesNum = 20;

        public static List<Vector3> GetRandomPointsOnPlane(Vector3 position, float width, float height, int pointsCount)
        {
            List<Vector3> resultPoints = new List<Vector3>();
            resultPoints.Add(GetRandomPointOnPlane(position, width, height));

            for (int i = 0; i < pointsCount - 1; i++)
            {
                resultPoints.Add(GetBestCandidate(position, width, height, resultPoints));
            }

            return resultPoints;

        } 
    
        private static Vector3 FindClosestPoint(Vector3 forPoint, List<Vector3> candidates)
        {
            Vector3 closestPoint = default;
            float closestDistance = float.PositiveInfinity;

            for (int i = 0; i < candidates.Count; i++)
            {
                float distance = (forPoint - candidates[i]).magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = candidates[i];
                }
            }

            return closestPoint;
        }

        private static Vector3 GetBestCandidate(Vector3 position, float width, float height, List<Vector3> candidates)
        {
            float bestDistance = 0;
            Vector3 bestCandidate = default;

            for (int i = 0; i < SamplesNum; i++)
            {
                Vector3 candidate = GetRandomPointOnPlane(position, width, height);
                Vector3 closestPoint = FindClosestPoint(candidate, candidates);
                float distance = (candidate - closestPoint).magnitude;
                if (distance > bestDistance && distance > 1)
                {
                    bestDistance = distance;
                    bestCandidate = candidate;
                }
            }

            return bestCandidate;
        }

        private static Vector3 GetRandomPointOnPlane(Vector3 position, float width, float height)
        {
            return new Vector3(Random.Range(-width/2, width/2), 0, Random.Range(-height/2, height/2)) + position;
        }
    }
}