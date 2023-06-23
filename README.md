# clumber
![Logo](docs/logo.png)

UI Tests for humans

## Summary
Clumber is a wrapper around Microsoft Playwright (originally Selenium) to offer a UI testing framework driven by test cases written in understandable English. The basic idea is that the tests can be written by anyone, not just developers, and can therefore be maintained by any user within a team.

## Example Test Case
The following example is a test case being used to help develop the application
```
goto https://sadtrombone.com
click play
is pagetitle Sad Trombone
```

## Current Experiments
### Delegate vs Factory
The initial command implementation used Func delegates to manage and move away from conditional logic to control which commands were run. As a direct comparrison, a "Command Factory" has been created to assess efficiency in both CPU and Memory. Even though there is _slightly_ more overhead in terms of code, the overal net gain is better memory efficiency. The factory is also more maintainable as a new command inherits from the base command. The factory contains a dictionary of which command to create but there is no overhead in instantiation of the method.
I have yet to see what the net effect would be by moving the methods to static (thus only going through one instantiation) but I will stick with the factory method for now.

### Memory Leak
There appears to be a memory leak somewhere with Playwright. Now I boil this down to my use of it and how it is being disposed. Some slight restructure is probably needed to ensure that I am not disturbing the process of disposing Playwright.

## Dev Notes
### Powershell - Raspberry Pi
[Raspbian Powershell install instructions](https://learn.microsoft.com/en-us/powershell/scripting/install/install-raspbian?view=powershell-7.3)

### Playwright Install
```
pwsh bin/Debug/netX/playwright.ps1 install
```