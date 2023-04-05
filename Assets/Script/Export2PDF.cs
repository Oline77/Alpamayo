using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Export2PDF : MonoBehaviour
{
    private List<GameObject> userObjects = new List<GameObject>();
    // Start is called before the first frame update 
    public void onClickPDF()
    {
        // Créer un nouveau document
        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("factureTest.pdf", FileMode.Create));
        document.Open();

        // Ajouter une en-tête
        Paragraph header = new Paragraph("FACTURE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24f, iTextSharp.text.Font.BOLD));
        header.Alignment = Element.ALIGN_CENTER;
        document.Add(header);

        // Ajouter l'adresse du client
        Paragraph address = new Paragraph("Adresse du client\nVille, Pays\nCode postal");
        address.SpacingBefore = 30f;
        document.Add(address);



        // Ajout d'un paragraphe de description de la table
        document.Add(new Paragraph("\n\nRécapitulatif des objets ajoutés à la scène :"));

        // Ajouter un tableau de données
        PdfPTable table = new PdfPTable(3);
        table.WidthPercentage = 100f;
        table.SpacingBefore = 20f;
        table.SpacingAfter = 20f;
        float[] columnWidths = { 1f, 3f, 2f };
        table.SetWidths(columnWidths);
        PdfPCell cell = new PdfPCell(new Phrase("NOM"));
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        table.AddCell(cell);
        cell = new PdfPCell(new Phrase("DESCRIPTION"));
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        table.AddCell(cell);
        cell = new PdfPCell(new Phrase("PRIX"));
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        table.AddCell(cell);
        /*foreach (GameObject obj in userObjects)
        {
            if (obj.transform.parent == null && obj.GetComponent<Light>() == null && obj.GetComponent<Camera>() == null && obj.GetComponent<Canvas>() == null && !(obj.name.Contains("EmptyAction")) && obj.GetComponent<EventSystem>() == null && !(obj.name.Contains("Plane")))
            {
                continue;
            }
            
            cell = new PdfPCell(new Phrase(obj.name));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(obj.name + " description"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("100.00 $"));
            table.AddCell(cell);
            
            
        }*/

        // Ajouter les objets de l'utilisateur
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.transform.parent == null && obj.GetComponent<Light>() == null && obj.GetComponent<Camera>() == null && obj.GetComponent<Canvas>() == null && !(obj.name.Contains("EmptyAction")) && obj.GetComponent<EventSystem>() == null && !(obj.name.Contains("Plane")))
            {
                userObjects.Add(obj);
                cell = new PdfPCell(new Phrase(obj.name));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(" Sapin de Haute-Savoie"));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("100.00 €"));
                table.AddCell(cell);
            }
        }

        document.Add(table);

        // Ajouter un pied de page avec la date
        Paragraph footer = new Paragraph("Date : " + System.DateTime.Now.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f));
        footer.Alignment = Element.ALIGN_RIGHT;
        footer.SpacingBefore = 20f;
        document.Add(footer);

        // Ajout d'un paragraphe de description du screenshot
        document.Add(new Paragraph("Voici une capture d'écran de la scène :"));

        // Ajouter une image de la caméra
        Camera cam = Camera.main;
        RenderTexture renderTexture = new RenderTexture(500, 300, 24);
        cam.targetTexture = renderTexture;
        Texture2D screenshot = new Texture2D(500, 200, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, 500, 300), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null;
        byte[] bytes = screenshot.EncodeToPNG();
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bytes);
        image.ScalePercent(50f);
        image.Alignment = Element.ALIGN_RIGHT;
        document.Add(image);

        // Ajoute une ligne supplémentaire à la table pour afficher le prix total
        table.AddCell(new PdfPCell(new Phrase("Total")));

        // Fermer le document
        document.Close();

        Debug.Log("Facture créée avec succès !");

    }
}
