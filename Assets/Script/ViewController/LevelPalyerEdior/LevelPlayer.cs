using System.Xml;
using UnityEngine;

public class LevelPlayer : MonoBehaviour
{
    public TextAsset LevelFile;

    void Start()
    {
        var xml = LevelFile.text;

        var document = new XmlDocument();

        document.LoadXml(xml);

        var levelNode = document.SelectSingleNode("Level");

        foreach (XmlElement levelItemNode in levelNode.ChildNodes)
        {
            var levelItemName = levelItemNode.GetAttribute("name");
            var levelItemX = int.Parse(levelItemNode.GetAttribute("x"));
            var levelItemY = int.Parse(levelItemNode.GetAttribute("y"));


            Debug.Log(levelItemName);
            var levelItemPrefab = Resources.Load<GameObject>(levelItemName);
            var levelItemGameObj = Instantiate(levelItemPrefab, transform);
            levelItemGameObj.transform.position = new Vector3(levelItemX, levelItemY, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
