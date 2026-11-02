using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KeyEventLogger;

/// <summary>
/// Ana pencere. Yalnızca bu pencere içinde oluşan tuş olaylarını (KeyPreview) kaydeder.
/// </summary>
public sealed class MainForm : Form
{
    private readonly KeyEventLog _log = new();
    private readonly ListView _listView = new();
    private readonly Label _statusLabel = new();
    private readonly Panel _topPanel = new();
    private readonly Button _clearButton = new();
    private readonly Button _exportButton = new();
    private readonly CheckBox _onlyDownCheckBox = new();

    public MainForm()
    {
        BuildUserInterface();
        _log.RecordAdded += OnRecordAdded;
    }

    private void BuildUserInterface()
    {
        Text = "Key Event Logger";
        ClientSize = new Size(900, 480);
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview = true;
        KeyDown += OnFormKeyDown;
        KeyUp += OnFormKeyUp;

        _listView.View = View.Details;
        _listView.FullRowSelect = true;
        _listView.GridLines = true;
        _listView.Dock = DockStyle.Fill;
        _listView.Columns.Add("Zaman", 130);
        _listView.Columns.Add("Olay", 90);
        _listView.Columns.Add("Tuş", 130);
        _listView.Columns.Add("Kod", 110);
        _listView.Columns.Add("Modifier", 150);

        _statusLabel.Dock = DockStyle.Bottom;
        _statusLabel.Height = 28;
        _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
        _statusLabel.Padding = new Padding(8, 0, 0, 0);

        _topPanel.Dock = DockStyle.Top;
        _topPanel.Height = 48;

        _clearButton.Text = "Temizle";
        _clearButton.Location = new Point(8, 10);
        _clearButton.Size = new Size(100, 28);
        _clearButton.Click += OnClearClick;

        _exportButton.Text = "Dışa Aktar";
        _exportButton.Location = new Point(116, 10);
        _exportButton.Size = new Size(110, 28);
        _exportButton.Click += OnExportClick;

        _onlyDownCheckBox.Text = "Yalnızca basılanları kaydet";
        _onlyDownCheckBox.AutoSize = true;
        _onlyDownCheckBox.Location = new Point(240, 14);

        _topPanel.Controls.Add(_clearButton);
        _topPanel.Controls.Add(_exportButton);
        _topPanel.Controls.Add(_onlyDownCheckBox);

        // Kenara yapışan (Dock) kontroller için Fill kontrolü ilk eklenir.
        Controls.Add(_listView);
        Controls.Add(_statusLabel);
        Controls.Add(_topPanel);

        UpdateStatus();
    }

    private void OnFormKeyDown(object? sender, KeyEventArgs e)
    {
        _log.Add(new KeyEventRecord(DateTime.Now, KeyEventType.Down, e.KeyCode, e.Modifiers));
    }

    private void OnFormKeyUp(object? sender, KeyEventArgs e)
    {
        if (_onlyDownCheckBox.Checked)
        {
            return;
        }

        _log.Add(new KeyEventRecord(DateTime.Now, KeyEventType.Up, e.KeyCode, e.Modifiers));
    }

    private void OnClearClick(object? sender, EventArgs e)
    {
        _log.Clear();
        _listView.Items.Clear();
        UpdateStatus();
    }

    private void OnExportClick(object? sender, EventArgs e)
    {
        if (_log.Count == 0)
        {
            MessageBox.Show(this, "Dışa aktarılacak kayıt yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "Metin dosyası (*.txt)|*.txt",
            DefaultExt = "txt",
            FileName = $"key-event-log-{DateTime.Now:yyyyMMdd-HHmmss}.txt"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            File.WriteAllLines(dialog.FileName, _log.ToTextLines(), Encoding.UTF8);
            MessageBox.Show(this, $"{_log.Count} kayıt dışa aktarıldı.", "Dışa Aktar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(this, $"Dosya yazılamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnRecordAdded(KeyEventRecord record)
    {
        var item = new ListViewItem(record.TimeText);
        item.SubItems.Add(record.EventTypeText);
        item.SubItems.Add(record.DisplayText);
        item.SubItems.Add(record.KeyCodeText);
        item.SubItems.Add(record.ModifierText);

        _listView.Items.Add(item);
        _listView.EnsureVisible(_listView.Items.Count - 1);
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _statusLabel.Text = $"Toplam kayıt: {_log.Count}";
    }
}
