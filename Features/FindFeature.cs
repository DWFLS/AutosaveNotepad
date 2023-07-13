namespace AutosaveNotepad
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        // FIND FEATURE

        private void searchButton_Click(object sender, EventArgs e)
        {
            NextPrevGlobalController(false);
            SearchInRichTextBox();
        }

        private void SearchInRichTextBox()
        {
            ResetFind();

            if (findTextBox.Text != "")
            {
                Search(richTextBox.Text, findTextBox.Text, out searchResultOK);
                ReplaceController(searchResultOK);

                if (searchResultOK)
                {
                    findButton.Enabled = true;

                    SelectText(allFinds[0], findLength);
                }

                else
                {
                    findButton.Enabled = false;
                    findNextButtonReal.Enabled = false;
                    findPrevButton.Enabled = false;
                    //debug.Text = ":(";
                }

            }

            else
            {
                findButton.Enabled = false;
                richTextBox.Enabled = true;
                findNextButtonReal.Enabled = false;
                findPrevButton.Enabled = false;
                richTextBox.Text = textBackup;
                textEditingLocked = false;
            }
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
        }

        private void Search(string aTextbox, string aQuery, out bool result)
        {
            string text = aTextbox;
            string query = aQuery;
            if (caseSensitiveSearchToolStripMenuItem.Checked == false)
            {
                text = text.ToLower();
                query = query.ToLower();
            }

            List<int> foundIndexes = new List<int>();
            bool found = false;

            for (int i = 0; i < text.Length; i++)
            {
                int occurrenceStreak = 0;

                if (text[i] == query[0]
                    && text.Length >= query.Length + i)
                {
                    for (int j = 0; j < query.Length; j++)
                    {
                        if (text[i + j] == query[j])
                        {
                            occurrenceStreak++;
                        }
                    }

                    if (occurrenceStreak == query.Length)
                    {
                        foundIndexes.Add(i);
                        found = true;
                    }
                }
            }

            if (found)
            {
                result = found;
                allFinds = foundIndexes;
                currentFindIndex = 0;
                findLength = query.Length;
                richTextBox.Enabled = true;
                foundQuery = query;
            }

            else
            {
                result = false;
                foundQuery = "";
            }
        }

        /*
         "Highlight" Button!!
         */

        private void findNextButton_Click(object sender, EventArgs e) // "Highlight" button
        {
            string findQuery = findTextBox.Text;
            string richText = richTextBox.Text;
            richTextBox.Text = richText;
            searchResultOK = false;

            FindBoxAndControlsGlobalController(searchResultOK);

            if (textEditingLocked)
            {
                EditingRichTextBoxFeaturesEnabled(true);
                textEditingLocked = false;
                CheckColors();
                findButton.Text = "Highlight!";
                richTextBox.Text = textBackup;
                FoundCounterController(currentFindIndex, allFinds.Count, "find");
                SearchInRichTextBox();
            }

            else
            {
                ResetFind();
                CheckColors();
                Search(richText, findQuery, out searchResultOK);

                if (searchResultOK)
                {
                    Highlight(allFinds, findQuery.Length);
                    ReplaceController(false);
                    FoundCounterController(currentFindIndex, allFinds.Count, "highlight");
                }
                else
                {
                    FoundCounterController(currentFindIndex, allFinds.Count, "find");
                }
            }
        }



        private void Highlight(List<int> indexes, int queryLength)
        {
            textBackup = richTextBox.Text;
            findButton.Text = "Done";
            EditingRichTextBoxFeaturesEnabled(false);
            textEditingLocked = true;
            CheckColors();

            for (int i = 0; i < indexes.Count; i++)
            {
                richTextBox.Select(indexes[i], queryLength);
                richTextBox.SelectionBackColor = Color.Yellow;
            }

            FindBoxAndControlsGlobalController(searchResultOK);
        }

        private void SelectText(int selectedIndex, int selectionLength)
        {
            richTextBox.SelectionStart = selectedIndex;
            richTextBox.SelectionLength = selectionLength;
            richTextBox.Focus();
            richTextBox.Refresh();
        }

        private void findNextButtonReal_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void Next()
        {
            if (currentFindIndex < allFinds.Count - 1)
            {
                currentFindIndex++;
                FoundCounterController(currentFindIndex, allFinds.Count, "find");
                SelectText(allFinds[currentFindIndex], foundQuery.Length);
            }

        }

        private void findPrevButton_Click(object sender, EventArgs e)
        {
            currentFindIndex--;
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
            SelectText(allFinds[currentFindIndex], foundQuery.Length);


        }

        private void findTextBox_Enter(object sender, EventArgs e)
        {

        }

        private void findTextBox_Leave(object sender, EventArgs e)
        {

        }
    }
}
