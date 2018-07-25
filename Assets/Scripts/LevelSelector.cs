using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject Worlds;
    public Sprite StarWon;
    public Sprite StarEmpty;

    private int worldIndex = 0;
    private int maxWorldIndex = 2;

    private DataController dataController;

    private void Start()
    {
        dataController = GetComponent<DataController>();

        FillWorldsLevels();
    }

    public void NextWorld()
    {
        worldIndex++;
        if (worldIndex > maxWorldIndex)
        {
            worldIndex = maxWorldIndex;
            return;
        }
        Worlds.GetComponent<RectTransform>().anchoredPosition = new Vector2(Worlds.GetComponent<RectTransform>().anchoredPosition.x - 550f, Worlds.GetComponent<RectTransform>().anchoredPosition.y);

    }
    public void PreviewWorld()
    {
        worldIndex--;
        if (worldIndex < 0)
        {
            worldIndex = 0;
            return;
        }
        Worlds.GetComponent<RectTransform>().anchoredPosition = new Vector2(Worlds.GetComponent<RectTransform>().anchoredPosition.x + 550f, Worlds.GetComponent<RectTransform>().anchoredPosition.y);
    }

    private void FillWorldsLevels()
    {
        Player playerData = dataController.GetPlayer();

        int levelIndex = 0;
        foreach (Transform world in Worlds.transform)
        {
            GameObject LevelsContainer = world.Find("LevelsContainer").gameObject;

            foreach (Transform level in LevelsContainer.transform)
            {
                Level levelData = dataController.GetLevels()[levelIndex];

                GameObject lockGameObject = level.Find("Lock").gameObject;
                GameObject numberGameObject = level.Find("Number").gameObject;
                GameObject starsContainer = level.Find("StarsContainer").gameObject;

                int starIndex = 0;
                int numberOfStars = levelData.stars;
                foreach (Transform Star in starsContainer.transform)
                {
                    if (starIndex > numberOfStars - 1)
                        Star.gameObject.GetComponent<Image>().sprite = StarEmpty;
                    else
                        Star.gameObject.GetComponent<Image>().sprite = StarWon;
                    starIndex++;
                }

                levelData.locked = playerData.numberOfStars >= levelData.starsToUnlock ? false : true;

                lockGameObject.SetActive(levelData.locked);
                numberGameObject.SetActive(!levelData.locked);

                levelIndex++;
            }
        }
    }
}
