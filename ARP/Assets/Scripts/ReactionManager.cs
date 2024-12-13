using System.Collections.Generic;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public static ReactionManager instance;

    [SerializeField] private List <GameObject> _reactions;

    private List<GameObject> _runningReactions;

    private void Awake()
    {
        instance = this;

        _runningReactions = new List<GameObject>();
    }

    public void StartReaction(short id, Vector3 coordinates)
    {

        //StopReactions();

        foreach (GameObject reaction in _reactions)
        {
            if (reaction.GetComponent<ReactionPrefab>().id == id)
            {
                GameObject obj = Instantiate(reaction, coordinates, Quaternion.identity);
                obj.SetActive(true);
                _runningReactions.Add(obj);
                break;
            }
        }
    }

    public void StopReactions()
    {
        
        foreach (GameObject runningReaction in _runningReactions)
        {
            GameObject x = runningReaction;
            Destroy(x);
        }

        _runningReactions.Clear();
    }
}
