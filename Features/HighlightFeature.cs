namespace AutosaveNotepad
{
    using System;
    public partial class formMain
    {
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

            // highlight button availability is controlled by search results, so the bellow switch statement controls buttons behaviour depending on results.
            // button is either in "Highlight!" mode or "done".

            if (!textEditingLocked) // "textEditingLocked" this bool controls text input.
            {
                //below is what happens when user clicks on Highlight button, pretty much search runs again, but it highlights all finds via Highlight() method.
                ResetFind();
                CheckColors();
                Search(richText, findQuery, out searchResultOK);

                if (searchResultOK)
                {
                    Highlight(allFinds, findQuery.Length);
                    findButton.Text = "Done";
                    ReplaceController(false);
                    FoundCounterController(currentFindIndex, allFinds.Count, "highlight");
                }
                else
                {
                    FoundCounterController(currentFindIndex, allFinds.Count, "find");
                }
            }

            else
            {
                //those are actions taken when user no longer wants to highlight and clicks on "Done" button
                EditingRichTextBoxFeaturesEnabled(true);
                textEditingLocked = false; // this unlocks text input
                CheckColors();
                findButton.Text = "Highlight!";
                richTextBox.Text = textBackup; //revert text to the one without yellow highlight
                FoundCounterController(currentFindIndex, allFinds.Count, "find");
                SearchInRichTextBox(); //this reverts to search mode and highlights first find
            }
        }

        private void Highlight(List<int> indexes, int queryLength) //highlights all finds by using the loop
        {
            textBackup = richTextBox.Text; //save backup before yellow hightlighting
            EditingRichTextBoxFeaturesEnabled(false);
            textEditingLocked = true; //this locks text input in richtextbox
            CheckColors();

            for (int i = 0; i < indexes.Count; i++)
            {
                richTextBox.Select(indexes[i], queryLength);
                richTextBox.SelectionBackColor = Color.Yellow;
            }

            FindBoxAndControlsGlobalController(searchResultOK);
        }
    }
}
