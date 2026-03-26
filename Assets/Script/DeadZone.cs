using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
void OnTriggerEnter(Collider other)
{
	SceneManager.LoadScene("Map");
}
}
