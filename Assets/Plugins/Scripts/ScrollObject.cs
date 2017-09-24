using UnityEngine;
using System.Collections;
using GameFramework;

namespace GameFramework
{
	/// <summary>
	/// Scroll object.
	/// Spawns N objects under this gameObject and scrolls it
	/// </summary>
	public class ScrollObject : BaseMonoBehavior
	{
		public Transform _SpawnObject;
		public int _NumObjectsToSpawn;
		public float _Scale;
		public float _ScrollSpeed = 1;

		void Start()
		{
			for(int i=0; i<_NumObjectsToSpawn;i++)
			{
				int xPos = Random.Range(-Screen.width / 2, Screen.width / 2);
				int yPos = Random.Range(-Screen.height / 2, Screen.height / 2);

				Vector3 position = new Vector3(xPos,yPos,0.0f);

				GameObject obj = GameObject.Instantiate(_SpawnObject.gameObject);
				obj.transform.SetParent(transform,false);
				obj.transform.localPosition = position;

				Vector3 scale = obj.transform.localScale;
				scale.x *= _Scale;
				scale.y *= _Scale;

				obj.transform.localScale = scale;
			}
		}

		void Update()
		{
			Vector3 pos = transform.position;
			pos.y -= (_ScrollSpeed * Time.deltaTime);

			if (pos.y < -Screen.height/2) 
				pos.y = Screen.height * 1.5f;

			transform.position = pos;
		}
	}
}