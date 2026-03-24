using UnityEngine;
using System.IO;
using System.Text;

public class SaveEngine : MonoBehaviour
{
    [SerializeField] private bool UseEncryption = true;
    [SerializeField] private string EncryptionKey = "YoussefAmr";
    [SerializeField] private string SaveFileName = "save.ysa";

    public SaveData SaveData { get; private set; }
    public static SaveEngine Instance { get; private set; }

    private string _savePath;

    void Awake()
    {
        _savePath = Path.Join(Application.persistentDataPath, SaveFileName);
        if (Instance != null)
        {
            Debug.LogError("there is more then one SaveEngine in scene");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (IsSaved())
        {
            LoadGameSave();
            return;
        }
        NewGame();
    }

    private void NewGame()
    {
        SaveData = new();
    }

    private bool IsSaved()
    {
        if (File.Exists(_savePath))
            return true;
        return false;
    }

    private void LoadGameSave()
    {
        if (!File.Exists(_savePath))
        {
            NewGame();
            return;
        }

        using (FileStream _s = new(_savePath, FileMode.OpenOrCreate))
        {
            using (StreamReader _r = new(_s))
            {
                SaveData = JsonUtility.FromJson<SaveData>(UseEncryption ? EncryptData(_r.ReadToEnd()) : _r.ReadToEnd());
            }
        }
    }

    private void SaveGameSave()
    {
        using (FileStream _s = new(_savePath, FileMode.OpenOrCreate))
        {
            using (StreamWriter _w = new(_s))
            {
                _w.Write(UseEncryption ? EncryptData(JsonUtility.ToJson(SaveData)) : JsonUtility.ToJson(SaveData));
            }
        }
    }

    private string EncryptData(string data)
    {
        StringBuilder m_string = new();
        for (int _char = 0; _char < data.Length; _char++)
            m_string.Append((char)(data[_char] ^ EncryptionKey[_char % EncryptionKey.Length]));

        return m_string.ToString();
    }
}
