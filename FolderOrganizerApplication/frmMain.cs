using FolderOrganizerApplication.DateTimes;

namespace FolderOrganizerApplication
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void btnOrganize_Click(object sender, EventArgs e)
        {
            var UnOrganizedFileList = Directory.GetFiles(txtSourceFolderPath.Text, "*.*", SearchOption.AllDirectories);
            prgOrganizing.Maximum = UnOrganizedFileList.Length;
            prgOrganizing.Minimum = 0;
            prgOrganizing.Value = 0;
            await Task.Factory.StartNew(() => OrganizingFiles(UnOrganizedFileList));
             
        }

        private void OrganizingFiles(string[] UnOrganizedFileList)
        {
            foreach (var UnOrganizedFile in UnOrganizedFileList)
            {
                var UnOrganizedFileInfo = new FileInfo(UnOrganizedFile);
                if (UnOrganizedFileInfo.Exists)
                {
                    var UnOrganizedFilePersianCreateDate = PersianDateTime.MiladiToPersian(UnOrganizedFileInfo.CreationTime);
                    var OrganizedPath = Path.Combine(txtDestinationFolderPath.Text,
                        UnOrganizedFilePersianCreateDate.ToYearMonthString('-'));
                    if (!Directory.Exists(OrganizedPath))
                        Directory.CreateDirectory(OrganizedPath);
                    UnOrganizedFileInfo.MoveTo(Path.Combine(OrganizedPath, UnOrganizedFileInfo.Name), false);
                }

                SetText(prgOrganizing.Value + 1);

            }
        }

        delegate void SetTextCallback(int value);

        private void SetText(int value)
        {
            if (prgOrganizing.InvokeRequired)
            {
                SetTextCallback d = SetText;
                Invoke(d, value);
            }
            else
                prgOrganizing.Value = value;

        }

        private void btnSelectSourceFolder_Click(object sender, EventArgs e)
        {
            if (fbdFolderSelect.ShowDialog() == DialogResult.OK)
            {
                txtSourceFolderPath.Text = fbdFolderSelect.SelectedPath;
            }

        }

        private void btnSelectDestinationFolder_Click(object sender, EventArgs e)
        {
            if (fbdFolderSelect.ShowDialog() == DialogResult.OK)
            {
                txtDestinationFolderPath.Text = fbdFolderSelect.SelectedPath;
            }
        }
    }
}