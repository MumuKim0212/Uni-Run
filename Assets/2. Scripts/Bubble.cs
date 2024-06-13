using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("SpawnTime")]
    public float timeBetSpawnMin = 5f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 8f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public ParticleSystem[] particles; // 미리 생성한 발판들
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    private int currentIndex = 0; // 사용할 현재 순번의 발판
    private float lastSpawnTime; // 마지막 배치 시점

    void Start()
    {
        lastSpawnTime = 0f;
        timeBetSpawn = timeBetSpawnMax;
    }

    void Update()
    {
        if (GameManager.instance.isGameover)
            return;

        if (Time.time >= lastSpawnTime)
        {
            lastSpawnTime = Time.time + timeBetSpawn;
            timeBetSpawn = UnityEngine.Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            if (!particles[currentIndex].isPlaying)
                particles[currentIndex].Play();
            currentIndex = (currentIndex + 1) % 2;

        }
    }
}
