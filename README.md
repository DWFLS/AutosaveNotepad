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
- [ ] Full Find feature
	- [x] Rewriting from scratch
	- [x] Fixed highlighting the last found entry
	- [x] Unmark when typing again
		- [x] resolve by enabling or reenabling upon pressing highlighting
		- [x] disable all edit keys when highlighting
	- [x] Fix issue when searched for string is inside of another, example: f in fff, issue with repeating chars (maybe skip past i's in the for loop? of split in two loops...)')
	- [ ] Find single previous or next
		- [ ] Merge FoundCounterController and Highlight to handle both selection, traversion and highlighting
	- [ ] Case sensitivity button for find feature

- [ ] Replace feature
- [ ] Enhance dark mode
- [ ] Add an option to hide quicksavePanel along with search panel
- [ ] Word count
- [x] URL the github status bar link
- [x] Add icons (toolbar/app)
- [x] Print function
- [x] Check if can cut/copy/paste
- [x] Handling pasting


v1.1.0
- [ ] "Open with"
- [ ] Make default txt app

- [ ] Release v1.1.0