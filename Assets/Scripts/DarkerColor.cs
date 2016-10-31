using UnityEngine;
using System.Collections;

public class DarkerColor : MonoBehaviour {
	void Start () {
        foreach(Transform child in transform)
        {
            if(child.childCount > 0)
            {
                foreach(Transform subchild in child)
                {
                    SpriteRenderer sr = subchild.GetComponent<SpriteRenderer>();
                    Color current = sr.color;
                    current.r = 128;
                    sr.color = current;
                }
            }
        }
	}
}
