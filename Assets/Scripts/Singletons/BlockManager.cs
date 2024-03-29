using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class BlockManager : Singleton<BlockManager>
    {
        [SerializeField] private GameObject blockTemplate;
        [SerializeField] private GameObject border;
        public int count = 1;
        public int[] weights = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };
        public List<GameObject> children = new();
        public void GenerateRow(int amount)
        {
            int[] newWeights = NewWeights(weights, count);
            for (int i = 0; i < newWeights.Length; i++)
            {
                if (newWeights[i] > 1)
                {
                    GameObject block = Instantiate(blockTemplate, transform);
                    block.transform.localPosition = new Vector3(-3.5f + i, 7.15f, 0);
                    block.GetComponent<Brick>().SetText(newWeights[i] - 1);
                    block.name = "Block";
                    children.Add(block);
                }
            }
            weights = newWeights;
            StartCoroutine(MoveDown(3f, amount));
        }

        public void GenerateThemeRow()
        {
            for(int i = 1; i < 9; i++)
            {
                GameObject block = Instantiate(blockTemplate, transform);
                block.transform.localPosition = new Vector3(-4.5f + i, 7.15f, 0);
                block.GetComponent<Brick>().SetText(i);
                block.name = "Block";
                children.Add(block);
            }
            StartCoroutine(MoveDown(3f, 1));
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

        IEnumerator MoveDown(float speed, int amount)
        {
            float distance = 0;
            int specialCases = 0; // 1 = gameover; 2 = last row
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
                if (Mathf.RoundToInt(child.transform.localPosition.y) == -7)
                {
                    child.GetComponent<Brick>().Blink();
                    specialCases = 1;
                    amount = 0;
                }
                if (Mathf.RoundToInt(child.transform.localPosition.y) == -6)
                {
                    if (specialCases == 0) { specialCases = 2; }
                    amount = 0;
                }
            }

            amount--;
            if(amount > 0)
            {
                GenerateRow(amount);
            }

            if(specialCases == 1)
            {
                Debug.Log("Game Over");
                GameManager.Main.Game = false;
            }
        }
    }
}