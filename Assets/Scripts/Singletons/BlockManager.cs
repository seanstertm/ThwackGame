using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class BlockManager : Singleton<BlockManager>
    {
        [SerializeField] private GameObject blockTemplate;
        public int count = 1;
        public int[] weights = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };
        public List<GameObject> children = new();
        public void GenerateRow()
        {
            int[] newWeights = NewWeights(weights, count);
            for (int i = 0; i < newWeights.Length; i++)
            {
                if (newWeights[i] > 1)
                {
                    GameObject block = Instantiate(blockTemplate, transform);
                    block.transform.localPosition = new Vector3(-3.5f + i, 7.15f, 0);
                    block.GetComponent<Brick>().SetText(newWeights[i] - 1);
                    children.Add(block);
                }
            }
            weights = newWeights;
            StartCoroutine(MoveDown(3f));
        }
        public int[] NewWeights(int[] prevWeights, int count)
        {
            int[] weights = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            while (count > 0)
            {
                int i = FindRow(prevWeights);
                if (weights[i] < 9)
                {
                    weights[i]++;
                }
                count--;
            }
            return weights;
        }
        public int FindRow(int[] weights)
        {
            int sum = 0;
            foreach (int weight in weights)
            {
                sum += weight;
            }
            int distance = Random.Range(0, sum);
            int i = 0;
            while (true)
            {
                distance -= weights[i];
                if (distance < 0) { break; }
                i++;
            }
            return i;
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            children.Remove(gameObject);
            Destroy(gameObject);
        }

        IEnumerator MoveDown(float speed)
        {
            float distance = 0;
            while (distance < 1)
            {
                distance += Time.deltaTime * speed;
                foreach (GameObject child in children)
                {
                    child.transform.localPosition = new Vector3(child.transform.localPosition.x, child.transform.localPosition.y - Time.deltaTime * speed, 0);
                }
                yield return null;
            }
            foreach (GameObject child in children)
            {
                child.transform.localPosition = new Vector3(child.transform.localPosition.x, child.transform.localPosition.y + distance - 1, 0);
            }
        }
    }
}