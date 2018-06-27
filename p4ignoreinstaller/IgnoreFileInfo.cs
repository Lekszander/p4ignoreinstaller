using System;

namespace p4ignoreinstaller
{
    class IgnoreFileInfo
    {
        public string FilePath { get; private set; }
        public string Text { get; private set; } = "# Ignore files" + Environment.NewLine +
                                                    "*.sln" + Environment.NewLine +
                                                    ".vs" + Environment.NewLine +
                                                    @"obj\" + Environment.NewLine +
                                                    "*.suo" + Environment.NewLine +
                                                    "*.opensdf" + Environment.NewLine +
                                                    "*.sdf" + Environment.NewLine +
                                                    "*.pdb" + Environment.NewLine +
                                                    "* -Debug.*" + Environment.NewLine +
                                                    "*.vcxproj" + Environment.NewLine +
                                                    @".\Makefile" + Environment.NewLine +
                                                    @".\CMakeLists.txt" + Environment.NewLine +
                                                    @".\.ue4dependencies" + Environment.NewLine +
                                                    "* BuildData.uasset" + Environment.NewLine +
                                                    Environment.NewLine +
                                                    "# Ignore folders" + Environment.NewLine +
                                                    "Binaries" + Environment.NewLine +
                                                    "Intermediate" + Environment.NewLine +
                                                    "Saved" + Environment.NewLine +
                                                    "DerivedDataCache" + Environment.NewLine +
                                                    "FileOpenOrder" + Environment.NewLine +
                                                    Environment.NewLine +
                                                    "# Ignore crash reports" + Environment.NewLine +
                                                    "crashinfo--*" + Environment.NewLine;

        public IgnoreFileInfo()
        {

        }

        public IgnoreFileInfo(string FilePath, string Text)
        {
            this.FilePath = FilePath;
            this.Text = Text;
        }
    }
}
