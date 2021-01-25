using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TODO: DISPLAY ERRORS on sections invalid!
/// </summary>
public class SectionManager : MonoBehaviour
{
    [SerializeField] private InputField width, sections;
    [SerializeField] private InputField[] sectionsWidth;
    [SerializeField] private ImplementSettings implemento;
    // Start is called before the first frame update
    public void OnSectionsChange(string s)
    {
        int sect = int.Parse(s);
        ShowSections(sect);
    }

    public void OnOneSectionChange()
    {
        int w = 0;
        List<int> temp_secciones = new List<int>();
        for (int i = 0; i < sectionsWidth.Length; i++)
        {
            if(sectionsWidth[i].text != "" && sectionsWidth[i].text != null)
            {
                if (int.Parse(sectionsWidth[i].text)>0)
                {
                    temp_secciones.Add(int.Parse(sectionsWidth[i].text));
                    w += int.Parse(sectionsWidth[i].text);
                }
            }
        }
        implemento.setSections(temp_secciones.ToArray());
        width.text = w.ToString();
    }

    void ShowSections(int s)
    {
        for (int i = 0; i < s; i++)
        {
            sectionsWidth[i].readOnly = false;
            if (sectionsWidth[i].text == "")
                sectionsWidth[i].text = "0";
        }
        for (int i = sectionsWidth.Length-1; i >= s; i--)
        {
            sectionsWidth[i].text = "";
            sectionsWidth[i].readOnly=true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
