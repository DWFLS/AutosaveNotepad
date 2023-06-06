namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void NextPrevGlobalController(bool state)
        {
            findNextButtonReal.Enabled = state;
            findPrevButton.Enabled = state;
        }

        private void FindBoxAndControlsGlobalController(bool status)
        {
            findTextBox.Enabled = !status;
            searchButton.Enabled = !status;
            findNextButtonReal.Enabled = !status;
            findPrevButton.Enabled = !status;
        }

        private void ResetFind()
        {
            //currentFindIndex = 0;
            allFinds.Clear();
            findLength = 0;
            searchResultOK = false;
        }

        private void FoundCounterController(int current, int total, string mode)
        {
            int index = current;

            currentFindIndexDisplayed = index;
            totalFindResultsDisplayed = total;
            if (total > 0)
            {
                foundCounter.Visible = true;
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
                    findPrevButton.Enabled = false;
                    findNextButtonReal.Enabled = false;

                    if (currentFindIndexDisplayed + 1 != 1)
                        findPrevButton.Enabled = true;

                    if (currentFindIndexDisplayed + 1 != totalFindResultsDisplayed)
                        findNextButtonReal.Enabled = true;
                    /*
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
                    }*/
                }
            }
            else
            {
                //findQuerySuccess = false;
                foundCounter.Visible = false;
            }

            //NextPrevStatusController();
        }

    }
}
