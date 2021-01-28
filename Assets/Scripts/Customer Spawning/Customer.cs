using UnityEngine;


public class Customer : MonoBehaviour
{
    #region Properties

    private Sprite[] customerImages;

    public int IDOfObjectToFind { get; set; }
    public float TimeForSearching { get; set; }
    public Sprite ImageOfObjectToFind { get; set; }

    #endregion

    void Awake()
    {
        customerImages = transform.parent.parent.GetComponent<CustomerImagesToFindController>().images;
    }

    #region Methods

    public void ConfigureProperties()
    {
        IDOfObjectToFind = Random.Range(0, 5);
        TimeForSearching = Random.Range(10, 20);

        int imageID = Random.Range(0, customerImages.Length);
        ImageOfObjectToFind = customerImages[imageID];
    }
    public void ConfigureProperties(int objectToFindID, float timeToSearch, Sprite image)
    {
        this.IDOfObjectToFind = objectToFindID;
        this.TimeForSearching = timeToSearch;
        this.ImageOfObjectToFind = image;
    }

    #endregion
}
