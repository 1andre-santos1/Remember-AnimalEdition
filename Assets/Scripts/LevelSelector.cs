using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject Worlds;
    public Sprite StarWon;
    public Sprite StarEmpty;
    public GameObject TextStarsNumber;

    private int worldIndex = 0;
    private int maxWorldIndex = 2;

    private DataController dataController;

    private void Start()
    {
        dataController = GameObject.FindObjectOfType<DataController>();

        dataController.LoadData();
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

        TextStarsNumber.GetComponent<Text>().text = "" + playerData.numberOfStars;

        int levelIndex = 0;
        foreach (Transform world in Worlds.transform)
        {
            GameObject LevelsContainer = world.Find("LevelsContainer").gameObject;

            foreach (Transform level in LevelsContainer.transform)
            {
                Level levelData = dataController.GetLevels()[levelIndex];

                levelData.locked = playerData.numberOfStars >= levelData.starsToUnlock ? false : true;

                GameObject LockedContainer = level.Find("Locked").gameObject;
                GameObject UnlockedContainer = level.Find("Unlocked").gameObject;

                if (levelData.locked)
                {
                    LockedContainer.SetActive(true);
                    UnlockedContainer.SetActive(false);

                    int starsLeftToUnlockLevel = levelData.starsToUnlock - playerData.numberOfStars;
                    LockedContainer.transform.Find("Text").GetComponent<Text>().text = "" + starsLeftToUnlockLevel;
                }
                else
                {
                    LockedContainer.SetActive(false);
                    UnlockedContainer.SetActive(true);

                    GameObject textLevelNumber = UnlockedContainer.transform.Find("Number").gameObject;
                    textLevelNumber.GetComponent<Text>().text = "" + (levelIndex + 1);

                    GameObject starsContainer = UnlockedContainer.transform.Find("StarsContainer").gameObject;

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
                }

                levelIndex++;
            }
        }
    }
}
