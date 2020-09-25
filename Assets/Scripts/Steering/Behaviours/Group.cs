using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private string _collisionTag;
    [SerializeField] private List<GameObject> _members = new List<GameObject>();

    public List<GameObject> Members => _members;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var gameObject = other.gameObject;

        if (gameObject && gameObject.CompareTag(_collisionTag) && !_members.Contains(gameObject))
        {
            _members.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var gameObject = other.gameObject;

        if (gameObject)
        {
            _members.Remove(gameObject);
        }
    }
}
