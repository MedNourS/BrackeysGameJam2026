/// <summary>
/// This object holds all the info needed to store a save
/// It gets converted to a JSON string and stored in PlayerPrefs
/// 
/// This approach is used because it is easiest to integrate with a WebGL build
/// </summary> 

public class SaveData {
    /// IMPORTANT: Assign DEFAULTS to save data
    // Otherwise they'll get instantiated with 0 or null!!
    public int unlockedLevels = 1;
}
