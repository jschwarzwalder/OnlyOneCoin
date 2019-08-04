using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiCoinPanel : MonoBehaviour
{
    public string[] ColorNames;
    public Color[] Colors;
    public float NeededAlpha;
    public Image ImagePrefab;
    public float Gutter;
    public float Margin;

    private float nextY;
    private Dictionary<string, Image> images = new Dictionary<string, Image>();
    private Dictionary<string, Color> colors = new Dictionary<string, Color>();

    // Start is called before the first frame update
    void Start()
    {
        nextY = -Margin;

        Debug.Assert(ColorNames.Length == Colors.Length);
        for (int i = 0; i < ColorNames.Length; ++i)
        {
            colors[ColorNames[i]] = Colors[i];
        }
        SceneManager.sceneUnloaded += OnSceneUnload;
        }

    private void OnSceneUnload(Scene arg0)
    {
        foreach (Image image in images.Values)
        {
            Destroy(image.gameObject);
        }
        images.Clear();
        nextY = -Margin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCoinCreate(string colorName)
    {
        if (!images.ContainsKey(colorName))
        {
            GameObject imageObject = Instantiate(ImagePrefab.gameObject, transform);

            RectTransform imageRect = imageObject.GetComponent<RectTransform>();
            Vector3 pos = new Vector3(Margin, nextY, 0);
            imageRect.anchoredPosition = pos;
            nextY -= (Gutter + imageRect.rect.height);

            Image image = imageObject.GetComponent<Image>();
            Color imageColor = colors[colorName];
            imageColor.a = NeededAlpha;
            image.color = imageColor;

            images[colorName] = image;
        }
    }

    public void OnCoinPickup(string colorName)
    {
        images[colorName].color = colors[colorName];
    }
}
