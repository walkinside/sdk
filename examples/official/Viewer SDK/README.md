Viewer SDK Examples
===================

## Using Example Projects

To use examples in this folder, install COMOS Walkinside 10.2 SP1 64-bit
or 32-bit in "Just Me" mode ("All Users" will not work), then open 
`ViewerSdkExamplesFor64BitWalkinside.sln` or `ViewerSdkExamplesFor32BitWalkinside.sln` 
accordingly in Visual Studio 2012 or higher.

Example projects are configured in such a way as to automatically reference
SDK assemblies from your Walkinside installation. Also, build output will be
written directly to your Walkinside's plug-ins folder. That way you can
compile example plug-ins, open your Walkinside and immediately examples in the
plug-in list.


## List of Examples

### Example 1: Plug-in Interface

Minimal implementation of a COMOS Walkinside plug-in.

### Example 2: Menu

Extends Example 1 and registers an item in COMOS Walkinside main menu.

### Example 3: Form

Opens an empty form when user clicks the registered menu item.

### Example 4: Projects and Branches

Detects when user opens and closes projects and displays information about
currently selected item of project breakdown structure.

### Example 5: Search Branches

Searches through the breakdown structure of a project and highlights the result.

### Example 6: Context Menu

Adds an action item to the context menu that pops up when user right-clicks
an object in 3D view.

### Example 7: Labels

Creates text labels in 3D view that can respond to mouse clicks.

### Example 8: Colors and Transparency

Modifies visual properties of a 3D item (color, transparency and blinking)
for the duration of the current session.

