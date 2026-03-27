using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits2 : MonoBehaviour
{
void OnTriggerEnter(Collider other)
{
	SceneManager.LoadScene("EndCredit");
}
}

