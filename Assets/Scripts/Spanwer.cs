﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour {

	// ========== ========== ========== VARIABLE SETTING ========== ========== ========== \\

	public SpawnerSystem spawnerSystem;

	public GameObject[] spawnerEnemy;

	void Start () {

		spawnerSystem = GameObject.Find ("Spawner_System").GetComponent<SpawnerSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnEnemy(int number, bool boss){

		GameObject enemy = Instantiate (spawnerEnemy[number], transform.position, transform.rotation);
		Zombie enemyInfo = enemy.GetComponent<Zombie> ();

		enemyInfo.enemyHealth = spawnerSystem.waveZombieHealth;
		enemyInfo.enemySpeed = spawnerSystem.waveZombieSpeed;
		if (boss)
			enemyInfo.enemyBoss = true;

	}

}
