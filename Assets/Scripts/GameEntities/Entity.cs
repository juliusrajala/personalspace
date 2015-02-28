﻿using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public int health;
	public float speed;
	public int damage;
	public int points;
	public float endPoint = -24;

	public int paused;



	protected void Start(){
		paused = FindObjectOfType<Menuscript>().paused;
	}


	public void takeDamage(int hit){
		this.health -= hit;
		if (health <= 0){
			this.die();
			GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().Score += points;
			GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().fragments += 1;
			//print(GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().Score);
		}
	}

	//Tarvii työstää ennen lisää testausta, tällä hetkellä optimoitu niin että pelaajahahmo teurastaa törmäilemällä kaikki muut.
	//Lol. T. Julius
	public void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Menu") { //Menussa colliderit. Crashaa jos törmää muihin pelin juttuihin
						//Ei tehä mittää
		} else {
				Entity otherPlayer = other.GetComponent<Entity> ();
				if (otherPlayer is basicParticle){
						//Debug.Log("Shots fired!");
						basicParticle temp = other.GetComponent<basicParticle>();
						temp.OnTriggerEnter2D(this.gameObject.GetComponent<Collider2D>());
						goto skippaus;
				}
				if (this is Player) {
						//print (other.gameObject.name + " Has collided with you!");
						takeDamage(otherPlayer.damage);
						otherPlayer.takeDamage (this.damage);
				} else if (otherPlayer is Player) {
						//print (this + " Has collided with you!");
						otherPlayer.takeDamage (this.damage);
						takeDamage (otherPlayer.damage);
				} else if (otherPlayer is TorpedoScript) {
						takeDamage (otherPlayer.damage);
						otherPlayer.takeDamage (damage);
				} else if (this is TorpedoScript) {
						takeDamage (otherPlayer.damage);
						otherPlayer.takeDamage (damage);
				} else {
						takeDamage (20);
				}
		skippaus:
				;
		}
	}
	
	protected void die(){
		if (this is Player) {
			Player temp = this.GetComponent<Player> ();
			temp.died = true;
		} else {
			Destroy (this.gameObject);
		}
	}

	protected void move(Vector2 amount){
		if (paused == -1 && !FindObjectOfType<Player>().died) {
				transform.Translate (new Vector3 (amount.x, amount.y, 0));
		}
	}

}
