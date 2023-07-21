# **AutosaveNotepad**
by DWFLS

## Features:
- [x] Text editing.
- [x] Autosave per input or edit action.
- [x] Search feature (case sensitive / not-case sensitive)
- [x] Highlight search results
- [x] Replace All / Replace One Feature
- [x] Printing Dialog Box
- [x] Quick Save Feature
- [x] Word Count / Wrap Feature
- [x] Display settings: Normal/Dark mode.
- [x] Create, open, save, backup documents.
- [x] Text editing functions (undo, redo, cut, copy, paste, select all)
- [x] Dark Mode
- [x] "Open with" functionality.

## Version history:

## **v1.0.1** - Fixes

- [x] Fixed a bug causing clearing of the previous file while creating another new document.
- [x] Removed redundant code.
- [x] Cleaner code.

## **v1.0.2** - Tweaks

- [x] Rename "Save and Backup" to "Create a *Backup* Copy"
- [x] Disable "Save as..." and "*Backup*" on app start.

## **v1.0.3** - More Functionalities

- [x] Display file name in window title
- [x] New readme format
- [x] Default save location
- [x] Stip Status constructor
- [x] Path in status strip
- [x] Quicksave bar ui (avoiding popup dialog and clicking through menus to quickly create a file; type in the name of the file, press enter and file is ready to edit)
- [x] Working quicksave
- [x] Path in status strip
- [x] Can write without autosave mode on again
- [x] Drag and drop

## **v1.0.4** - Context menu, Autosave toggle and word wrap.

- [x] Right mouse button context menu
- [x] Added regions
- [x] Toggle editing functions availability on context (undo only if action made etc)
- [x] Autosave toggle
- [x] Line wrapping toggle

## **v1.0.5** Find Feautre, Print and handling copy/paste/cut

- [x] Full find feature
	- [x] Rewriting from scratch
	- [x] Fixed highlighting the last found entry
	- [x] Unmark when typing again
		- [x] resolve by enabling or reenabling upon pressing highlighting
		- [x] disable all edit keys when highlighting
	- [x] Fix issue when searched for string is inside of another, example: f in fff, issue with repeating chars (maybe skip past i's in the for loop? of split in two loops...)')
	- [x] Separate Find from highlight button function
	- [x] Find single previous or next		
		- [x] Program the prev and next buttons
		- [x] Limit character in the label that displays number of finds
		- [x] URL the github status bar link
- [x] Add icons (toolbar/app)
- [x] Print function
- [x] Check if can cut/copy/paste
- [x] Handling pasting
- [x] Improved roadmap

## **v1.0.6.** Modular Programming, fixes and some functionalities 

- [x] 0.X - Split Form1.cs into file modules
- [x] 1 - Case sensitivity control for find feature
- [x] 2 - Move more controllers and handlers
- [x] 3 - Fix filename not showing when using quicksave
- [x] 4 - Adjust dark mode behavior when in highlight mode
- [x] 5 - Add an option to hide quicksavePanel along with search panel, rename display to view ""

## **v1.0.7** Word Count and functionalities

- [x] 0 - Word count
- [x] 1 - Debug window toggle + feature
- [x] 2 - Change richtextbox font to monospace-like.

## **v1.0.8.** Replace all/one feature
	
- [x] 1.0 - **Replace all function complete**
	- [x] 0.0 - Replace all button conditional on "search ok" bool
- [x] 2.0 - **Replace "one" function complete**
- [x] 2.1 - Hotfix a crash when opening currently opened text file
- [x] 2.2 - Reformatted readme.md
- [x] 2.3 - Reformatted readme.md
- [x] 2.4 - Reformatted readme.md


## **v1.1.0.** "Open with" feature and settings
- [x] 0 - "Open with" complete
- [x] - - Make default txt app - to be handled by installer or else
- [x] 2 - Handle Settings
	- [x] .1 - fix not saving on "X" button.
	- [x] .2 - fix case sensitivity
- [x] 3 - Add comments for readiblity, separated "highlight" feature from "Find" module, separated few controllers and improved debug console.
	- [ ] .1 - Update readme to list all noticeable features
- [x] 4 - Release v1.1.4 (Portable)

## **v1.2** Quick Load mode
- [x] .0 - moved word count to toolstrip
	- [x] .1 - fixed error caused by deleted word count label and tweaked status strip constructor to work better with word count.
- [x] .1 - quick load create a dropdown menu that reads all text files in def folder and opens them when clicked
	- [x] rearranged the quickload/save panel
	- [x] rename panel to quicksave/load
	- [x] circumstantial enable and disable
	- [x] deal with interference from AutosaveActive() method
	- [x] add manual Save button (use case: autosave is disabled by user)
	- [x] rename menustrip item
	- [x] refresh dropdown menu when clicking it
- [x] .2 - refresh default folder check and rescan for textfiles for the dropdown menu
	- [x] on formwindow focus regain
	- [x] file menu item click post any method call
	- [x] handle save, autosave and default folder on focus regain after deleting files
- [x] .3 - add ctrl+f shortcut for search, ctr+p for print and ctrl+q for quickload/save panel	
- [x] .4 - change quicksave button text to "overwrite" when file already exists, as well as the color of the the quicksaveTextBox.
- [x] .5 - mod an.cfg file content so it's understandable without knowing the array key (convert from "true" to "setting = true")
	- [x] .1 fixes and tweaks
		- fixed not loading default settings after generating them.
		- moved initial elements settings to settings controller module.
	- [X] .2 minor tweaks to strip status text linked to quicksave and word wrap
- [ ] .6 - update readme and release v1.2.5