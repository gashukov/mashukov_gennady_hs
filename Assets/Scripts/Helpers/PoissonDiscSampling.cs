using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public static class PoissonDiscSampling {

        public static List<Vector3> GeneratePoints(Vector3 position, float radius, float width, float height, int pickRandomCount = 0, int numSamplesBeforeRejection = 10) {
            float cellSize = radius/Mathf.Sqrt(2);

            int[,] grid = new int[Mathf.CeilToInt(width/cellSize), Mathf.CeilToInt(height/cellSize)];
            List<Vector3> points = new List<Vector3>();
            List<Vector3> spawnPoints = new List<Vector3>(); 
            Vector3 shift = new Vector3(width/2f, 0, height/2f);

            spawnPoints.Add(position + shift);
            while (spawnPoints.Count > 0) {
                int spawnIndex = Random.Range(0,spawnPoints.Count);
                Vector3 spawnCentre = spawnPoints[spawnIndex];
                bool candidateAccepted = false;

                for (int i = 0; i < numSamplesBeforeRejection; i++)
                {
                    float angle = Random.value * Mathf.PI * 2;
                    Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                    Vector3 candidate = spawnCentre + dir * Random.Range(radius, 2*radius);
                    if (IsValid(candidate, width, height, cellSize, radius, points, grid)) {
                        points.Add(candidate);
                        spawnPoints.Add(candidate);
                        grid[(int)(candidate.x / cellSize),(int)(candidate.z / cellSize)] = points.Count;
                        candidateAccepted = true;
                        break;
                    }
                }
                if (!candidateAccepted) {
                    spawnPoints.RemoveAt(spawnIndex);
                }
            }

            if (pickRandomCount > 1)
            {
                points = points.PickRandom(pickRandomCount).ToList();
            }

        
            for (int i = 0; i < points.Count; i++)
            {
                points[i] -= shift;
            }

            return points;
        }

        static bool IsValid(Vector3 candidate, float width, float height, float cellSize, float radius, List<Vector3> points, int[,] grid) {
            if (candidate.x >= 0 && candidate.x < width && candidate.z >= 0 && candidate.z < height) {
                int cellX = (int)(candidate.x / cellSize);
                int cellZ = (int)(candidate.z / cellSize);
                int searchStartX = Mathf.Max(0,cellX -2);
                int searchEndX = Mathf.Min(cellX+2, grid.GetLength(0)-1);
                int searchStartZ = Mathf.Max(0,cellZ -2);
                int searchEndZ = Mathf.Min(cellZ+2, grid.GetLength(1)-1);

                for (int x = searchStartX; x <= searchEndX; x++) {
                    for (int z = searchStartZ; z <= searchEndZ; z++) {
                        int pointIndex = grid[x,z]-1;
                        if (pointIndex != -1) {
                            float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
                            if (sqrDst < radius*radius) {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}