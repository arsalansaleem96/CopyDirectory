# CopyDirectory

A Windows Forms application to copy directories recursively.

## Description

* The copy function(recursive function) is in a separate assembly which injected in main assembly by using Dependency Injection(Microsoft Extension Dependency Injection).
* During the copying process, it displays each file info such as source file path and file name .
* Onclick events on source and target textbox to select the folder.
* Tasks used for UI responsiveness(Followed singleton pattern to restricts the instantiation of a class to one "single" instance).
