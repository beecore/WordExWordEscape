/***************************************************************************************
 * THIS AN EXTENTION CLASS. ADD ALL THE EXTENTION METHODS HERE.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIExtentions {

	// Clear all child gameobjects of the given gameobject.
	public static void ClearAllChild(this GameObject obj) {
		if(obj.transform.childCount > 0) {
			foreach (Transform child in obj.transform) {
				GameObject.Destroy(child.gameObject);
			}
		}
	}

	// Activates the given gameobject with animation. used for only popups of the game.
	public static void Activate(this GameObject target)
	{
		target.gameObject.SetActive(true);
		target.transform.SetAsLastSibling();
		UIController.Instance.Push(target.name);
	}

	// Deactivates the game object with animation.
	public static void Deactivate(this GameObject target)
	{
		Animator animator = target.GetComponent<Animator> ();
		if (animator != null) {
			animator.Play("Close");
			UIController.Instance.StartCoroutine(DisableWindow(target));
		}
		else {
			target.SetActive(false);
			UIController.Instance.Pop(target.name);
		}
	}

	static IEnumerator DisableWindow(GameObject target)
	{
		yield return new WaitForSeconds(0.3F);
		target.SetActive(false);
		UIController.Instance.Pop(target.name);
	}

	// Shuffles the generic list.
	public static void Shuffle<T>(this IList<T> list)  
	{  
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}
