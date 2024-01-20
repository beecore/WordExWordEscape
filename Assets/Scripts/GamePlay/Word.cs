/***************************************************************************************
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour {

	WaitForSeconds wait = new WaitForSeconds(0.01F);
	public void Init() {
		StartCoroutine(InitSequential());
	}

	IEnumerator InitSequential() {
		foreach(Transform t in transform) {
			yield return new WaitForSeconds(0.01F);
			t.GetComponent<InputChar>().Init();
		}
	}

	public void Remove() {
		StartCoroutine(RemoveSequential());
	}

	IEnumerator RemoveSequential() {
		foreach(Transform t in transform) {
			yield return wait;
			t.GetComponent<InputChar>().Remove();
		}
	}
}
