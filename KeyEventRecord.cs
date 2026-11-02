using System.Windows.Forms;

namespace KeyEventLogger;

/// <summary>Bir tuş olayının türü: tuşa basma (Down) veya tuşu bırakma (Up).</summary>
public enum KeyEventType
{
    Down,
    Up
}

/// <summary>
/// Tek bir tuş olayının kaydı: zaman damgası, olay türü, tuş kodu ve modifier tuşları.
/// </summary>
public sealed class KeyEventRecord
{
    public KeyEventRecord(DateTime timestamp, KeyEventType eventType, Keys key, Keys modifiers)
    {
        Timestamp = timestamp;
        EventType = eventType;
        Key = key;
        Modifiers = modifiers;
    }

    public DateTime Timestamp { get; }

    public KeyEventType EventType { get; }

    /// <summary>Tuşun <see cref="Keys"/> değeri (modifier bitleri olmadan).</summary>
    public Keys Key { get; }

    /// <summary>O anda basılı olan modifier tuşları (Ctrl, Shift, Alt).</summary>
    public Keys Modifiers { get; }

    /// <summary>Tuşun okunabilir adı, örneğin "A" veya "ControlKey".</summary>
    public string DisplayText => Key.ToString();

    /// <summary>Sayısal tuş kodu.</summary>
    public int KeyCode => (int)Key;

    public string TimeText => Timestamp.ToString("HH:mm:ss.fff");

    public string EventTypeText => EventType == KeyEventType.Down ? "Basıldı" : "Bırakıldı";

    public string KeyCodeText => $"{KeyCode} (0x{KeyCode:X2})";

    public string ModifierText
    {
        get
        {
            var parts = new List<string>(3);
            if (Modifiers.HasFlag(Keys.Control))
            {
                parts.Add("Ctrl");
            }

            if (Modifiers.HasFlag(Keys.Shift))
            {
                parts.Add("Shift");
            }

            if (Modifiers.HasFlag(Keys.Alt))
            {
                parts.Add("Alt");
            }

            return parts.Count == 0 ? "-" : string.Join("+", parts);
        }
    }

    /// <summary>Dışa aktarma için tek satırlık, sekmeyle ayrılmış metin.</summary>
    public string ToTextLine()
    {
        return $"{Timestamp:yyyy-MM-dd HH:mm:ss.fff}\t{EventType}\t{Key}\t{KeyCode}\t{ModifierText}";
    }
}
