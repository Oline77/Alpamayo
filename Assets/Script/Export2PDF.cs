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
        PdfWriter.GetInstance(document, new FileStream("facture.pdf", FileMode.Create));
        document.Open();

        // Ajouter un titre à la facture
        Paragraph title = new Paragraph("Facture");
        title.Alignment = Element.ALIGN_CENTER;
        document.Add(title);

        // Ajouter un tableau avec les informations de chaque objet de la scène
        PdfPTable table = new PdfPTable(3);
        table.AddCell("Nom de l'objet");
        table.AddCell("Position X");
        table.AddCell("Position Y");

        // Récupérer tous les objets de la scène
        List<GameObject> allObjects = new List<GameObject>();
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (obj.activeInHierarchy)
            {
                allObjects.Add(obj);
            }
        }

        // Ajouter les informations de chaque objet dans le tableau
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
