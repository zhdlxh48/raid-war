using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Damageable : UnityEvent<int>
{ 

}

[System.Serializable]
public class DamageableRig : UnityEvent<int, Vector3>
{

}
