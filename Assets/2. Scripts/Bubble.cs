using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("SpawnTime")]
    public float timeBetSpawnMin = 5f; // ���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMax = 8f; // ���� ��ġ������ �ð� ���� �ִ�
    private float timeBetSpawn; // ���� ��ġ������ �ð� ����

    public ParticleSystem[] particles; // �̸� ������ ���ǵ�
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    private int currentIndex = 0; // ����� ���� ������ ����
    private float lastSpawnTime; // ������ ��ġ ����

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
