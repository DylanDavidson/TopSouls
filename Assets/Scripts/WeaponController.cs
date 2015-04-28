using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	
	public int damage;
	public int force;
	public float collisionCooldown;
	public AudioClip hitSound;

	private AudioSource source;
	protected float lastCollision;


	public void OnTriggerStay2D(Collider2D other)
	{
		if((other.CompareTag("Shield") && !other.CompareTag("Player")) || 
		   (!other.CompareTag("Shield") && other.CompareTag("Player")) || 
		   (other.CompareTag("Enemy") && !this.CompareTag("EnemyWeapon")) ||
		   (other.CompareTag("runner") && !this.CompareTag("EnemyWeapon"))||
		   (other.CompareTag("boss") && !this.CompareTag("EnemyWeapon"))  ||
		   (other.CompareTag("ranger") && !this.CompareTag("EnemyWeapon"))) 
		{
			if(Time.time-lastCollision >=  collisionCooldown)
			{
				lastCollision = Time.time;
				DoDamage (other);
				Push (other);
			}
		}
	}

	public void Push(Collider2D other)
	{
		if(other == null)
			return;
		Vector2 dir = (other.transform.position - transform.parent.position).normalized;
	
		//Debug.DrawRay (other.transform.position, dir);
	
		if(other.CompareTag("Shield"))
			other.GetComponentInParent<PlayerController>().GetPushed (dir*force, true);
		else if(other.CompareTag("Enemy"))
			other.GetComponent<EnemyPlaceholderController>().GetPushed (dir*force, false);
		else if (other.CompareTag ("runner"))
			other.GetComponent<runnerController> ().GetPushed (dir*force, false);
		else if (other.CompareTag ("boss"))
			other.GetComponent<bossController> ().GetPushed (dir*force, false);
		else if(other.CompareTag("ranger"))
			other.GetComponent<rangerController>().GetPushed (dir*force, false);
		else if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().GetPushed (dir*force, false);
	}
	
	public void DoDamage(Collider2D other)
	{
		if(hitSound != null)
			source.PlayOneShot (hitSound);

		if (other.CompareTag ("Shield"))
			other.GetComponentInParent<PlayerController> ().TakeDamage (damage, true);
		else if (other.CompareTag ("Enemy")) 
			other.GetComponent<EnemyPlaceholderController> ().TakeDamage (damage, false);
		else if (other.CompareTag ("runner"))
			other.GetComponent<runnerController> ().TakeDamage (damage, false);
		else if (other.CompareTag ("boss"))
			other.GetComponent<bossController> ().TakeDamage (damage, false);
		else if (other.CompareTag ("ranger"))
			other.GetComponent<rangerController> ().TakeDamage (damage, false);
		else if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().TakeDamage(damage, false);
	}

	void Start()
	{
		source = GetComponent<AudioSource> ();
	}
}
