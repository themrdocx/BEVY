using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject;
    [SerializeField] private float delayBeforeStart = 1f;
    [SerializeField] private float timeBtwChars = 0.1f;
    [SerializeField] private string leadingChar = "|";
    [SerializeField] private bool leadingCharBeforeDelay = false;
    
    private string _writer;

    public List<string> texts;
    private int textCount = 0;

    [SerializeField] private float delayBwStrings = 1f;

    [SerializeField] private GameObject button;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeOutTime;

    [SerializeField] private GameObject GameScene;
    [SerializeField] private GameObject UIScene;

    private void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();

        if(texts.Count>0)
            StartTyping();
    }

    public void StartTyping()
    {
        StartCoroutine(nameof(TMPTypeWriter));
    }

    private IEnumerator TMPTypeWriter()
    {
        foreach (var t in texts)
        {
            textObject.text = "";
            
            _writer = t;
            textObject.text = leadingCharBeforeDelay ? leadingChar : "";
            yield return new WaitForSeconds(delayBeforeStart);

            foreach (var c in _writer)
            {
                if (textObject.text.Length > 0)
                    textObject.text = textObject.text[..^leadingChar.Length];

                textObject.text += c;
                textObject.text += leadingChar;
                if(RandomSound.Singleton.audioClips.Length>0)
                    RandomSound.Singleton.SetSourceClip(
                        RandomSound.Singleton.audioClips[Random.Range(0, RandomSound.Singleton.audioClips.Length)]);
                yield return new WaitForSeconds(timeBtwChars);
            }
            
            if (leadingChar != "")
                textObject.text = textObject.text[..^leadingChar.Length];
            yield return new WaitForSeconds(delayBwStrings);
        }
        button.SetActive(true);
    }

    public void StartParty()
    {
        StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeOut()
    {
        GameScene.SetActive(true);
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeOutTime)
        {
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutTime);
            
            canvasGroup.alpha = newAlpha;
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        
        canvasGroup.alpha = 0f;
        UIScene.SetActive(false);
    }
}
