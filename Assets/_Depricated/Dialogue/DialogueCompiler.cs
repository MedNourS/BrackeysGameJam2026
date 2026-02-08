using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions; // regex my lord and saviour


// Deal with #meta and #lang-settings later

public sealed class DialogueSection
{
    public string Name;
    public List<DialogueBlock> Blocks = new();
}

public sealed class DialogueBlock
{
    public string Speaker;
    public List<DialogueContentNode> Content = new();
}

// Is a template for all the kinds of content nodes that can go inside
public abstract class DialogueContentNode { }

public sealed class DialogueTextNode
{

}

public class DialogueCompiler
{
    private string baseDialogueFolder = "Assets/Dialogue";

    // load and stick file contents in a string
    public string loadDialogueFile(string filepath, string lang)
    {
        // foo/bar -> Assets/Dialogue/foo/bar/bar_lang.dialogue
        string childBase = Path.GetFileName(filepath);
        string path = Path.Combine(baseDialogueFolder, filepath, $"{childBase}_{lang}.dialogue");
        return File.ReadAllText(path);
    }



}