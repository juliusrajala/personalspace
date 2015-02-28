
using System.Collections;
using UnityEngine;

public class ZigShip : Entity{ //Vaihdetaan perittäväksi basicParticle kun kuolinanimaatio on toteutettu T: Konsta

	protected float lowerBound;
	protected int dir = 1;
	protected float upperBound;
	public float speedUpDown = 6;

	protected void Start(){
		this.speed = 4;
		upperBound = transform.position.y + 4;
		lowerBound = transform.position.y - 4;
		base.Start();
	}

	void Update () {

		paused = FindObjectOfType<Menuscript>().paused;

		if (transform.position.x > endPoint && !FindObjectOfType<Player>().died) {
			move (new Vector2(speed * Time.deltaTime * -1,speed * Time.deltaTime * dir));
			if(transform.position.y + speed * Time.deltaTime *dir > upperBound || transform.position.y +speed * Time.deltaTime *dir < lowerBound){
				dir = dir*(-1);
			}

		} else {
			die ();
		}
	}
}


