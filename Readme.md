BOSS is a UPS Brokerage system.

WpfSandbox is a sandbox environment for the development of C# WPF custom controls and behaviors for BOSS.
A number of projects are identical to projects in the BOSS BlCommon solution.

The solution consists of the projects:
- BcWpfCommon
A library of common WPF functionality.
Synchronized with BlCommon.BcWpfCommon.
- BcWPFCustomControls
A library of custom WPF controls.
Synchronized with BlCommon.BcWPFCustomControls.
- BOSSAutoRef
A mock of the BOSS Core BOSSAutoRef library with sample data for testing custom combobox controls.
- BossSandbox
Undocumented.
- ChapterControlTemplates
Training from book Pro WPF in C# 2010 by Matthew MacDonald.
- CustomerApp
Host application for development and test of custom WPF controls.
- CustomerControls
Child project that contains WPF view CustomerView.xaml that is the WPF customer controls playground.
- CustomerCreator
Undocumented.
- CustomerData
Undocumented.
- CustomerData.Test
Tests for CustomerData.
- DiPresentationLogic
Project that was used to analyse a WPF dependency injection architecture
- DiSandbox
Environment that was used to develop the BOSS WPF dependency injection architecture
- SaveChanges
WPF project that contains the WPF child view with controls that respond to a Windows Forms host Save event.
- SaveChangesApp
Windows Forms Host application that hosts the WPF CustomerView in the SaveChanges project.
In combination with the SaveChanges project it is a POC for solving the BOSS WPF save problem.
Selecting Save from the menu item or clicking the save button from the toolbar on the main form will 
make the WPF control that has the focus store its data to the model.