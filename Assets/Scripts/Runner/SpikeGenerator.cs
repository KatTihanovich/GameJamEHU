using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spike;
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
        float randomWait= Random.Range(0.1f, 1.2f);
        Invoke("GenerateSpike", randomWait);
    }
    void GenerateSpike()
    {
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);
        
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }

    void Update()
    {
        if (currentSpeed < MaxSpeed)
        {
            currentSpeed += speedMultiplier;
        }
    }
}
