﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour {

	// ========== ========== ========== VARIABLE SETTING ========== ========== ========== \\

	public int wave = 0;

	public int waveZombie;
	public int waveZombieNumber = 10;
	public int waveZombieNumberLeft;
	public int waveZombieNumberCurrent;
	public float waveZombieHealth = 10f;
	public float waveZombieSpeed = 2.5f;

	public float waveSpawnDelay = 2.5f;
	public int waveSpawnNumber = 1;
	public bool waveSpawnAvailable = true;

	public bool waveWait = true;
	public float waveWaitTime = 15f;

	public Spanwer[] spawners;

	// ========== ========== ========== UNITY FUNCTION ========== ========== ========== \\

	void Start() {

		WaveNext ();

	}

	void Update() {

		if (!waveWait) {

			if (waveZombieNumberCurrent == 0 && waveZombieNumberLeft == 0)
				WaveNext ();

		}

	}

	// ========== ========== ========== FUNCTION ========== ========== ========== \\

	public void WaveNext() {

		WaveCalculate (wave++);
		StartCoroutine (WaveWait());

	}
	IEnumerator WaveWait() {

		waveWait = true;
		Debug.Log ("웨이브" + wave + " 준비");

		yield return new WaitForSeconds (waveWaitTime);

		waveWait = false;
		StartCoroutine (WaveSpawn ());
		Debug.Log ("웨이브" + wave + " 시작");

	}

	public void WaveCalculate(int wave) {

		waveZombie = Mathf.FloorToInt (10f + (wave / 3));
		waveZombieHealth = 10f + (wave * 2);
		waveZombieSpeed = Mathf.Min(10f, 2.5f + (wave * 0.1f));

		waveSpawnDelay = Mathf.Max (1f, 2.5f - (wave * 0.05f));
		waveSpawnNumber = Mathf.Min(spawners.Length, Mathf.FloorToInt (1 + (wave * 0.1f)));
		waveZombieNumberLeft = waveZombieNumber;

	}

	IEnumerator WaveSpawn() {

		waveSpawnAvailable = false;

		yield return new WaitForSeconds (waveSpawnDelay);

		float chance = spawners.Length;
		int number = waveSpawnNumber;
		while (number > 0) {
			
			for (int i = 0; i < spawners.Length; i++) {

				if (Mathf.RoundToInt (Random.Range (1f, chance)) == 1) {

					chance--;
					if (number > 0 && waveZombieNumberLeft > 0) {

						number--;
						waveZombieNumberLeft--;
						spawners [i].SpawnEnemy ();
						if (waveZombieNumberLeft == 0 || number == 0)
							break;

					} else
						break;

				} else
					chance--;

			}
			if (waveZombieNumberLeft == 0 || number == 0)
				break;
			
		}

		if (waveZombieNumberLeft > 0) {
			
			waveSpawnAvailable = true;
			StartCoroutine (WaveSpawn ());

		}

	}

}
