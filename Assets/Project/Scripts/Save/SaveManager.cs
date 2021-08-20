public interface ISave
{
    SaveMap SaveMap { get; set; }
    void Load();
    void Save();
    string GetSaveMapString();
}

public class SaveManager : GameSystem
{
    private readonly ISave CurSave = new JsonSave();

    public SaveManager(Game game) : base(game)
    {
    }

    public SaveMap SaveMap => CurSave.SaveMap;

    private void Load()
    {
        CurSave.Save();
    }

    private void Save()
    {
        CurSave.Save();
    }

    private string GetSaveMapString()
    {
        return CurSave.GetSaveMapString();
    }
}