using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossRockSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject rockPrefab;

    [SerializeField]
    private List<Transform> spawnLocations;




    [Header("SpawnIndicatorVariables")]
    [SerializeField]
    private GameObject spawnIndicator;
    [SerializeField]
    private float flashDelay = 2f;
    [SerializeField]
    private int flashRepeatCount = 3;


    public bool disabled = false;
    private void Start()
    {
        //SpawnRockRandom();
    }

    public void SpawnRockRandom()
    {
        disabled = false;
        int randomIndex = Random.Range(0, spawnLocations.Count);
        Transform randomSpawnPoint = spawnLocations[randomIndex];

        //DOVirtual.DelayedCall(flashDelay, () => Flash(randomSpawnPoint.position)).SetLoops(flashRepeatCount).OnComplete(() =>



        //);
        //Instantiate(rockPrefab, randomSpawnPoint.position, Quaternion.identity);

        DOTween.Sequence()
           .Append(DOVirtual.DelayedCall(flashDelay, () => Flash(randomSpawnPoint.position)))
           .AppendInterval(flashDelay) // Optional delay between each flash
           .SetLoops(flashRepeatCount)
           .OnComplete(() => InstantiateRock(randomSpawnPoint.position));

    }

    private void Flash(Vector3 pos)
    {
        spawnIndicator.transform.position = pos;
        spawnIndicator.SetActive(!spawnIndicator.activeSelf);

    }

    private void InstantiateRock(Vector3 pos)
    {
        if (disabled == true)
        {
            disabled = false;
            return;
        }
        Instantiate(rockPrefab, pos, Quaternion.identity);
    }
}
