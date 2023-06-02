namespace AutosaveNotepad
{
    public partial class formMain
    {
        private void CheckForUndoRedo()
        {
            if (richTextBox.CanUndo)
            {
                undoToolStripMenuItem.Enabled = true;
                undoToolStripMenuItem1.Enabled = true;
            }

            else
            {
                undoToolStripMenuItem.Enabled = false;
                undoToolStripMenuItem1.Enabled = false;
            }

            if (richTextBox.CanRedo)
            {
                redoToolStripMenuItem.Enabled = true;
                redoToolStripMenuItem1.Enabled = true;
            }

            else
            {
                redoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem1.Enabled = false;
            }
        }
    }
}
