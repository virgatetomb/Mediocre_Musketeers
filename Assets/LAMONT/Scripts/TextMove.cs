using UnityEngine;
using System.Collections;

public class TextMove : MonoBehaviour 
	{
		public float movementSpeed = 20;

		void Update(){

			transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);

		}
	}