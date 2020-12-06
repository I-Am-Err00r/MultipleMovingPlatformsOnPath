using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveablePlatform : MonoBehaviour
{
    [HideInInspector]
    public int index;
}