using UnityEngine;

public class Corpse : MonoBehaviour
{
    public int CorpseID;

    private void Update()
    {
        if (CorpseID > 8) { CorpseID = 8; }
        if (CorpseID < 0) { CorpseID = 0; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<movement>().currentCorpse = CorpseID;
        }
    }
}
