using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitago.Controls
{
    public partial class Notepad
    {

        public void Save(string path)
        {
            File.WriteAllText(path, _textEditor.Text);
        }

        public void Load(string path)
        {
            _textEditor.Text = File.ReadAllText(path);
        }

    }
}
