/***************************************************************************************
 * DESPAEW THE PARTICLE AFTER GIVEN DELAY WHEN ATTACHED TO ANY GAMEOBJECT.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnParticle : MonoBehaviour 
{
	[SerializeField] float despawnDelay;

	void OnEnable() {
		if(!IsInvoking("DesapwnParticle")) {
			Invoke("DesapwnParticle",despawnDelay);
		}
	}

	void DesapwnParticle() {
		gameObject.SetActive(false);
	}
}
