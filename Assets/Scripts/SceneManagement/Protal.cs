using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Protal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B,C,D,E
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float FadeOutTime = 1f;
        [SerializeField] float FadeInTime = 2f;
        [SerializeField] float FadeWaitTime = 1f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag =="Player")
            {
                StartCoroutine(Transition());
            }
        }
        private IEnumerator Transition()
        {
            if (sceneToLoad <0)
            {
                Debug.LogError("error no protal scene set");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(FadeOutTime);

            //Save current level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            //Load current level
            wrapper.Load();

            Protal otherPortal = GetOtherProtal();
            UpdatePlater(otherPortal);

            wrapper.Save();

            yield return new WaitForSeconds(FadeWaitTime);
            yield return fader.FadeIN(FadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlater(Protal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }

        private Protal GetOtherProtal()
        {
            foreach (Protal portal in FindObjectsOfType<Protal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }

}
