using UnityEngine.UI;
using UnityEngine;



public class TutorialLogic : MonoBehaviour
{
    public Image imageContainer;
    public Button goPreviousButton;
    public Button goNextButton;
    public Sprite[] tutorialPagesArray;

    private int currentImage;

    void Awake()
    {
        currentImage = 0;
        goPreviousButton.gameObject.SetActive(false);
    }

    public void GoToPreviousImage()
    {
        if (currentImage == tutorialPagesArray.Length - 1)
        {
            goNextButton.gameObject.SetActive(true);
        }
        if (currentImage == 1)
        {
            goPreviousButton.gameObject.SetActive(false);
        }
        currentImage -= 1;
        imageContainer.sprite = tutorialPagesArray[currentImage];
        Debug.Log("Previous page opened");
    }
    public void GoToNextImage()
    {
        if (currentImage == tutorialPagesArray.Length - 2)
        {
            goNextButton.gameObject.SetActive(false);
        }
        if (currentImage == 0)
        {
            goPreviousButton.gameObject.SetActive(true);
        }
        currentImage += 1;
        imageContainer.sprite = tutorialPagesArray[currentImage];
        Debug.Log("Next page opened");
    }
}
