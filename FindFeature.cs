namespace AutosaveNotepad
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        // SEARCH BUTTON

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
                Search(richTextBox.Text, findTextBox.Text, false, out searchResultOK);

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
                    debug.Text = ":(";
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

        /*
         "Highlight" Button.
         */

        private void findNextButton_Click(object sender, EventArgs e) // "Highlight" button
        {
            string findQuery = findTextBox.Text;
            string richText = richTextBox.Text;
            richTextBox.Text = richText;
            //currentFindIndex = savedFindIndex;
            bool caseSensitive = false;
            searchResultOK = false;

            FindBoxAndControlsGlobalController(searchResultOK);

            if (textEditingLocked)
            {
                EditingRichTextBoxFeaturesEnabled(true);
                textEditingLocked = false;
                findButton.Text = "Highlight!";
                richTextBox.Text = textBackup;
                FoundCounterController(currentFindIndex, allFinds.Count, "find");
                SearchInRichTextBox();
            }

            else
            {
                ResetFind();
                Search(richText, findQuery, caseSensitive, out searchResultOK);

                if (searchResultOK)
                {
                    Highlight(allFinds, findQuery.Length);
                    FoundCounterController(currentFindIndex, allFinds.Count, "highlight");
                }
                else
                {
                    FoundCounterController(currentFindIndex, allFinds.Count, "find");
                }
            }
        }

        private void Search(string aTextbox, string aQuery, bool aCasing, out bool result)
        {
            string text = aTextbox;
            if (!aCasing) text = text.ToLower();
            List<int> foundIndexes = new List<int>();
            bool found = false;

            for (int i = 0; i < text.Length; i++)
            {
                int occurrenceStreak = 0;
                if (text[i] == aQuery[0]
                    && text.Length >= aQuery.Length + i)
                {
                    for (int j = 0; j < aQuery.Length; j++)
                    {
                        if (text[i + j] == aQuery[j])
                        {
                            occurrenceStreak++;
                        }
                    }

                    if (occurrenceStreak == aQuery.Length)
                    {
                        foundIndexes.Add(i);
                        //i += query.Length;
                        found = true;
                    }
                }
            }
            if (found)
            {
                result = found;
                allFinds = foundIndexes;
                currentFindIndex = 0;
                findLength = aQuery.Length;
                richTextBox.Enabled = true;
                foundQuery = aQuery;
            }

            else
            {
                result = false;
                foundQuery = "";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (textEditingLocked && findTextBox.Focused != true)
            {
                // Disable all key input and editing actions
                return true;
            }

            else if (textEditingLocked && richTextBox.Focused)
            {
                return true;
            }

            else return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NextPrevGlobalController(bool state)
        {
            findNextButtonReal.Enabled = state;
            findPrevButton.Enabled = state;
        }

        private void EditingRichTextBoxFeaturesEnabled(bool state)
        {
            richTextBox.Enabled = state;
            richTextBox.ShortcutsEnabled = state;
        }

        private void ResetFind()
        {
            //currentFindIndex = 0;
            allFinds.Clear();
            findLength = 0;
            searchResultOK = false;
            //savedFindIndex = 0;
        }


        private void Highlight(List<int> indexes, int queryLength)
        {
            textBackup = richTextBox.Text;
            findButton.Text = "Done";
            EditingRichTextBoxFeaturesEnabled(false);
            textEditingLocked = true;


            for (int i = 0; i < indexes.Count; i++)
            {
                richTextBox.Select(indexes[i], queryLength);
                richTextBox.SelectionBackColor = Color.Yellow;
            }

            FindBoxAndControlsGlobalController(searchResultOK);

        }

        private void FindBoxAndControlsGlobalController(bool status)
        {
            findTextBox.Enabled = !status;
            searchButton.Enabled = !status;
            findNextButtonReal.Enabled = !status;
            findPrevButton.Enabled = !status;
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
            currentFindIndex++;
            SelectText(allFinds[currentFindIndex], foundQuery.Length);
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
            //savedFindIndex = currentFindIndex;
        }

        private void findPrevButton_Click(object sender, EventArgs e)
        {
            currentFindIndex--;
            SelectText(allFinds[currentFindIndex], foundQuery.Length);
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
            //savedFindIndex = currentFindIndex;
        }

        private void FoundCounterController(int current, int total, string mode)
        {
            int index = current;
            if (total > 0)
            {
                foundCounter.Visible = true;
                //findQuerySuccess = true;
                foundCounter.Text = (index + 1).ToString() + "/" + total.ToString();
                if (foundCounter.Text.Count() > 11)
                {
                    foundCounter.Text = total.ToString() + " found.";
                    if (foundCounter.Text.Count() > 11)
                    {
                        foundCounter.Text = "9999999999+";
                    }
                }


                if (mode != "highlight")
                {
                    findPrevButton.Enabled = true;
                    findNextButtonReal.Enabled = true;

                    if (index == allFinds[0] && allFinds.Count == 1)
                    {
                        findPrevButton.Enabled = false;
                        findNextButtonReal.Enabled = false;
                    }

                    else if (index == allFinds[0]) // when index it at a start
                    {
                        findPrevButton.Enabled = false;
                    }


                    else if (index + 1 == total)
                    {

                        findNextButtonReal.Enabled = false;
                    }
                }
            }
            else
            {
                //findQuerySuccess = false;
                foundCounter.Visible = false;
            }
        }

        private void findTextBox_Enter(object sender, EventArgs e)
        {

        }

        private void findTextBox_Leave(object sender, EventArgs e)
        {

        }
    }
}
