using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spikeType1; // First spike type
    public GameObject spikeType2; // Second spike type

    public float MinSpeed;
    public float currentSpeed;
    public float MaxSpeed;
    public float speedMultiplier;

    void Awake()
    {
        currentSpeed = MinSpeed;
        GenerateSpike();
    }

    public void GenerateNextSpikeWithGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f);
        Invoke("GenerateSpike", randomWait);
    }

    void GenerateSpike()
    {
        // Randomly choose between the two spike types
        GameObject selectedSpike = Random.Range(0, 2) == 0 ? spikeType1 : spikeType2;

        GameObject SpikeIns = Instantiate(selectedSpike, transform.position, transform.rotation);

        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }

    void Update()
    {
        if (currentSpeed < MaxSpeed)
        {
            currentSpeed += speedMultiplier * Time.deltaTime;
        }
    }
}
