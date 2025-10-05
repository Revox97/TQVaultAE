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
    - [x] Add layout
    - [x] Add data structure
    - [x] Add position
    - [ ] Add correct bindings
    - [ ] Handle position if offscreen
- [ ] Items
  - [ ] Rarity support
    - [x] Apply background color
    - [ ] Show red bg color if requirements are not met
    - [x] Item rarity overlay
      - [ ] Define colors
      - [ ] Redo this to allow correct scaling (if possible using gradients)
      - [ ] Hide rarity overlay for requirements not met, potions, relics, etc.
  - [x] Hover color support
- [ ] Add save functionality
  - [x] Add UI
- [ ] Add multi character support
  - [ ] Load actual items
  - [ ] Add character selection
    - [ ] Add icon for selection
  - [ ] Add storage area support
- [ ] Vault component
    - [x] Add UI
    - [ ] Multi vault support
      - [ ] Load actual items
      - [ ] Vault selection
    - [ ] Vault customization
      - [ ] Icon customization (Configuration)
      - [ ] Name customization (Configuration)
    - [ ] Autosort
- [ ] Player component
  - [ ] Inventory
    - [x] Add UI
    - [ ] Load actual items
    - [ ] Autosort
    - [ ] Multisack support
    - [ ] Support for characters with less then four sacks
        - [ ] Hide tabs for non available sacks
  - [ ] Equipment
    - [x] Add UI
    - [ ] Load actual items
    - [ ] Add rings support
    - [ ] Statistics
      - [ ] Load actual statistics
      - [ ] Add correct background
      - [ ] Scale font size
  - [ ] Add transfer area support
    - [x] Add UI
    - [ ] Load actual items
    - [ ] Autosort
  - [ ] Add relic area support
    - [x] Add UI
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
  - [x] Add UI
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
- [x] Add UI
- [x] Read chr files
- [ ] Read dxb files
- [ ] Read dxg files
- [ ] Implement export functionality
- [ ] Rename copy button to save
- [ ] Add localization
- [ ] File Infos
  - [x] Add UI
  - [ ] Bind actual data
    - [x] File path
      - [x] Make it a link
      - [x] On click open directory
    - [x] File size
    - [x] File name
    - [x] File ext
    - [ ] File version
    - [ ] Found keys
    - [ ] Data type errors
    - [ ] Unknown Segments
- [x] Detected Keys
  - [x] Add UI
  - [x] Bind actual data
- [ ] Key
  - [x] Add UI
  - [ ] Bind actual data
    - [x] Key name
    - [x] Offset
    - [x] Length
    - [x] IsSubStructureOpening
    - [x] IsStructureClosing
    - [ ] IsUnknownSegment
    - [ ] IsDataTypeError
    - [ ] IsKeyValue
- [ ] Value
  - [x] Add UI
  - [ ] Bind actual data
    - [x] Offset
    - [x] Length
    - [ ] DataType
    - [x] As Int
    - [x] As bool
    - [x] As String
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
  - [x] Help
    - [x] About

***NEW FEATURES***
- [ ] Add hex data explorer

***FIXES***
- [x] Fix wrong data collection of nested tree view items

***
**ARZ EXPLORER**

***TO DO***
- [ ] Add UI
- [ ] Add localization
- [ ] Read ARZ files
- [ ] Read ARC files

***NEW FEATURES***

***FIXES***

