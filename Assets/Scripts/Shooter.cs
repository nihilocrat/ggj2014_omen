using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
	public GameObject bullet;
	public float bulletForce = 1000f;
	public float bulletLife = 1f;
	public float fireRate = 0f;
	public Transform muzzle;

	private float lastFire = 0f;

	void Start ()
	{
	
	}
	
	void FixedUpdate ()
	{
		if(fireRate > 0f && Time.fixedTime - lastFire > fireRate)
		{
			Fire();
		}
	}

	void Fire()
	{
		var obj = Instantiate(bullet, muzzle.position, muzzle.rotation) as GameObject;
		obj.rigidbody.AddForce(muzzle.forward * bulletForce * obj.rigidbody.mass);

		Destroy(obj, bulletLife);

		lastFire = Time.fixedTime;
	}
}
