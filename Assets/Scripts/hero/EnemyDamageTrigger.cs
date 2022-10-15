using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
	[SerializeField]
	ParametersController _heroParams;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Trigger enter");
		if (collision.tag == "Enemy")
			collision.GetComponent<EnemyBehaviour>().AttackTarget(_heroParams);
	}

}
