using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ZPackage.Utility;

namespace ZPackage
{
    public class LevelSpawner : GenericSingleton<LevelSpawner>
    {
        [SerializeField] List<GameObject> Levels;
        [SerializeField] GameObject Spikes;
        [SerializeField] GameObject Scores;
        List<Vector3> points;

        public void Init()
        {

        }
        private void Start()
        {
            // points = PoissonDiscSampling.GeneratePoints(5f, new Vector2(180, 100)).Select(x => (Vector3)x).ToList();
            // for (int i = 0; i < points.Count; i++)
            // {
            //     points[i] += new Vector3(-90, 5);
            // }
            // foreach (Transform child in Scores.transform)
            // {
            //     int index = Random.Range(0, points.Count);
            //     child.position = points[index];
            //     points.RemoveAt(index);
            // }
            // foreach (Transform spike in Spikes.transform)
            // {
            //     int index = Random.Range(0, points.Count);
            //     spike.position = points[index];
            //     points.RemoveAt(index);
            // }

            // for (int i = 0; i < 100; i++)
            // {
            //     Instantiate(Score, new Vector3(Random.Range(-90, 90), Random.Range(1, 15), 0), Quaternion.identity, transform);
            // }
            // for (int i = 0; i < 50; i++)
            // {
            //     Instantiate(Score, new Vector3(Random.Range(-90, 90), Random.Range(16, 30), 0), Quaternion.identity, transform);
            // }
            // for (int i = 0; i < 20; i++)
            // {
            //     Instantiate(Score, new Vector3(Random.Range(-90, 90), Random.Range(30, 60), 0), Quaternion.identity, transform);
            // }

            // foreach (Transform child in Scores.transform)
            // {
            //     PutSomeWhere(child);
            // }
            // foreach (Transform spike in Spikes.transform)
            // {
            //     PutSomeWhere(spike);
            // }
        }

        public static void PutSomeWhere(Transform child)
        {
            float randamValue = Random.value;
            if (randamValue < 0.4f)
            {
                child.transform.position = new Vector3(Random.Range(-90, 90), Random.Range(10, 25), 0);
            }
            else if (randamValue < 0.7f)
            {
                child.transform.position = new Vector3(Random.Range(-80, 80), Random.Range(25, 55), 0);
            }
            else if (randamValue < 0.9f)
            {
                child.transform.position = new Vector3(Random.Range(-70, 70), Random.Range(55, 100), 0);
            }
            else
            {
                child.transform.position = new Vector3(Random.Range(-60, 60), Random.Range(1, 10), 0);
            }
        }
        public static void PutSomeWhere(Transform child, int wait)
        {
            NewMethod();

            async void NewMethod()
            {
                await Task.Delay(wait);

                float randamValue = Random.value;
                if (randamValue < 0.4f)
                {
                    child.transform.position = new Vector3(Random.Range(-90, 90), Random.Range(10, 25), 0);
                }
                else if (randamValue < 0.7f)
                {
                    child.transform.position = new Vector3(Random.Range(-80, 80), Random.Range(25, 55), 0);
                }
                else if (randamValue < 0.9f)
                {
                    child.transform.position = new Vector3(Random.Range(-70, 70), Random.Range(55, 100), 0);
                }
                else
                {
                    child.transform.position = new Vector3(Random.Range(-60, 60), Random.Range(1, 10), 0);
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (points != null)
            {
                foreach (var item in points)
                {
                    Gizmos.DrawSphere(item, 1);
                }
            }

        }

    }
}

