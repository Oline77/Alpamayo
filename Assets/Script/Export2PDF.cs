using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using UnityEngine.SceneManagement;

using UnityEngine.UI;



public class Export2PDF : MonoBehaviour
{
   
    // Start is called before the first frame update 
    public void onClickPDF()
    {
        // Créer un nouveau document PDF
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream("scene_objects.pdf", FileMode.Create));
        document.Open();

        // Créer un tableau dans le document
        PdfPTable table = new PdfPTable(3);
        table.AddCell("Nom de l'objet");
        table.AddCell("Position X");
        table.AddCell("Position Y");

        // Récupérer tous les objets de la scène
        Scene currentScene = SceneManager.GetActiveScene();
        List<GameObject> allObjects = new List<GameObject>();
        currentScene.GetRootGameObjects(allObjects);

        // Parcourir les objets récupérés et ajouter leurs noms et positions dans le tableau
        foreach (GameObject obj in allObjects)
        {
            table.AddCell(obj.name);
            table.AddCell(obj.transform.position.x.ToString());
            table.AddCell(obj.transform.position.y.ToString());
        }

        // Ajouter le tableau au document
        document.Add(table);
        document.Close();

    }
}
