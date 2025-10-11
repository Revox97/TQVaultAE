**Main Application**

***TO DO***
- [ ] Read out player list
- [ ] Load payers
- [ ] Read out vault list
- [ ] Load vaults
- [ ] Save vaults
- [ ] Save player data
- [ ] Save transfer area
- [ ] Save relics area
- [ ] Localization (Settings NOT WINDOWS!)
- [ ] Items Detail Window
    - [ ] Add layout
    - [ ] Add data structure
    - [ ] Add position
    - [ ] Add correct bindings
    - [ ] Handle position if offscreen
- [ ] Items
  - [ ] Rarity support
    - [ ] Apply background color
    - [ ] Show red bg color if requirements are not met
    - [ ] Item rarity overlay
      - [ ] Define colors
      - [ ] Redo this to allow correct scaling (if possible using gradients)
      - [ ] Hide rarity overlay for requirements not met, potions, relics, etc.
  - [ ] Hover color support
- [ ] Add save functionality
  - [x] Add UI
- [ ] Add multi character support
  - [ ] Load actual items
  - [x] Add character selection
    - [ ] Load actual characters
    - [ ] Implement switching
    - [ ] Add icon for selection
  - [ ] Add storage area support
- [ ] Vault component
    - [ ] Add UI
    - [ ] Multi vault support
      - [ ] Load actual items
      - [x] Vault selection
    - [ ] Vault customization
      - [ ] Icon customization (Configuration)
      - [ ] Name customization (Configuration)
    - [ ] Autosort
- [ ] Player component
  - [ ] Inventory
    - [ ] Add UI
    - [ ] Load actual items
    - [ ] Autosort
    - [ ] Multisack support
    - [ ] Support for characters with less then four sacks
        - [ ] Hide tabs for non available sacks
  - [ ] Equipment
    - [ ] Add UI
    - [ ] Load actual items
    - [ ] Add rings support
    - [ ] Statistics
      - [ ] Load actual statistics
      - [ ] Add correct background
      - [ ] Scale font size
  - [ ] Add transfer area support
    - [ ] Add UI
    - [ ] Load actual items
    - [ ] Autosort
  - [ ] Add relic area support
    - [ ] Add UI
    - [ ] Load actual items
    - [ ] Autosort
- [ ] Search functionality (menu bar)
  - [ ] Add UI
  - [ ] Highlight matches
  - [ ] Allow more specific filters
    - [ ] Add reset functionality
    - [ ] Add apply functionality
    - [ ] Min requirements
    - [ ] Max requirements
    - [ ] Prefix
    - [ ] Suffix
    - [ ] Relic
    - [ ] Charm
    - [ ] Set item
    - [ ] Item type
    - [ ] Rarity
    - [ ] Game Version (e.g. Immortal Throne)
- [ ] Search page
  - [ ] Add UI
      - [ ] Add search filters
      - [ ] Add fulltext search
      - [ ] Add search result component
  - [ ] Add search controller
- [ ] Configuration
  - [ ] Add UI
  - [ ] Save Configuration
  - [ ] Load Configuration
- [ ] Backups
    - [ ] Add git sync
    - [ ] Add backup support (player files etc.)
    - [ ] Add Dropbox support
    - [ ] Add Google Drive support
    - [ ] Add OneDrive support
- [ ] About
  - [x] Add UI
  - [ ] Load correct data
- [ ] Archiving
  - [ ] Add UI
  - [ ] Show Archived players
  - [ ] Archive player
  - [ ] Unarchive player
  - [ ] Archive all
  - [ ] Unarchive all

***NEW FEATURES***
- [ ] Show correct item stats instead of incorrect stats 
- [ ] Add item cost (sell) | can be calculated by using db

***FIXES***
- [ ] UI scaling in main window
- [ ] Spacing in player statistics
- [ ] Item highlight for weapons/shields


***
**FILE EXPLORER**

***TO DO***
- [ ] Add icon
  - [ ] Create icon
- [ ] Add UI
- [ ] Read chr files
- [ ] Read dxb files
- [ ] Read dxg files
- [ ] Implement export functionality
- [ ] Rename copy button to save
- [ ] Add localization
- [ ] File Infos
  - [ ] Add UI
  - [ ] Bind actual data
    - [ ] File path
      - [ ] Make it a link
      - [ ] On click open directory
    - [ ] File size
    - [ ] File name
    - [ ] File ext
    - [ ] File version
    - [ ] Found keys
    - [ ] Data type errors
    - [ ] Unknown Segments
- [ ] Detected Keys
  - [ ] Add UI
  - [ ] Bind actual data
- [ ] Key
  - [ ] Add UI
  - [ ] Bind actual data
    - [ ] Key name
    - [ ] Offset
    - [ ] Length
    - [ ] IsSubStructureOpening
    - [ ] IsStructureClosing
    - [ ] IsUnknownSegment
    - [ ] IsDataTypeError
    - [ ] IsKeyValue
- [ ] Value
  - [ ] Add UI
  - [ ] Bind actual data
    - [ ] Offset
    - [ ] Length
    - [ ] DataType
    - [ ] As Int
    - [ ] As bool
    - [ ] As String
    - [ ] As Byte[]
- [ ] Menu
  - [ ] File
    - [ ] Open player directory
    - [ ] Open file
  - [ ] Detected Players
    - [ ] Load selected player
    - [ ] Fetch players
    - [ ] Fetch transfer area
    - [ ] Fetch mod players
      - [ ] Figure out what mod means here
  - [ ] Help
    - [ ] About

***NEW FEATURES***
- [ ] Add hex data explorer

***FIXES***

***
**ARZ EXPLORER**

***TO DO***
- [ ] Add UI
- [ ] Add localization
- [ ] Read ARZ files
- [ ] Read ARC files

***NEW FEATURES***

***FIXES***

