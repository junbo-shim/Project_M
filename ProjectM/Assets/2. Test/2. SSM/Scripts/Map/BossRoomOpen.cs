using UnityEngine;

public class BossRoomOpen : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject portal;
    public Inventory inventory;




    public void OnTriggerEnter(Collider other)
    {
        for (int i = 0; inventory.items.Count > 0; i++)
        {
           
            if (inventory.items[i].itemName.Contains("SymbolStone_A"))
            {
                if (!gameObject1.activeSelf)
                {

                    gameObject1.SetActive(true);
                }

            }
        }

        for (int i = 0; inventory.items.Count > 0; i++)
        {
            if (inventory.items[i].itemName.Contains("SymbolStone_B"))
            {
                if (!gameObject2.activeSelf)
                {
                    gameObject2.SetActive(true);
                }
            }
        }
        if (gameObject1.activeSelf && gameObject2.activeSelf)
        {
            portal.SetActive(true);
            ParticleSystem particle = portal.GetComponent<ParticleSystem>();
            if (particle != null)
            {
                particle.Play();
            }
        }
    }
}
