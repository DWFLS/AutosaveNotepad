AutosaveNotepad v1.0.1


by DWFLS


Features:
- [x] Text editing.
- [x] Autosave per input or edit action.
- [x] Display settings: Normal/Dark mode.
- [x] Create, open, save, backup documents.
- [x] Text editing functions (undo, redo, cut, copy, paste, select all)

Version history:

v1.0.2

- [x] Rename "Save and Backup" to "Create a *Backup* Copy"
- [x] Disable "Save as..." and "*Backup*" on app start.

v1.0.1

- [x] Fixed a bug causing clearing of the previous file while creating another new document.
- [x] Removed redundant code.
- [x] Cleaner code.


To-do:

v1.0.3

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

v1.0.4

- [x] Right mouse button context menu
- [x] Added regions
- [x] Toggle editing functions availability on context (undo only if action made etc)
- [x] Autosave toggle
- [x] Line wrapping toggle



v1.0.5
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

v1.0.6.
[x] 0.X - Split Form1.cs into file modules
[x] 1 - Case sensitivity control for find feature
[x] 2 - Move more controllers and handlers
[ ] 3 - Fix filename not showing when using quicksave
[ ] 4 - Enhance dark mode
	[ ] Dark mode interacting with highlighting option (change disabled textbox color?)
[ ] 5 - Add an option to hide quicksavePanel along with search panel


v1.0.7
[ ] 0 - Replace feature

v1.0.8
[ ] 0 -  Word count

v1.1.0.
[ ] 0 - "Open with"
[ ] 1 - Make default txt app
[ ] 2 - Add comments for readiblity
[ ] 3 - Release v1.1.0