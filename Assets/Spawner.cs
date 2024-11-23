using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] _objects;
    public BoxCollider2D[] _wallColliders;

    public Slider cubeSlider;
    public Slider sphereSlider;
    public Slider capsuleSlider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        float minX = _wallColliders[0].bounds.min.x;
        float maxX = _wallColliders[1].bounds.max.x;
        float minY = _wallColliders[2].bounds.min.y;
        float maxY = _wallColliders[3].bounds.max.y;

        Vector2 randomPosition;
        do
        {
            randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        } while (Physics2D.OverlapPoint(randomPosition) != null);

        GameObject objToSpawn = ChooseObjectBasedOnSliders();

        GameObject obj = Instantiate(objToSpawn, randomPosition, Quaternion.identity);
        obj.SetActive(true);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    GameObject ChooseObjectBasedOnSliders()
    {
        float totalProbability = cubeSlider.value + sphereSlider.value + capsuleSlider.value;

        if (totalProbability == 0)
        {
            
            return _objects[Random.Range(0, _objects.Length)];
        }
        else
        {
           
            float randomValue = Random.Range(0f, totalProbability);

            if (randomValue < cubeSlider.value)
            {
                return _objects[0]; // Куб
            }
            else if (randomValue < cubeSlider.value + sphereSlider.value)
            {
                return _objects[1]; // Шар
            }
            else
            {
                return _objects[2]; // Капсула
            }
        }
    }
}