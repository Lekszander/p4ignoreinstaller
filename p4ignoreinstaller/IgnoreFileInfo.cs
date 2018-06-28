using System;

namespace p4ignoreinstaller
{
    class IgnoreFileInfo
    {

        public string FilePath { get; set; }
        public string File
        {
            get
            {
                if(!String.IsNullOrEmpty(FilePath))
                {
                    return FilePath + "\\.p4ignore";
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        public string Text { get; set; }

        public IgnoreFileInfo()
        {
            FilePath = String.Empty;
            Text = String.Empty;
        }

        public IgnoreFileInfo(string Text)
        {
            FilePath = String.Empty;
            this.Text = Text;
        }

        public IgnoreFileInfo(string FilePath, string Text)
        {
            this.FilePath = FilePath;
            this.Text = Text;
        }
    }
}