namespace KeyEventLogger;

/// <summary>
/// Tuş olaylarının koleksiyonu. Yeni kayıt eklendiğinde <see cref="RecordAdded"/> olayını fırlatır.
/// </summary>
public sealed class KeyEventLog
{
    private const string HeaderLine = "Zaman\tOlay\tTuş\tKod\tModifier";

    private readonly List<KeyEventRecord> _records = new();

    /// <summary>Yeni bir kayıt eklendiğinde tetiklenir.</summary>
    public event Action<KeyEventRecord>? RecordAdded;

    public int Count => _records.Count;

    public IReadOnlyList<KeyEventRecord> Records => _records;

    public void Add(KeyEventRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        _records.Add(record);
        RecordAdded?.Invoke(record);
    }

    public void Clear()
    {
        _records.Clear();
    }

    /// <summary>Kayıtları başlık satırıyla birlikte metin satırlarına çevirir.</summary>
    public IReadOnlyList<string> ToTextLines()
    {
        var lines = new List<string>(_records.Count + 1) { HeaderLine };
        lines.AddRange(_records.Select(record => record.ToTextLine()));
        return lines;
    }
}
