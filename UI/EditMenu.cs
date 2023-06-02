namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        //
        // EDIT menu
        //

        // Undo

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanUndo)
            {
                richTextBox.Undo();
                undoToolStripMenuItem.Enabled = true;

            }

            else
            {
                undoToolStripMenuItem.Enabled = false;
            }

            Autosave();
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (richTextBox.CanUndo)
            {
                richTextBox.Undo();
                undoToolStripMenuItem1.Enabled = true;

            }

            else
            {
                undoToolStripMenuItem1.Enabled = false;
            }

            Autosave();
        }

        // Redo

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanRedo)
            {
                richTextBox.Redo();
                redoToolStripMenuItem.Enabled = true;

            }

            else
            {
                richTextBox.Redo();
                redoToolStripMenuItem.Enabled = false;
            }

            Autosave();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanRedo)
            {
                richTextBox.Redo();
                redoToolStripMenuItem1.Enabled = true;

            }

            else
            {
                richTextBox.Redo();
                redoToolStripMenuItem1.Enabled = false;
            }
            Autosave();
        }

        // Cut

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
            Autosave();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
            Autosave();
        }

        // Copy

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
            Autosave();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
            Autosave();
        }

        // Paste

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessPaste();
            Autosave();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcessPaste();
            Autosave();
        }

        // Clear

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            Autosave();
        }

        private void clearAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            Autosave();
        }

        // Select All
        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
            Autosave();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
            Autosave();
        }

        // Word Wrap

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
            {
                WordWrapActive(true);
            }

            else
            {
                WordWrapActive(false);
            }
        }

        private void wordWrapToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
            {
                WordWrapActive(true);
            }

            else
            {
                WordWrapActive(false);
            }
        }
    }
}
