using UnityEngine;

public class DetectorCaidaItem : MonoBehaviour
{
    public bool toco_suelo = false;

    void OnCollisionEnter(Collision otro)
    {
        if(toco_suelo)
        {
            return;
        }

        toco_suelo = true;

        // Destruir el Rigidbody cuando toque el suelo.
        Rigidbody fisica = GetComponent<Rigidbody>();

        if(fisica != null)
        {
            Destroy(fisica);
        }

        Debug.Log("EL OBJETO TOCO EL SUELO");
    }
}
