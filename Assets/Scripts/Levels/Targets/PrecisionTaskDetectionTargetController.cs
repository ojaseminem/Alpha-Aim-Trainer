using System;
using System.Collections;
using UnityEngine;

namespace Levels.Targets
{
    public class PrecisionTaskDetectionTargetController : MonoBehaviour
    {
        [HideInInspector] public Material greenMat;
        [HideInInspector] public Material redMat;

        private PrecisionTaskDetectionLevelManager _precisionTaskDetectionLevelManager;
        private bool _next;

        private void Start()
        {
            _precisionTaskDetectionLevelManager = GameObject.Find("PrecisionTaskDetectionLevelManager").transform.GetComponent<PrecisionTaskDetectionLevelManager>();
            StartCoroutine(WaitingForSeconds());
            StartCoroutine(WaitingForInput());
        }

        private IEnumerator WaitingForSeconds()
        {
            yield return new WaitForSeconds(.5f);
            transform.GetComponent<MeshRenderer>().material = redMat;
            _precisionTaskDetectionLevelManager.IncrementMisses();
            yield return new WaitForSeconds(.2f);
            _precisionTaskDetectionLevelManager.RedirectToSpawnTarget();
            Destroy(gameObject);
        }

        private IEnumerator WaitingForInput()
        {
            yield return new WaitUntil((() => Input.GetMouseButtonDown(0)));
            transform.GetComponent<MeshRenderer>().material = greenMat;
            _precisionTaskDetectionLevelManager.IncrementHits();
            yield return new WaitForSeconds(.2f);
            _precisionTaskDetectionLevelManager.RedirectToSpawnTarget();
            Destroy(gameObject);
        }
    }
}
