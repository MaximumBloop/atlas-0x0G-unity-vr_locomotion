using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class GameObjectListEvent : UnityEvent<List<GameObject>> { }

[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> { }