namespace AutosaveNotepad
{
    using System;
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
                ReplaceController(searchResultOK); //this enables a possibility to replace text

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

        private void ResetFind()
        {
            allFinds.Clear();
            findLength = 0;
            searchResultOK = false;
        }

        private void Search(string aTextbox, string aQuery, out bool result)
        {
            string text = aTextbox;
            string query = aQuery;

            // this controls Case Sensitive search by lowercasing text and query
            if (caseSensitiveSearchToolStripMenuItem.Checked == false)
            {
                text = text.ToLower();
                query = query.ToLower();
            }

            List<int> foundIndexes = new List<int>();
            bool found = false;

            // double loop: first for scanning, and second for comparing query to text starting at the [i] index,
            // occurrency streak tests if the query is the same in length, if it's the same then the index at which it's found is added to the foundIndexes list

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

                        else
                        {
                            break;
                        }
                    }

                    if (occurrenceStreak == query.Length)
                    {
                        foundIndexes.Add(i);
                        found = true;
                    }
                }
            }

            if (found) // this replaces global variables with results from this local method for use in any other methods
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

        //this takes positive results from Search method and selects find at an index
        private void SelectText(int selectedIndex, int selectionLength)
        {
            richTextBox.SelectionStart = selectedIndex;
            richTextBox.SelectionLength = selectionLength;
            richTextBox.Focus();
            richTextBox.Refresh();
        }

        /*
         
        Going back and forth in the search results and handling constraints via found counter controller

         */

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
