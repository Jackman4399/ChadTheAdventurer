using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class SingletonSO<T> : ScriptableObject where T : ScriptableObject {

	private static T _instance;
	public static T Instance { 
		get {
			if (!_instance) {
				_instance = CreateInstance<T>();
				AssetDatabase.CreateAsset(_instance, "Assets\\" + typeof(T).Name + ".asset");
			}

			return _instance;
		} 
	}

	protected virtual void Awake() {
		if(_instance) _instance = this as T;
	}

	protected virtual void OnDestroy() => _instance = null;

}
