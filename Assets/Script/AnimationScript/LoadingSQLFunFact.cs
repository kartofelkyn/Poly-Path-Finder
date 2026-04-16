using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingSQLFunFact : MonoBehaviour
{
    public TextMeshProUGUI factText;
    public float changeInterval = 5f;

    private List<string> facts = new List<string>()
    {
        "SQL was originally called SEQUEL, but the name was shortened due to trademark issues.",
        "The first version of SQL was developed at IBM in the 1970s.",
        "MySQL is named after the daughter of its co-founder, Michael Widenius.",
        "PostgreSQL is known for being one of the most advanced open-source databases.",
        "An index in SQL works like a book index, helping the database find data faster.",
        "SELECT * might look simple, but it can slow down performance in large databases.",
        "NULL in SQL doesn't mean zero—it means 'unknown' or 'missing' value.",
        "JOIN operations can combine data from multiple tables in powerful ways.",
        "SQL is used by major companies like Google, Facebook, and Amazon.",
        "A poorly written SQL query can significantly slow down an entire system.",
        "Normalization helps reduce data redundancy in databases.",
        "SQL is not case-sensitive for commands like SELECT, INSERT, and UPDATE.",
        "Transactions in SQL ensure that operations are completed fully or not at all.",
        "Foreign keys help maintain relationships between tables.",
        "Some databases support JSON data directly inside SQL tables."
    };

    void Start()
    {
        StartCoroutine(ChangeFacts());
    }

    IEnumerator ChangeFacts()
    {
        while (true)
        {
            factText.text = facts[Random.Range(0, facts.Count)];
            yield return new WaitForSeconds(changeInterval);
        }
    }
}
