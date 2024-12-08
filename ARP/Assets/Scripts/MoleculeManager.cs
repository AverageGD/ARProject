using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoleculeManager : MonoBehaviour
{
    public GameObject x;

    // Singleton instance for easy access from other classes
    public static MoleculeManager instance;

    // List of all possible reactions represented as Reaction objects
    public List<Reaction> reactions;

    // List of molecules currently present in the scene (GameObjects)
    [SerializeField] private List<GameObject> _molecules;

    private void Awake()
    {
        // Initialize the singleton instance to access it from other classes
        instance = this;

        // Normalize reaction strings by sorting their characters for consistency
        // Sorting ensures that reactions are order-independent (e.g., "AB" == "BA")
        for (int i = 0; i < reactions.Count; i++)
        {
            // Create a temporary variable to hold the current reaction
            Reaction buff = reactions[i];

            // Get the reaction elements (the string of molecule IDs that form the reaction)
            string change = reactions[i].ReactionElements;

            // Sort the reaction elements alphabetically to ensure consistency in comparison
            change = new string(change.OrderBy(c => c).ToArray());

            // Update the reaction with the sorted reaction elements
            buff.ReactionElements = change;

            // Replace the old reaction with the updated one
            reactions[i] = buff;
        }
    }

    // Method to add a molecule to the list of molecules
    public void AddMolecule(GameObject m)
    {
        // Add the molecule to the list
        _molecules.Add(m);

        // Compare the current set of molecules with all defined reactions
        foreach (Reaction reaction in reactions)
        {
            string currMolecules = ""; // Holds the IDs of all molecules in the scene as a concatenated string

            // Loop through all molecules in the scene and concatenate their IDs to the currMolecules string
            foreach (GameObject molecule in _molecules)
            {
                currMolecules += Convert.ToChar(molecule.GetComponent<Molecule>().id);
            }

            // Normalize the current molecule string by sorting its characters (to ensure order-independent comparison)
            currMolecules = new string(currMolecules.OrderBy(c => c).ToArray());

            // Check if the current set of molecules matches any reaction
            if (reaction.ReactionElements.Contains(currMolecules))
            {
                ReactionInvoker();
                // If a reaction matches, invoke the corresponding reaction logic here
                Debug.Log("Reaction triggered!");
                Debug.Log("Reaction Name: " + reaction.ReactionName);
                break; // Exit the loop once a matching reaction is found
            }
        }
    }

    // Method to remove a molecule from the list by its ID
    public void DeleteMolecule(short id)
    {
        // Iterate through the list of molecules
        for (short i = 0; i < _molecules.Count; i++)
        {
            // If the molecule's ID matches, remove it from the list
            if (_molecules[i].GetComponent<Molecule>().id == id)
            {
                // Remove the molecule from the list at the current index
                _molecules.RemoveAt(i);
                break; // Exit the loop once the molecule is removed
            }
        }
    }

    private void ReactionInvoker()
    {
        x.SetActive(true);
    }
}

// Structure to represent a chemical reaction

[System.Serializable]
public struct Reaction
{
    // Private fields to store the reaction name and the reaction elements
    [SerializeField] private string _reactionName;
    [SerializeField] private string _reactionElements;

    // Property to get the reaction name
    public string ReactionName { get { return _reactionName; } }

    // Property to get and set the reaction elements (a string that represents the molecules involved in the reaction)
    public string ReactionElements { get { return _reactionElements; } set { _reactionElements = value; } }
}
